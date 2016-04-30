#pragma once
#include "drsr.h"
#include "drsr_alloc.h"
#include <string>

namespace DSInternals
{
	namespace Replication
	{
		namespace Interop
		{
			using namespace DSInternals::Common::Data;
			using namespace DSInternals::Replication::Model;
			using namespace System::Security::Principal;
			using namespace Microsoft::Win32::SafeHandles;
			using namespace System;
			using namespace System::Runtime::InteropServices;

			delegate void SecurityCallback(void* rpcContext);

			public ref class DrsConnection : SafeHandleZeroOrMinusOneIsInvalid
			{
			private:
				array<byte>^ _sessionKey;
				Guid _clientDsa;
				Guid _serverSiteObjectGuid;
				DWORD _serverReplEpoch;
				SecurityCallback^ _securityCallback;
				static const size_t defaultMaxObjects = 1000;
				// 8MB
				static const size_t defaultMaxBytes = 8 * 1024 * 1024;
				static const DWORD defaultReplEpoch = 0;
			public:
				DrsConnection(IntPtr rpcHandle, Guid clientDsa);
				DrsConnection(IntPtr preexistingDrssHandle, bool ownsHandle);
				property array<byte>^ SessionKey
				{
					array<byte>^ get();
					void set(array<byte>^ newKey);
				}
				ReplicaObject^ ReplicateSingleObject(Guid objectGuid);
				ReplicaObject^ ReplicateSingleObject(Guid objectGuid, array<ATTRTYP>^ partialAttributeSet);
				ReplicaObject^ ReplicateSingleObject(String^ distinguishedName);
				ReplicaObject^ ReplicateSingleObject(String^ distinguishedName, array<ATTRTYP>^ partialAttributeSet);
				ReplicationResult^ ReplicateAllObjects(ReplicationCookie^ cookie);
				ReplicationResult^ ReplicateAllObjects(ReplicationCookie^ cookie, ULONG maxBytes, ULONG maxObjects);
				ReplicationResult^ ReplicateAllObjects(ReplicationCookie^ cookie, array<ATTRTYP>^ partialAttributeSet, ULONG maxBytes, ULONG maxObjects);
				String^ ResolveDistinguishedName(NTAccount^ accountName);
				String^ ResolveDistinguishedName(SecurityIdentifier^ objectSid);
				Guid ResolveGuid(NTAccount^ accountName);
				Guid ResolveGuid(SecurityIdentifier^ objectSid);
			protected:
				virtual bool ReleaseHandle() override;
			private:
				DrsConnection();
				void Bind(IntPtr rpcHandle);
				midl_ptr<DRS_MSG_GETCHGREPLY_V6> GetNCChanges(midl_ptr<DRS_MSG_GETCHGREQ_V8> &&request);
				midl_ptr<DRS_MSG_CRACKREPLY_V1> CrackNames(midl_ptr<DRS_MSG_CRACKREQ_V1> &&request);
				String^ ResolveName(String^ name, DS_NAME_FORMAT formatOffered, DS_NAME_FORMAT formatDesired);
				String^ ResolveName(midl_ptr<DRS_MSG_CRACKREQ_V1> &&request);
				midl_ptr<DRS_EXTENSIONS_INT> CreateClientInfo();
				midl_ptr<DRS_MSG_GETCHGREQ_V8> CreateReplicateAllRequest(ReplicationCookie^ cookie, array<ATTRTYP>^ partialAttributeSet, ULONG maxBytes, ULONG maxObjects);
				midl_ptr<DRS_MSG_GETCHGREQ_V8> CreateReplicateSingleRequest(String^ distinguishedName, array<ATTRTYP>^ partialAttributeSet);
				midl_ptr<DRS_MSG_GETCHGREQ_V8> CreateReplicateSingleRequest(Guid objectGuid, array<ATTRTYP>^ partialAttributeSet);
				midl_ptr<DRS_MSG_GETCHGREQ_V8> CreateGenericReplicateRequest(midl_ptr<DSNAME> &&dsName, array<ATTRTYP>^ partialAttributeSet, ULONG maxBytes, ULONG maxObjects);
				void RetrieveSessionKey(void* rpcContext);
				static midl_ptr<PARTIAL_ATTR_VECTOR_V1_EXT> CreateNativePas(array<ATTRTYP>^ partialAttributeSet);
				static array<byte>^ ReadValue(const ATTRVAL &value);
				static array<array<byte>^>^ ReadValues(const ATTRVALBLOCK &values);
				static ReplicaAttribute^ ReadAttribute(const ATTR &attribute);
				static ReplicaAttributeCollection^ ReadAttributes(const ATTRBLOCK &attributes);
				static ReplicaObject^ ReadObject(const ENTINF &object);
				static ReplicaObjectCollection^ ReadObjects(const REPLENTINFLIST *objects, int count);
				static Guid ReadGuid(GUID const &guid);
				static String^ ReadName(const DSNAME* dsName);
				static SecurityIdentifier^ ReadSid(const DSNAME* dsName);
			};
		}
	}
}
