#include "stdafx.h"
#include "drsr_addons.h"

#define SuppressRpcException(Function,...)								\
	RpcTryExcept										\
		return Function(__VA_ARGS__);					\
	RpcExcept(RpcExceptionFilter(RpcExceptionCode()))	\
		return RpcExceptionCode();						\
	RpcEndExcept

ULONG IDL_DRSBind_NoSEH(
	/* [in] */ handle_t rpc_handle,
	/* [unique][in] */ UUID* puuidClientDsa,
	/* [unique][in] */ DRS_EXTENSIONS* pextClient,
	/* [out] */ DRS_EXTENSIONS** ppextServer,
	/* [ref][out] */ DRS_HANDLE* phDrs)
{
	SuppressRpcException(IDL_DRSBind, rpc_handle, puuidClientDsa, pextClient, ppextServer, phDrs)
}

ULONG IDL_DRSGetNCChanges_NoSEH(
	/* [ref][in] */ DRS_HANDLE hDrs,
	/* [in] */ DWORD dwInVersion,
	/* [switch_is][ref][in] */ DRS_MSG_GETCHGREQ* pmsgIn,
	/* [ref][out] */ DWORD* pdwOutVersion,
	/* [switch_is][ref][out] */ DRS_MSG_GETCHGREPLY* pmsgOut)
{
	SuppressRpcException(IDL_DRSGetNCChanges, hDrs, dwInVersion, pmsgIn, pdwOutVersion, pmsgOut)
}

ULONG IDL_DRSCrackNames_NoSEH(
	/* [ref][in] */ DRS_HANDLE hDrs,
	/* [in] */ DWORD dwInVersion,
	/* [switch_is][ref][in] */ DRS_MSG_CRACKREQ* pmsgIn,
	/* [ref][out] */ DWORD* pdwOutVersion,
	/* [switch_is][ref][out] */ DRS_MSG_CRACKREPLY* pmsgOut)
{
	SuppressRpcException(IDL_DRSCrackNames, hDrs, dwInVersion, pmsgIn, pdwOutVersion, pmsgOut)
}

ULONG IDL_DRSGetReplInfo_NoSEH(
	/* [ref][in] */ DRS_HANDLE hDrs,
	/* [in] */ DWORD dwInVersion,
	/* [switch_is][ref][in] */ DRS_MSG_GETREPLINFO_REQ* pmsgIn,
	/* [ref][out] */ DWORD* pdwOutVersion,
	/* [switch_is][ref][out] */ DRS_MSG_GETREPLINFO_REPLY* pmsgOut)
{
	SuppressRpcException(IDL_DRSGetReplInfo, hDrs, dwInVersion, pmsgIn, pdwOutVersion, pmsgOut)
}

ULONG IDL_DRSWriteNgcKey_NoSEH(
	/* [in, ref] */ DRS_HANDLE hDrs,
	/* [in] */ DWORD dwInVersion,
	/* [in, ref, switch_is(dwInVersion)]*/ DRS_MSG_WRITENGCKEYREQ* pmsgIn,
	/* [out, ref] */ DWORD* pdwOutVersion,
	/* [out, ref, switch_is(*pdwOutVersion)] */ DRS_MSG_WRITENGCKEYREPLY* pmsgOut)
{
	SuppressRpcException(IDL_DRSWriteNgcKey, hDrs, dwInVersion, pmsgIn, pdwOutVersion, pmsgOut)
}

ULONG IDL_DRSUnbind_NoSEH(
	/* [ref][out][in] */ DRS_HANDLE* phDrs)
{
	SuppressRpcException(IDL_DRSUnbind, phDrs)
}

DRS_EXTENSIONS_INT::DRS_EXTENSIONS_INT()
{
}

DRS_EXTENSIONS_INT::DRS_EXTENSIONS_INT(DRS_EXTENSIONS* genericExtensions)
{
	if (genericExtensions != 0)
	{
		DWORD numDataBytes = genericExtensions->cb;
		DWORD maxDataBytes = cb;
		DWORD bytesToCopy = min(numDataBytes, maxDataBytes) + sizeof(DWORD);
		memcpy(this, genericExtensions, bytesToCopy);
	}
}
