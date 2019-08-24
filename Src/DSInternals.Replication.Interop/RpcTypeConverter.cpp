#include "stdafx.h"
#include "RpcTypeConverter.h"

using namespace DSInternals::Common;
using namespace System;
using namespace System::Runtime::InteropServices;
using namespace std;
using namespace msclr::interop;

// Code taken from https://msdn.microsoft.com/en-us/library/wb8scw8f.aspx
namespace DSInternals
{
	namespace Replication
	{
		namespace Interop
		{
			UUID RpcTypeConverter::ToNative(Guid guid)
			{
				cli::array<BYTE>^ guidData = guid.ToByteArray();
				pin_ptr<BYTE> data = &(guidData[0]);
				return *(UUID*)data;
			}

			cli::array<ReplicationCursor^>^ RpcTypeConverter::ToManaged(midl_ptr<DS_REPL_CURSORS>&& nativeCursors)
			{
				if (!nativeCursors)
				{
					return nullptr;
				}

				DWORD numCursors = nativeCursors->cNumCursors;
				auto managedCursors = gcnew cli::array<ReplicationCursor^>(numCursors);

				// Process all cursors, one-by-one
				for (DWORD i = 0; i < numCursors; i++)
				{
					auto currentCursor = nativeCursors->rgCursor[i];
					auto invocationId = RpcTypeConverter::ToManaged(currentCursor.uuidSourceDsaInvocationID);
					managedCursors[i] = gcnew ReplicationCursor(invocationId, currentCursor.usnAttributeFilter);
				}

				return managedCursors;
			}

			midl_ptr<wchar_t> RpcTypeConverter::ToNative(String^ input)
			{
				if (input == nullptr)
				{
					return nullptr;
				}
				// Copy the source string from managed to unmanaged memory
				wstring nativeStr = marshal_as<wstring>(input);
				// Length + the trailing \0:
				auto strLen = input->Length + 1;
				// Allocate a new string
				auto result = make_midl_ptr<wchar_t>(strLen);
				// Copy chars
				wcscpy_s(result.get(), strLen, nativeStr.c_str());
				return result;
			}

			midl_ptr<DSNAME> RpcTypeConverter::ToDsName(String^ distinguishedName)
			{
				// Validate the parameter
				Validator::AssertNotNullOrWhiteSpace(distinguishedName, "distinguishedName");

				// Allocate and initialize the DSNAME struct
				auto dnLen = distinguishedName->Length;
				auto dsName = make_midl_ptr<DSNAME>(dnLen);

				// Set DN by copying it into the DSNAME struct
				wstring nativeDN = marshal_as<wstring>(distinguishedName);
				wchar_t* dst = (wchar_t*)(&dsName->StringName);
				wcscpy_s(dst, dnLen + 1, nativeDN.c_str());

				return dsName;
			}

			midl_ptr<DSNAME> RpcTypeConverter::ToDsName(Guid objectGuid)
			{
				// Allocate and initialize the DSNAME struct with empty distinguished name
				auto dsName = make_midl_ptr<DSNAME>(0);

				// Set Guid
				dsName->Guid = RpcTypeConverter::ToNative(objectGuid);

				return dsName;
			}

			midl_ptr<byte> RpcTypeConverter::ToNative(cli::array<byte>^ managedArray)
			{
				// Copy the byte array from managed memory to unmanaged
				auto nativeArray = make_midl_ptr<byte>(managedArray->Length);
				pin_ptr<byte> pinnedManagedArray = &managedArray[0];
				memcpy(nativeArray.get(), pinnedManagedArray, managedArray->Length);

				return nativeArray;
			}

			midl_ptr<PARTIAL_ATTR_VECTOR_V1_EXT> RpcTypeConverter::CreateNativePas(cli::array<ATTRTYP>^ partialAttributeSet)
			{
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
				ATTRTYP* nativePasAttIds = (ATTRTYP*)& nativePas->rgPartialAttr;
				for (int i = 0; i < attrCount; i++)
				{
					nativePasAttIds[i] = partialAttributeSet[i];
				}

				return nativePas;
			}

			Guid RpcTypeConverter::ToManaged(const UUID& uuid)
			{
				return Guid(uuid.Data1, uuid.Data2, uuid.Data3,
					uuid.Data4[0], uuid.Data4[1],
					uuid.Data4[2], uuid.Data4[3],
					uuid.Data4[4], uuid.Data4[5],
					uuid.Data4[6], uuid.Data4[7]);
			}

			String^ RpcTypeConverter::ToString(const DSNAME* dsName)
			{
				if (dsName == nullptr || dsName->NameLen <= 0)
				{
					return nullptr;
				}

				wchar_t* nativeName = (wchar_t*)& dsName->StringName;
				return marshal_as<String^>(nativeName);
			}

			SecurityIdentifier^ RpcTypeConverter::ToSid(const DSNAME* dsName)
			{
				if (dsName == nullptr || dsName->SidLen <= 0)
				{
					return nullptr;
				}

				return gcnew SecurityIdentifier(IntPtr((void*)& dsName->Sid));
			}
		}
	}
}
