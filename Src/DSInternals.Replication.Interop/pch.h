#pragma once

// Inhibit definition of the indicated items:
#define WIN32_LEAN_AND_MEAN // Exclude rarely-used stuff from Windows headers
#define NOIME               // Input Method Manager definitions
#define NOSYSPARAMSINFO     // Parameters for SystemParametersInfo.
#define NOWINABLE           // Active Accessibility hooks
#define NOKEYSTATES         // MK_*
#define NOGDI               // All GDI defines and routines
#define NOUSER              // All USER defines and routines
#define NOSERVICE           // All Service Controller routines, SERVICE_ equates, etc.
#define NOHELP              // Help engine interface.
#define NOMCX               // Modem Configuration Extensions

#define SECURITY_WIN32

// Windows libraries
#include <SDKDDKVer.h>
#include <Windows.h>
#include <security.h>
#include <rpc.h>
#include <ntdsapi.h>

// C++ libraries
#include <malloc.h>
#include <memory>
#include <string>
#include <msclr\marshal_cppstd.h>
