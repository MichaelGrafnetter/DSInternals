using System.Globalization;
using Windows.Win32.Networking.WinSock;

namespace DSInternals.Common;

internal static class IpProtoExtensions
{
    /// <summary>
    /// Returns the lowercase textual name of the IP protocol (e.g., <c>tcp</c>, <c>udp</c>) as used in
    /// DNS zone files, falling back to the numeric protocol value for protocols other than TCP/UDP.
    /// </summary>
    public static string ToProtocolName(this IPPROTO protocol)
    {
        return protocol switch
        {
            IPPROTO.IPPROTO_TCP => "tcp",
            IPPROTO.IPPROTO_UDP => "udp",
            _ => ((byte)protocol).ToString(CultureInfo.InvariantCulture)
        };
    }

    extension(IPPROTO)
    {
        /// <summary>
        /// Parses the lowercase textual name of an IP protocol (case-insensitive). Recognizes <c>tcp</c> and
        /// <c>udp</c>; any other value yields <c>false</c>.
        /// </summary>
        public static bool TryParseProtocolName(string name, out IPPROTO protocol)
        {
            if (name.Equals("tcp", StringComparison.OrdinalIgnoreCase))
            {
                protocol = IPPROTO.IPPROTO_TCP;
                return true;
            }

            if (name.Equals("udp", StringComparison.OrdinalIgnoreCase))
            {
                protocol = IPPROTO.IPPROTO_UDP;
                return true;
            }

            protocol = default;
            return false;
        }
    }
}
