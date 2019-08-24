#pragma once
#include "drsr.h"

// Following enums are required by the DRSR protocol, but they are not defined in any public header file.
// Their definitions have been loosely taken from the MS-DRSR specification.

/// <summary>
/// DRS extensions that may be supported by server/client.
/// </summary>
/// <see>DRS_EXT_INT</see>
enum DRS_EXT : DWORD
{
	/// <summary>
	/// Unused. SHOULD be 1 and MUST be ignored.
	/// </summary>
	DRS_EXT_BASE = 0x00000001,
	/// <summary>
	/// If present, signifies that the DC supports DRS_MSG_REPADD_V2.
	/// </summary>
	DRS_EXT_ASYNCREPL = 0x00000002,
	/// <summary>
	/// If present, signifies that the DC supports IDL_DRSRemoveDsServer and IDL_DRSRemoveDsDomain.
	/// </summary>
	DRS_EXT_REMOVEAPI = 0x00000004,
	/// <summary>
	/// If present, signifies that the DC supports DRS_MSG_MOVEREQ_V2.
	/// </summary>
	DRS_EXT_MOVEREQ_V2 = 0x00000008,
	/// <summary>
	/// If present, signifies that the DC supports DRS_MSG_GETCHGREPLY_V2.
	/// </summary>
	DRS_EXT_GETCHG_DEFLATE = 0x00000010,
	/// <summary>
	/// If present, signifies that the DC supports IDL_DRSDomainControllerInfo.
	/// </summary>
	DRS_EXT_DCINFO_V1 = 0x00000020,
	/// <summary>
	/// Unused. SHOULD be 1 and MUST be ignored.
	/// </summary>
	DRS_EXT_RESTORE_USN_OPTIMIZATION = 0x00000040,
	/// <summary>
	/// If present, signifies that the DC supports IDL_DRSAddEntry.
	/// </summary>
	DRS_EXT_ADDENTRY = 0x00000080,
	/// <summary>
	/// If present, signifies that the DC supports IDL_DRSExecuteKCC.
	/// </summary>
	DRS_EXT_KCC_EXECUTE = 0x00000100,
	/// <summary>
	/// If present, signifies that the DC supports DRS_MSG_ADDENTRYREQ_V2.
	/// </summary>
	DRS_EXT_ADDENTRY_V2 = 0x00000200,
	/// <summary>
	/// If present, signifies that the DC supports link valuereplication, and this support is enabled.
	/// </summary>
	DRS_EXT_LINKED_VALUE_REPLICATION = 0x00000400,
	/// <summary>
	/// If present, signifies that the DC supports DRS_MSG_DCINFOREPLY_V2.
	/// </summary>
	DRS_EXT_DCINFO_V2 = 0x00000800,
	/// <summary>
	/// Unused. SHOULD be 1 and MUST be ignored.
	/// </summary>
	DRS_EXT_INSTANCE_TYPE_NOT_REQ_ON_MOD = 0x00001000,
	/// <summary>
	/// A client - only flag. If present, it indicates that the security provider used for the connection supports session keys through RPC(example, Kerberos connections with mutual authentication enable RPC to expose session keys, but NTLM connections do not enable RPC to expose session keys).
	/// </summary>
	DRS_EXT_CRYPTO_BIND = 0x00002000,
	/// <summary>
	/// If present, signifies that the DC supports IDL_DRSGetReplInfo.
	/// </summary>
	DRS_EXT_GET_REPL_INFO = 0x00004000,
	/// <summary>
	/// If present, signifies that the DC supports additional 128 - bit encryption for passwords over the wire.DCs MUST NOT replicate passwords to other DCs that do not support this extension.
	/// </summary>
	DRS_EXT_STRONG_ENCRYPTION = 0x00008000,
	/// <summary>
	/// If present, signifies that the DC supports DRS_MSG_DCINFOREPLY_VFFFFFFFF.
	/// </summary>
	DRS_EXT_DCINFO_VFFFFFFFF = 0x00010000,
	/// <summary>
	/// If present, signifies that the DC supports IDL_DRSGetMemberships.
	/// </summary>
	DRS_EXT_TRANSITIVE_MEMBERSHIP = 0x00020000,
	/// <summary>
	/// If present, signifies that the DC supports IDL_DRSAddSidHistory.
	/// </summary>
	DRS_EXT_ADD_SID_HISTORY = 0x00040000,
	/// <summary>
	/// Unused.SHOULD be 1 and MUST be ignored.
	/// </summary>
	DRS_EXT_POST_BETA3 = 0x00080000,
	/// <summary>
	/// If present, signifies that the DC supports DRS_MSG_GETCHGREQ_V5.
	/// </summary>
	DRS_EXT_GETCHGREQ_V5 = 0x00100000,
	/// <summary>
	/// If present, signifies that the DC supports IDL_DRSGetMemberships2.
	/// </summary>
	DRS_EXT_GETMEMBERSHIPS2 = 0x00200000,
	/// <summary>
	/// Unused. This bit was used for a pre - release version of Windows.No released version of Windows references it.This bit can be set or unset with no change in behavior.
	/// </summary>
	DRS_EXT_GETCHGREQ_V6 = 0x00400000,
	/// <summary>
	/// If present, signifies that the DC supports application NCs.
	/// </summary>
	DRS_EXT_NONDOMAIN_NCS = 0x00800000,
	/// <summary>
	/// If present, signifies that the DC supports DRS_MSG_GETCHGREQ_V8.
	/// </summary>
	DRS_EXT_GETCHGREQ_V8 = 0x01000000,
	/// <summary>
	/// Unused. SHOULD be 1 and MUST be ignored.
	/// </summary>
	DRS_EXT_GETCHGREPLY_V5 = 0x02000000,
	/// <summary>
	/// If present, signifies that the DC supports DRS_MSG_GETCHGREPLY_V6.
	/// </summary>
	DRS_EXT_GETCHGREPLY_V6 = 0x04000000,
	/// <summary>
	///  If present, signifies that the DC supports DRS_MSG_ADDENTRYREPLY_V3, DRS_MSG_REPVERIFYOBJ, DRS_MSG_GETCHGREPLY_V7, and DRS_MSG_QUERYSITESREQ_V1.
	/// </summary>
	DRS_EXT_WHISTLER_BETA3 = 0x08000000,
	/// <summary>
	///  If present, signifies that the DC supports the W2K3 AD deflation library.
	/// </summary>
	DRS_EXT_W2K3_DEFLATE = 0x10000000,
	/// <summary>
	/// If present, signifies that the DC supports DRS_MSG_GETCHGREQ_V10.
	/// </summary>
	DRS_EXT_GETCHGREQ_V10 = 0x20000000,
	/// <summary>
	/// Unused. MUST be 0 and ignored.
	/// </summary>
	DRS_EXT_RESERVED_FOR_WIN2K_OR_DOTNET_PART2 = 0x40000000,
	/// <summary>
	/// Unused. MUST be 0 and ignored.
	/// </summary>
	DRS_EXT_RESERVED_FOR_WIN2K_OR_DOTNET_PART3 = 0x80000000,

	// Our custom combination follows:
	NO_EXT = DRS_EXT_BASE +
	DRS_EXT_RESTORE_USN_OPTIMIZATION +
	DRS_EXT_INSTANCE_TYPE_NOT_REQ_ON_MOD +
	DRS_EXT_POST_BETA3,
	// 	CRYPTO_BIND + STRONG_ENCRYPTION = 0x00008000, W2K3_DEFLATE + 	GETCHGREQ_V10
	MIN_EXT = DRS_EXT_BASE + DRS_EXT_GETCHGREQ_V8 + DRS_EXT_GETCHGREPLY_V6,
	ALL_EXT = DRS_EXT_BASE +
	DRS_EXT_CRYPTO_BIND +
	DRS_EXT_STRONG_ENCRYPTION +
	DRS_EXT_ASYNCREPL +
	DRS_EXT_REMOVEAPI +
	DRS_EXT_MOVEREQ_V2 +
	DRS_EXT_GETCHG_DEFLATE +
	DRS_EXT_DCINFO_V1 +
	DRS_EXT_RESTORE_USN_OPTIMIZATION +
	DRS_EXT_ADDENTRY +
	DRS_EXT_KCC_EXECUTE +
	DRS_EXT_ADDENTRY_V2 +
	DRS_EXT_LINKED_VALUE_REPLICATION +
	DRS_EXT_DCINFO_V2 +
	DRS_EXT_INSTANCE_TYPE_NOT_REQ_ON_MOD +
	DRS_EXT_GET_REPL_INFO +
	DRS_EXT_DCINFO_VFFFFFFFF +
	DRS_EXT_TRANSITIVE_MEMBERSHIP +
	DRS_EXT_ADD_SID_HISTORY +
	DRS_EXT_POST_BETA3 +
	DRS_EXT_GETCHGREQ_V5 +
	DRS_EXT_GETMEMBERSHIPS2 +
	DRS_EXT_GETCHGREQ_V6 +
	DRS_EXT_NONDOMAIN_NCS +
	DRS_EXT_GETCHGREQ_V8 +
	DRS_EXT_GETCHGREPLY_V5 +
	DRS_EXT_GETCHGREPLY_V6 +
	DRS_EXT_WHISTLER_BETA3 +
	DRS_EXT_W2K3_DEFLATE +
	DRS_EXT_GETCHGREQ_V10
};

DEFINE_ENUM_FLAG_OPERATORS(DRS_EXT)

/// <summary>
/// Newer DRS extensions.
/// Note that Microsoft has this flags as part of DRS_EXT enum, but the DRS_EXT and DRS_EXT2 flags are used exclusively.
/// </summary>
/// <see>DRS_EXT_INT</see>
enum DRS_EXT2 : DWORD
{
	NO_EXTS = 0,
	/// <summary>
	/// If present, signifies that the DC supports DRS_MSG_REPSYNC_V1, DRS_MSG_UPDREFS_V1, DRS_MSG_INIT_DEMOTIONREQ_V1, DRS_MSG_REPLICA_DEMOTIONREQ_V1, and DRS_MSG_FINISH_DEMOTIONREQ_V1.
	/// </summary>
	DRS_EXT_ADAM = 0x00000001,
	/// <summary>
	/// If present, signifies that the DC supports the DRS_SPECIAL_SECRET_PROCESSING and DRS_GET_ALL_GROUP_MEMBERSHIP flags as well as InfoLevel 3 in DRS_MSG_DCINFOREQ_V1.
	/// </summary>
	DRS_EXT_LH_BETA2 = 0x00000002,
	/// <summary>
	/// If present, signifies that the DC has enabled the Recycle Bin optional feature.
	/// </summary>
	DRS_EXT_RECYCLE_BIN = 0x00000004,
	/// <summary>
	/// If present, signifies that the DC supports DRS_MSG_GETCHGREPLY_V9.
	/// </summary>
	DRS_EXT_GETCHGREPLY_V9 = 0x00000100,
	/// <summary>
	/// If present, signifies that the DC has enabled the Privileged Access Management optional feature.
	/// </summary>
	DRS_EXT_PAM = 0x00000200
};

DEFINE_ENUM_FLAG_OPERATORS(DRS_EXT2)

/// <summary>
/// The DRS_EXTENSIONS_INT structure is a concrete type for structured capabilities information used in version negotiation.
/// </summary>
/// <see>https://msdn.microsoft.com/en-us/library/cc228475.aspx</see>
struct DRS_EXTENSIONS_INT
{
	DRS_EXTENSIONS_INT();
	DRS_EXTENSIONS_INT(DRS_EXTENSIONS* genericExtensions);
	/// <summary>
	/// The count of bytes in the fields dwFlags through dwExtCaps, inclusive.
	/// </summary>
	DWORD cb = sizeof(DRS_EXTENSIONS_INT) - sizeof(DWORD);
	/// <summary>
	/// The dwFlags field contains individual bit flags that describe the capabilities of the DC that produced the DRS_EXTENSIONS_INT structure.
	/// </summary>
	DRS_EXT dwFlags = DRS_EXT::NO_EXT;
	/// <summary>
	/// A GUID. The objectGUID of the site object of which the DC's DSA object is a descendant.
	/// For non-DC client callers, this field SHOULD be set to zero.
	/// </summary>
	UUID siteObjGuid = {};
	/// <summary>
	/// A 32-bit, signed integer value that specifies the process identifier of the client.
	/// This is for informational and debugging purposes only.
	/// </summary>
	DWORD pid = 0;
	/// <summary>
	///  A 32-bit, unsigned integer value that specifies the replication epoch.
	/// This value is set to zero by all client callers. The server sets this value by assigning the value of msDS-ReplicationEpoch from its nTDSDSA object.
	/// If dwReplEpoch is not included in DRS_EXTENSIONS_INT, the value is considered to be zero.
	/// </summary>
	DWORD dwReplEpoch = 0;
	/// <summary>
	/// An extension of the dwFlags field that contains individual bit flags. These bit flags determine which extended capabilities are enabled in the DC that produced the DRS_EXTENSIONS_INT structure. For non-DC client callers, no bits SHOULD be set. If dwFlagsExt is not included in DRS_EXTENSIONS_INT, all bit flags are considered unset.
	/// </summary>
	DRS_EXT2 dwFlagsExt = DRS_EXT2::NO_EXTS;
	/// <summary>
	/// A GUID. This field is set to zero by all client callers. The server sets this field by assigning it the value of the objectGUID of the config NC object. If ConfigObjGUID is not included in DRS_EXTENSIONS_INT, the value is considered to be the NULL GUID value.
	/// </summary>
	UUID configObjGUID = {};
	/// <summary>
	/// A mask for the dwFlagsExt field that contains individual bit flags. These bit flags describe the potential extended capabilities of the DC that produced the DRS_EXTENSIONS_INT structure. For non-DC client callers, no bits SHOULD be set.
	/// </summary>
	DRS_EXT2 dwExtCaps = DRS_EXT2::NO_EXTS;
};

/// <summary>
/// DRS_OPTIONS is a concrete type for a set of options sent to and received from various drsuapi methods.
/// </summary>
/// <see>https://msdn.microsoft.com/en-us/library/cc228477.aspx</see>
enum DRS_OPTIONS : DWORD
{
	/// <summary>
	/// Perform the operation asynchronously.
	/// </summary>
	DRS_ASYNC_OP = 0x00000001,
	/// <summary>
	/// Treat ERROR_DS_DRA_REF_NOT_FOUND and ERROR_DS_DRA_REF_ALREADY_EXISTS as success for calls to IDL_DRSUpdateRefs(section 4.1.26).
	/// </summary>
	DRS_GETCHG_CHECK = 0x00000002,
	/// <summary>
	/// Identifies a call to IDL_DRSReplicaSync that was generated due to a replication notification.See[MS - ADTS] section 3.1.1.5.1.6 for more details on replication notifications.This flag is ignored by the server.
	/// </summary>
	DRS_UPDATE_NOTIFICATION = 0x00000002,
	/// <summary>
	/// Register a client DC for notifications of updates to the NC replica.
	/// </summary>
	DRS_ADD_REF = 0x00000004,
	/// <summary>
	/// Replicate from all server DCs.
	/// </summary>
	DRS_SYNC_ALL = 0x00000008,
	/// <summary>
	/// Deregister a client DC from notifications of updates to the NC replica.
	/// </summary>
	DRS_DEL_REF = 0x00000008,
	/// <summary>
	/// Replicate a writable replica, not a read - only partial replica or read - only full replica.
	/// </summary>
	DRS_WRIT_REP = 0x00000010,
	/// <summary>
	/// Perform replication at startup.
	/// </summary>
	DRS_INIT_SYNC = 0x00000020,
	/// <summary>
	/// Perform replication periodically.
	/// </summary>
	DRS_PER_SYNC = 0x00000040,
	/// <summary>
	/// Perform replication using SMTP as a transport.
	/// </summary>
	DRS_MAIL_REP = 0x00000080,
	/// <summary>
	/// Populate the NC replica asynchronously.
	/// </summary>
	DRS_ASYNC_REP = 0x00000100,
	/// <summary>
	///  Ignore errors.
	/// </summary>
	DRS_IGNORE_ERROR = 0x00000100,
	/// <summary>
	/// Inform the server DC to replicate from the client DC.
	/// </summary>
	DRS_TWOWAY_SYNC = 0x00000200,
	/// <summary>
	/// Replicate only system - critical objects.
	/// </summary>
	DRS_CRITICAL_ONLY = 0x00000400,
	/// <summary>
	/// Include updates to ancestor objects before updates to their descendants.
	/// </summary>
	DRS_GET_ANC = 0x00000800,
	/// <summary>
	/// Get the approximate size of the server NC replica.
	/// </summary>
	DRS_GET_NC_SIZE = 0x00001000,
	/// <summary>
	/// Perform the operation locally without contacting any other DC.
	/// </summary>
	DRS_LOCAL_ONLY = 0x00001000,
	/// <summary>
	/// Replicate a read - only full replica.Not a writable or partial replica.
	/// </summary>
	DRS_NONGC_RO_REP = 0x00002000,
	/// <summary>
	/// Choose the source server by network name.
	/// </summary>
	DRS_SYNC_BYNAME = 0x00004000,
	/// <summary>
	/// Allow the NC replica to be removed even if other DCs use this DC as a replication server DC.
	/// </summary>
	DRS_REF_OK = 0x00004000,
	/// <summary>
	/// Replicate all updates in the replication cycle, even those that would normally be filtered.
	/// </summary>
	DRS_FULL_SYNC_NOW = 0x00008000,
	/// <summary>
	///  The NC replica has no server DCs.
	/// </summary>
	DRS_NO_SOURCE = 0x00008000,
	/// <summary>
	/// When the flag DRS_FULL_SYNC_NOW is received in a call to IDL_DRSReplicaSync, the flag DRS_FULL_SYNC_IN_PROGRESS is sent in the associated calls to IDL_DRSGetNCChanges until the replication cycle completes.This flag is ignored by the server.
	/// </summary>
	DRS_FULL_SYNC_IN_PROGRESS = 0x00010000,
	/// <summary>
	/// Replicate all updates in the replication request, even those that would normally be filtered.
	/// </summary>
	DRS_FULL_SYNC_PACKET = 0x00020000,
	/// <summary>
	/// This flag is specific to the Microsoft client implementation of IDL_DRSGetNCChanges.It is used to identify whether the call was placed in the replicationQueue more than once due to implementation - specific errors.This flag is ignored by the server.
	/// </summary>
	DRS_SYNC_REQUEUE = 0x00040000,
	/// <summary>
	///  Perform the requested replication immediately, do not wait for any timeouts or delays.For information about urgent replication, see[MS - ADTS] section 3.1.1.5.1.7.
	/// </summary>
	DRS_SYNC_URGENT = 0x00080000,
	/// <summary>
	/// Requests that the server add an entry to repsTo for the client on the root object of the NC replica that is being replicated.When repsTo is set using this flag, the notifying client DC contacts the server DC using the service principal name that begins with "GC" (section 2.2.3.2).
	/// </summary>
	DRS_REF_GCSPN = 0x00100000,
	/// <summary>
	///  This flag is specific to the Microsoft implementation.It identifies when the client DC should call the requested IDL_DRSReplicaSync method individually, without overlapping other outstanding calls to IDL_DRSReplicaSync.This flag is ignored by the server.
	/// </summary>
	DRS_NO_DISCARD = 0x00100000,
	/// <summary>
	/// There is no successfully completed replication from this source server.
	/// </summary>
	DRS_NEVER_SYNCED = 0x00200000,
	/// <summary>
	/// Do not replicate attribute values of attributes that contain secret data.
	/// </summary>
	DRS_SPECIAL_SECRET_PROCESSING = 0x00400000,
	/// <summary>
	///  Perform initial replication now.
	/// </summary>
	DRS_INIT_SYNC_NOW = 0x00800000,
	/// <summary>
	/// The replication attempt is preempted by a higher priority replication request.
	/// </summary>
	DRS_PREEMPTED = 0x01000000,
	/// <summary>
	/// Force replication, even if the replication system is otherwise disabled.
	/// </summary>
	DRS_SYNC_FORCED = 0x02000000,
	/// <summary>
	/// Disable replication induced by update notifications.
	/// </summary>
	DRS_DISABLE_AUTO_SYNC = 0x04000000,
	/// <summary>
	/// Disable periodic replication.
	/// </summary>
	DRS_DISABLE_PERIODIC_SYNC = 0x08000000,
	/// <summary>
	/// Compress response messages.
	/// </summary>
	DRS_USE_COMPRESSION = 0x10000000,
	/// <summary>
	/// Do not send update notifications.
	/// </summary>
	DRS_NEVER_NOTIFY = 0x20000000,
	/// <summary>
	/// Expand the partial attribute set of the partial replica.
	/// </summary>
	DRS_SYNC_PAS = 0x40000000,
	/// <summary>
	/// Replicate all kinds of group membership.If this flag is not present nonuniversal group membership will not be replicated.
	/// </summary>
	DRS_GET_ALL_GROUP_MEMBERSHIP = 0x80000000
};

DEFINE_ENUM_FLAG_OPERATORS(DRS_OPTIONS)

/// <summary>
/// The following values are request codes for extended operation.
/// </summary>
/// <see>https://msdn.microsoft.com/en-us/library/dd207705.aspx</see>
enum EXOP_REQ : DWORD
{
	NO_EXOP = 0,
	/// <summary>
	/// Request a FSMO role owner transfer.
	/// </summary>
	EXOP_FSMO_REQ_ROLE = 0x00000001,
	/// <summary>
	/// Request RID allocation from the RID Master FSMO role owner.
	/// </summary>
	EXOP_FSMO_REQ_RID_ALLOC = 0x00000002,
	/// <summary>
	/// Request transfer of the RID Master FSMO role.
	/// </summary>
	EXOP_FSMO_RID_REQ_ROLE = 0x00000003,
	/// <summary>
	/// Request transfer of the PDC FSMO role.
	/// </summary>
	EXOP_FSMO_REQ_PDC = 0x00000004,
	/// <summary>
	/// Performs a chained request to the current FSMO role owner to make the server DC the FSMO role owner. This request is sent to help avoid entering a state in which no DC considers itself the owner of the role.
	/// </summary>
	EXOP_FSMO_ABANDON_ROLE = 0x00000005,
	/// <summary>
	/// Replicate Single Object. Adds any changes to the specified object to the response.
	/// </summary>
	EXOP_REPL_OBJ = 0x00000006,
	/// <summary>
	///  Replicate Single Object including Secret Data. Adds any changes to the specified object to the response. In addition, it also adds the secret attribute values of the specified object to the response, regardless of whether they have recent changes.
	/// </summary>
	EXOP_REPL_SECRETS = 0x00000007
};

DEFINE_ENUM_FLAG_OPERATORS(EXOP_REQ)

// Function wrappers that translate SEH exceptions to Win32 error codes:

ULONG IDL_DRSBind_NoSEH(
	/* [in] */ handle_t rpc_handle,
	/* [unique][in] */ UUID* puuidClientDsa,
	/* [unique][in] */ DRS_EXTENSIONS* pextClient,
	/* [out] */ DRS_EXTENSIONS** ppextServer,
	/* [ref][out] */ DRS_HANDLE* phDrs);

ULONG IDL_DRSGetNCChanges_NoSEH(
	/* [ref][in] */ DRS_HANDLE hDrs,
	/* [in] */ DWORD dwInVersion,
	/* [switch_is][ref][in] */ DRS_MSG_GETCHGREQ* pmsgIn,
	/* [ref][out] */ DWORD* pdwOutVersion,
	/* [switch_is][ref][out] */ DRS_MSG_GETCHGREPLY* pmsgOut);

ULONG IDL_DRSCrackNames_NoSEH(
	/* [ref][in] */ DRS_HANDLE hDrs,
	/* [in] */ DWORD dwInVersion,
	/* [switch_is][ref][in] */ DRS_MSG_CRACKREQ* pmsgIn,
	/* [ref][out] */ DWORD* pdwOutVersion,
	/* [switch_is][ref][out] */ DRS_MSG_CRACKREPLY* pmsgOut);

ULONG IDL_DRSGetReplInfo_NoSEH(
	/* [ref][in] */ DRS_HANDLE hDrs,
	/* [in] */ DWORD dwInVersion,
	/* [switch_is][ref][in] */ DRS_MSG_GETREPLINFO_REQ* pmsgIn,
	/* [ref][out] */ DWORD* pdwOutVersion,
	/* [switch_is][ref][out] */ DRS_MSG_GETREPLINFO_REPLY* pmsgOut);

ULONG IDL_DRSWriteNgcKey_NoSEH(
	/* [in, ref] */ DRS_HANDLE hDrs,
	/* [in] */ DWORD dwInVersion,
	/* [in, ref, switch_is(dwInVersion)]*/ DRS_MSG_WRITENGCKEYREQ* pmsgIn,
	/* [out, ref] */ DWORD* pdwOutVersion,
	/* [out, ref, switch_is(*pdwOutVersion)] */ DRS_MSG_WRITENGCKEYREPLY* pmsgOut);

ULONG IDL_DRSUnbind_NoSEH(
	/* [ref][out][in] */ DRS_HANDLE* phDrs);
