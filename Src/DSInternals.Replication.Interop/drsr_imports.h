#pragma once

#ifdef MIDL_PASS
// MIDL won't compile ntdsapi.h without these definitions:
#define DECLSPEC_IMPORT
#define WINAPI

// This is starndard definition from windows.h, but including it causes many conflicts.
// It is also defined in drsr.idl, but we use it before it is defined.
typedef LONGLONG USN;
#endif

#include <specstrings.h>
#include <ntdsapi.h>