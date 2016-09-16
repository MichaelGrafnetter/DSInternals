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
			UUID RpcTypeConverter::ToUUID(Guid guid)
			{
				cli::array<BYTE>^ guidData = guid.ToByteArray();
				pin_ptr<BYTE> data = &(guidData[0]);
				return *(UUID *)data;
			}

			Guid RpcTypeConverter::ToGuid(const UUID &uuid)
			{
				return Guid(uuid.Data1, uuid.Data2, uuid.Data3,
					uuid.Data4[0], uuid.Data4[1],
					uuid.Data4[2], uuid.Data4[3],
					uuid.Data4[4], uuid.Data4[5],
					uuid.Data4[6], uuid.Data4[7]);
			}

			array<ReplicationCursor^>^ RpcTypeConverter::ToReplicationCursors(midl_ptr<DS_REPL_CURSORS> &&nativeCursors)
			{
				if (!nativeCursors)
				{
					return nullptr;
				}

				DWORD numCursors = nativeCursors->cNumCursors;
				auto managedCursors = gcnew array<ReplicationCursor^>(numCursors);
				
				// Process all cursors, one-by-one
				for (DWORD i = 0; i < numCursors; i++)
				{
					auto currentCursor = nativeCursors->rgCursor[i];
					auto invocationId = RpcTypeConverter::ToGuid(currentCursor.uuidSourceDsaInvocationID);
					managedCursors[i] = gcnew ReplicationCursor(invocationId, currentCursor.usnAttributeFilter);
				}

				return managedCursors;
			}

			midl_ptr<wchar_t> RpcTypeConverter::ToNativeString(String^ input)
			{
				if (input == nullptr)
				{
					return nullptr;
				}
				// Copy the source string from managed to unmanaged memory
				wstring nativeStr = marshal_as<wstring>(input);
				// Length + the trailing \0:
				auto strLen = input->Length+1;
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
				dsName->Guid = RpcTypeConverter::ToUUID(objectGuid);

				return dsName;
			}
		}
	}
}
