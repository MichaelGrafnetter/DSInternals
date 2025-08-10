
namespace NDceRpc.Microsoft.Interop
{
#pragma warning disable 1591
    /// <summary>
    /// Defines the various types of protocols that are supported by Win32 RPC. NCA stands for Network Computing Architecture
    /// </summary>
    public enum RpcProtseq
    {
        /// <summary>
        /// Connection-oriented NetBIOS over Transmission Control Protocol (TCP) Client only: MS-DOS, Windows 3.x
        /// Client and Server: Windows Server 2003, Windows XP, Windows 2000, Windows NT
        /// </summary>
        ncacn_nb_tcp,
        /// <summary>
        /// Connection-oriented NetBIOS over Internet Packet Exchange (IPX) Client only: MS-DOS, Windows 3.x
        /// Client and Server: Windows Server 2003, Windows XP, Windows 2000, Windows NT
        /// </summary>
        ncacn_nb_ipx,
        /// <summary>
        /// Connection-oriented NetBIOS Enhanced User Interface (NetBEUI) Client only: MS-DOS, Windows 3.x
        /// Client and Server: Windows Server 2003, Windows XP, Windows 2000, Windows NT, Windows Me, Windows 98, Windows 95
        /// </summary>
        ncacn_nb_nb,
        /// <summary>
        /// Connection-oriented Transmission Control Protocol/Internet Protocol (TCP/IP) Client only: MS-DOS, Windows 3.x, and Apple Macintosh
        /// Client and Server: Windows Server 2003, Windows XP, Windows 2000, Windows NT, Windows Me, Windows 98, Windows 95
        /// </summary>
        ncacn_ip_tcp,
        /// <summary>
        /// Connection-oriented named pipes Client only: MS-DOS, Windows 3.x, Windows 95
        /// Client and Server: Windows Server 2003, Windows XP, Windows 2000, Windows NT
        /// </summary>
        ncacn_np,
        /// <summary>
        /// Connection-oriented Sequenced Packet Exchange (SPX) Client only: MS-DOS, Windows 3.x
        /// Client and Server: Windows Server 2003, Windows XP, Windows 2000, Windows NT, Windows Me, Windows 98, Windows 95
        /// </summary>
        ncacn_spx,
        /// <summary>
        /// Connection-oriented DECnet transport 
        /// Client only: MS-DOS, Windows 3.x
        /// </summary>
        ncacn_dnet_nsp,
        /// <summary>
        /// Connection-oriented AppleTalk DSP Client: Apple Macintosh
        /// Server: Windows Server 2003, Windows XP, Windows 2000, Windows NT
        /// </summary>
        ncacn_at_dsp,
        /// <summary>
        /// Connection-oriented Vines scalable parallel processing (SPP) transport Client only: MS-DOS, Windows 3.x
        /// Client and Server: Windows Server 2003, Windows XP, Windows 2000, Windows NT
        /// </summary>
        ncacn_vns_spp,
        /// <summary>
        /// Datagram (connectionless) User Datagram Protocol/Internet Protocol (UDP/IP) Client only: MS-DOS, Windows 3.x
        /// Client and Server: Windows Server 2003, Windows XP, Windows 2000, Windows NT
        /// </summary>
        ncadg_ip_udp,
        /// <summary>
        /// Datagram (connectionless) IPX Client only: MS-DOS, Windows 3.x
        /// Client and Server: Windows Server 2003, Windows XP, Windows 2000, Windows NT
        /// </summary>
        ncadg_ipx,
        /// <summary>
        /// Datagram (connectionless) over the Microsoft Message Queue Server (MSMQ) Client only: Windows Me/98/95
        /// Client and Server: Windows Server 2003, Windows XP, Windows 2000, Windows NT Server 4.0 with SP3 and later
        /// </summary>
        ncadg_mq,
        /// <summary>
        /// Connection-oriented TCP/IP using Microsoft Internet Information Server as HTTP proxy Client only: Windows Me/98/95
        /// Client and Server: Windows Server 2003, Windows XP, Windows 2000
        /// </summary>
        ncacn_http,
        /// <summary>
        /// Local procedure call 
        /// Client and Server: Windows Server 2003, Windows XP, Windows 2000, Windows NT, Windows Me, Windows 98, Windows 95
        /// </summary>
        ncalrpc
    }
}