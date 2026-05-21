using System.Globalization;
using Windows.Win32.Networking.WinSock;

namespace DSInternals.Common.DNS;

/// <summary>
/// Translates TCP/UDP port numbers to IANA service names using the system <c>services</c> file
/// (<c>%SystemRoot%\System32\drivers\etc\services</c>). The lookup table is loaded lazily on first use.
/// </summary>
internal static class ServiceNameTranslator
{
    /// <summary>
    /// IANA service name lookup loaded once from <c>%SystemRoot%\System32\drivers\etc\services</c>.
    /// </summary>
    private static readonly Lazy<Dictionary<(IPPROTO Protocol, ushort Port), string>> ServiceNames =
        new(Load);
    internal static readonly char[] ColumnSeparators = [' ', '\t'];

    /// <summary>
    /// Resolves the IANA service name for the given protocol and port, falling back to the numeric port
    /// when no mapping is available (e.g., the protocol is not TCP/UDP or the services file lacks the entry).
    /// </summary>
    public static string GetServiceName(IPPROTO protocol, ushort port)
    {
        if ((protocol == IPPROTO.IPPROTO_TCP || protocol == IPPROTO.IPPROTO_UDP)
            && ServiceNames.Value.TryGetValue((protocol, port), out string name))
        {
            return name;
        }

        return port.ToString(CultureInfo.InvariantCulture);
    }

    private static Dictionary<(IPPROTO Protocol, ushort Port), string> Load()
    {
        var result = new Dictionary<(IPPROTO Protocol, ushort Port), string>();
        string servicesPath = Path.Combine(Environment.SystemDirectory, "drivers", "etc", "services");

        try
        {
            foreach (string rawLine in File.ReadLines(servicesPath))
            {
                // Strip inline comments.
                int commentIndex = rawLine.IndexOf('#');
                string line = (commentIndex >= 0 ? rawLine.Substring(0, commentIndex) : rawLine).Trim();
                if (line.Length == 0)
                {
                    continue;
                }

                // Format: <service-name> <port>/<protocol> [aliases...]
                string[] tokens = line.Split(ColumnSeparators, 3, StringSplitOptions.RemoveEmptyEntries);
                if (tokens.Length < 2)
                {
                    continue;
                }

                string[] portProto = tokens[1].Split('/');
                if (portProto.Length != 2
                    || !ushort.TryParse(portProto[0], out ushort port)
                    || !IPPROTO.TryParseProtocolName(portProto[1], out IPPROTO protocol))
                {
                    continue;
                }

                // Keep the first canonical name encountered per (protocol, port).
                var key = (protocol, port);
                if (!result.ContainsKey(key))
                {
                    result[key] = tokens[0];
                }
            }
        }
        catch (IOException)
        {
            // Fall back to numeric ports when the services file cannot be read.
        }
        catch (UnauthorizedAccessException)
        {
        }

        return result;
    }
}
