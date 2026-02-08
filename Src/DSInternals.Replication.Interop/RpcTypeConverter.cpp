#include "pch.h"
#include "RpcTypeConverter.h"

using namespace DSInternals::Common;
using namespace DSInternals::Common::Schema;
using namespace System;
using namespace System::Runtime::InteropServices;
using namespace msclr::interop;

// Code taken from https://msdn.microsoft.com/en-us/library/wb8scw8f.aspx
namespace DSInternals::Replication::Interop
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
		std::wstring nativeStr = marshal_as<std::wstring>(input);
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
		if (distinguishedName == nullptr || distinguishedName->Trim()->Length == 0)
		{
			throw gcnew ArgumentException("The distinguished name cannot be null or empty.", "distinguishedName");
		}

		// Allocate and initialize the DSNAME struct
		auto dnLen = distinguishedName->Length;
		auto dsName = make_midl_ptr<DSNAME>(dnLen);

		// Set DN by copying it into the DSNAME struct
		std::wstring nativeDN = marshal_as<std::wstring>(distinguishedName);
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

	midl_ptr<System::Byte> RpcTypeConverter::ToNative(cli::array<System::Byte>^ managedArray)
	{
		// Copy the byte array from managed memory to unmanaged
		auto nativeArray = make_midl_ptr<System::Byte>(managedArray->Length);
		pin_ptr<System::Byte> pinnedManagedArray = &managedArray[0];
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

	cli::array<System::Byte>^ RpcTypeConverter::ToByteArray(const OID_t& prefix)
	{
		// Allocate managed array
		auto managedValue = gcnew cli::array<System::Byte>(prefix.length);
		// Pin it so the GC does not touch it
		pin_ptr<System::Byte> managedValuePin = &managedValue[0];
		// Copy data from native to managed memory
		memcpy(managedValuePin, prefix.elements, prefix.length);
		return managedValue;
	}

	PrefixTable^ RpcTypeConverter::ToManaged(const SCHEMA_PREFIX_TABLE& prefixTable)
	{
		// Create an empty prefix table, without the built-in prefixes
		auto managedPrefixTable = gcnew PrefixTable(nullptr, false);

		for (DWORD i = 0; i < prefixTable.PrefixCount; i++)
		{
			unsigned long prefixIndex = prefixTable.pPrefixEntry[i].ndx;

			// Do not re-add prefixes 0-38
			if (prefixIndex > PrefixTable::LastBuiltInPrefixIndex)
			{
				OID_t nativePrefix = prefixTable.pPrefixEntry[i].prefix;
				auto managedPrefix = ToByteArray(nativePrefix);
				managedPrefixTable->Add(static_cast<unsigned short>(prefixIndex), managedPrefix);
			}
		}

		return managedPrefixTable;
	}

	array<byte>^ RpcTypeConverter::ReadValue(const ATTRVAL& value)
	{
		// Allocate managed array
		auto managedValue = gcnew array<byte>(value.valLen);
		// Pin it so the GC does not touch it
		pin_ptr<byte> managedValuePin = &managedValue[0];
		// Copy data from native to managed memory
		memcpy(managedValuePin, value.pVal, value.valLen);
		return managedValue;
	}

	array<array<byte>^>^ RpcTypeConverter::ReadValues(const ATTRVALBLOCK& values)
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

	ReplicaAttribute^ RpcTypeConverter::ReadAttribute(const ATTR& attribute)
	{
		auto values = ReadValues(attribute.AttrVal);
		auto managedAttribute = gcnew ReplicaAttribute((AttributeType)attribute.attrTyp, values);
		return managedAttribute;
	}

	ReplicaAttribute^ RpcTypeConverter::ReadAttribute(const REPLVALINF_V3& attribute)
	{
		auto value = ReadValue(attribute.Aval);
		auto managedAttribute = gcnew ReplicaAttribute((AttributeType)attribute.attrTyp, value);
		return managedAttribute;
	}

	ReplicaAttributeCollection^ RpcTypeConverter::ReadAttributes(const ATTRBLOCK& attributes)
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

	ReplicaObject^ RpcTypeConverter::ReadObject(const ENTINF& object, BaseSchema^ schema)
	{
		auto attributes = ReadAttributes(object.AttrBlock);
		auto guid = RpcTypeConverter::ToManaged(object.pName->Guid);
		auto sid = RpcTypeConverter::ToSid(object.pName);
		auto dn = RpcTypeConverter::ToString(object.pName);
		return gcnew ReplicaObject(dn, guid, sid, attributes, schema);
	}

	List<ReplicaObject^>^ RpcTypeConverter::ReadObjects(const REPLENTINFLIST* objects, int objectCount, const REPLVALINF_V3* linkedValues, int valueCount, BaseSchema^ schema)
	{
		// Read linked values first
		// TODO: Handle the case when linked attributes of an object are split between several responses.
		auto linkedValueCollection = gcnew ReplicatedLinkedValueCollection();
		for (int i = 0; i < valueCount; i++)
		{
			auto linkedValue = linkedValues[i];
			if (linkedValue.fIsPresent)
			{
				auto objectId = RpcTypeConverter::ToManaged(linkedValue.pObject->Guid);
				auto attribute = ReadAttribute(linkedValue);
				linkedValueCollection->Add(objectId, attribute);
			}
		}

		// Now read the replicated objects
		auto managedObjects = gcnew List<ReplicaObject^>(objectCount);
		auto currentObject = objects;
		while (currentObject != nullptr)
		{
			auto managedObject = ReadObject(currentObject->Entinf, schema);
			managedObject->LoadLinkedValues(linkedValueCollection);
			managedObjects->Add(managedObject);
			currentObject = currentObject->pNextEntInf;
		}
		return managedObjects;
	}
}
