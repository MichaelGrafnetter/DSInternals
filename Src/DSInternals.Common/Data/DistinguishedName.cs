﻿namespace DSInternals.Common.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using DSInternals.Common.Schema;

    /// <summary>
    /// Represents an LDAP distinguished name (DN) that uniquely identifies an object in a directory hierarchy.
    /// </summary>
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
                    throw new ArgumentException("Error parsing distinguished name.", "dn");
                }
                try
                {
                    var component = new DistinguishedNameComponent(rdnSegments[0].Trim(), rdnSegments[1].Trim());
                    this.components.Add(component);
                }
                catch (ArgumentNullException)
                {
                    throw new ArgumentException("Error parsing distinguished name.", "dn");
                }
            }
        }

        /// <summary>
        /// Converts the distinguished name to a DNS hostname by extracting CN and DC components.
        /// </summary>
        /// <returns>A DNS hostname representation of the distinguished name.</returns>
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
                    .TakeWhile(component => component.Name.Equals(CommonDirectoryAttributes.DomainComponent, StringComparison.InvariantCultureIgnoreCase))
                    .Reverse();
                var result = new DistinguishedName();
                result.components.AddRange(dcComponents);
                return result;
            }
        }

        /// <summary>
        /// AddParent implementation.
        /// </summary>
        public void AddParent(DistinguishedName dn)
        {
            if(dn == null)
            {
                // There is nothing to add.
                return;
            }

            foreach (var component in dn.Components)
            {
                this.AddParent(component);
            }
        }

        /// <summary>
        /// AddParent implementation.
        /// </summary>
        public void AddParent(string name, string value)
        {
            // Validation will be done in the DistinguishedNameComponent constructor
            var component = new DistinguishedNameComponent(name, value);
            this.AddParent(component);
        }

        /// <summary>
        /// AddParent implementation.
        /// </summary>
        public void AddParent(DistinguishedNameComponent component)
        {
            if (component != null)
            {
                this.components.Add(component);
            }
        }

        /// <summary>
        /// AddChild implementation.
        /// </summary>
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

        /// <summary>
        /// AddChild implementation.
        /// </summary>
        public void AddChild(string name, string value)
        {
            // Validation will be performed by the DistinguishedNameComponent contructor.
            var component = new DistinguishedNameComponent(name, value);
            this.AddChild(component);
        }

        /// <summary>
        /// AddChild implementation.
        /// </summary>
        public void AddChild(DistinguishedNameComponent component)
        {
            if (component != null)
            {
                this.components.Insert(0, component);
            }
        }

        /// <summary>
        /// Returns a string representation of the object.
        /// </summary>
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
                        // Skip the next char if not at the end of the string:
                        if (i < dn.Length - 1)
                        {
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
                // TODO: Extract as resource
                throw new ArgumentException("Error parsing distinguished name.", "dn");
            }
            // Add the last segment to the list
            segments.Add(currentSegment.ToString());
            return segments.ToArray();
        }

        /// <summary>
        /// GetDnsNameFromDN implementation.
        /// </summary>
        public static string GetDnsNameFromDN(string dn)
        {
            var dnParsed = new DistinguishedName(dn);
            return dnParsed.GetDnsName();
        }

        /// <summary>
        /// GetDNFromDNSName implementation.
        /// </summary>
        public static DistinguishedName GetDNFromDNSName(string domainName)
        {
            Validator.AssertNotNullOrWhiteSpace(domainName, "domainName");

            var dn = new DistinguishedName();
            var dnsComponents = domainName.Split(dnsNameSeparator);

            foreach (var component in dnsComponents)
            {
                dn.AddParent(CommonDirectoryAttributes.DomainComponent, component);
            }

            return dn;
        }

        /// <summary>
        /// Determines whether the specified object is equal to this instance.
        /// </summary>
        public override bool Equals(object obj)
        {
            // Check for null values and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
                return false;

            // Now simply compare the string representation of both DNs
            return this.ToString() == obj.ToString();
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        public override int GetHashCode()
        {
            // This DN implementation is not immutable so we do not calculate the hash of the DN.
            return base.GetHashCode();
        }
    }
}
