namespace DSInternals.Replication;

/// <summary>
/// Defines the various types of protocols that are supported by Win32 RPC.
/// </summary>
public enum RpcProtseq
{
    /// <summary>
    /// Connection-oriented NetBIOS over Transmission Control Protocol (TCP)
    /// </summary>
    ncacn_nb_tcp,

    /// <summary>
    /// Connection-oriented NetBIOS over Internet Packet Exchange (IPX)
    /// </summary>
    ncacn_nb_ipx,

    /// <summary>
    /// Connection-oriented NetBIOS Enhanced User Interface (NetBEUI)
    /// </summary>
    ncacn_nb_nb,

    /// <summary>
    /// Connection-oriented Transmission Control Protocol/Internet Protocol (TCP/IP)
    /// </summary>
    ncacn_ip_tcp,

    /// <summary>
    /// Connection-oriented named pipes
    /// </summary>
    ncacn_np,

    /// <summary>
    /// Connection-oriented Sequenced Packet Exchange (SPX)
    /// </summary>
    ncacn_spx,

    /// <summary>
    /// Connection-oriented DECnet transport 
    /// </summary>
    ncacn_dnet_nsp,

    /// <summary>
    /// Connection-oriented AppleTalk DSP
    /// </summary>
    ncacn_at_dsp,

    /// <summary>
    /// Connection-oriented Vines scalable parallel processing (SPP) transport
    /// </summary>
    ncacn_vns_spp,

    /// <summary>
    /// Datagram (connectionless) User Datagram Protocol/Internet Protocol (UDP/IP)
    /// </summary>
    ncadg_ip_udp,

    /// <summary>
    /// Datagram (connectionless) IPX
    /// </summary>
    ncadg_ipx,

    /// <summary>
    /// Datagram (connectionless) over the Microsoft Message Queue Server (MSMQ)
    /// </summary>
    ncadg_mq,

    /// <summary>
    /// Connection-oriented TCP/IP using Microsoft Internet Information Server as HTTP proxy
    /// </summary>
    ncacn_http,

    /// <summary>
    /// Local procedure call 
    /// </summary>
    ncalrpc
}
