// Version information defined in this header file
// is used in version.rc and in AssemblyInfo.cpp.
#pragma once

#define STRINGIZE2(s) #s
#define STRINGIZE(s) STRINGIZE2(s)

#define VERSION_MAJOR               6
#define VERSION_MINOR               2
#define VERSION_REVISION            0
#define VERSION_BUILD               0

// Build file and product version strings
#define VER_FILE_VERSION            VERSION_MAJOR, VERSION_MINOR, VERSION_REVISION, VERSION_BUILD
#define VER_FILE_VERSION_STR        STRINGIZE(VERSION_MAJOR)        \
                                    "." STRINGIZE(VERSION_MINOR)    \
                                    "." STRINGIZE(VERSION_REVISION) \
                                    "." STRINGIZE(VERSION_BUILD)    \
 
#define VER_PRODUCT_VERSION         VER_FILE_VERSION
#define VER_PRODUCT_VERSION_STR     VER_FILE_VERSION_STR

#define VER_FILE_DESCRIPTION_STR    "DSInternals Replication Interop Library"
#define VER_COPYRIGHT_STR           "Copyright (c) 2015-2025 Michael Grafnetter. All rights reserved."
#define VER_COMPANY_STR             "Michael Grafnetter"
#define VER_PRODUCT_NAME_STR        "DSInternals PowerShell Module"
