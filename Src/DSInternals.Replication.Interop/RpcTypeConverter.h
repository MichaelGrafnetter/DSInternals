#pragma once
#include "drsr.h"
#include "drsr_addons.h"
#include "drsr_alloc.h"

using namespace DSInternals::Common::Data;
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
				// TODO: Reference
				static UUID ToUUID(Guid guid);
				static Guid ToGuid(const UUID &uuid);
				static midl_ptr<wchar_t> ToNativeString(String^ input);
				static midl_ptr<DSNAME> ToDsName(String^ distinguishedName);
				static midl_ptr<DSNAME> ToDsName(Guid objectGuid);
			};
		}
	}
}