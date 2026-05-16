using System.Text;
using DSInternals.Common.Schema;

namespace DSInternals.Common.Data;

public class DistinguishedName
{
    private const char escapeChar = '\\';
    private const char quoteChar = '"';
    private const char dnSeparator = ',';
    private const char rdnSeparator = '=';
    private const char dnsNameSeparator = '.';

    private List<DistinguishedNameComponent> components = new List<DistinguishedNameComponent>();

    public IReadOnlyList<DistinguishedNameComponent> Components
    {
        get
        {
            return this.components;
        }
    }

    public DistinguishedName()
    {
    }

    public DistinguishedName(DistinguishedNameComponent rdn)
    {
        this.AddParent(rdn);
    }

    public DistinguishedName(string dn)
    {
        if (String.IsNullOrEmpty(dn))
        {
            // Empty DN
            return;
        }

        string[] dnSegments = SplitDN(dn, false);
        foreach (string segment in dnSegments)
        {
            string[] rdnSegments = SplitDN(segment, true);
            if (rdnSegments.Length != 2)
            {
                throw new ArgumentException("Error parsing distinguished name.", nameof(dn));
            }
            try
            {
                var component = new DistinguishedNameComponent(rdnSegments[0].Trim(), rdnSegments[1].Trim());
                this.components.Add(component);
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentException("Error parsing distinguished name.", nameof(dn));
            }
        }
    }

    public string GetDnsName()
    {
        if (Components.Count == 0)
        {
            // TODO: Throw exception
            return string.Empty;
        }

        var hostName = new StringBuilder();
        hostName.Append(this.components[0].Value);

        for (int i = 1; i < this.components.Count; i++)
        {
            // TODO: Check name if it really is DC=
            hostName.Append(dnsNameSeparator);
            hostName.Append(this.components[i].Value);
        }

        return hostName.ToString();
    }

    public DistinguishedName Parent
    {
        get
        {
            var result = new DistinguishedName();
            result.components.AddRange(this.components.Skip(1));
            return result;
        }
    }

    public DistinguishedName RootNamingContext
    {
        get
        {
            var dcComponents = this.components
                .Reverse<DistinguishedNameComponent>()
                .TakeWhile(component => component.Name.Equals(CommonDirectoryAttributes.DomainComponent, StringComparison.OrdinalIgnoreCase))
                .Reverse();
            var result = new DistinguishedName();
            result.components.AddRange(dcComponents);
            return result;
        }
    }

    public void AddParent(DistinguishedName dn)
    {
        if (dn == null)
        {
            // There is nothing to add.
            return;
        }

        foreach (var component in dn.Components)
        {
            this.AddParent(component);
        }
    }

    public void AddParent(string name, string value)
    {
        // Validation will be done in the DistinguishedNameComponent constructor
        var component = new DistinguishedNameComponent(name, value);
        this.AddParent(component);
    }

    public void AddParent(DistinguishedNameComponent component)
    {
        if (component != null)
        {
            this.components.Add(component);
        }
    }

    public void AddChild(DistinguishedName dn)
    {
        if (dn == null)
        {
            // There os nothing to add.
            return;
        }

        foreach (var component in dn.Components.Reverse())
        {
            this.AddChild(component);
        }
    }

    public void AddChild(string name, string value)
    {
        // Validation will be performed by the DistinguishedNameComponent contructor.
        var component = new DistinguishedNameComponent(name, value);
        this.AddChild(component);
    }

    public void AddChild(DistinguishedNameComponent component)
    {
        if (component != null)
        {
            this.components.Insert(0, component);
        }
    }

    public override string ToString()
    {
        if (Components.Count == 0)
        {
            return String.Empty;
        }

        StringBuilder dn = new StringBuilder();
        foreach (var component in this.components)
        {
            dn.Append(component);
            dn.Append(dnSeparator);
        }

        // Remove the last separator while returning
        return dn.ToString(0, dn.Length - 1);
    }

    private static string[] SplitDN(string dn, bool isRDN)
    {
        var segments = new List<string>();
        bool inQuotes = false;
        var currentSegment = new StringBuilder(dn.Length);
        for (int i = 0; i < dn.Length; i++)
        {
            char currentChar = dn[i];
            bool separatorFound = false;
            switch (currentChar)
            {
                case escapeChar:
                    if (i + 2 < dn.Length && IsHexDigit(dn[i + 1]) && IsHexDigit(dn[i + 2]))
                    {
                        // RFC 4514 hex-pair escape (\XX). Consecutive \XX pairs form a UTF-8
                        // byte sequence and must be decoded together to handle multi-byte
                        // characters (e.g. \C3\B6 → "ö").
                        var bytes = new List<byte>(2);
                        do
                        {
                            bytes.Add((byte)((HexValue(dn[i + 1]) << 4) | HexValue(dn[i + 2])));
                            i += 3;
                        }
                        while (i + 2 < dn.Length && dn[i] == escapeChar && IsHexDigit(dn[i + 1]) && IsHexDigit(dn[i + 2]));
                        // The outer for-loop will increment i.
                        i--;
                        currentSegment.Append(Encoding.UTF8.GetString(bytes.ToArray()));
                    }
                    else if (i < dn.Length - 1)
                    {
                        // Single-character escape (e.g. \, \= \\ \").
                        i++;
                        currentSegment.Append(dn[i]);
                    }
                    // TODO: What if we are at the end of the string?
                    break;
                case quoteChar:
                    inQuotes = !inQuotes;
                    break;
                case dnSeparator:
                    if (!inQuotes && !isRDN)
                    {
                        separatorFound = true;
                    }
                    else
                    {
                        currentSegment.Append(currentChar);
                    }
                    break;
                case rdnSeparator:
                    if (!inQuotes && isRDN)
                    {
                        separatorFound = true;
                    }
                    else
                    {
                        currentSegment.Append(currentChar);
                    }
                    break;
                default:
                    currentSegment.Append(currentChar);
                    break;
            }

            if (separatorFound)
            {
                segments.Add(currentSegment.ToString());
                currentSegment.Clear();
            }
        }
        if (inQuotes)
        {
            // Unpaired quotes
            throw new ArgumentException("Error parsing distinguished name.", nameof(dn));
        }
        // Add the last segment to the list
        segments.Add(currentSegment.ToString());
        return segments.ToArray();
    }

    private static bool IsHexDigit(char c)
    {
        return (c >= '0' && c <= '9') || (c >= 'A' && c <= 'F') || (c >= 'a' && c <= 'f');
    }

    private static int HexValue(char c)
    {
        if (c <= '9') return c - '0';
        if (c <= 'F') return c - 'A' + 10;
        return c - 'a' + 10;
    }

    public static string GetDnsNameFromDN(string dn)
    {
        var dnParsed = new DistinguishedName(dn);
        return dnParsed.GetDnsName();
    }

    public static DistinguishedName GetDNFromDNSName(string domainName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(domainName);

        var dn = new DistinguishedName();
        var dnsComponents = domainName.Split(dnsNameSeparator);

        foreach (var component in dnsComponents)
        {
            dn.AddParent(CommonDirectoryAttributes.DomainComponent, component);
        }

        return dn;
    }

    public override bool Equals(object? obj)
    {
        // Check for null values and compare run-time types.
        if (obj == null || GetType() != obj.GetType())
            return false;

        // Now simply compare the string representation of both DNs
        return this.ToString() == obj.ToString();
    }

    public override int GetHashCode()
    {
        // This DN implementation is not immutable so we do not calculate the hash of the DN.
        return base.GetHashCode();
    }
}
