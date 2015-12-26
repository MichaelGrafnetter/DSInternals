#pragma once

#define WIN32_LEAN_AND_MEAN // Exclude rarely-used stuff from Windows headers
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
