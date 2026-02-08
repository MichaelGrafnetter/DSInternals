#pragma once
#include "drsr.h"
#include "drsr_addons.h"
#include "drsr_alloc.h"

using namespace DSInternals::Common::Data;
using namespace DSInternals::Common::Schema;
using namespace DSInternals::Replication::Model;
using namespace System;
using namespace System::Collections::Generic;
using namespace System::Security::Principal;

namespace DSInternals::Replication::Interop
{
	class RpcTypeConverter
	{
	public:
		static UUID ToNative(Guid guid);
		static Guid ToManaged(const UUID& guid);
		static midl_ptr<wchar_t> ToNative(String^ input);
		static midl_ptr<System::Byte> ToNative(cli::array<System::Byte>^ managedArray);
		static cli::array<ReplicationCursor^>^ ToManaged(midl_ptr<DS_REPL_CURSORS>&& nativeCursors);
		static midl_ptr<DSNAME> ToDsName(String^ distinguishedName);
		static midl_ptr<DSNAME> ToDsName(Guid objectGuid);
		static midl_ptr<PARTIAL_ATTR_VECTOR_V1_EXT> CreateNativePas(cli::array<ATTRTYP>^ partialAttributeSet);
		static String^ ToString(const DSNAME* dsName);
		static SecurityIdentifier^ ToSid(const DSNAME* dsName);
		static cli::array<System::Byte>^ ToByteArray(const OID_t& prefix);
        static PrefixTable^ ToManaged(const SCHEMA_PREFIX_TABLE& prefixTable);
		static array<byte>^ ReadValue(const ATTRVAL& value);
		static array<array<byte>^>^ ReadValues(const ATTRVALBLOCK& values);
		static ReplicaAttribute^ ReadAttribute(const ATTR& attribute);
		static ReplicaAttribute^ ReadAttribute(const REPLVALINF_V3& attribute);
		static ReplicaAttributeCollection^ ReadAttributes(const ATTRBLOCK& attributes);
		static ReplicaObject^ ReadObject(const ENTINF& object, BaseSchema^ schema);
		static List<ReplicaObject^>^ ReadObjects(const REPLENTINFLIST* objects, int objectCount, const REPLVALINF_V3* linkedValues, int valueCount, BaseSchema^ schema);
	};
}
