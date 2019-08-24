#pragma once
#include "drsr.h"
#include "drsr_addons.h"
#include "drsr_alloc.h"

using namespace DSInternals::Common::Data;
using namespace DSInternals::Replication::Model;
using namespace System;
using namespace System::Security::Principal;

namespace DSInternals
{
	namespace Replication
	{
		namespace Interop
		{
			class RpcTypeConverter
			{
			public:
				static UUID ToNative(Guid guid);
				static Guid ToManaged(const UUID& guid);
				static midl_ptr<wchar_t> ToNative(String^ input);
				static midl_ptr<byte> ToNative(cli::array<byte>^ managedArray);
				static cli::array<ReplicationCursor^>^ ToManaged(midl_ptr<DS_REPL_CURSORS>&& nativeCursors);
				static midl_ptr<DSNAME> ToDsName(String^ distinguishedName);
				static midl_ptr<DSNAME> ToDsName(Guid objectGuid);
				static midl_ptr<PARTIAL_ATTR_VECTOR_V1_EXT> CreateNativePas(cli::array<ATTRTYP>^ partialAttributeSet);
				static String^ ToString(const DSNAME* dsName);
				static SecurityIdentifier^ ToSid(const DSNAME* dsName);
			};
		}
	}
}
