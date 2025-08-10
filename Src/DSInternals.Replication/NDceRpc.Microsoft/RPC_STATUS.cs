using System.ComponentModel;
namespace NDceRpc.Microsoft.Interop
{
    /// <summary> WIN32 RPC Error Codes </summary>
    /// <seealso href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms681386.aspx"/>
    public enum RPC_STATUS : uint
    {
        [Description("Operation successful.")]
        RPC_S_OK = 0,
         [Description("The parameter is incorrect.")]
        RPC_S_INVALID_ARG = 87,
         [Description("Not enough storage is available to complete this operation.")]
        RPC_S_OUT_OF_MEMORY = 14,
        [Description("No more threads can be created in the system.")] 
        RPC_S_OUT_OF_THREADS = 164,
        RPC_S_INVALID_LEVEL = 87,
        [Description("The data area passed to a system call is too small.")] 
        RPC_S_BUFFER_TOO_SMALL = 122,
        [Description("The security descriptor structure is invalid.")] 
        RPC_S_INVALID_SECURITY_DESC = 1338,
         [Description("Access is denied.")] 
        RPC_S_ACCESS_DENIED = 5,
         [Description("Not enough server storage is available to process this command.")]
        RPC_S_SERVER_OUT_OF_MEMORY = 1130,
        [Description("Overlapped I/O operation is in progress.")] 
        RPC_S_ASYNC_CALL_PENDING = 997,
        [Description("No mapping between account names and security IDs was done.")]
        RPC_S_UNKNOWN_PRINCIPAL = 1332,
          [Description("This operation returned because the timeout period expired.")] 
        RPC_S_TIMEOUT = 1460,

          [Description("The string binding is invalid.")]
          RPC_S_INVALID_STRING_BINDING = 1700,

        /// <summary>
        /// The binding handle is not the correct type.
        /// </summary>
          [Description("The binding handle is not the correct type.")] 
        RPC_S_WRONG_KIND_OF_BINDING = 1701,// (0x6A5)

        /// <summary>
        /// The binding handle is invalid.
        /// </summary>
        [Description("The binding handle is invalid.")]
        RPC_S_INVALID_BINDING = 1702,// (0x6A6)


        [Description("The RPC protocol sequence is not supported.")]
        RPC_S_PROTSEQ_NOT_SUPPORTED = 1703,

        [Description("The RPC protocol sequence is invalid.")]
        RPC_S_INVALID_RPC_PROTSEQ = 1704,


        [Description("The string universal unique identifier (UUID) is invalid.")]
        RPC_S_INVALID_STRING_UUID = 1705,

        [Description("The endpoint format is invalid.")]
        RPC_S_INVALID_ENDPOINT_FORMAT = 1706,

        [Description("The network address is invalid.")]
        RPC_S_INVALID_NET_ADDR = 1707,

        [Description("No endpoint was found.")]
        RPC_S_NO_ENDPOINT_FOUND = 1708,

        [Description("The timeout value is invalid.")]
        RPC_S_INVALID_TIMEOUT = 1709,

        [Description("The object universal unique identifier (UUID) was not found.")]
        RPC_S_OBJECT_NOT_FOUND = 1710,

        /// <summary>
        /// The object universal unique identifier (UUID) has already been registered.
        /// </summary>
        [Description("The object universal unique identifier (UUID) has already been registered.")]
        RPC_S_ALREADY_REGISTERED = 1711,
            [Description("The type universal unique identifier (UUID) has already been registered.")]
        RPC_S_TYPE_ALREADY_REGISTERED = 1712,
     [Description("The RPC server is already listening.")] 
        RPC_S_ALREADY_LISTENING = 1713,
           [Description("No protocol sequences have been registered.")] 
        RPC_S_NO_PROTSEQS_REGISTERED = 1714,
    [Description("The RPC server is not listening.")]
        RPC_S_NOT_LISTENING = 1715,
    [Description("The manager type is unknown.")]
    RPC_S_UNKNOWN_MGR_TYPE = 1716,
    [Description("The interface is unknown.")]
    RPC_S_UNKNOWN_IF = 1717,
    [Description("There are no bindings.")]
    RPC_S_NO_BINDINGS = 1718,
    [Description("There are no protocol sequences.")]
    RPC_S_NO_PROTSEQS = 1719,
    [Description("The endpoint cannot be created.")]
    RPC_S_CANT_CREATE_ENDPOINT = 1720,
    [Description("Not enough resources are available to complete this operation.")]
    RPC_S_OUT_OF_RESOURCES = 1721,
    [Description("The RPC server is unavailable.")]
    RPC_S_SERVER_UNAVAILABLE = 1722,
    [Description("The RPC server is too busy to complete this operation.")]
    RPC_S_SERVER_TOO_BUSY = 1723,
    [Description("The network options are invalid.")]
    RPC_S_INVALID_NETWORK_OPTIONS = 1724,
    [Description("There are no remote procedure calls active on this thread.")]
    RPC_S_NO_CALL_ACTIVE = 1725,
    [Description("The remote procedure call failed.")]
    RPC_S_CALL_FAILED = 1726,
    [Description("The remote procedure call failed and did not execute.")]
    RPC_S_CALL_FAILED_DNE = 1727,
    [Description("A remote procedure call (RPC) protocol error occurred.")]
    RPC_S_PROTOCOL_ERROR = 1728,
    [Description("Access to the HTTP proxy is denied.")]
    RPC_S_PROXY_ACCESS_DENIED = 1729,
    [Description("The transfer syntax is not supported by the RPC server.")]
    RPC_S_UNSUPPORTED_TRANS_SYN = 1730,
    [Description("The universal unique identifier (UUID) type is not supported.")]
    RPC_S_UNSUPPORTED_TYPE = 1732,
    [Description("The tag is invalid.")]
    RPC_S_INVALID_TAG = 1733,
    [Description("The array bounds are invalid.")]
    RPC_S_INVALID_BOUND = 1734,
    [Description("The binding does not contain an entry name.")]
    RPC_S_NO_ENTRY_NAME = 1735,
    [Description("The name syntax is invalid.")]
    RPC_S_INVALID_NAME_SYNTAX = 1736,
    [Description("The name syntax is not supported.")]
    RPC_S_UNSUPPORTED_NAME_SYNTAX = 1737,
    [Description("No network address is available to use to construct a universal unique identifier (UUID).")]
    RPC_S_UUID_NO_ADDRESS = 1739,
    [Description("The endpoint is a duplicate.")]
        RPC_S_DUPLICATE_ENDPOINT = 1740,
    [Description("The authentication type is unknown.")]
    RPC_S_UNKNOWN_AUTHN_TYPE = 1741,
    [Description("The maximum number of calls is too small.")]
    RPC_S_MAX_CALLS_TOO_SMALL = 1742,
    /// <summary>
    /// The string is too long.
    /// </summary>
    [Description("The string is too long.")]
    RPC_S_STRING_TOO_LONG = 1743,
    [Description("The RPC protocol sequence was not found.")]
    RPC_S_PROTSEQ_NOT_FOUND = 1744,
    [Description("The procedure number is out of range.")]
    RPC_S_PROCNUM_OUT_OF_RANGE = 1745,
    [Description("The binding does not contain any authentication information.")]
    RPC_S_BINDING_HAS_NO_AUTH = 1746,
    [Description("The authentication service is unknown.")]
    RPC_S_UNKNOWN_AUTHN_SERVICE = 1747,
    [Description("The authentication level is unknown.")]
    RPC_S_UNKNOWN_AUTHN_LEVEL = 1748,
    [Description("The security context is invalid.")]
    RPC_S_INVALID_AUTH_IDENTITY = 1749,
    [Description("The authorization service is unknown.")]
    RPC_S_UNKNOWN_AUTHZ_SERVICE = 1750,
    [Description("The entry is invalid.")]
    EPT_S_INVALID_ENTRY = 1751,
    [Description("The server endpoint cannot perform the operation.")]
    EPT_S_CANT_PERFORM_OP = 1752,
    [Description("There are no more endpoints available from the endpoint mapper.")]
    EPT_S_NOT_REGISTERED = 1753,
    [Description("No interfaces have been exported.")]
    RPC_S_NOTHING_TO_EXPORT = 1754,
    [Description("The entry name is incomplete.")]
    RPC_S_INCOMPLETE_NAME = 1755,
    [Description("The version option is invalid.")]
    RPC_S_INVALID_VERS_OPTION = 1756,
    [Description("There are no more members.")]
    RPC_S_NO_MORE_MEMBERS = 1757,
    [Description("There is nothing to unexport.")]
    RPC_S_NOT_ALL_OBJS_UNEXPORTED = 1758,
    [Description("The interface was not found.")]
    RPC_S_INTERFACE_NOT_FOUND = 1759,
    [Description("The entry already exists.")]
    RPC_S_ENTRY_ALREADY_EXISTS = 1760,
    [Description("The entry is not found.")]
    RPC_S_ENTRY_NOT_FOUND = 1761,
    [Description("The name service is unavailable.")]
    RPC_S_NAME_SERVICE_UNAVAILABLE = 1762,
    [Description("The network address family is invalid.")]
    RPC_S_INVALID_NAF_ID = 1763,
    [Description("The requested operation is not supported.")]
    RPC_S_CANNOT_SUPPORT = 1764,


          [Description("No security context is available to allow impersonation.")] RPC_S_NO_CONTEXT_AVAILABLE = 1765,
        [Description("An internal error occurred in a remote procedure call (RPC).")] RPC_S_INTERNAL_ERROR = 1766,
        [Description("The RPC server attempted an integer division by zero.")] RPC_S_ZERO_DIVIDE = 1767,
        [Description("An addressing error occurred in the RPC server.")] RPC_S_ADDRESS_ERROR = 1768,
        [Description("A floating-point operation at the RPC server caused a division by zero.")] RPC_S_FP_DIV_ZERO = 1769,
        [Description("A floating-point underflow occurred at the RPC server.")] RPC_S_FP_UNDERFLOW = 1770,
        [Description("A floating-point overflow occurred at the RPC server.")] RPC_S_FP_OVERFLOW = 1771,
        [Description("The list of RPC servers available for the binding of auto handles has been exhausted.")] RPC_X_NO_MORE_ENTRIES = 1772,
        [Description("Unable to open the character translation table file.")] RPC_X_SS_CHAR_TRANS_OPEN_FAIL = 1773,
        [Description("The file containing the character translation table has fewer than 512 bytes.")] RPC_X_SS_CHAR_TRANS_SHORT_FILE = 1774,
        [Description("A null context handle was passed from the client to the host during a remote procedure call.")] RPC_X_SS_IN_NULL_CONTEXT = 1775,
        [Description("The context handle changed during a remote procedure call.")] RPC_X_SS_CONTEXT_DAMAGED = 1777,
        [Description("The binding handles passed to a remote procedure call do not match.")] RPC_X_SS_HANDLES_MISMATCH = 1778,
        [Description("The stub is unable to get the remote procedure call handle.")] RPC_X_SS_CANNOT_GET_CALL_HANDLE = 1779,
        [Description("A null reference pointer was passed to the stub.")] RPC_X_NULL_REF_POINTER = 1780,
        [Description("The enumeration value is out of range.")] RPC_X_ENUM_VALUE_OUT_OF_RANGE = 1781,
        [Description("The byte count is too small.")] RPC_X_BYTE_COUNT_TOO_SMALL = 1782,
        [Description("The stub received bad data.")] RPC_X_BAD_STUB_DATA = 1783,

		[Description("HTTP proxy server rejected the connection because the cookie authentication failed.")]
		RPC_S_COOKIE_AUTH_FAILED = 1833, //0x729
        /// <summary>
        /// The object universal unique identifier (UUID) is the nil UUID.
        /// </summary>
        RPC_S_INVALID_OBJECT = 1900,

        [Description("Unspecified failure.")]
        RPC_E_FAIL = 0x80004005u
    }
}