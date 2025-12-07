#pragma once
#include "drsr.h"
#include "drsr_alloc.h"
#include <string>

#define nameof(parameterName) #parameterName

namespace DSInternals
{
	namespace Replication
	{
		namespace Interop
		{
			using namespace DSInternals::Common::Data;
			using namespace DSInternals::Common::Schema;
			using namespace DSInternals::Replication::Model;
            using namespace System::Collections::Generic;
            using namespace System::Security::Principal;
			using namespace Microsoft::Win32::SafeHandles;
			using namespace System;
			using namespace System::Runtime::InteropServices;

			delegate void SecurityCallback(void* rpcContext);

			public ref class DrsConnection : SafeHandleZeroOrMinusOneIsInvalid
			{
			private:
				literal size_t DefaultMaxObjects = 1000;
				literal size_t DefaultMaxBytes = 8 * 1024 * 1024; // 8MB
				literal DWORD DefaultReplEpoch = 0;
				literal String^ UpnSeparator = "@";
				literal String^ DummyName = "NULL";
				// The default session key is "{B84290B0-0C3E-4542-B409-A96DF8DE3D93}"
				static initonly cli::array<byte>^ DefaultSessionKey = gcnew cli::array<byte> { 0xb8, 0x42, 0x90, 0xb0, 0x0c, 0x3e, 0x45, 0x42, 0xb4, 0x09, 0xa9, 0x6d, 0xf8, 0xde, 0x3d, 0x93 };

				cli::array<byte>^ _sessionKey;
				BaseSchema^ _schema;
				Guid _clientDsa;
				Guid _serverSiteObjectGuid;
				Guid _configurationObjectGuid;
				DRS_EXT _serverCapabilities;
				DWORD _serverReplEpoch;
				SecurityCallback^ _securityCallback;
			public:
				DrsConnection(IntPtr rpcHandle, Guid clientDsa, BaseSchema^ schema);
				DrsConnection(IntPtr preexistingDrssHandle, bool ownsHandle);
				property cli::array<byte>^ SessionKey
				{
					cli::array<byte>^ get();
				}
				property Guid ServerSiteGuid
				{
					Guid get();
				}
				property Guid ConfigurationPartitionGuid
				{
					Guid get();
				}
				cli::array<ReplicationCursor^>^ GetReplicationCursors(String^ namingContext);
				ReplicaObject^ ReplicateSingleObject(Guid objectGuid);
				ReplicaObject^ ReplicateSingleObject(Guid objectGuid, cli::array<ATTRTYP>^ partialAttributeSet);
				ReplicaObject^ ReplicateSingleObject(String^ distinguishedName);
				ReplicaObject^ ReplicateSingleObject(String^ distinguishedName, cli::array<ATTRTYP>^ partialAttributeSet);
				ReplicationResult^ ReplicateAllObjects(ReplicationCookie^ cookie);
				ReplicationResult^ ReplicateAllObjects(ReplicationCookie^ cookie, ULONG maxBytes, ULONG maxObjects);
				ReplicationResult^ ReplicateAllObjects(ReplicationCookie^ cookie, cli::array<ATTRTYP>^ partialAttributeSet, ULONG maxBytes, ULONG maxObjects);
				String^ ResolveDistinguishedName(NTAccount^ accountName);
				String^ ResolveDistinguishedName(SecurityIdentifier^ objectSid);
				String^ ResolveDistinguishedName(Guid objectGuid);
				Guid ResolveGuid(NTAccount^ accountName);
				Guid ResolveGuid(SecurityIdentifier^ objectSid);
				NTAccount^ ResolveAccountName(String^ distinguishedName);
				ActiveDirectoryRoleInformation^ ListRoles();
				array<String^>^ ListSites();
				array<String^>^ ListNamingContexts();
				array<String^>^ ListDomains();
				array<String^>^ ListServersInSite(String^ name);
				array<String^>^ ListDomainsInSite(String^ name);
				DomainControllerInformation^ ListInfoForServer(String^ name);
				bool TestObjectExistence(String^ distinguishedName);
				bool TestObjectExistence(Guid objectGuid);
				void WriteNgcKey(String^ distinguishedName, cli::array<byte>^ key);
                void UpdateSchemaCache(BaseSchema^ newSchema);
			protected:
				virtual bool ReleaseHandle() override;
			private:
				property DWORD MaxSupportedReplicationRequestVersion
				{
					DWORD get();
				}
				DrsConnection();
				void Bind(IntPtr rpcHandle);
				midl_ptr<DRS_MSG_GETCHGREPLY_V9> GetNCChanges(midl_ptr<DRS_MSG_GETCHGREQ_V10>&& request);
				midl_ptr<DRS_MSG_CRACKREPLY_V1> CrackNames(midl_ptr<DRS_MSG_CRACKREQ_V1>&& request);
				array<String^>^ ResolveNames(array<String^>^ names, DS_NAME_FORMAT formatOffered, DS_NAME_FORMAT formatDesired, bool mustExist);
				String^ ResolveName(String^ name, DS_NAME_FORMAT formatOffered, DS_NAME_FORMAT formatDesired, bool mustExist);
				array<String^>^ ListInfo(DS_NAME_FORMAT_EXT infoType);
				array<String^>^ ListInfo(DS_NAME_FORMAT_EXT infoType, String^ targetName);
				midl_ptr<DRS_EXTENSIONS_INT> CreateClientInfo();
				midl_ptr<DRS_MSG_GETCHGREQ_V10> CreateReplicateAllRequest(ReplicationCookie^ cookie, array<ATTRTYP>^ partialAttributeSet, ULONG maxBytes, ULONG maxObjects);
				midl_ptr<DRS_MSG_GETCHGREQ_V10> CreateReplicateSingleRequest(String^ distinguishedName, array<ATTRTYP>^ partialAttributeSet);
				midl_ptr<DRS_MSG_GETCHGREQ_V10> CreateReplicateSingleRequest(Guid objectGuid, array<ATTRTYP>^ partialAttributeSet);
				midl_ptr<DRS_MSG_GETCHGREQ_V10> CreateGenericReplicateRequest(midl_ptr<DSNAME>&& dsName, array<ATTRTYP>^ partialAttributeSet, ULONG maxBytes, ULONG maxObjects);
				midl_ptr<DRS_MSG_WRITENGCKEYREQ_V1> CreateWriteNgcKeyRequest(String^ distinguishedName, array<Byte>^ key);
				void RetrieveSessionKey(void* rpcContext);
				void ValidateConnection();
				static midl_ptr<DRS_MSG_GETREPLINFO_REQ_V1> CreateReplicationCursorsRequest(String^ namingContext);
				static array<byte>^ ReadValue(const ATTRVAL& value);
				static array<array<byte>^>^ ReadValues(const ATTRVALBLOCK& values);
				static ReplicaAttribute^ ReadAttribute(const ATTR& attribute);
				static ReplicaAttribute^ ReadAttribute(const REPLVALINF_V3& attribute);
				static ReplicaAttributeCollection^ ReadAttributes(const ATTRBLOCK& attributes);
				static ReplicaObject^ ReadObject(const ENTINF& object, BaseSchema^ schema);
				static List<ReplicaObject^>^ ReadObjects(const REPLENTINFLIST* objects, int objectCount, const REPLVALINF_V3* linkedValues, int valueCount, BaseSchema^ schema);
				static DS_NAME_FORMAT GetAccountNameFormat(NTAccount^ accountName);
			};
		}
	}
}
