#include "stdafx.h"
#include "DrsConnection.h"
#include "RpcTypeConverter.h"

using namespace DSInternals::Common;
using namespace DSInternals::Common::Interop;
using namespace DSInternals::Replication::Model;

using namespace System;
using namespace System::ComponentModel;
using namespace System::Reflection;
using namespace System::Security::Principal;
using namespace System::Runtime::InteropServices;
using namespace Microsoft::Win32::SafeHandles;
using namespace msclr::interop;

namespace DSInternals
{
	namespace Replication
	{
		namespace Interop
		{
			DrsConnection::DrsConnection(IntPtr rpcHandle, Guid clientDsa)
				: SafeHandleZeroOrMinusOneIsInvalid(true)
			{
				this->_clientDsa = clientDsa;
				this->_serverReplEpoch = DrsConnection::defaultReplEpoch;

				// Register the RetrieveSessionKey as RCP security callback. Mind the delegate lifecycle.
				this->_securityCallback = gcnew SecurityCallback(this, &DrsConnection::RetrieveSessionKey);
				RPC_STATUS status = RpcBindingSetOption(rpcHandle.ToPointer(), RPC_C_OPT_SECURITY_CALLBACK, (ULONG_PTR)Marshal::GetFunctionPointerForDelegate(this->_securityCallback).ToPointer());

				this->Bind(rpcHandle);
				if (this->_serverReplEpoch != DrsConnection::defaultReplEpoch)
				{
					// The domain must have been renamed, so we need to rebind with the proper dwReplEpoch.
					this->ReleaseHandle();
					this->Bind(rpcHandle);
				}
			}

			DrsConnection::DrsConnection(IntPtr preexistingDrssHandle, bool ownsHandle)
				: SafeHandleZeroOrMinusOneIsInvalid(ownsHandle)
			{
				this->SetHandle(preexistingDrssHandle);
			}

			DrsConnection::DrsConnection()
				: SafeHandleZeroOrMinusOneIsInvalid(true)
			{
			}

			void DrsConnection::Bind(IntPtr rpcHandle)
			{
				// Init binding parameters
				UUID clientDsaUuid = RpcTypeConverter::ToUUID(this->_clientDsa);
				auto clientInfo = this->CreateClientInfo();
				DRS_EXTENSIONS *genericServerInfo = nullptr;
				DRS_HANDLE drsHandle = nullptr;

				// Bind
				ULONG result = IDL_DRSBind_NoSEH(rpcHandle.ToPointer(), &clientDsaUuid, (DRS_EXTENSIONS*)clientInfo.get(), &genericServerInfo, &drsHandle);
				Validator::AssertSuccess((Win32ErrorCode)result);
				
				// Prevent memory leak by storing the genericServerInfo in midl_ptr
				auto genericServerInfoSafePtr = midl_ptr<DRS_EXTENSIONS>(genericServerInfo);

				// Store the DRS handle
				this->SetHandle((IntPtr)drsHandle);

				// Parse the server info
				DRS_EXTENSIONS_INT serverInfo = DRS_EXTENSIONS_INT(genericServerInfo);
				this->_serverSiteObjectGuid = RpcTypeConverter::ToGuid(serverInfo.siteObjGuid);
				this->_serverReplEpoch = serverInfo.dwReplEpoch;
			}

			array<byte>^ DrsConnection::SessionKey::get()
			{
				return this->_sessionKey;
			}
			void DrsConnection::SessionKey::set(array<byte>^ newKey)
			{
				this->_sessionKey = newKey;
			}

			midl_ptr<DRS_EXTENSIONS_INT> DrsConnection::CreateClientInfo()
			{
				auto clientInfo = make_midl_ptr<DRS_EXTENSIONS_INT>();
				clientInfo->dwFlags = DRS_EXT::ALL_EXT;
				clientInfo->dwFlagsExt = DRS_EXT2::DRS_EXT_LH_BETA2;
				clientInfo->dwExtCaps = DRS_EXT2::DRS_EXT_LH_BETA2 | DRS_EXT2::DRS_EXT_RECYCLE_BIN | DRS_EXT2::DRS_EXT_PAM;
				clientInfo->dwReplEpoch = this->_serverReplEpoch;
				return clientInfo;
			}

			midl_ptr<DRS_MSG_GETCHGREQ_V8> DrsConnection::CreateGenericReplicateRequest(midl_ptr<DSNAME> &&dsName, array<ATTRTYP>^ partialAttributeSet, ULONG maxBytes, ULONG maxObjects)
			{
				// TODO: Add support for Windows Server 2003
				auto request = make_midl_ptr<DRS_MSG_GETCHGREQ_V8>();
				// Inset client ID:
				request->uuidDsaObjDest = RpcTypeConverter::ToUUID(this->_clientDsa);
				// Insert DSNAME
				request->pNC = dsName.release(); // Note: Request deleter will also delete DSNAME.
				// Insert PAS:
				auto nativePas = CreateNativePas(partialAttributeSet);
				request->pPartialAttrSetEx = nativePas.release(); // Note: Request deleter will also delete PAS.
				// Insert response size limits:
				request->cMaxBytes = maxBytes;
				request->cMaxObjects = maxObjects;
				// Set correct flags:
				// TODO: + DRS_OPTIONS::PER_SYNC ?
				request->ulFlags = DRS_OPTIONS::DRS_INIT_SYNC |
					DRS_OPTIONS::DRS_WRIT_REP |
					DRS_OPTIONS::DRS_NEVER_SYNCED;
				return request;
			}

			midl_ptr<DRS_MSG_GETCHGREQ_V8> DrsConnection::CreateReplicateAllRequest(ReplicationCookie^ cookie, array<ATTRTYP>^ partialAttributeSet, ULONG maxBytes, ULONG maxObjects)
			{
				auto ncToReplicate = RpcTypeConverter::ToDsName(cookie->NamingContext);
				auto request = CreateGenericReplicateRequest(move(ncToReplicate), partialAttributeSet, maxBytes, maxObjects);
				// Insert replication state from cookie:
				request->usnvecFrom.usnHighObjUpdate = cookie->HighObjUpdate;
				request->usnvecFrom.usnHighPropUpdate = cookie->HighPropUpdate;
				request->usnvecFrom.usnReserved = cookie->Reserved;
				request->uuidInvocIdSrc = RpcTypeConverter::ToUUID(cookie->InvocationId);
				return request;
			}

			midl_ptr<DRS_MSG_GETCHGREQ_V8> DrsConnection::CreateReplicateSingleRequest(Guid objectGuid, array<ATTRTYP>^ partialAttributeSet)
			{
				auto objectToReplicate = RpcTypeConverter::ToDsName(objectGuid);
				// TODO: Are sizes important?
				auto request = CreateGenericReplicateRequest(move(objectToReplicate), partialAttributeSet, defaultMaxBytes, defaultMaxObjects);
				request->ulExtendedOp = EXOP_REQ::EXOP_REPL_OBJ;
				// Guid of an existing DC must be set for the replication to work
				request->uuidDsaObjDest = RpcTypeConverter::ToUUID(this->_serverSiteObjectGuid);
				return request;
			}

			midl_ptr<DRS_MSG_GETCHGREQ_V8> DrsConnection::CreateReplicateSingleRequest(String^ distinguishedName, array<ATTRTYP>^ partialAttributeSet)
			{
				auto objectToReplicate = RpcTypeConverter::ToDsName(distinguishedName);
				// TODO: Are sizes important?
				auto request = CreateGenericReplicateRequest(move(objectToReplicate), partialAttributeSet, defaultMaxBytes, defaultMaxObjects);
				request->ulExtendedOp = EXOP_REQ::EXOP_REPL_OBJ;
				// Guid of an existing DC must be set for the replication to work
				request->uuidDsaObjDest = RpcTypeConverter::ToUUID(this->_serverSiteObjectGuid);
				return request;
			}

			ReplicationResult^ DrsConnection::ReplicateAllObjects(ReplicationCookie^ cookie)
			{
				return this->ReplicateAllObjects(cookie, nullptr, DrsConnection::defaultMaxBytes, DrsConnection::defaultMaxObjects);
			}

			ReplicationResult^ DrsConnection::ReplicateAllObjects(ReplicationCookie^ cookie, ULONG maxBytes, ULONG maxObjects)
			{
				return this->ReplicateAllObjects(cookie, nullptr, maxBytes, maxObjects);
			}

			ReplicationResult^ DrsConnection::ReplicateAllObjects(ReplicationCookie^ cookie, array<ATTRTYP>^ partialAttributeSet, ULONG maxBytes, ULONG maxObjects)
			{
				// TODO: Validate Cookie not null
				// TODO: To Params
				auto request = CreateReplicateAllRequest(cookie, partialAttributeSet, maxBytes, maxObjects);
				auto reply = GetNCChanges(move(request));
				auto objects = ReadObjects(reply->pObjects, reply->cNumObjects);
				USN_VECTOR usnTo = reply->usnvecTo;
				Guid invocationId = RpcTypeConverter::ToGuid(reply->uuidInvocIdSrc);
				auto newCookie = gcnew ReplicationCookie(cookie->NamingContext, invocationId, usnTo.usnHighObjUpdate, usnTo.usnHighPropUpdate, usnTo.usnReserved);
				bool hasMoreData = reply->fMoreData != 0;
				return gcnew ReplicationResult(objects, hasMoreData, newCookie);
			}

			ReplicaObject^ DrsConnection::ReplicateSingleObject(String^ distinguishedName)
			{
				return this->ReplicateSingleObject(distinguishedName, nullptr);
			}

			ReplicaObject^ DrsConnection::ReplicateSingleObject(String^ distinguishedName, array<ATTRTYP>^ partialAttributeSet)
			{
				auto request = CreateReplicateSingleRequest(distinguishedName, partialAttributeSet);
				auto reply = GetNCChanges(move(request));
				auto objects = ReadObjects(reply->pObjects, reply->cNumObjects);
				// TODO: Assert objects.Count == 1; It is guaranteed that it is > 0
				return objects[0];
			}

			ReplicaObject^ DrsConnection::ReplicateSingleObject(Guid objectGuid)
			{
				return this->ReplicateSingleObject(objectGuid, nullptr);
			}

			ReplicaObject^ DrsConnection::ReplicateSingleObject(Guid objectGuid, array<ATTRTYP>^ partialAttributeSet)
			{
				auto request = CreateReplicateSingleRequest(objectGuid, partialAttributeSet);
				auto reply = GetNCChanges(move(request));
				auto objects = ReadObjects(reply->pObjects, reply->cNumObjects);
				// TODO: Assert objects.Count == 1; It is guaranteed that it is > 0
				return objects[0];
			}

			midl_ptr<DRS_MSG_GETCHGREPLY_V6> DrsConnection::GetNCChanges(midl_ptr<DRS_MSG_GETCHGREQ_V8> &&request)
			{
				// Validate connection
				if (this->IsInvalid)
				{
					// TODO: Exception type
					throw gcnew Exception("Not connected");
				}
				DRS_HANDLE handle = this->handle.ToPointer();
				const DWORD inVersion = 8;
				DWORD outVersion = 0;
				auto reply = make_midl_ptr<DRS_MSG_GETCHGREPLY_V6>();
				// Send message:
				ULONG result = IDL_DRSGetNCChanges_NoSEH(handle, inVersion, (DRS_MSG_GETCHGREQ*)request.get(), &outVersion, (DRS_MSG_GETCHGREPLY*)reply.get());
				Validator::AssertSuccess((Win32ErrorCode)result);
				// TODO: Check the returned version
				// Validate result
				if (reply->cNumObjects == 0)
				{
					// TODO: DirectoryObjectNotFound ex.
					throw gcnew Exception("Directory object not found.");
				}
				// TODO: Test extended error code:
				DWORD extendedError = reply->dwDRSError;
				return reply;
			}

			midl_ptr<DRS_MSG_CRACKREPLY_V1> DrsConnection::CrackNames(midl_ptr<DRS_MSG_CRACKREQ_V1> &&request)
			{
				// Validate connection
				if (this->IsInvalid)
				{
					// TODO: Exception type
					throw gcnew Exception("Not connected");
				}
				const DWORD inVersion = 1;
				// TODO: Check the returned version
				DWORD outVersion = 0;
				midl_ptr<DRS_MSG_CRACKREPLY_V1> reply = make_midl_ptr<DRS_MSG_CRACKREPLY_V1>();
				DRS_HANDLE handle = this->handle.ToPointer();
				auto result = IDL_DRSCrackNames_NoSEH(handle, inVersion, (DRS_MSG_CRACKREQ*)request.get(), &outVersion, (DRS_MSG_CRACKREPLY*)reply.get());
				Validator::AssertSuccess((Win32ErrorCode)result);

				if (reply->pResult->cItems != request->cNames)
				{
					// TODO: Exxception type
					throw gcnew Exception("Obj not found");
				}
				return reply;
			}

			String^ DrsConnection::ResolveDistinguishedName(NTAccount^ accountName)
			{
				auto stringAccountName = accountName->Value;
				auto dn = this->ResolveName(stringAccountName, DS_NAME_FORMAT::DS_NT4_ACCOUNT_NAME, DS_NAME_FORMAT::DS_FQDN_1779_NAME);
				return dn;
			}

			String^ DrsConnection::ResolveDistinguishedName(SecurityIdentifier^ objectSid)
			{
				auto stringSid = objectSid->ToString();
				auto dn = this->ResolveName(stringSid, DS_NAME_FORMAT::DS_SID_OR_SID_HISTORY_NAME, DS_NAME_FORMAT::DS_FQDN_1779_NAME);
				return dn;
			}

			Guid DrsConnection::ResolveGuid(NTAccount^ accountName)
			{
				auto stringAccountName = accountName->Value;
				auto stringGuid = this->ResolveName(stringAccountName, DS_NAME_FORMAT::DS_NT4_ACCOUNT_NAME, DS_NAME_FORMAT::DS_UNIQUE_ID_NAME);
				return Guid::Parse(stringGuid);
			}

			Guid DrsConnection::ResolveGuid(SecurityIdentifier^ objectSid)
			{
				auto stringSid = objectSid->ToString();
				auto stringGuid = this->ResolveName(stringSid, DS_NAME_FORMAT::DS_SID_OR_SID_HISTORY_NAME, DS_NAME_FORMAT::DS_UNIQUE_ID_NAME);
				return Guid::Parse(stringGuid);
			}

			String^ DrsConnection::ResolveName(String^ name, DS_NAME_FORMAT formatOffered, DS_NAME_FORMAT formatDesired)
			{
				// We only want to resolve 1 name at a time:
				const size_t numItems = 1;
				auto request = make_midl_ptr<DRS_MSG_CRACKREQ_V1>(numItems);
				request->formatOffered = formatOffered;
				request->formatDesired = formatDesired;
				request->rpNames[0] = RpcTypeConverter::ToNativeString(name).release();
				auto result = this->ResolveName(move(request));
				// TODO: Merge with ResolveName(midl_ptr<DRS_MSG_CRACKREQ_V1> &&request)?
				return result;
			}

			String^ DrsConnection::ResolveName(midl_ptr<DRS_MSG_CRACKREQ_V1> &&request)
			{
				auto reply = this->CrackNames(move(request));
				auto item = reply->pResult->rItems[0];
				// TODO: const 0
				if (item.status != 0)
				{
					// TODO: Exception type
					throw gcnew Exception("Object not found");
				}
				auto name = marshal_as<String^>(item.pName);
				return name;
			}

			bool DrsConnection::ReleaseHandle()
			{
				DRS_HANDLE ptr = this->handle.ToPointer();
				ULONG result = IDL_DRSUnbind_NoSEH(&ptr);
				// Do not validate result, as we do not want any exceptions in ReleaseHandle
				this->handle = (IntPtr)ptr;
				// Return true if the pointer has been nulled by the UnBind operation
				return ptr == nullptr;
			}
			
			midl_ptr<PARTIAL_ATTR_VECTOR_V1_EXT> DrsConnection::CreateNativePas(array<ATTRTYP>^ partialAttributeSet)
			{
				// TODO: Move to type converter?
				if (partialAttributeSet == nullptr)
				{
					return nullptr;
				}
				auto attrCount = partialAttributeSet->Length;
				if (attrCount < 1)
				{
					// Must request at least one attribute
					return nullptr;
				}
				// Initialize native PAS (maybe just attrCount-1 items, but safety first)
				auto nativePas = make_midl_ptr<PARTIAL_ATTR_VECTOR_V1_EXT>(attrCount);
				// Copy array of attribute ids.
				ATTRTYP* nativePasAttIds = (ATTRTYP*)&nativePas->rgPartialAttr;
				for (int i = 0; i < attrCount; i++)
				{
					nativePasAttIds[i] = partialAttributeSet[i];
				}
				return nativePas;
			}

			array<byte>^ DrsConnection::ReadValue(const ATTRVAL &value)
			{
				// Allocate managed array
				auto managedValue = gcnew array<byte>(value.valLen);
				// Pin it so the GC does not touch it
				pin_ptr<byte> managedValuePin = &managedValue[0];
				// Copy data from native to managed memory
				memcpy(managedValuePin, value.pVal, value.valLen);
				return managedValue;
			}

			array<array<byte>^>^ DrsConnection::ReadValues(const ATTRVALBLOCK &values)
			{
				auto valCount = values.valCount;
				auto valArray = gcnew array<array<byte>^>(valCount);
				for (ULONG i = 0; i < valCount; i++)
				{
					auto value = values.pAVal[i];
					auto managedValue = ReadValue(value);
					valArray[i] = managedValue;
				}
				return valArray;
			}
			ReplicaAttribute^ DrsConnection::ReadAttribute(const ATTR &attribute)
			{
				auto values = ReadValues(attribute.AttrVal);
				auto managedAttribute = gcnew ReplicaAttribute(attribute.attrTyp, values);
				return managedAttribute;
			}
			ReplicaAttributeCollection^ DrsConnection::ReadAttributes(const ATTRBLOCK &attributes)
			{
				auto attributeCount = attributes.attrCount;
				auto managedAttributes = gcnew ReplicaAttributeCollection(attributeCount);

				for (size_t i = 0; i < attributeCount; i++)
				{
					auto attribute = attributes.pAttr[i];
					auto managedAttribute = ReadAttribute(attribute);
					if (managedAttribute->Values->Length > 0)
					{
						managedAttributes->Add(managedAttribute);
					}
				}
				return managedAttributes;
			}
			ReplicaObject^ DrsConnection::ReadObject(const ENTINF &object)
			{
				auto attributes = ReadAttributes(object.AttrBlock);
				auto guid = ReadGuid(object.pName->Guid);
				auto sid = ReadSid(object.pName);
				auto dn = ReadName(object.pName);
				return gcnew ReplicaObject(dn, guid, sid, attributes);
			}
			ReplicaObjectCollection^ DrsConnection::ReadObjects(const REPLENTINFLIST *objects, int count)
			{
				auto managedObjects = gcnew ReplicaObjectCollection(count);
				auto currentObject = objects;
				while (currentObject != nullptr)
				{
					auto managedObject = ReadObject(currentObject->Entinf);
					managedObjects->Add(managedObject);
					currentObject = currentObject->pNextEntInf;
				}
				return managedObjects;
			}

			Guid DrsConnection::ReadGuid(const GUID &guid)
			{
				// TODO: Type converter needed?
				return *reinterpret_cast<Guid *>(const_cast<GUID *>(&guid));
			}

			String^ DrsConnection::ReadName(const DSNAME* dsName)
			{
				// TODO: Move to type converter
				if (dsName == nullptr || dsName->NameLen <= 0)
				{
					return nullptr;
				}

				wchar_t* nativeName = (wchar_t*)&dsName->StringName;
				return marshal_as<String^>(nativeName);
			}

			SecurityIdentifier^ DrsConnection::ReadSid(const DSNAME* dsName)
			{
				// TODO: Move to type converter
				if (dsName == nullptr || dsName->SidLen <= 0)
				{
					return nullptr;
				}

				return gcnew SecurityIdentifier(IntPtr((void*)&dsName->Sid));
			}

			void DrsConnection::RetrieveSessionKey(void* rpcContext)
			{
				if (this->SessionKey != nullptr)
				{
					// This function sometimes gets called twice by Windows, so do not continue, if we already have the session key
					return;
				}
				// Retrieve RPC Security Context
				PSecHandle securityContext = nullptr;
				RPC_STATUS status1 = I_RpcBindingInqSecurityContext(rpcContext, (void**)&securityContext);
				if (status1 != 0)
				{
					// TODO: Error
				}
				// Retrieve the Session Key information from Security Context
				SecPkgContext_SessionKey nativeKey = {};
				SECURITY_STATUS status2 = QueryContextAttributes(securityContext, SECPKG_ATTR_SESSION_KEY, &nativeKey);
				// Extract the actual key if the authentication schema uses one
				if (nativeKey.SessionKey != nullptr)
				{
					array<byte>^ managedKey = gcnew array<byte>(nativeKey.SessionKeyLength);
					// Pin it so the GC does not touch it
					pin_ptr<byte> pinnedManagedKey = &managedKey[0];
					// Copy data from native to managed memory
					memcpy(pinnedManagedKey, nativeKey.SessionKey, nativeKey.SessionKeyLength);
					// Do not forget to free the unmanaged memory
					SECURITY_STATUS status3 = FreeContextBuffer(nativeKey.SessionKey);
					this->SessionKey = managedKey;
				}
			}
		}
	}
}