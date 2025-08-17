#include "pch.h"
#include "version.h"

using namespace System;
using namespace System::Reflection;
using namespace System::Runtime::CompilerServices;
using namespace System::Runtime::InteropServices;
using namespace System::Security::Permissions;

//
// General Information about an assembly is controlled through the following
// set of attributes. Values are defined in the version.h file.
//
[assembly:AssemblyTitleAttribute(VER_FILE_DESCRIPTION_STR)];
[assembly:AssemblyVersionAttribute(VER_PRODUCT_VERSION_STR)];
[assembly:AssemblyProductAttribute(VER_PRODUCT_NAME_STR)];
[assembly:AssemblyCopyrightAttribute(VER_COPYRIGHT_STR)];
[assembly:AssemblyDescriptionAttribute(VER_FILE_DESCRIPTION_STR)];
[assembly:AssemblyConfigurationAttribute(L"")];
[assembly:AssemblyCompanyAttribute(VER_COMPANY_STR)];
[assembly:AssemblyTrademarkAttribute(L"")];
[assembly:AssemblyCultureAttribute(L"")];

[assembly:ComVisible(false)];
[assembly:CLSCompliantAttribute(true)];
