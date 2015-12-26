

/* this ALWAYS GENERATED file contains the RPC client stubs */


 /* File created by MIDL compiler version 8.00.0603 */
/* at Sat Dec 26 23:39:06 2015
 */
/* Compiler settings for drsr.idl:
    Oicf, W1, Zp8, env=Win64 (32b run), target_arch=AMD64 8.00.0603 
    protocol : dce , ms_ext, c_ext, robust
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
/* @@MIDL_FILE_HEADING(  ) */

#if defined(_M_AMD64)


#pragma warning( disable: 4049 )  /* more than 64k source lines */
#if _MSC_VER >= 1200
#pragma warning(push)
#endif

#pragma warning( disable: 4211 )  /* redefine extern to static */
#pragma warning( disable: 4232 )  /* dllimport identity*/
#pragma warning( disable: 4024 )  /* array to pointer mapping*/

#include <string.h>

#include "drsr.h"

#define TYPE_FORMAT_STRING_SIZE   7241                              
#define PROC_FORMAT_STRING_SIZE   1977                              
#define EXPR_FORMAT_STRING_SIZE   1                                 
#define TRANSMIT_AS_TABLE_SIZE    0            
#define WIRE_MARSHAL_TABLE_SIZE   0            

typedef struct _drsr_MIDL_TYPE_FORMAT_STRING
    {
    short          Pad;
    unsigned char  Format[ TYPE_FORMAT_STRING_SIZE ];
    } drsr_MIDL_TYPE_FORMAT_STRING;

typedef struct _drsr_MIDL_PROC_FORMAT_STRING
    {
    short          Pad;
    unsigned char  Format[ PROC_FORMAT_STRING_SIZE ];
    } drsr_MIDL_PROC_FORMAT_STRING;

typedef struct _drsr_MIDL_EXPR_FORMAT_STRING
    {
    long          Pad;
    unsigned char  Format[ EXPR_FORMAT_STRING_SIZE ];
    } drsr_MIDL_EXPR_FORMAT_STRING;


static const RPC_SYNTAX_IDENTIFIER  _RpcTransferSyntax = 
{{0x8A885D04,0x1CEB,0x11C9,{0x9F,0xE8,0x08,0x00,0x2B,0x10,0x48,0x60}},{2,0}};


extern const drsr_MIDL_TYPE_FORMAT_STRING drsr__MIDL_TypeFormatString;
extern const drsr_MIDL_PROC_FORMAT_STRING drsr__MIDL_ProcFormatString;
extern const drsr_MIDL_EXPR_FORMAT_STRING drsr__MIDL_ExprFormatString;

#define GENERIC_BINDING_TABLE_SIZE   0            


/* Standard interface: drsuapi, ver. 4.0,
   GUID={0xe3514235,0x4b06,0x11d1,{0xab,0x04,0x00,0xc0,0x4f,0xc2,0xdc,0xd2}} */



static const RPC_CLIENT_INTERFACE drsuapi___RpcClientInterface =
    {
    sizeof(RPC_CLIENT_INTERFACE),
    {{0xe3514235,0x4b06,0x11d1,{0xab,0x04,0x00,0xc0,0x4f,0xc2,0xdc,0xd2}},{4,0}},
    {{0x8A885D04,0x1CEB,0x11C9,{0x9F,0xE8,0x08,0x00,0x2B,0x10,0x48,0x60}},{2,0}},
    0,
    0,
    0,
    0,
    0,
    0x00000000
    };
RPC_IF_HANDLE drsuapi_v4_0_c_ifspec = (RPC_IF_HANDLE)& drsuapi___RpcClientInterface;

extern const MIDL_STUB_DESC drsuapi_StubDesc;

static RPC_BINDING_HANDLE drsuapi__MIDL_AutoBindHandle;


ULONG IDL_DRSBind( 
    /* [in] */ handle_t rpc_handle,
    /* [unique][in] */ UUID *puuidClientDsa,
    /* [unique][in] */ DRS_EXTENSIONS *pextClient,
    /* [out] */ DRS_EXTENSIONS **ppextServer,
    /* [ref][out] */ DRS_HANDLE *phDrs)
{

    CLIENT_CALL_RETURN _RetVal;

    _RetVal = NdrClientCall2(
                  ( PMIDL_STUB_DESC  )&drsuapi_StubDesc,
                  (PFORMAT_STRING) &drsr__MIDL_ProcFormatString.Format[0],
                  rpc_handle,
                  puuidClientDsa,
                  pextClient,
                  ppextServer,
                  phDrs);
    return ( ULONG  )_RetVal.Simple;
    
}


ULONG IDL_DRSUnbind( 
    /* [ref][out][in] */ DRS_HANDLE *phDrs)
{

    CLIENT_CALL_RETURN _RetVal;

    _RetVal = NdrClientCall2(
                  ( PMIDL_STUB_DESC  )&drsuapi_StubDesc,
                  (PFORMAT_STRING) &drsr__MIDL_ProcFormatString.Format[60],
                  phDrs);
    return ( ULONG  )_RetVal.Simple;
    
}


ULONG IDL_DRSReplicaSync( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwVersion,
    /* [switch_is][ref][in] */ DRS_MSG_REPSYNC *pmsgSync)
{

    CLIENT_CALL_RETURN _RetVal;

    _RetVal = NdrClientCall2(
                  ( PMIDL_STUB_DESC  )&drsuapi_StubDesc,
                  (PFORMAT_STRING) &drsr__MIDL_ProcFormatString.Format[104],
                  hDrs,
                  dwVersion,
                  pmsgSync);
    return ( ULONG  )_RetVal.Simple;
    
}


ULONG IDL_DRSGetNCChanges( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_GETCHGREQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DRS_MSG_GETCHGREPLY *pmsgOut)
{

    CLIENT_CALL_RETURN _RetVal;

    _RetVal = NdrClientCall2(
                  ( PMIDL_STUB_DESC  )&drsuapi_StubDesc,
                  (PFORMAT_STRING) &drsr__MIDL_ProcFormatString.Format[160],
                  hDrs,
                  dwInVersion,
                  pmsgIn,
                  pdwOutVersion,
                  pmsgOut);
    return ( ULONG  )_RetVal.Simple;
    
}


ULONG IDL_DRSUpdateRefs( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwVersion,
    /* [switch_is][ref][in] */ DRS_MSG_UPDREFS *pmsgUpdRefs)
{

    CLIENT_CALL_RETURN _RetVal;

    _RetVal = NdrClientCall2(
                  ( PMIDL_STUB_DESC  )&drsuapi_StubDesc,
                  (PFORMAT_STRING) &drsr__MIDL_ProcFormatString.Format[228],
                  hDrs,
                  dwVersion,
                  pmsgUpdRefs);
    return ( ULONG  )_RetVal.Simple;
    
}


ULONG IDL_DRSReplicaAdd( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwVersion,
    /* [switch_is][ref][in] */ DRS_MSG_REPADD *pmsgAdd)
{

    CLIENT_CALL_RETURN _RetVal;

    _RetVal = NdrClientCall2(
                  ( PMIDL_STUB_DESC  )&drsuapi_StubDesc,
                  (PFORMAT_STRING) &drsr__MIDL_ProcFormatString.Format[284],
                  hDrs,
                  dwVersion,
                  pmsgAdd);
    return ( ULONG  )_RetVal.Simple;
    
}


ULONG IDL_DRSReplicaDel( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwVersion,
    /* [switch_is][ref][in] */ DRS_MSG_REPDEL *pmsgDel)
{

    CLIENT_CALL_RETURN _RetVal;

    _RetVal = NdrClientCall2(
                  ( PMIDL_STUB_DESC  )&drsuapi_StubDesc,
                  (PFORMAT_STRING) &drsr__MIDL_ProcFormatString.Format[340],
                  hDrs,
                  dwVersion,
                  pmsgDel);
    return ( ULONG  )_RetVal.Simple;
    
}


ULONG IDL_DRSReplicaModify( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwVersion,
    /* [switch_is][ref][in] */ DRS_MSG_REPMOD *pmsgMod)
{

    CLIENT_CALL_RETURN _RetVal;

    _RetVal = NdrClientCall2(
                  ( PMIDL_STUB_DESC  )&drsuapi_StubDesc,
                  (PFORMAT_STRING) &drsr__MIDL_ProcFormatString.Format[396],
                  hDrs,
                  dwVersion,
                  pmsgMod);
    return ( ULONG  )_RetVal.Simple;
    
}


ULONG IDL_DRSVerifyNames( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_VERIFYREQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DRS_MSG_VERIFYREPLY *pmsgOut)
{

    CLIENT_CALL_RETURN _RetVal;

    _RetVal = NdrClientCall2(
                  ( PMIDL_STUB_DESC  )&drsuapi_StubDesc,
                  (PFORMAT_STRING) &drsr__MIDL_ProcFormatString.Format[452],
                  hDrs,
                  dwInVersion,
                  pmsgIn,
                  pdwOutVersion,
                  pmsgOut);
    return ( ULONG  )_RetVal.Simple;
    
}


ULONG IDL_DRSGetMemberships( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_REVMEMB_REQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DRS_MSG_REVMEMB_REPLY *pmsgOut)
{

    CLIENT_CALL_RETURN _RetVal;

    _RetVal = NdrClientCall2(
                  ( PMIDL_STUB_DESC  )&drsuapi_StubDesc,
                  (PFORMAT_STRING) &drsr__MIDL_ProcFormatString.Format[520],
                  hDrs,
                  dwInVersion,
                  pmsgIn,
                  pdwOutVersion,
                  pmsgOut);
    return ( ULONG  )_RetVal.Simple;
    
}


ULONG IDL_DRSInterDomainMove( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_MOVEREQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DRS_MSG_MOVEREPLY *pmsgOut)
{

    CLIENT_CALL_RETURN _RetVal;

    _RetVal = NdrClientCall2(
                  ( PMIDL_STUB_DESC  )&drsuapi_StubDesc,
                  (PFORMAT_STRING) &drsr__MIDL_ProcFormatString.Format[588],
                  hDrs,
                  dwInVersion,
                  pmsgIn,
                  pdwOutVersion,
                  pmsgOut);
    return ( ULONG  )_RetVal.Simple;
    
}


ULONG IDL_DRSGetNT4ChangeLog( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_NT4_CHGLOG_REQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DRS_MSG_NT4_CHGLOG_REPLY *pmsgOut)
{

    CLIENT_CALL_RETURN _RetVal;

    _RetVal = NdrClientCall2(
                  ( PMIDL_STUB_DESC  )&drsuapi_StubDesc,
                  (PFORMAT_STRING) &drsr__MIDL_ProcFormatString.Format[656],
                  hDrs,
                  dwInVersion,
                  pmsgIn,
                  pdwOutVersion,
                  pmsgOut);
    return ( ULONG  )_RetVal.Simple;
    
}


ULONG IDL_DRSCrackNames( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_CRACKREQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DRS_MSG_CRACKREPLY *pmsgOut)
{

    CLIENT_CALL_RETURN _RetVal;

    _RetVal = NdrClientCall2(
                  ( PMIDL_STUB_DESC  )&drsuapi_StubDesc,
                  (PFORMAT_STRING) &drsr__MIDL_ProcFormatString.Format[724],
                  hDrs,
                  dwInVersion,
                  pmsgIn,
                  pdwOutVersion,
                  pmsgOut);
    return ( ULONG  )_RetVal.Simple;
    
}


ULONG IDL_DRSWriteSPN( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_SPNREQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DRS_MSG_SPNREPLY *pmsgOut)
{

    CLIENT_CALL_RETURN _RetVal;

    _RetVal = NdrClientCall2(
                  ( PMIDL_STUB_DESC  )&drsuapi_StubDesc,
                  (PFORMAT_STRING) &drsr__MIDL_ProcFormatString.Format[792],
                  hDrs,
                  dwInVersion,
                  pmsgIn,
                  pdwOutVersion,
                  pmsgOut);
    return ( ULONG  )_RetVal.Simple;
    
}


ULONG IDL_DRSRemoveDsServer( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_RMSVRREQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DRS_MSG_RMSVRREPLY *pmsgOut)
{

    CLIENT_CALL_RETURN _RetVal;

    _RetVal = NdrClientCall2(
                  ( PMIDL_STUB_DESC  )&drsuapi_StubDesc,
                  (PFORMAT_STRING) &drsr__MIDL_ProcFormatString.Format[860],
                  hDrs,
                  dwInVersion,
                  pmsgIn,
                  pdwOutVersion,
                  pmsgOut);
    return ( ULONG  )_RetVal.Simple;
    
}


ULONG IDL_DRSRemoveDsDomain( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_RMDMNREQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DRS_MSG_RMDMNREPLY *pmsgOut)
{

    CLIENT_CALL_RETURN _RetVal;

    _RetVal = NdrClientCall2(
                  ( PMIDL_STUB_DESC  )&drsuapi_StubDesc,
                  (PFORMAT_STRING) &drsr__MIDL_ProcFormatString.Format[928],
                  hDrs,
                  dwInVersion,
                  pmsgIn,
                  pdwOutVersion,
                  pmsgOut);
    return ( ULONG  )_RetVal.Simple;
    
}


ULONG IDL_DRSDomainControllerInfo( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_DCINFOREQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DRS_MSG_DCINFOREPLY *pmsgOut)
{

    CLIENT_CALL_RETURN _RetVal;

    _RetVal = NdrClientCall2(
                  ( PMIDL_STUB_DESC  )&drsuapi_StubDesc,
                  (PFORMAT_STRING) &drsr__MIDL_ProcFormatString.Format[996],
                  hDrs,
                  dwInVersion,
                  pmsgIn,
                  pdwOutVersion,
                  pmsgOut);
    return ( ULONG  )_RetVal.Simple;
    
}


ULONG IDL_DRSAddEntry( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_ADDENTRYREQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DRS_MSG_ADDENTRYREPLY *pmsgOut)
{

    CLIENT_CALL_RETURN _RetVal;

    _RetVal = NdrClientCall2(
                  ( PMIDL_STUB_DESC  )&drsuapi_StubDesc,
                  (PFORMAT_STRING) &drsr__MIDL_ProcFormatString.Format[1064],
                  hDrs,
                  dwInVersion,
                  pmsgIn,
                  pdwOutVersion,
                  pmsgOut);
    return ( ULONG  )_RetVal.Simple;
    
}


ULONG IDL_DRSExecuteKCC( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_KCC_EXECUTE *pmsgIn)
{

    CLIENT_CALL_RETURN _RetVal;

    _RetVal = NdrClientCall2(
                  ( PMIDL_STUB_DESC  )&drsuapi_StubDesc,
                  (PFORMAT_STRING) &drsr__MIDL_ProcFormatString.Format[1132],
                  hDrs,
                  dwInVersion,
                  pmsgIn);
    return ( ULONG  )_RetVal.Simple;
    
}


ULONG IDL_DRSGetReplInfo( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_GETREPLINFO_REQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DRS_MSG_GETREPLINFO_REPLY *pmsgOut)
{

    CLIENT_CALL_RETURN _RetVal;

    _RetVal = NdrClientCall2(
                  ( PMIDL_STUB_DESC  )&drsuapi_StubDesc,
                  (PFORMAT_STRING) &drsr__MIDL_ProcFormatString.Format[1188],
                  hDrs,
                  dwInVersion,
                  pmsgIn,
                  pdwOutVersion,
                  pmsgOut);
    return ( ULONG  )_RetVal.Simple;
    
}


ULONG IDL_DRSAddSidHistory( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_ADDSIDREQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DRS_MSG_ADDSIDREPLY *pmsgOut)
{

    CLIENT_CALL_RETURN _RetVal;

    _RetVal = NdrClientCall2(
                  ( PMIDL_STUB_DESC  )&drsuapi_StubDesc,
                  (PFORMAT_STRING) &drsr__MIDL_ProcFormatString.Format[1256],
                  hDrs,
                  dwInVersion,
                  pmsgIn,
                  pdwOutVersion,
                  pmsgOut);
    return ( ULONG  )_RetVal.Simple;
    
}


ULONG IDL_DRSGetMemberships2( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_GETMEMBERSHIPS2_REQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DRS_MSG_GETMEMBERSHIPS2_REPLY *pmsgOut)
{

    CLIENT_CALL_RETURN _RetVal;

    _RetVal = NdrClientCall2(
                  ( PMIDL_STUB_DESC  )&drsuapi_StubDesc,
                  (PFORMAT_STRING) &drsr__MIDL_ProcFormatString.Format[1324],
                  hDrs,
                  dwInVersion,
                  pmsgIn,
                  pdwOutVersion,
                  pmsgOut);
    return ( ULONG  )_RetVal.Simple;
    
}


ULONG IDL_DRSReplicaVerifyObjects( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwVersion,
    /* [switch_is][ref][in] */ DRS_MSG_REPVERIFYOBJ *pmsgVerify)
{

    CLIENT_CALL_RETURN _RetVal;

    _RetVal = NdrClientCall2(
                  ( PMIDL_STUB_DESC  )&drsuapi_StubDesc,
                  (PFORMAT_STRING) &drsr__MIDL_ProcFormatString.Format[1392],
                  hDrs,
                  dwVersion,
                  pmsgVerify);
    return ( ULONG  )_RetVal.Simple;
    
}


ULONG IDL_DRSGetObjectExistence( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_EXISTREQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DRS_MSG_EXISTREPLY *pmsgOut)
{

    CLIENT_CALL_RETURN _RetVal;

    _RetVal = NdrClientCall2(
                  ( PMIDL_STUB_DESC  )&drsuapi_StubDesc,
                  (PFORMAT_STRING) &drsr__MIDL_ProcFormatString.Format[1448],
                  hDrs,
                  dwInVersion,
                  pmsgIn,
                  pdwOutVersion,
                  pmsgOut);
    return ( ULONG  )_RetVal.Simple;
    
}


ULONG IDL_DRSQuerySitesByCost( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_QUERYSITESREQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DRS_MSG_QUERYSITESREPLY *pmsgOut)
{

    CLIENT_CALL_RETURN _RetVal;

    _RetVal = NdrClientCall2(
                  ( PMIDL_STUB_DESC  )&drsuapi_StubDesc,
                  (PFORMAT_STRING) &drsr__MIDL_ProcFormatString.Format[1516],
                  hDrs,
                  dwInVersion,
                  pmsgIn,
                  pdwOutVersion,
                  pmsgOut);
    return ( ULONG  )_RetVal.Simple;
    
}


ULONG IDL_DRSInitDemotion( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_INIT_DEMOTIONREQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DRS_MSG_INIT_DEMOTIONREPLY *pmsgOut)
{

    CLIENT_CALL_RETURN _RetVal;

    _RetVal = NdrClientCall2(
                  ( PMIDL_STUB_DESC  )&drsuapi_StubDesc,
                  (PFORMAT_STRING) &drsr__MIDL_ProcFormatString.Format[1584],
                  hDrs,
                  dwInVersion,
                  pmsgIn,
                  pdwOutVersion,
                  pmsgOut);
    return ( ULONG  )_RetVal.Simple;
    
}


ULONG IDL_DRSReplicaDemotion( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_REPLICA_DEMOTIONREQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DRS_MSG_REPLICA_DEMOTIONREPLY *pmsgOut)
{

    CLIENT_CALL_RETURN _RetVal;

    _RetVal = NdrClientCall2(
                  ( PMIDL_STUB_DESC  )&drsuapi_StubDesc,
                  (PFORMAT_STRING) &drsr__MIDL_ProcFormatString.Format[1652],
                  hDrs,
                  dwInVersion,
                  pmsgIn,
                  pdwOutVersion,
                  pmsgOut);
    return ( ULONG  )_RetVal.Simple;
    
}


ULONG IDL_DRSFinishDemotion( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_FINISH_DEMOTIONREQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DRS_MSG_FINISH_DEMOTIONREPLY *pmsgOut)
{

    CLIENT_CALL_RETURN _RetVal;

    _RetVal = NdrClientCall2(
                  ( PMIDL_STUB_DESC  )&drsuapi_StubDesc,
                  (PFORMAT_STRING) &drsr__MIDL_ProcFormatString.Format[1720],
                  hDrs,
                  dwInVersion,
                  pmsgIn,
                  pdwOutVersion,
                  pmsgOut);
    return ( ULONG  )_RetVal.Simple;
    
}


ULONG IDL_DRSAddCloneDC( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_ADDCLONEDCREQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DRS_MSG_ADDCLONEDCREPLY *pmsgOut)
{

    CLIENT_CALL_RETURN _RetVal;

    _RetVal = NdrClientCall2(
                  ( PMIDL_STUB_DESC  )&drsuapi_StubDesc,
                  (PFORMAT_STRING) &drsr__MIDL_ProcFormatString.Format[1788],
                  hDrs,
                  dwInVersion,
                  pmsgIn,
                  pdwOutVersion,
                  pmsgOut);
    return ( ULONG  )_RetVal.Simple;
    
}


/* Standard interface: dsaop, ver. 1.0,
   GUID={0x7c44d7d4,0x31d5,0x424c,{0xbd,0x5e,0x2b,0x3e,0x1f,0x32,0x3d,0x22}} */



static const RPC_CLIENT_INTERFACE dsaop___RpcClientInterface =
    {
    sizeof(RPC_CLIENT_INTERFACE),
    {{0x7c44d7d4,0x31d5,0x424c,{0xbd,0x5e,0x2b,0x3e,0x1f,0x32,0x3d,0x22}},{1,0}},
    {{0x8A885D04,0x1CEB,0x11C9,{0x9F,0xE8,0x08,0x00,0x2B,0x10,0x48,0x60}},{2,0}},
    0,
    0,
    0,
    0,
    0,
    0x00000000
    };
RPC_IF_HANDLE dsaop_v1_0_c_ifspec = (RPC_IF_HANDLE)& dsaop___RpcClientInterface;

extern const MIDL_STUB_DESC dsaop_StubDesc;

static RPC_BINDING_HANDLE dsaop__MIDL_AutoBindHandle;


ULONG IDL_DSAPrepareScript( 
    /* [in] */ handle_t hRpc,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DSA_MSG_PREPARE_SCRIPT_REQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DSA_MSG_PREPARE_SCRIPT_REPLY *pmsgOut)
{

    CLIENT_CALL_RETURN _RetVal;

    _RetVal = NdrClientCall2(
                  ( PMIDL_STUB_DESC  )&dsaop_StubDesc,
                  (PFORMAT_STRING) &drsr__MIDL_ProcFormatString.Format[1856],
                  hRpc,
                  dwInVersion,
                  pmsgIn,
                  pdwOutVersion,
                  pmsgOut);
    return ( ULONG  )_RetVal.Simple;
    
}


ULONG IDL_DSAExecuteScript( 
    /* [in] */ handle_t hRpc,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DSA_MSG_EXECUTE_SCRIPT_REQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DSA_MSG_EXECUTE_SCRIPT_REPLY *pmsgOut)
{

    CLIENT_CALL_RETURN _RetVal;

    _RetVal = NdrClientCall2(
                  ( PMIDL_STUB_DESC  )&dsaop_StubDesc,
                  (PFORMAT_STRING) &drsr__MIDL_ProcFormatString.Format[1916],
                  hRpc,
                  dwInVersion,
                  pmsgIn,
                  pdwOutVersion,
                  pmsgOut);
    return ( ULONG  )_RetVal.Simple;
    
}


#if !defined(__RPC_WIN64__)
#error  Invalid build platform for this stub.
#endif

static const drsr_MIDL_PROC_FORMAT_STRING drsr__MIDL_ProcFormatString =
    {
        0,
        {

	/* Procedure IDL_DRSBind */

			0x0,		/* 0 */
			0x48,		/* Old Flags:  */
/*  2 */	NdrFcLong( 0x0 ),	/* 0 */
/*  6 */	NdrFcShort( 0x0 ),	/* 0 */
/*  8 */	NdrFcShort( 0x30 ),	/* X64 Stack size/offset = 48 */
/* 10 */	0x32,		/* FC_BIND_PRIMITIVE */
			0x0,		/* 0 */
/* 12 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 14 */	NdrFcShort( 0x44 ),	/* 68 */
/* 16 */	NdrFcShort( 0x40 ),	/* 64 */
/* 18 */	0x47,		/* Oi2 Flags:  srv must size, clt must size, has return, has ext, */
			0x5,		/* 5 */
/* 20 */	0xa,		/* 10 */
			0x47,		/* Ext Flags:  new corr desc, clt corr check, srv corr check, has range on conformance */
/* 22 */	NdrFcShort( 0x1 ),	/* 1 */
/* 24 */	NdrFcShort( 0x1 ),	/* 1 */
/* 26 */	NdrFcShort( 0x0 ),	/* 0 */
/* 28 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter rpc_handle */

/* 30 */	NdrFcShort( 0xa ),	/* Flags:  must free, in, */
/* 32 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 34 */	NdrFcShort( 0x2 ),	/* Type Offset=2 */

	/* Parameter puuidClientDsa */

/* 36 */	NdrFcShort( 0xb ),	/* Flags:  must size, must free, in, */
/* 38 */	NdrFcShort( 0x10 ),	/* X64 Stack size/offset = 16 */
/* 40 */	NdrFcShort( 0x18 ),	/* Type Offset=24 */

	/* Parameter pextClient */

/* 42 */	NdrFcShort( 0x2013 ),	/* Flags:  must size, must free, out, srv alloc size=8 */
/* 44 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 46 */	NdrFcShort( 0x3a ),	/* Type Offset=58 */

	/* Parameter ppextServer */

/* 48 */	NdrFcShort( 0x110 ),	/* Flags:  out, simple ref, */
/* 50 */	NdrFcShort( 0x20 ),	/* X64 Stack size/offset = 32 */
/* 52 */	NdrFcShort( 0x42 ),	/* Type Offset=66 */

	/* Parameter phDrs */

/* 54 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 56 */	NdrFcShort( 0x28 ),	/* X64 Stack size/offset = 40 */
/* 58 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure IDL_DRSUnbind */


	/* Return value */

/* 60 */	0x0,		/* 0 */
			0x48,		/* Old Flags:  */
/* 62 */	NdrFcLong( 0x0 ),	/* 0 */
/* 66 */	NdrFcShort( 0x1 ),	/* 1 */
/* 68 */	NdrFcShort( 0x10 ),	/* X64 Stack size/offset = 16 */
/* 70 */	0x30,		/* FC_BIND_CONTEXT */
			0xe0,		/* Ctxt flags:  via ptr, in, out, */
/* 72 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 74 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 76 */	NdrFcShort( 0x38 ),	/* 56 */
/* 78 */	NdrFcShort( 0x40 ),	/* 64 */
/* 80 */	0x44,		/* Oi2 Flags:  has return, has ext, */
			0x2,		/* 2 */
/* 82 */	0xa,		/* 10 */
			0x41,		/* Ext Flags:  new corr desc, has range on conformance */
/* 84 */	NdrFcShort( 0x0 ),	/* 0 */
/* 86 */	NdrFcShort( 0x0 ),	/* 0 */
/* 88 */	NdrFcShort( 0x0 ),	/* 0 */
/* 90 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter phDrs */

/* 92 */	NdrFcShort( 0x118 ),	/* Flags:  in, out, simple ref, */
/* 94 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 96 */	NdrFcShort( 0x4a ),	/* Type Offset=74 */

	/* Return value */

/* 98 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 100 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 102 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure IDL_DRSReplicaSync */

/* 104 */	0x0,		/* 0 */
			0x48,		/* Old Flags:  */
/* 106 */	NdrFcLong( 0x0 ),	/* 0 */
/* 110 */	NdrFcShort( 0x2 ),	/* 2 */
/* 112 */	NdrFcShort( 0x20 ),	/* X64 Stack size/offset = 32 */
/* 114 */	0x30,		/* FC_BIND_CONTEXT */
			0x40,		/* Ctxt flags:  in, */
/* 116 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 118 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 120 */	NdrFcShort( 0x2c ),	/* 44 */
/* 122 */	NdrFcShort( 0x8 ),	/* 8 */
/* 124 */	0x46,		/* Oi2 Flags:  clt must size, has return, has ext, */
			0x4,		/* 4 */
/* 126 */	0xa,		/* 10 */
			0x45,		/* Ext Flags:  new corr desc, srv corr check, has range on conformance */
/* 128 */	NdrFcShort( 0x0 ),	/* 0 */
/* 130 */	NdrFcShort( 0x1 ),	/* 1 */
/* 132 */	NdrFcShort( 0x0 ),	/* 0 */
/* 134 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter hDrs */

/* 136 */	NdrFcShort( 0x8 ),	/* Flags:  in, */
/* 138 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 140 */	NdrFcShort( 0x4e ),	/* Type Offset=78 */

	/* Parameter dwVersion */

/* 142 */	NdrFcShort( 0x48 ),	/* Flags:  in, base type, */
/* 144 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 146 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgSync */

/* 148 */	NdrFcShort( 0x10b ),	/* Flags:  must size, must free, in, simple ref, */
/* 150 */	NdrFcShort( 0x10 ),	/* X64 Stack size/offset = 16 */
/* 152 */	NdrFcShort( 0x56 ),	/* Type Offset=86 */

	/* Return value */

/* 154 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 156 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 158 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure IDL_DRSGetNCChanges */

/* 160 */	0x0,		/* 0 */
			0x48,		/* Old Flags:  */
/* 162 */	NdrFcLong( 0x0 ),	/* 0 */
/* 166 */	NdrFcShort( 0x3 ),	/* 3 */
/* 168 */	NdrFcShort( 0x30 ),	/* X64 Stack size/offset = 48 */
/* 170 */	0x30,		/* FC_BIND_CONTEXT */
			0x40,		/* Ctxt flags:  in, */
/* 172 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 174 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 176 */	NdrFcShort( 0x2c ),	/* 44 */
/* 178 */	NdrFcShort( 0x24 ),	/* 36 */
/* 180 */	0x47,		/* Oi2 Flags:  srv must size, clt must size, has return, has ext, */
			0x6,		/* 6 */
/* 182 */	0xa,		/* 10 */
			0x47,		/* Ext Flags:  new corr desc, clt corr check, srv corr check, has range on conformance */
/* 184 */	NdrFcShort( 0x1 ),	/* 1 */
/* 186 */	NdrFcShort( 0x1 ),	/* 1 */
/* 188 */	NdrFcShort( 0x0 ),	/* 0 */
/* 190 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter hDrs */

/* 192 */	NdrFcShort( 0x8 ),	/* Flags:  in, */
/* 194 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 196 */	NdrFcShort( 0x4e ),	/* Type Offset=78 */

	/* Parameter dwInVersion */

/* 198 */	NdrFcShort( 0x48 ),	/* Flags:  in, base type, */
/* 200 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 202 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgIn */

/* 204 */	NdrFcShort( 0x10b ),	/* Flags:  must size, must free, in, simple ref, */
/* 206 */	NdrFcShort( 0x10 ),	/* X64 Stack size/offset = 16 */
/* 208 */	NdrFcShort( 0xcc ),	/* Type Offset=204 */

	/* Parameter pdwOutVersion */

/* 210 */	NdrFcShort( 0x2150 ),	/* Flags:  out, base type, simple ref, srv alloc size=8 */
/* 212 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 214 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgOut */

/* 216 */	NdrFcShort( 0x113 ),	/* Flags:  must size, must free, out, simple ref, */
/* 218 */	NdrFcShort( 0x20 ),	/* X64 Stack size/offset = 32 */
/* 220 */	NdrFcShort( 0x2f4 ),	/* Type Offset=756 */

	/* Return value */

/* 222 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 224 */	NdrFcShort( 0x28 ),	/* X64 Stack size/offset = 40 */
/* 226 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure IDL_DRSUpdateRefs */

/* 228 */	0x0,		/* 0 */
			0x48,		/* Old Flags:  */
/* 230 */	NdrFcLong( 0x0 ),	/* 0 */
/* 234 */	NdrFcShort( 0x4 ),	/* 4 */
/* 236 */	NdrFcShort( 0x20 ),	/* X64 Stack size/offset = 32 */
/* 238 */	0x30,		/* FC_BIND_CONTEXT */
			0x40,		/* Ctxt flags:  in, */
/* 240 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 242 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 244 */	NdrFcShort( 0x2c ),	/* 44 */
/* 246 */	NdrFcShort( 0x8 ),	/* 8 */
/* 248 */	0x46,		/* Oi2 Flags:  clt must size, has return, has ext, */
			0x4,		/* 4 */
/* 250 */	0xa,		/* 10 */
			0x45,		/* Ext Flags:  new corr desc, srv corr check, has range on conformance */
/* 252 */	NdrFcShort( 0x0 ),	/* 0 */
/* 254 */	NdrFcShort( 0x1 ),	/* 1 */
/* 256 */	NdrFcShort( 0x0 ),	/* 0 */
/* 258 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter hDrs */

/* 260 */	NdrFcShort( 0x8 ),	/* Flags:  in, */
/* 262 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 264 */	NdrFcShort( 0x4e ),	/* Type Offset=78 */

	/* Parameter dwVersion */

/* 266 */	NdrFcShort( 0x48 ),	/* Flags:  in, base type, */
/* 268 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 270 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgUpdRefs */

/* 272 */	NdrFcShort( 0x10b ),	/* Flags:  must size, must free, in, simple ref, */
/* 274 */	NdrFcShort( 0x10 ),	/* X64 Stack size/offset = 16 */
/* 276 */	NdrFcShort( 0x584 ),	/* Type Offset=1412 */

	/* Return value */

/* 278 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 280 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 282 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure IDL_DRSReplicaAdd */

/* 284 */	0x0,		/* 0 */
			0x48,		/* Old Flags:  */
/* 286 */	NdrFcLong( 0x0 ),	/* 0 */
/* 290 */	NdrFcShort( 0x5 ),	/* 5 */
/* 292 */	NdrFcShort( 0x20 ),	/* X64 Stack size/offset = 32 */
/* 294 */	0x30,		/* FC_BIND_CONTEXT */
			0x40,		/* Ctxt flags:  in, */
/* 296 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 298 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 300 */	NdrFcShort( 0x2c ),	/* 44 */
/* 302 */	NdrFcShort( 0x8 ),	/* 8 */
/* 304 */	0x46,		/* Oi2 Flags:  clt must size, has return, has ext, */
			0x4,		/* 4 */
/* 306 */	0xa,		/* 10 */
			0x45,		/* Ext Flags:  new corr desc, srv corr check, has range on conformance */
/* 308 */	NdrFcShort( 0x0 ),	/* 0 */
/* 310 */	NdrFcShort( 0x1 ),	/* 1 */
/* 312 */	NdrFcShort( 0x0 ),	/* 0 */
/* 314 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter hDrs */

/* 316 */	NdrFcShort( 0x8 ),	/* Flags:  in, */
/* 318 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 320 */	NdrFcShort( 0x4e ),	/* Type Offset=78 */

	/* Parameter dwVersion */

/* 322 */	NdrFcShort( 0x48 ),	/* Flags:  in, base type, */
/* 324 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 326 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgAdd */

/* 328 */	NdrFcShort( 0x10b ),	/* Flags:  must size, must free, in, simple ref, */
/* 330 */	NdrFcShort( 0x10 ),	/* X64 Stack size/offset = 16 */
/* 332 */	NdrFcShort( 0x5c2 ),	/* Type Offset=1474 */

	/* Return value */

/* 334 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 336 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 338 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure IDL_DRSReplicaDel */

/* 340 */	0x0,		/* 0 */
			0x48,		/* Old Flags:  */
/* 342 */	NdrFcLong( 0x0 ),	/* 0 */
/* 346 */	NdrFcShort( 0x6 ),	/* 6 */
/* 348 */	NdrFcShort( 0x20 ),	/* X64 Stack size/offset = 32 */
/* 350 */	0x30,		/* FC_BIND_CONTEXT */
			0x40,		/* Ctxt flags:  in, */
/* 352 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 354 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 356 */	NdrFcShort( 0x2c ),	/* 44 */
/* 358 */	NdrFcShort( 0x8 ),	/* 8 */
/* 360 */	0x46,		/* Oi2 Flags:  clt must size, has return, has ext, */
			0x4,		/* 4 */
/* 362 */	0xa,		/* 10 */
			0x45,		/* Ext Flags:  new corr desc, srv corr check, has range on conformance */
/* 364 */	NdrFcShort( 0x0 ),	/* 0 */
/* 366 */	NdrFcShort( 0x1 ),	/* 1 */
/* 368 */	NdrFcShort( 0x0 ),	/* 0 */
/* 370 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter hDrs */

/* 372 */	NdrFcShort( 0x8 ),	/* Flags:  in, */
/* 374 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 376 */	NdrFcShort( 0x4e ),	/* Type Offset=78 */

	/* Parameter dwVersion */

/* 378 */	NdrFcShort( 0x48 ),	/* Flags:  in, base type, */
/* 380 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 382 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgDel */

/* 384 */	NdrFcShort( 0x10b ),	/* Flags:  must size, must free, in, simple ref, */
/* 386 */	NdrFcShort( 0x10 ),	/* X64 Stack size/offset = 16 */
/* 388 */	NdrFcShort( 0x636 ),	/* Type Offset=1590 */

	/* Return value */

/* 390 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 392 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 394 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure IDL_DRSReplicaModify */

/* 396 */	0x0,		/* 0 */
			0x48,		/* Old Flags:  */
/* 398 */	NdrFcLong( 0x0 ),	/* 0 */
/* 402 */	NdrFcShort( 0x7 ),	/* 7 */
/* 404 */	NdrFcShort( 0x20 ),	/* X64 Stack size/offset = 32 */
/* 406 */	0x30,		/* FC_BIND_CONTEXT */
			0x40,		/* Ctxt flags:  in, */
/* 408 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 410 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 412 */	NdrFcShort( 0x2c ),	/* 44 */
/* 414 */	NdrFcShort( 0x8 ),	/* 8 */
/* 416 */	0x46,		/* Oi2 Flags:  clt must size, has return, has ext, */
			0x4,		/* 4 */
/* 418 */	0xa,		/* 10 */
			0x45,		/* Ext Flags:  new corr desc, srv corr check, has range on conformance */
/* 420 */	NdrFcShort( 0x0 ),	/* 0 */
/* 422 */	NdrFcShort( 0x1 ),	/* 1 */
/* 424 */	NdrFcShort( 0x0 ),	/* 0 */
/* 426 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter hDrs */

/* 428 */	NdrFcShort( 0x8 ),	/* Flags:  in, */
/* 430 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 432 */	NdrFcShort( 0x4e ),	/* Type Offset=78 */

	/* Parameter dwVersion */

/* 434 */	NdrFcShort( 0x48 ),	/* Flags:  in, base type, */
/* 436 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 438 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgMod */

/* 440 */	NdrFcShort( 0x10b ),	/* Flags:  must size, must free, in, simple ref, */
/* 442 */	NdrFcShort( 0x10 ),	/* X64 Stack size/offset = 16 */
/* 444 */	NdrFcShort( 0x670 ),	/* Type Offset=1648 */

	/* Return value */

/* 446 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 448 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 450 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure IDL_DRSVerifyNames */

/* 452 */	0x0,		/* 0 */
			0x48,		/* Old Flags:  */
/* 454 */	NdrFcLong( 0x0 ),	/* 0 */
/* 458 */	NdrFcShort( 0x8 ),	/* 8 */
/* 460 */	NdrFcShort( 0x30 ),	/* X64 Stack size/offset = 48 */
/* 462 */	0x30,		/* FC_BIND_CONTEXT */
			0x40,		/* Ctxt flags:  in, */
/* 464 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 466 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 468 */	NdrFcShort( 0x2c ),	/* 44 */
/* 470 */	NdrFcShort( 0x24 ),	/* 36 */
/* 472 */	0x47,		/* Oi2 Flags:  srv must size, clt must size, has return, has ext, */
			0x6,		/* 6 */
/* 474 */	0xa,		/* 10 */
			0x47,		/* Ext Flags:  new corr desc, clt corr check, srv corr check, has range on conformance */
/* 476 */	NdrFcShort( 0x1 ),	/* 1 */
/* 478 */	NdrFcShort( 0x1 ),	/* 1 */
/* 480 */	NdrFcShort( 0x0 ),	/* 0 */
/* 482 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter hDrs */

/* 484 */	NdrFcShort( 0x8 ),	/* Flags:  in, */
/* 486 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 488 */	NdrFcShort( 0x4e ),	/* Type Offset=78 */

	/* Parameter dwInVersion */

/* 490 */	NdrFcShort( 0x48 ),	/* Flags:  in, base type, */
/* 492 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 494 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgIn */

/* 496 */	NdrFcShort( 0x10b ),	/* Flags:  must size, must free, in, simple ref, */
/* 498 */	NdrFcShort( 0x10 ),	/* X64 Stack size/offset = 16 */
/* 500 */	NdrFcShort( 0x6b2 ),	/* Type Offset=1714 */

	/* Parameter pdwOutVersion */

/* 502 */	NdrFcShort( 0x2150 ),	/* Flags:  out, base type, simple ref, srv alloc size=8 */
/* 504 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 506 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgOut */

/* 508 */	NdrFcShort( 0x8113 ),	/* Flags:  must size, must free, out, simple ref, srv alloc size=32 */
/* 510 */	NdrFcShort( 0x20 ),	/* X64 Stack size/offset = 32 */
/* 512 */	NdrFcShort( 0x718 ),	/* Type Offset=1816 */

	/* Return value */

/* 514 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 516 */	NdrFcShort( 0x28 ),	/* X64 Stack size/offset = 40 */
/* 518 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure IDL_DRSGetMemberships */

/* 520 */	0x0,		/* 0 */
			0x48,		/* Old Flags:  */
/* 522 */	NdrFcLong( 0x0 ),	/* 0 */
/* 526 */	NdrFcShort( 0x9 ),	/* 9 */
/* 528 */	NdrFcShort( 0x30 ),	/* X64 Stack size/offset = 48 */
/* 530 */	0x30,		/* FC_BIND_CONTEXT */
			0x40,		/* Ctxt flags:  in, */
/* 532 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 534 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 536 */	NdrFcShort( 0x2c ),	/* 44 */
/* 538 */	NdrFcShort( 0x24 ),	/* 36 */
/* 540 */	0x47,		/* Oi2 Flags:  srv must size, clt must size, has return, has ext, */
			0x6,		/* 6 */
/* 542 */	0xa,		/* 10 */
			0x47,		/* Ext Flags:  new corr desc, clt corr check, srv corr check, has range on conformance */
/* 544 */	NdrFcShort( 0x1 ),	/* 1 */
/* 546 */	NdrFcShort( 0x1 ),	/* 1 */
/* 548 */	NdrFcShort( 0x0 ),	/* 0 */
/* 550 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter hDrs */

/* 552 */	NdrFcShort( 0x8 ),	/* Flags:  in, */
/* 554 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 556 */	NdrFcShort( 0x4e ),	/* Type Offset=78 */

	/* Parameter dwInVersion */

/* 558 */	NdrFcShort( 0x48 ),	/* Flags:  in, base type, */
/* 560 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 562 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgIn */

/* 564 */	NdrFcShort( 0x10b ),	/* Flags:  must size, must free, in, simple ref, */
/* 566 */	NdrFcShort( 0x10 ),	/* X64 Stack size/offset = 16 */
/* 568 */	NdrFcShort( 0x77a ),	/* Type Offset=1914 */

	/* Parameter pdwOutVersion */

/* 570 */	NdrFcShort( 0x2150 ),	/* Flags:  out, base type, simple ref, srv alloc size=8 */
/* 572 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 574 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgOut */

/* 576 */	NdrFcShort( 0xa113 ),	/* Flags:  must size, must free, out, simple ref, srv alloc size=40 */
/* 578 */	NdrFcShort( 0x20 ),	/* X64 Stack size/offset = 32 */
/* 580 */	NdrFcShort( 0x7ec ),	/* Type Offset=2028 */

	/* Return value */

/* 582 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 584 */	NdrFcShort( 0x28 ),	/* X64 Stack size/offset = 40 */
/* 586 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure IDL_DRSInterDomainMove */

/* 588 */	0x0,		/* 0 */
			0x48,		/* Old Flags:  */
/* 590 */	NdrFcLong( 0x0 ),	/* 0 */
/* 594 */	NdrFcShort( 0xa ),	/* 10 */
/* 596 */	NdrFcShort( 0x30 ),	/* X64 Stack size/offset = 48 */
/* 598 */	0x30,		/* FC_BIND_CONTEXT */
			0x40,		/* Ctxt flags:  in, */
/* 600 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 602 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 604 */	NdrFcShort( 0x2c ),	/* 44 */
/* 606 */	NdrFcShort( 0x24 ),	/* 36 */
/* 608 */	0x47,		/* Oi2 Flags:  srv must size, clt must size, has return, has ext, */
			0x6,		/* 6 */
/* 610 */	0xa,		/* 10 */
			0x47,		/* Ext Flags:  new corr desc, clt corr check, srv corr check, has range on conformance */
/* 612 */	NdrFcShort( 0x1 ),	/* 1 */
/* 614 */	NdrFcShort( 0x1 ),	/* 1 */
/* 616 */	NdrFcShort( 0x0 ),	/* 0 */
/* 618 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter hDrs */

/* 620 */	NdrFcShort( 0x8 ),	/* Flags:  in, */
/* 622 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 624 */	NdrFcShort( 0x4e ),	/* Type Offset=78 */

	/* Parameter dwInVersion */

/* 626 */	NdrFcShort( 0x48 ),	/* Flags:  in, base type, */
/* 628 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 630 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgIn */

/* 632 */	NdrFcShort( 0x10b ),	/* Flags:  must size, must free, in, simple ref, */
/* 634 */	NdrFcShort( 0x10 ),	/* X64 Stack size/offset = 16 */
/* 636 */	NdrFcShort( 0x896 ),	/* Type Offset=2198 */

	/* Parameter pdwOutVersion */

/* 638 */	NdrFcShort( 0x2150 ),	/* Flags:  out, base type, simple ref, srv alloc size=8 */
/* 640 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 642 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgOut */

/* 644 */	NdrFcShort( 0x8113 ),	/* Flags:  must size, must free, out, simple ref, srv alloc size=32 */
/* 646 */	NdrFcShort( 0x20 ),	/* X64 Stack size/offset = 32 */
/* 648 */	NdrFcShort( 0x950 ),	/* Type Offset=2384 */

	/* Return value */

/* 650 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 652 */	NdrFcShort( 0x28 ),	/* X64 Stack size/offset = 40 */
/* 654 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure IDL_DRSGetNT4ChangeLog */

/* 656 */	0x0,		/* 0 */
			0x48,		/* Old Flags:  */
/* 658 */	NdrFcLong( 0x0 ),	/* 0 */
/* 662 */	NdrFcShort( 0xb ),	/* 11 */
/* 664 */	NdrFcShort( 0x30 ),	/* X64 Stack size/offset = 48 */
/* 666 */	0x30,		/* FC_BIND_CONTEXT */
			0x40,		/* Ctxt flags:  in, */
/* 668 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 670 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 672 */	NdrFcShort( 0x2c ),	/* 44 */
/* 674 */	NdrFcShort( 0x24 ),	/* 36 */
/* 676 */	0x47,		/* Oi2 Flags:  srv must size, clt must size, has return, has ext, */
			0x6,		/* 6 */
/* 678 */	0xa,		/* 10 */
			0x47,		/* Ext Flags:  new corr desc, clt corr check, srv corr check, has range on conformance */
/* 680 */	NdrFcShort( 0x1 ),	/* 1 */
/* 682 */	NdrFcShort( 0x1 ),	/* 1 */
/* 684 */	NdrFcShort( 0x0 ),	/* 0 */
/* 686 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter hDrs */

/* 688 */	NdrFcShort( 0x8 ),	/* Flags:  in, */
/* 690 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 692 */	NdrFcShort( 0x4e ),	/* Type Offset=78 */

	/* Parameter dwInVersion */

/* 694 */	NdrFcShort( 0x48 ),	/* Flags:  in, base type, */
/* 696 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 698 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgIn */

/* 700 */	NdrFcShort( 0x10b ),	/* Flags:  must size, must free, in, simple ref, */
/* 702 */	NdrFcShort( 0x10 ),	/* X64 Stack size/offset = 16 */
/* 704 */	NdrFcShort( 0x9a6 ),	/* Type Offset=2470 */

	/* Parameter pdwOutVersion */

/* 706 */	NdrFcShort( 0x2150 ),	/* Flags:  out, base type, simple ref, srv alloc size=8 */
/* 708 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 710 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgOut */

/* 712 */	NdrFcShort( 0x113 ),	/* Flags:  must size, must free, out, simple ref, */
/* 714 */	NdrFcShort( 0x20 ),	/* X64 Stack size/offset = 32 */
/* 716 */	NdrFcShort( 0x9f2 ),	/* Type Offset=2546 */

	/* Return value */

/* 718 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 720 */	NdrFcShort( 0x28 ),	/* X64 Stack size/offset = 40 */
/* 722 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure IDL_DRSCrackNames */

/* 724 */	0x0,		/* 0 */
			0x48,		/* Old Flags:  */
/* 726 */	NdrFcLong( 0x0 ),	/* 0 */
/* 730 */	NdrFcShort( 0xc ),	/* 12 */
/* 732 */	NdrFcShort( 0x30 ),	/* X64 Stack size/offset = 48 */
/* 734 */	0x30,		/* FC_BIND_CONTEXT */
			0x40,		/* Ctxt flags:  in, */
/* 736 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 738 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 740 */	NdrFcShort( 0x2c ),	/* 44 */
/* 742 */	NdrFcShort( 0x24 ),	/* 36 */
/* 744 */	0x47,		/* Oi2 Flags:  srv must size, clt must size, has return, has ext, */
			0x6,		/* 6 */
/* 746 */	0xa,		/* 10 */
			0x47,		/* Ext Flags:  new corr desc, clt corr check, srv corr check, has range on conformance */
/* 748 */	NdrFcShort( 0x1 ),	/* 1 */
/* 750 */	NdrFcShort( 0x1 ),	/* 1 */
/* 752 */	NdrFcShort( 0x0 ),	/* 0 */
/* 754 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter hDrs */

/* 756 */	NdrFcShort( 0x8 ),	/* Flags:  in, */
/* 758 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 760 */	NdrFcShort( 0x4e ),	/* Type Offset=78 */

	/* Parameter dwInVersion */

/* 762 */	NdrFcShort( 0x48 ),	/* Flags:  in, base type, */
/* 764 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 766 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgIn */

/* 768 */	NdrFcShort( 0x10b ),	/* Flags:  must size, must free, in, simple ref, */
/* 770 */	NdrFcShort( 0x10 ),	/* X64 Stack size/offset = 16 */
/* 772 */	NdrFcShort( 0xa7c ),	/* Type Offset=2684 */

	/* Parameter pdwOutVersion */

/* 774 */	NdrFcShort( 0x2150 ),	/* Flags:  out, base type, simple ref, srv alloc size=8 */
/* 776 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 778 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgOut */

/* 780 */	NdrFcShort( 0x2113 ),	/* Flags:  must size, must free, out, simple ref, srv alloc size=8 */
/* 782 */	NdrFcShort( 0x20 ),	/* X64 Stack size/offset = 32 */
/* 784 */	NdrFcShort( 0xade ),	/* Type Offset=2782 */

	/* Return value */

/* 786 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 788 */	NdrFcShort( 0x28 ),	/* X64 Stack size/offset = 40 */
/* 790 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure IDL_DRSWriteSPN */

/* 792 */	0x0,		/* 0 */
			0x48,		/* Old Flags:  */
/* 794 */	NdrFcLong( 0x0 ),	/* 0 */
/* 798 */	NdrFcShort( 0xd ),	/* 13 */
/* 800 */	NdrFcShort( 0x30 ),	/* X64 Stack size/offset = 48 */
/* 802 */	0x30,		/* FC_BIND_CONTEXT */
			0x40,		/* Ctxt flags:  in, */
/* 804 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 806 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 808 */	NdrFcShort( 0x2c ),	/* 44 */
/* 810 */	NdrFcShort( 0x24 ),	/* 36 */
/* 812 */	0x47,		/* Oi2 Flags:  srv must size, clt must size, has return, has ext, */
			0x6,		/* 6 */
/* 814 */	0xa,		/* 10 */
			0x47,		/* Ext Flags:  new corr desc, clt corr check, srv corr check, has range on conformance */
/* 816 */	NdrFcShort( 0x1 ),	/* 1 */
/* 818 */	NdrFcShort( 0x1 ),	/* 1 */
/* 820 */	NdrFcShort( 0x0 ),	/* 0 */
/* 822 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter hDrs */

/* 824 */	NdrFcShort( 0x8 ),	/* Flags:  in, */
/* 826 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 828 */	NdrFcShort( 0x4e ),	/* Type Offset=78 */

	/* Parameter dwInVersion */

/* 830 */	NdrFcShort( 0x48 ),	/* Flags:  in, base type, */
/* 832 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 834 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgIn */

/* 836 */	NdrFcShort( 0x10b ),	/* Flags:  must size, must free, in, simple ref, */
/* 838 */	NdrFcShort( 0x10 ),	/* X64 Stack size/offset = 16 */
/* 840 */	NdrFcShort( 0xb60 ),	/* Type Offset=2912 */

	/* Parameter pdwOutVersion */

/* 842 */	NdrFcShort( 0x2150 ),	/* Flags:  out, base type, simple ref, srv alloc size=8 */
/* 844 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 846 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgOut */

/* 848 */	NdrFcShort( 0x2113 ),	/* Flags:  must size, must free, out, simple ref, srv alloc size=8 */
/* 850 */	NdrFcShort( 0x20 ),	/* X64 Stack size/offset = 32 */
/* 852 */	NdrFcShort( 0xbc6 ),	/* Type Offset=3014 */

	/* Return value */

/* 854 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 856 */	NdrFcShort( 0x28 ),	/* X64 Stack size/offset = 40 */
/* 858 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure IDL_DRSRemoveDsServer */

/* 860 */	0x0,		/* 0 */
			0x48,		/* Old Flags:  */
/* 862 */	NdrFcLong( 0x0 ),	/* 0 */
/* 866 */	NdrFcShort( 0xe ),	/* 14 */
/* 868 */	NdrFcShort( 0x30 ),	/* X64 Stack size/offset = 48 */
/* 870 */	0x30,		/* FC_BIND_CONTEXT */
			0x40,		/* Ctxt flags:  in, */
/* 872 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 874 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 876 */	NdrFcShort( 0x2c ),	/* 44 */
/* 878 */	NdrFcShort( 0x24 ),	/* 36 */
/* 880 */	0x47,		/* Oi2 Flags:  srv must size, clt must size, has return, has ext, */
			0x6,		/* 6 */
/* 882 */	0xa,		/* 10 */
			0x47,		/* Ext Flags:  new corr desc, clt corr check, srv corr check, has range on conformance */
/* 884 */	NdrFcShort( 0x1 ),	/* 1 */
/* 886 */	NdrFcShort( 0x1 ),	/* 1 */
/* 888 */	NdrFcShort( 0x0 ),	/* 0 */
/* 890 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter hDrs */

/* 892 */	NdrFcShort( 0x8 ),	/* Flags:  in, */
/* 894 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 896 */	NdrFcShort( 0x4e ),	/* Type Offset=78 */

	/* Parameter dwInVersion */

/* 898 */	NdrFcShort( 0x48 ),	/* Flags:  in, base type, */
/* 900 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 902 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgIn */

/* 904 */	NdrFcShort( 0x10b ),	/* Flags:  must size, must free, in, simple ref, */
/* 906 */	NdrFcShort( 0x10 ),	/* X64 Stack size/offset = 16 */
/* 908 */	NdrFcShort( 0xbf0 ),	/* Type Offset=3056 */

	/* Parameter pdwOutVersion */

/* 910 */	NdrFcShort( 0x2150 ),	/* Flags:  out, base type, simple ref, srv alloc size=8 */
/* 912 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 914 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgOut */

/* 916 */	NdrFcShort( 0x2113 ),	/* Flags:  must size, must free, out, simple ref, srv alloc size=8 */
/* 918 */	NdrFcShort( 0x20 ),	/* X64 Stack size/offset = 32 */
/* 920 */	NdrFcShort( 0xc2a ),	/* Type Offset=3114 */

	/* Return value */

/* 922 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 924 */	NdrFcShort( 0x28 ),	/* X64 Stack size/offset = 40 */
/* 926 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure IDL_DRSRemoveDsDomain */

/* 928 */	0x0,		/* 0 */
			0x48,		/* Old Flags:  */
/* 930 */	NdrFcLong( 0x0 ),	/* 0 */
/* 934 */	NdrFcShort( 0xf ),	/* 15 */
/* 936 */	NdrFcShort( 0x30 ),	/* X64 Stack size/offset = 48 */
/* 938 */	0x30,		/* FC_BIND_CONTEXT */
			0x40,		/* Ctxt flags:  in, */
/* 940 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 942 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 944 */	NdrFcShort( 0x2c ),	/* 44 */
/* 946 */	NdrFcShort( 0x24 ),	/* 36 */
/* 948 */	0x47,		/* Oi2 Flags:  srv must size, clt must size, has return, has ext, */
			0x6,		/* 6 */
/* 950 */	0xa,		/* 10 */
			0x47,		/* Ext Flags:  new corr desc, clt corr check, srv corr check, has range on conformance */
/* 952 */	NdrFcShort( 0x1 ),	/* 1 */
/* 954 */	NdrFcShort( 0x1 ),	/* 1 */
/* 956 */	NdrFcShort( 0x0 ),	/* 0 */
/* 958 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter hDrs */

/* 960 */	NdrFcShort( 0x8 ),	/* Flags:  in, */
/* 962 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 964 */	NdrFcShort( 0x4e ),	/* Type Offset=78 */

	/* Parameter dwInVersion */

/* 966 */	NdrFcShort( 0x48 ),	/* Flags:  in, base type, */
/* 968 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 970 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgIn */

/* 972 */	NdrFcShort( 0x10b ),	/* Flags:  must size, must free, in, simple ref, */
/* 974 */	NdrFcShort( 0x10 ),	/* X64 Stack size/offset = 16 */
/* 976 */	NdrFcShort( 0xc4e ),	/* Type Offset=3150 */

	/* Parameter pdwOutVersion */

/* 978 */	NdrFcShort( 0x2150 ),	/* Flags:  out, base type, simple ref, srv alloc size=8 */
/* 980 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 982 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgOut */

/* 984 */	NdrFcShort( 0x2113 ),	/* Flags:  must size, must free, out, simple ref, srv alloc size=8 */
/* 986 */	NdrFcShort( 0x20 ),	/* X64 Stack size/offset = 32 */
/* 988 */	NdrFcShort( 0xc80 ),	/* Type Offset=3200 */

	/* Return value */

/* 990 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 992 */	NdrFcShort( 0x28 ),	/* X64 Stack size/offset = 40 */
/* 994 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure IDL_DRSDomainControllerInfo */

/* 996 */	0x0,		/* 0 */
			0x48,		/* Old Flags:  */
/* 998 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1002 */	NdrFcShort( 0x10 ),	/* 16 */
/* 1004 */	NdrFcShort( 0x30 ),	/* X64 Stack size/offset = 48 */
/* 1006 */	0x30,		/* FC_BIND_CONTEXT */
			0x40,		/* Ctxt flags:  in, */
/* 1008 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 1010 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 1012 */	NdrFcShort( 0x2c ),	/* 44 */
/* 1014 */	NdrFcShort( 0x24 ),	/* 36 */
/* 1016 */	0x47,		/* Oi2 Flags:  srv must size, clt must size, has return, has ext, */
			0x6,		/* 6 */
/* 1018 */	0xa,		/* 10 */
			0x47,		/* Ext Flags:  new corr desc, clt corr check, srv corr check, has range on conformance */
/* 1020 */	NdrFcShort( 0x1 ),	/* 1 */
/* 1022 */	NdrFcShort( 0x1 ),	/* 1 */
/* 1024 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1026 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter hDrs */

/* 1028 */	NdrFcShort( 0x8 ),	/* Flags:  in, */
/* 1030 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 1032 */	NdrFcShort( 0x4e ),	/* Type Offset=78 */

	/* Parameter dwInVersion */

/* 1034 */	NdrFcShort( 0x48 ),	/* Flags:  in, base type, */
/* 1036 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 1038 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgIn */

/* 1040 */	NdrFcShort( 0x10b ),	/* Flags:  must size, must free, in, simple ref, */
/* 1042 */	NdrFcShort( 0x10 ),	/* X64 Stack size/offset = 16 */
/* 1044 */	NdrFcShort( 0xca4 ),	/* Type Offset=3236 */

	/* Parameter pdwOutVersion */

/* 1046 */	NdrFcShort( 0x2150 ),	/* Flags:  out, base type, simple ref, srv alloc size=8 */
/* 1048 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 1050 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgOut */

/* 1052 */	NdrFcShort( 0x4113 ),	/* Flags:  must size, must free, out, simple ref, srv alloc size=16 */
/* 1054 */	NdrFcShort( 0x20 ),	/* X64 Stack size/offset = 32 */
/* 1056 */	NdrFcShort( 0xcd8 ),	/* Type Offset=3288 */

	/* Return value */

/* 1058 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 1060 */	NdrFcShort( 0x28 ),	/* X64 Stack size/offset = 40 */
/* 1062 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure IDL_DRSAddEntry */

/* 1064 */	0x0,		/* 0 */
			0x48,		/* Old Flags:  */
/* 1066 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1070 */	NdrFcShort( 0x11 ),	/* 17 */
/* 1072 */	NdrFcShort( 0x30 ),	/* X64 Stack size/offset = 48 */
/* 1074 */	0x30,		/* FC_BIND_CONTEXT */
			0x40,		/* Ctxt flags:  in, */
/* 1076 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 1078 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 1080 */	NdrFcShort( 0x2c ),	/* 44 */
/* 1082 */	NdrFcShort( 0x24 ),	/* 36 */
/* 1084 */	0x47,		/* Oi2 Flags:  srv must size, clt must size, has return, has ext, */
			0x6,		/* 6 */
/* 1086 */	0xa,		/* 10 */
			0x47,		/* Ext Flags:  new corr desc, clt corr check, srv corr check, has range on conformance */
/* 1088 */	NdrFcShort( 0x1 ),	/* 1 */
/* 1090 */	NdrFcShort( 0x1 ),	/* 1 */
/* 1092 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1094 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter hDrs */

/* 1096 */	NdrFcShort( 0x8 ),	/* Flags:  in, */
/* 1098 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 1100 */	NdrFcShort( 0x4e ),	/* Type Offset=78 */

	/* Parameter dwInVersion */

/* 1102 */	NdrFcShort( 0x48 ),	/* Flags:  in, base type, */
/* 1104 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 1106 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgIn */

/* 1108 */	NdrFcShort( 0x10b ),	/* Flags:  must size, must free, in, simple ref, */
/* 1110 */	NdrFcShort( 0x10 ),	/* X64 Stack size/offset = 16 */
/* 1112 */	NdrFcShort( 0xeae ),	/* Type Offset=3758 */

	/* Parameter pdwOutVersion */

/* 1114 */	NdrFcShort( 0x2150 ),	/* Flags:  out, base type, simple ref, srv alloc size=8 */
/* 1116 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 1118 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgOut */

/* 1120 */	NdrFcShort( 0x113 ),	/* Flags:  must size, must free, out, simple ref, */
/* 1122 */	NdrFcShort( 0x20 ),	/* X64 Stack size/offset = 32 */
/* 1124 */	NdrFcShort( 0xf22 ),	/* Type Offset=3874 */

	/* Return value */

/* 1126 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 1128 */	NdrFcShort( 0x28 ),	/* X64 Stack size/offset = 40 */
/* 1130 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure IDL_DRSExecuteKCC */

/* 1132 */	0x0,		/* 0 */
			0x48,		/* Old Flags:  */
/* 1134 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1138 */	NdrFcShort( 0x12 ),	/* 18 */
/* 1140 */	NdrFcShort( 0x20 ),	/* X64 Stack size/offset = 32 */
/* 1142 */	0x30,		/* FC_BIND_CONTEXT */
			0x40,		/* Ctxt flags:  in, */
/* 1144 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 1146 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 1148 */	NdrFcShort( 0x2c ),	/* 44 */
/* 1150 */	NdrFcShort( 0x8 ),	/* 8 */
/* 1152 */	0x46,		/* Oi2 Flags:  clt must size, has return, has ext, */
			0x4,		/* 4 */
/* 1154 */	0xa,		/* 10 */
			0x45,		/* Ext Flags:  new corr desc, srv corr check, has range on conformance */
/* 1156 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1158 */	NdrFcShort( 0x1 ),	/* 1 */
/* 1160 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1162 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter hDrs */

/* 1164 */	NdrFcShort( 0x8 ),	/* Flags:  in, */
/* 1166 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 1168 */	NdrFcShort( 0x4e ),	/* Type Offset=78 */

	/* Parameter dwInVersion */

/* 1170 */	NdrFcShort( 0x48 ),	/* Flags:  in, base type, */
/* 1172 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 1174 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgIn */

/* 1176 */	NdrFcShort( 0x10b ),	/* Flags:  must size, must free, in, simple ref, */
/* 1178 */	NdrFcShort( 0x10 ),	/* X64 Stack size/offset = 16 */
/* 1180 */	NdrFcShort( 0x115c ),	/* Type Offset=4444 */

	/* Return value */

/* 1182 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 1184 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 1186 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure IDL_DRSGetReplInfo */

/* 1188 */	0x0,		/* 0 */
			0x49,		/* Old Flags:  full ptr, */
/* 1190 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1194 */	NdrFcShort( 0x13 ),	/* 19 */
/* 1196 */	NdrFcShort( 0x30 ),	/* X64 Stack size/offset = 48 */
/* 1198 */	0x30,		/* FC_BIND_CONTEXT */
			0x40,		/* Ctxt flags:  in, */
/* 1200 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 1202 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 1204 */	NdrFcShort( 0x2c ),	/* 44 */
/* 1206 */	NdrFcShort( 0x24 ),	/* 36 */
/* 1208 */	0x47,		/* Oi2 Flags:  srv must size, clt must size, has return, has ext, */
			0x6,		/* 6 */
/* 1210 */	0xa,		/* 10 */
			0x47,		/* Ext Flags:  new corr desc, clt corr check, srv corr check, has range on conformance */
/* 1212 */	NdrFcShort( 0x1 ),	/* 1 */
/* 1214 */	NdrFcShort( 0x1 ),	/* 1 */
/* 1216 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1218 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter hDrs */

/* 1220 */	NdrFcShort( 0x8 ),	/* Flags:  in, */
/* 1222 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 1224 */	NdrFcShort( 0x4e ),	/* Type Offset=78 */

	/* Parameter dwInVersion */

/* 1226 */	NdrFcShort( 0x48 ),	/* Flags:  in, base type, */
/* 1228 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 1230 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgIn */

/* 1232 */	NdrFcShort( 0x10b ),	/* Flags:  must size, must free, in, simple ref, */
/* 1234 */	NdrFcShort( 0x10 ),	/* X64 Stack size/offset = 16 */
/* 1236 */	NdrFcShort( 0x1188 ),	/* Type Offset=4488 */

	/* Parameter pdwOutVersion */

/* 1238 */	NdrFcShort( 0x2150 ),	/* Flags:  out, base type, simple ref, srv alloc size=8 */
/* 1240 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 1242 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgOut */

/* 1244 */	NdrFcShort( 0x2113 ),	/* Flags:  must size, must free, out, simple ref, srv alloc size=8 */
/* 1246 */	NdrFcShort( 0x20 ),	/* X64 Stack size/offset = 32 */
/* 1248 */	NdrFcShort( 0x11e8 ),	/* Type Offset=4584 */

	/* Return value */

/* 1250 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 1252 */	NdrFcShort( 0x28 ),	/* X64 Stack size/offset = 40 */
/* 1254 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure IDL_DRSAddSidHistory */

/* 1256 */	0x0,		/* 0 */
			0x49,		/* Old Flags:  full ptr, */
/* 1258 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1262 */	NdrFcShort( 0x14 ),	/* 20 */
/* 1264 */	NdrFcShort( 0x30 ),	/* X64 Stack size/offset = 48 */
/* 1266 */	0x30,		/* FC_BIND_CONTEXT */
			0x40,		/* Ctxt flags:  in, */
/* 1268 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 1270 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 1272 */	NdrFcShort( 0x2c ),	/* 44 */
/* 1274 */	NdrFcShort( 0x24 ),	/* 36 */
/* 1276 */	0x47,		/* Oi2 Flags:  srv must size, clt must size, has return, has ext, */
			0x6,		/* 6 */
/* 1278 */	0xa,		/* 10 */
			0x47,		/* Ext Flags:  new corr desc, clt corr check, srv corr check, has range on conformance */
/* 1280 */	NdrFcShort( 0x1 ),	/* 1 */
/* 1282 */	NdrFcShort( 0x1 ),	/* 1 */
/* 1284 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1286 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter hDrs */

/* 1288 */	NdrFcShort( 0x8 ),	/* Flags:  in, */
/* 1290 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 1292 */	NdrFcShort( 0x4e ),	/* Type Offset=78 */

	/* Parameter dwInVersion */

/* 1294 */	NdrFcShort( 0x48 ),	/* Flags:  in, base type, */
/* 1296 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 1298 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgIn */

/* 1300 */	NdrFcShort( 0x10b ),	/* Flags:  must size, must free, in, simple ref, */
/* 1302 */	NdrFcShort( 0x10 ),	/* X64 Stack size/offset = 16 */
/* 1304 */	NdrFcShort( 0x1666 ),	/* Type Offset=5734 */

	/* Parameter pdwOutVersion */

/* 1306 */	NdrFcShort( 0x2150 ),	/* Flags:  out, base type, simple ref, srv alloc size=8 */
/* 1308 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 1310 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgOut */

/* 1312 */	NdrFcShort( 0x2113 ),	/* Flags:  must size, must free, out, simple ref, srv alloc size=8 */
/* 1314 */	NdrFcShort( 0x20 ),	/* X64 Stack size/offset = 32 */
/* 1316 */	NdrFcShort( 0x1706 ),	/* Type Offset=5894 */

	/* Return value */

/* 1318 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 1320 */	NdrFcShort( 0x28 ),	/* X64 Stack size/offset = 40 */
/* 1322 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure IDL_DRSGetMemberships2 */

/* 1324 */	0x0,		/* 0 */
			0x48,		/* Old Flags:  */
/* 1326 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1330 */	NdrFcShort( 0x15 ),	/* 21 */
/* 1332 */	NdrFcShort( 0x30 ),	/* X64 Stack size/offset = 48 */
/* 1334 */	0x30,		/* FC_BIND_CONTEXT */
			0x40,		/* Ctxt flags:  in, */
/* 1336 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 1338 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 1340 */	NdrFcShort( 0x2c ),	/* 44 */
/* 1342 */	NdrFcShort( 0x24 ),	/* 36 */
/* 1344 */	0x47,		/* Oi2 Flags:  srv must size, clt must size, has return, has ext, */
			0x6,		/* 6 */
/* 1346 */	0xa,		/* 10 */
			0x47,		/* Ext Flags:  new corr desc, clt corr check, srv corr check, has range on conformance */
/* 1348 */	NdrFcShort( 0x1 ),	/* 1 */
/* 1350 */	NdrFcShort( 0x1 ),	/* 1 */
/* 1352 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1354 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter hDrs */

/* 1356 */	NdrFcShort( 0x8 ),	/* Flags:  in, */
/* 1358 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 1360 */	NdrFcShort( 0x4e ),	/* Type Offset=78 */

	/* Parameter dwInVersion */

/* 1362 */	NdrFcShort( 0x48 ),	/* Flags:  in, base type, */
/* 1364 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 1366 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgIn */

/* 1368 */	NdrFcShort( 0x10b ),	/* Flags:  must size, must free, in, simple ref, */
/* 1370 */	NdrFcShort( 0x10 ),	/* X64 Stack size/offset = 16 */
/* 1372 */	NdrFcShort( 0x172a ),	/* Type Offset=5930 */

	/* Parameter pdwOutVersion */

/* 1374 */	NdrFcShort( 0x2150 ),	/* Flags:  out, base type, simple ref, srv alloc size=8 */
/* 1376 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 1378 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgOut */

/* 1380 */	NdrFcShort( 0x4113 ),	/* Flags:  must size, must free, out, simple ref, srv alloc size=16 */
/* 1382 */	NdrFcShort( 0x20 ),	/* X64 Stack size/offset = 32 */
/* 1384 */	NdrFcShort( 0x1788 ),	/* Type Offset=6024 */

	/* Return value */

/* 1386 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 1388 */	NdrFcShort( 0x28 ),	/* X64 Stack size/offset = 40 */
/* 1390 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure IDL_DRSReplicaVerifyObjects */

/* 1392 */	0x0,		/* 0 */
			0x48,		/* Old Flags:  */
/* 1394 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1398 */	NdrFcShort( 0x16 ),	/* 22 */
/* 1400 */	NdrFcShort( 0x20 ),	/* X64 Stack size/offset = 32 */
/* 1402 */	0x30,		/* FC_BIND_CONTEXT */
			0x40,		/* Ctxt flags:  in, */
/* 1404 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 1406 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 1408 */	NdrFcShort( 0x2c ),	/* 44 */
/* 1410 */	NdrFcShort( 0x8 ),	/* 8 */
/* 1412 */	0x46,		/* Oi2 Flags:  clt must size, has return, has ext, */
			0x4,		/* 4 */
/* 1414 */	0xa,		/* 10 */
			0x45,		/* Ext Flags:  new corr desc, srv corr check, has range on conformance */
/* 1416 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1418 */	NdrFcShort( 0x1 ),	/* 1 */
/* 1420 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1422 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter hDrs */

/* 1424 */	NdrFcShort( 0x8 ),	/* Flags:  in, */
/* 1426 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 1428 */	NdrFcShort( 0x4e ),	/* Type Offset=78 */

	/* Parameter dwVersion */

/* 1430 */	NdrFcShort( 0x48 ),	/* Flags:  in, base type, */
/* 1432 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 1434 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgVerify */

/* 1436 */	NdrFcShort( 0x10b ),	/* Flags:  must size, must free, in, simple ref, */
/* 1438 */	NdrFcShort( 0x10 ),	/* X64 Stack size/offset = 16 */
/* 1440 */	NdrFcShort( 0x17e6 ),	/* Type Offset=6118 */

	/* Return value */

/* 1442 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 1444 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 1446 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure IDL_DRSGetObjectExistence */

/* 1448 */	0x0,		/* 0 */
			0x48,		/* Old Flags:  */
/* 1450 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1454 */	NdrFcShort( 0x17 ),	/* 23 */
/* 1456 */	NdrFcShort( 0x30 ),	/* X64 Stack size/offset = 48 */
/* 1458 */	0x30,		/* FC_BIND_CONTEXT */
			0x40,		/* Ctxt flags:  in, */
/* 1460 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 1462 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 1464 */	NdrFcShort( 0x2c ),	/* 44 */
/* 1466 */	NdrFcShort( 0x24 ),	/* 36 */
/* 1468 */	0x47,		/* Oi2 Flags:  srv must size, clt must size, has return, has ext, */
			0x6,		/* 6 */
/* 1470 */	0xa,		/* 10 */
			0x47,		/* Ext Flags:  new corr desc, clt corr check, srv corr check, has range on conformance */
/* 1472 */	NdrFcShort( 0x1 ),	/* 1 */
/* 1474 */	NdrFcShort( 0x1 ),	/* 1 */
/* 1476 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1478 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter hDrs */

/* 1480 */	NdrFcShort( 0x8 ),	/* Flags:  in, */
/* 1482 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 1484 */	NdrFcShort( 0x4e ),	/* Type Offset=78 */

	/* Parameter dwInVersion */

/* 1486 */	NdrFcShort( 0x48 ),	/* Flags:  in, base type, */
/* 1488 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 1490 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgIn */

/* 1492 */	NdrFcShort( 0x10b ),	/* Flags:  must size, must free, in, simple ref, */
/* 1494 */	NdrFcShort( 0x10 ),	/* X64 Stack size/offset = 16 */
/* 1496 */	NdrFcShort( 0x181e ),	/* Type Offset=6174 */

	/* Parameter pdwOutVersion */

/* 1498 */	NdrFcShort( 0x2150 ),	/* Flags:  out, base type, simple ref, srv alloc size=8 */
/* 1500 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 1502 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgOut */

/* 1504 */	NdrFcShort( 0x4113 ),	/* Flags:  must size, must free, out, simple ref, srv alloc size=16 */
/* 1506 */	NdrFcShort( 0x20 ),	/* X64 Stack size/offset = 32 */
/* 1508 */	NdrFcShort( 0x1866 ),	/* Type Offset=6246 */

	/* Return value */

/* 1510 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 1512 */	NdrFcShort( 0x28 ),	/* X64 Stack size/offset = 40 */
/* 1514 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure IDL_DRSQuerySitesByCost */

/* 1516 */	0x0,		/* 0 */
			0x48,		/* Old Flags:  */
/* 1518 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1522 */	NdrFcShort( 0x18 ),	/* 24 */
/* 1524 */	NdrFcShort( 0x30 ),	/* X64 Stack size/offset = 48 */
/* 1526 */	0x30,		/* FC_BIND_CONTEXT */
			0x40,		/* Ctxt flags:  in, */
/* 1528 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 1530 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 1532 */	NdrFcShort( 0x2c ),	/* 44 */
/* 1534 */	NdrFcShort( 0x24 ),	/* 36 */
/* 1536 */	0x47,		/* Oi2 Flags:  srv must size, clt must size, has return, has ext, */
			0x6,		/* 6 */
/* 1538 */	0xa,		/* 10 */
			0x47,		/* Ext Flags:  new corr desc, clt corr check, srv corr check, has range on conformance */
/* 1540 */	NdrFcShort( 0x1 ),	/* 1 */
/* 1542 */	NdrFcShort( 0x1 ),	/* 1 */
/* 1544 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1546 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter hDrs */

/* 1548 */	NdrFcShort( 0x8 ),	/* Flags:  in, */
/* 1550 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 1552 */	NdrFcShort( 0x4e ),	/* Type Offset=78 */

	/* Parameter dwInVersion */

/* 1554 */	NdrFcShort( 0x48 ),	/* Flags:  in, base type, */
/* 1556 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 1558 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgIn */

/* 1560 */	NdrFcShort( 0x10b ),	/* Flags:  must size, must free, in, simple ref, */
/* 1562 */	NdrFcShort( 0x10 ),	/* X64 Stack size/offset = 16 */
/* 1564 */	NdrFcShort( 0x18c4 ),	/* Type Offset=6340 */

	/* Parameter pdwOutVersion */

/* 1566 */	NdrFcShort( 0x2150 ),	/* Flags:  out, base type, simple ref, srv alloc size=8 */
/* 1568 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 1570 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgOut */

/* 1572 */	NdrFcShort( 0x6113 ),	/* Flags:  must size, must free, out, simple ref, srv alloc size=24 */
/* 1574 */	NdrFcShort( 0x20 ),	/* X64 Stack size/offset = 32 */
/* 1576 */	NdrFcShort( 0x192a ),	/* Type Offset=6442 */

	/* Return value */

/* 1578 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 1580 */	NdrFcShort( 0x28 ),	/* X64 Stack size/offset = 40 */
/* 1582 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure IDL_DRSInitDemotion */

/* 1584 */	0x0,		/* 0 */
			0x48,		/* Old Flags:  */
/* 1586 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1590 */	NdrFcShort( 0x19 ),	/* 25 */
/* 1592 */	NdrFcShort( 0x30 ),	/* X64 Stack size/offset = 48 */
/* 1594 */	0x30,		/* FC_BIND_CONTEXT */
			0x40,		/* Ctxt flags:  in, */
/* 1596 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 1598 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 1600 */	NdrFcShort( 0x2c ),	/* 44 */
/* 1602 */	NdrFcShort( 0x24 ),	/* 36 */
/* 1604 */	0x47,		/* Oi2 Flags:  srv must size, clt must size, has return, has ext, */
			0x6,		/* 6 */
/* 1606 */	0xa,		/* 10 */
			0x47,		/* Ext Flags:  new corr desc, clt corr check, srv corr check, has range on conformance */
/* 1608 */	NdrFcShort( 0x1 ),	/* 1 */
/* 1610 */	NdrFcShort( 0x1 ),	/* 1 */
/* 1612 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1614 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter hDrs */

/* 1616 */	NdrFcShort( 0x8 ),	/* Flags:  in, */
/* 1618 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 1620 */	NdrFcShort( 0x4e ),	/* Type Offset=78 */

	/* Parameter dwInVersion */

/* 1622 */	NdrFcShort( 0x48 ),	/* Flags:  in, base type, */
/* 1624 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 1626 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgIn */

/* 1628 */	NdrFcShort( 0x10b ),	/* Flags:  must size, must free, in, simple ref, */
/* 1630 */	NdrFcShort( 0x10 ),	/* X64 Stack size/offset = 16 */
/* 1632 */	NdrFcShort( 0x198a ),	/* Type Offset=6538 */

	/* Parameter pdwOutVersion */

/* 1634 */	NdrFcShort( 0x2150 ),	/* Flags:  out, base type, simple ref, srv alloc size=8 */
/* 1636 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 1638 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgOut */

/* 1640 */	NdrFcShort( 0x2113 ),	/* Flags:  must size, must free, out, simple ref, srv alloc size=8 */
/* 1642 */	NdrFcShort( 0x20 ),	/* X64 Stack size/offset = 32 */
/* 1644 */	NdrFcShort( 0x19ae ),	/* Type Offset=6574 */

	/* Return value */

/* 1646 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 1648 */	NdrFcShort( 0x28 ),	/* X64 Stack size/offset = 40 */
/* 1650 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure IDL_DRSReplicaDemotion */

/* 1652 */	0x0,		/* 0 */
			0x48,		/* Old Flags:  */
/* 1654 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1658 */	NdrFcShort( 0x1a ),	/* 26 */
/* 1660 */	NdrFcShort( 0x30 ),	/* X64 Stack size/offset = 48 */
/* 1662 */	0x30,		/* FC_BIND_CONTEXT */
			0x40,		/* Ctxt flags:  in, */
/* 1664 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 1666 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 1668 */	NdrFcShort( 0x2c ),	/* 44 */
/* 1670 */	NdrFcShort( 0x24 ),	/* 36 */
/* 1672 */	0x47,		/* Oi2 Flags:  srv must size, clt must size, has return, has ext, */
			0x6,		/* 6 */
/* 1674 */	0xa,		/* 10 */
			0x47,		/* Ext Flags:  new corr desc, clt corr check, srv corr check, has range on conformance */
/* 1676 */	NdrFcShort( 0x1 ),	/* 1 */
/* 1678 */	NdrFcShort( 0x1 ),	/* 1 */
/* 1680 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1682 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter hDrs */

/* 1684 */	NdrFcShort( 0x8 ),	/* Flags:  in, */
/* 1686 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 1688 */	NdrFcShort( 0x4e ),	/* Type Offset=78 */

	/* Parameter dwInVersion */

/* 1690 */	NdrFcShort( 0x48 ),	/* Flags:  in, base type, */
/* 1692 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 1694 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgIn */

/* 1696 */	NdrFcShort( 0x10b ),	/* Flags:  must size, must free, in, simple ref, */
/* 1698 */	NdrFcShort( 0x10 ),	/* X64 Stack size/offset = 16 */
/* 1700 */	NdrFcShort( 0x19d2 ),	/* Type Offset=6610 */

	/* Parameter pdwOutVersion */

/* 1702 */	NdrFcShort( 0x2150 ),	/* Flags:  out, base type, simple ref, srv alloc size=8 */
/* 1704 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 1706 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgOut */

/* 1708 */	NdrFcShort( 0x2113 ),	/* Flags:  must size, must free, out, simple ref, srv alloc size=8 */
/* 1710 */	NdrFcShort( 0x20 ),	/* X64 Stack size/offset = 32 */
/* 1712 */	NdrFcShort( 0x1a0a ),	/* Type Offset=6666 */

	/* Return value */

/* 1714 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 1716 */	NdrFcShort( 0x28 ),	/* X64 Stack size/offset = 40 */
/* 1718 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure IDL_DRSFinishDemotion */

/* 1720 */	0x0,		/* 0 */
			0x48,		/* Old Flags:  */
/* 1722 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1726 */	NdrFcShort( 0x1b ),	/* 27 */
/* 1728 */	NdrFcShort( 0x30 ),	/* X64 Stack size/offset = 48 */
/* 1730 */	0x30,		/* FC_BIND_CONTEXT */
			0x40,		/* Ctxt flags:  in, */
/* 1732 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 1734 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 1736 */	NdrFcShort( 0x2c ),	/* 44 */
/* 1738 */	NdrFcShort( 0x24 ),	/* 36 */
/* 1740 */	0x47,		/* Oi2 Flags:  srv must size, clt must size, has return, has ext, */
			0x6,		/* 6 */
/* 1742 */	0xa,		/* 10 */
			0x47,		/* Ext Flags:  new corr desc, clt corr check, srv corr check, has range on conformance */
/* 1744 */	NdrFcShort( 0x1 ),	/* 1 */
/* 1746 */	NdrFcShort( 0x1 ),	/* 1 */
/* 1748 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1750 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter hDrs */

/* 1752 */	NdrFcShort( 0x8 ),	/* Flags:  in, */
/* 1754 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 1756 */	NdrFcShort( 0x4e ),	/* Type Offset=78 */

	/* Parameter dwInVersion */

/* 1758 */	NdrFcShort( 0x48 ),	/* Flags:  in, base type, */
/* 1760 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 1762 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgIn */

/* 1764 */	NdrFcShort( 0x10b ),	/* Flags:  must size, must free, in, simple ref, */
/* 1766 */	NdrFcShort( 0x10 ),	/* X64 Stack size/offset = 16 */
/* 1768 */	NdrFcShort( 0x1a2e ),	/* Type Offset=6702 */

	/* Parameter pdwOutVersion */

/* 1770 */	NdrFcShort( 0x2150 ),	/* Flags:  out, base type, simple ref, srv alloc size=8 */
/* 1772 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 1774 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgOut */

/* 1776 */	NdrFcShort( 0x4113 ),	/* Flags:  must size, must free, out, simple ref, srv alloc size=16 */
/* 1778 */	NdrFcShort( 0x20 ),	/* X64 Stack size/offset = 32 */
/* 1780 */	NdrFcShort( 0x1a66 ),	/* Type Offset=6758 */

	/* Return value */

/* 1782 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 1784 */	NdrFcShort( 0x28 ),	/* X64 Stack size/offset = 40 */
/* 1786 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure IDL_DRSAddCloneDC */

/* 1788 */	0x0,		/* 0 */
			0x48,		/* Old Flags:  */
/* 1790 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1794 */	NdrFcShort( 0x1c ),	/* 28 */
/* 1796 */	NdrFcShort( 0x30 ),	/* X64 Stack size/offset = 48 */
/* 1798 */	0x30,		/* FC_BIND_CONTEXT */
			0x40,		/* Ctxt flags:  in, */
/* 1800 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 1802 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 1804 */	NdrFcShort( 0x2c ),	/* 44 */
/* 1806 */	NdrFcShort( 0x24 ),	/* 36 */
/* 1808 */	0x47,		/* Oi2 Flags:  srv must size, clt must size, has return, has ext, */
			0x6,		/* 6 */
/* 1810 */	0xa,		/* 10 */
			0x47,		/* Ext Flags:  new corr desc, clt corr check, srv corr check, has range on conformance */
/* 1812 */	NdrFcShort( 0x1 ),	/* 1 */
/* 1814 */	NdrFcShort( 0x1 ),	/* 1 */
/* 1816 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1818 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter hDrs */

/* 1820 */	NdrFcShort( 0x8 ),	/* Flags:  in, */
/* 1822 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 1824 */	NdrFcShort( 0x4e ),	/* Type Offset=78 */

	/* Parameter dwInVersion */

/* 1826 */	NdrFcShort( 0x48 ),	/* Flags:  in, base type, */
/* 1828 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 1830 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgIn */

/* 1832 */	NdrFcShort( 0x10b ),	/* Flags:  must size, must free, in, simple ref, */
/* 1834 */	NdrFcShort( 0x10 ),	/* X64 Stack size/offset = 16 */
/* 1836 */	NdrFcShort( 0x1a92 ),	/* Type Offset=6802 */

	/* Parameter pdwOutVersion */

/* 1838 */	NdrFcShort( 0x2150 ),	/* Flags:  out, base type, simple ref, srv alloc size=8 */
/* 1840 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 1842 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pmsgOut */

/* 1844 */	NdrFcShort( 0x8113 ),	/* Flags:  must size, must free, out, simple ref, srv alloc size=32 */
/* 1846 */	NdrFcShort( 0x20 ),	/* X64 Stack size/offset = 32 */
/* 1848 */	NdrFcShort( 0x1aca ),	/* Type Offset=6858 */

	/* Return value */

/* 1850 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 1852 */	NdrFcShort( 0x28 ),	/* X64 Stack size/offset = 40 */
/* 1854 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure IDL_DSAPrepareScript */

/* 1856 */	0x0,		/* 0 */
			0x48,		/* Old Flags:  */
/* 1858 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1862 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1864 */	NdrFcShort( 0x30 ),	/* X64 Stack size/offset = 48 */
/* 1866 */	0x32,		/* FC_BIND_PRIMITIVE */
			0x0,		/* 0 */
/* 1868 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 1870 */	NdrFcShort( 0x8 ),	/* 8 */
/* 1872 */	NdrFcShort( 0x24 ),	/* 36 */
/* 1874 */	0x47,		/* Oi2 Flags:  srv must size, clt must size, has return, has ext, */
			0x5,		/* 5 */
/* 1876 */	0xa,		/* 10 */
			0x47,		/* Ext Flags:  new corr desc, clt corr check, srv corr check, has range on conformance */
/* 1878 */	NdrFcShort( 0x1 ),	/* 1 */
/* 1880 */	NdrFcShort( 0x1 ),	/* 1 */
/* 1882 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1884 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter hRpc */

/* 1886 */	NdrFcShort( 0x48 ),	/* Flags:  in, base type, */
/* 1888 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 1890 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter dwInVersion */

/* 1892 */	NdrFcShort( 0x10b ),	/* Flags:  must size, must free, in, simple ref, */
/* 1894 */	NdrFcShort( 0x10 ),	/* X64 Stack size/offset = 16 */
/* 1896 */	NdrFcShort( 0x1b1e ),	/* Type Offset=6942 */

	/* Parameter pmsgIn */

/* 1898 */	NdrFcShort( 0x2150 ),	/* Flags:  out, base type, simple ref, srv alloc size=8 */
/* 1900 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 1902 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pdwOutVersion */

/* 1904 */	NdrFcShort( 0x113 ),	/* Flags:  must size, must free, out, simple ref, */
/* 1906 */	NdrFcShort( 0x20 ),	/* X64 Stack size/offset = 32 */
/* 1908 */	NdrFcShort( 0x1b42 ),	/* Type Offset=6978 */

	/* Parameter pmsgOut */

/* 1910 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 1912 */	NdrFcShort( 0x28 ),	/* X64 Stack size/offset = 40 */
/* 1914 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Procedure IDL_DSAExecuteScript */


	/* Return value */

/* 1916 */	0x0,		/* 0 */
			0x48,		/* Old Flags:  */
/* 1918 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1922 */	NdrFcShort( 0x1 ),	/* 1 */
/* 1924 */	NdrFcShort( 0x30 ),	/* X64 Stack size/offset = 48 */
/* 1926 */	0x32,		/* FC_BIND_PRIMITIVE */
			0x0,		/* 0 */
/* 1928 */	NdrFcShort( 0x0 ),	/* X64 Stack size/offset = 0 */
/* 1930 */	NdrFcShort( 0x8 ),	/* 8 */
/* 1932 */	NdrFcShort( 0x24 ),	/* 36 */
/* 1934 */	0x47,		/* Oi2 Flags:  srv must size, clt must size, has return, has ext, */
			0x5,		/* 5 */
/* 1936 */	0xa,		/* 10 */
			0x47,		/* Ext Flags:  new corr desc, clt corr check, srv corr check, has range on conformance */
/* 1938 */	NdrFcShort( 0x1 ),	/* 1 */
/* 1940 */	NdrFcShort( 0x1 ),	/* 1 */
/* 1942 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1944 */	NdrFcShort( 0x0 ),	/* 0 */

	/* Parameter hRpc */

/* 1946 */	NdrFcShort( 0x48 ),	/* Flags:  in, base type, */
/* 1948 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 1950 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter dwInVersion */

/* 1952 */	NdrFcShort( 0x10b ),	/* Flags:  must size, must free, in, simple ref, */
/* 1954 */	NdrFcShort( 0x10 ),	/* X64 Stack size/offset = 16 */
/* 1956 */	NdrFcShort( 0x1bce ),	/* Type Offset=7118 */

	/* Parameter pmsgIn */

/* 1958 */	NdrFcShort( 0x2150 ),	/* Flags:  out, base type, simple ref, srv alloc size=8 */
/* 1960 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 1962 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter pdwOutVersion */

/* 1964 */	NdrFcShort( 0x4113 ),	/* Flags:  must size, must free, out, simple ref, srv alloc size=16 */
/* 1966 */	NdrFcShort( 0x20 ),	/* X64 Stack size/offset = 32 */
/* 1968 */	NdrFcShort( 0x1c18 ),	/* Type Offset=7192 */

	/* Parameter pmsgOut */

/* 1970 */	NdrFcShort( 0x70 ),	/* Flags:  out, return, base type, */
/* 1972 */	NdrFcShort( 0x28 ),	/* X64 Stack size/offset = 40 */
/* 1974 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

			0x0
        }
    };

static const drsr_MIDL_TYPE_FORMAT_STRING drsr__MIDL_TypeFormatString =
    {
        0,
        {
			NdrFcShort( 0x0 ),	/* 0 */
/*  2 */	
			0x12, 0x0,	/* FC_UP */
/*  4 */	NdrFcShort( 0x8 ),	/* Offset= 8 (12) */
/*  6 */	
			0x1d,		/* FC_SMFARRAY */
			0x0,		/* 0 */
/*  8 */	NdrFcShort( 0x8 ),	/* 8 */
/* 10 */	0x1,		/* FC_BYTE */
			0x5b,		/* FC_END */
/* 12 */	
			0x15,		/* FC_STRUCT */
			0x3,		/* 3 */
/* 14 */	NdrFcShort( 0x10 ),	/* 16 */
/* 16 */	0x8,		/* FC_LONG */
			0x6,		/* FC_SHORT */
/* 18 */	0x6,		/* FC_SHORT */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 20 */	0x0,		/* 0 */
			NdrFcShort( 0xfff1 ),	/* Offset= -15 (6) */
			0x5b,		/* FC_END */
/* 24 */	
			0x12, 0x0,	/* FC_UP */
/* 26 */	NdrFcShort( 0x18 ),	/* Offset= 24 (50) */
/* 28 */	
			0x1b,		/* FC_CARRAY */
			0x0,		/* 0 */
/* 30 */	NdrFcShort( 0x1 ),	/* 1 */
/* 32 */	0x9,		/* Corr desc: FC_ULONG */
			0x0,		/*  */
/* 34 */	NdrFcShort( 0xfffc ),	/* -4 */
/* 36 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 38 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 40 */	NdrFcLong( 0x1 ),	/* 1 */
/* 44 */	NdrFcLong( 0x2710 ),	/* 10000 */
/* 48 */	0x1,		/* FC_BYTE */
			0x5b,		/* FC_END */
/* 50 */	
			0x17,		/* FC_CSTRUCT */
			0x3,		/* 3 */
/* 52 */	NdrFcShort( 0x4 ),	/* 4 */
/* 54 */	NdrFcShort( 0xffe6 ),	/* Offset= -26 (28) */
/* 56 */	0x8,		/* FC_LONG */
			0x5b,		/* FC_END */
/* 58 */	
			0x11, 0x14,	/* FC_RP [alloced_on_stack] [pointer_deref] */
/* 60 */	NdrFcShort( 0xffdc ),	/* Offset= -36 (24) */
/* 62 */	
			0x11, 0x4,	/* FC_RP [alloced_on_stack] */
/* 64 */	NdrFcShort( 0x2 ),	/* Offset= 2 (66) */
/* 66 */	0x30,		/* FC_BIND_CONTEXT */
			0xa0,		/* Ctxt flags:  via ptr, out, */
/* 68 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 70 */	
			0x11, 0x4,	/* FC_RP [alloced_on_stack] */
/* 72 */	NdrFcShort( 0x2 ),	/* Offset= 2 (74) */
/* 74 */	0x30,		/* FC_BIND_CONTEXT */
			0xe1,		/* Ctxt flags:  via ptr, in, out, can't be null */
/* 76 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 78 */	0x30,		/* FC_BIND_CONTEXT */
			0x41,		/* Ctxt flags:  in, can't be null */
/* 80 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 82 */	
			0x11, 0x0,	/* FC_RP */
/* 84 */	NdrFcShort( 0x2 ),	/* Offset= 2 (86) */
/* 86 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 88 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x0,		/*  */
/* 90 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 92 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 94 */	0x0 , 
			0x0,		/* 0 */
/* 96 */	NdrFcLong( 0x0 ),	/* 0 */
/* 100 */	NdrFcLong( 0x0 ),	/* 0 */
/* 104 */	NdrFcShort( 0x2 ),	/* Offset= 2 (106) */
/* 106 */	NdrFcShort( 0x28 ),	/* 40 */
/* 108 */	NdrFcShort( 0x1 ),	/* 1 */
/* 110 */	NdrFcLong( 0x1 ),	/* 1 */
/* 114 */	NdrFcShort( 0x3c ),	/* Offset= 60 (174) */
/* 116 */	NdrFcShort( 0xffff ),	/* Offset= -1 (115) */
/* 118 */	
			0x1d,		/* FC_SMFARRAY */
			0x0,		/* 0 */
/* 120 */	NdrFcShort( 0x1c ),	/* 28 */
/* 122 */	0x2,		/* FC_CHAR */
			0x5b,		/* FC_END */
/* 124 */	
			0x15,		/* FC_STRUCT */
			0x0,		/* 0 */
/* 126 */	NdrFcShort( 0x1c ),	/* 28 */
/* 128 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 130 */	NdrFcShort( 0xfff4 ),	/* Offset= -12 (118) */
/* 132 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 134 */	
			0x1b,		/* FC_CARRAY */
			0x1,		/* 1 */
/* 136 */	NdrFcShort( 0x2 ),	/* 2 */
/* 138 */	0x9,		/* Corr desc: FC_ULONG */
			0x57,		/* FC_ADD_1 */
/* 140 */	NdrFcShort( 0xfffc ),	/* -4 */
/* 142 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 144 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 146 */	NdrFcLong( 0x0 ),	/* 0 */
/* 150 */	NdrFcLong( 0xa00001 ),	/* 10485761 */
/* 154 */	0x5,		/* FC_WCHAR */
			0x5b,		/* FC_END */
/* 156 */	
			0x17,		/* FC_CSTRUCT */
			0x3,		/* 3 */
/* 158 */	NdrFcShort( 0x38 ),	/* 56 */
/* 160 */	NdrFcShort( 0xffe6 ),	/* Offset= -26 (134) */
/* 162 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 164 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 166 */	NdrFcShort( 0xff66 ),	/* Offset= -154 (12) */
/* 168 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 170 */	NdrFcShort( 0xffd2 ),	/* Offset= -46 (124) */
/* 172 */	0x8,		/* FC_LONG */
			0x5b,		/* FC_END */
/* 174 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 176 */	NdrFcShort( 0x28 ),	/* 40 */
/* 178 */	NdrFcShort( 0x0 ),	/* 0 */
/* 180 */	NdrFcShort( 0xc ),	/* Offset= 12 (192) */
/* 182 */	0x36,		/* FC_POINTER */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 184 */	0x0,		/* 0 */
			NdrFcShort( 0xff53 ),	/* Offset= -173 (12) */
			0x36,		/* FC_POINTER */
/* 188 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 190 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 192 */	
			0x11, 0x0,	/* FC_RP */
/* 194 */	NdrFcShort( 0xffda ),	/* Offset= -38 (156) */
/* 196 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 198 */	
			0x22,		/* FC_C_CSTRING */
			0x5c,		/* FC_PAD */
/* 200 */	
			0x11, 0x0,	/* FC_RP */
/* 202 */	NdrFcShort( 0x2 ),	/* Offset= 2 (204) */
/* 204 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 206 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x0,		/*  */
/* 208 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 210 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 212 */	0x0 , 
			0x0,		/* 0 */
/* 214 */	NdrFcLong( 0x0 ),	/* 0 */
/* 218 */	NdrFcLong( 0x0 ),	/* 0 */
/* 222 */	NdrFcShort( 0x2 ),	/* Offset= 2 (224) */
/* 224 */	NdrFcShort( 0xa8 ),	/* 168 */
/* 226 */	NdrFcShort( 0x5 ),	/* 5 */
/* 228 */	NdrFcLong( 0x4 ),	/* 4 */
/* 232 */	NdrFcShort( 0x12e ),	/* Offset= 302 (534) */
/* 234 */	NdrFcLong( 0x5 ),	/* 5 */
/* 238 */	NdrFcShort( 0x144 ),	/* Offset= 324 (562) */
/* 240 */	NdrFcLong( 0x7 ),	/* 7 */
/* 244 */	NdrFcShort( 0x166 ),	/* Offset= 358 (602) */
/* 246 */	NdrFcLong( 0x8 ),	/* 8 */
/* 250 */	NdrFcShort( 0x184 ),	/* Offset= 388 (638) */
/* 252 */	NdrFcLong( 0xa ),	/* 10 */
/* 256 */	NdrFcShort( 0x1b4 ),	/* Offset= 436 (692) */
/* 258 */	NdrFcShort( 0xffff ),	/* Offset= -1 (257) */
/* 260 */	
			0x15,		/* FC_STRUCT */
			0x7,		/* 7 */
/* 262 */	NdrFcShort( 0x18 ),	/* 24 */
/* 264 */	0xb,		/* FC_HYPER */
			0xb,		/* FC_HYPER */
/* 266 */	0xb,		/* FC_HYPER */
			0x5b,		/* FC_END */
/* 268 */	
			0x1b,		/* FC_CARRAY */
			0x0,		/* 0 */
/* 270 */	NdrFcShort( 0x1 ),	/* 1 */
/* 272 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 274 */	NdrFcShort( 0x0 ),	/* 0 */
/* 276 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 278 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 280 */	NdrFcLong( 0x0 ),	/* 0 */
/* 284 */	NdrFcLong( 0x2710 ),	/* 10000 */
/* 288 */	0x1,		/* FC_BYTE */
			0x5b,		/* FC_END */
/* 290 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 292 */	NdrFcShort( 0x10 ),	/* 16 */
/* 294 */	NdrFcShort( 0x0 ),	/* 0 */
/* 296 */	NdrFcShort( 0x6 ),	/* Offset= 6 (302) */
/* 298 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 300 */	0x36,		/* FC_POINTER */
			0x5b,		/* FC_END */
/* 302 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 304 */	NdrFcShort( 0xffdc ),	/* Offset= -36 (268) */
/* 306 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 308 */	NdrFcShort( 0x18 ),	/* 24 */
/* 310 */	NdrFcShort( 0x0 ),	/* 0 */
/* 312 */	NdrFcShort( 0x0 ),	/* Offset= 0 (312) */
/* 314 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 316 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 318 */	NdrFcShort( 0xffe4 ),	/* Offset= -28 (290) */
/* 320 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 322 */	
			0x21,		/* FC_BOGUS_ARRAY */
			0x3,		/* 3 */
/* 324 */	NdrFcShort( 0x0 ),	/* 0 */
/* 326 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 328 */	NdrFcShort( 0x0 ),	/* 0 */
/* 330 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 332 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 334 */	NdrFcLong( 0x0 ),	/* 0 */
/* 338 */	NdrFcLong( 0x100000 ),	/* 1048576 */
/* 342 */	NdrFcLong( 0xffffffff ),	/* -1 */
/* 346 */	NdrFcShort( 0x0 ),	/* Corr flags:  */
/* 348 */	0x0 , 
			0x0,		/* 0 */
/* 350 */	NdrFcLong( 0x0 ),	/* 0 */
/* 354 */	NdrFcLong( 0x0 ),	/* 0 */
/* 358 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 360 */	NdrFcShort( 0xffca ),	/* Offset= -54 (306) */
/* 362 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 364 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 366 */	NdrFcShort( 0x10 ),	/* 16 */
/* 368 */	NdrFcShort( 0x0 ),	/* 0 */
/* 370 */	NdrFcShort( 0x6 ),	/* Offset= 6 (376) */
/* 372 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 374 */	0x36,		/* FC_POINTER */
			0x5b,		/* FC_END */
/* 376 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 378 */	NdrFcShort( 0xffc8 ),	/* Offset= -56 (322) */
/* 380 */	
			0x15,		/* FC_STRUCT */
			0x7,		/* 7 */
/* 382 */	NdrFcShort( 0x18 ),	/* 24 */
/* 384 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 386 */	NdrFcShort( 0xfe8a ),	/* Offset= -374 (12) */
/* 388 */	0xb,		/* FC_HYPER */
			0x5b,		/* FC_END */
/* 390 */	
			0x1b,		/* FC_CARRAY */
			0x7,		/* 7 */
/* 392 */	NdrFcShort( 0x18 ),	/* 24 */
/* 394 */	0x9,		/* Corr desc: FC_ULONG */
			0x0,		/*  */
/* 396 */	NdrFcShort( 0xfff8 ),	/* -8 */
/* 398 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 400 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 402 */	NdrFcLong( 0x0 ),	/* 0 */
/* 406 */	NdrFcLong( 0x100000 ),	/* 1048576 */
/* 410 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 412 */	NdrFcShort( 0xffe0 ),	/* Offset= -32 (380) */
/* 414 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 416 */	
			0x17,		/* FC_CSTRUCT */
			0x7,		/* 7 */
/* 418 */	NdrFcShort( 0x10 ),	/* 16 */
/* 420 */	NdrFcShort( 0xffe2 ),	/* Offset= -30 (390) */
/* 422 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 424 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 426 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 428 */	
			0x1b,		/* FC_CARRAY */
			0x3,		/* 3 */
/* 430 */	NdrFcShort( 0x4 ),	/* 4 */
/* 432 */	0x9,		/* Corr desc: FC_ULONG */
			0x0,		/*  */
/* 434 */	NdrFcShort( 0xfffc ),	/* -4 */
/* 436 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 438 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 440 */	NdrFcLong( 0x1 ),	/* 1 */
/* 444 */	NdrFcLong( 0x100000 ),	/* 1048576 */
/* 448 */	0x8,		/* FC_LONG */
			0x5b,		/* FC_END */
/* 450 */	
			0x17,		/* FC_CSTRUCT */
			0x3,		/* 3 */
/* 452 */	NdrFcShort( 0xc ),	/* 12 */
/* 454 */	NdrFcShort( 0xffe6 ),	/* Offset= -26 (428) */
/* 456 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 458 */	0x8,		/* FC_LONG */
			0x5b,		/* FC_END */
/* 460 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x7,		/* 7 */
/* 462 */	NdrFcShort( 0x70 ),	/* 112 */
/* 464 */	NdrFcShort( 0x0 ),	/* 0 */
/* 466 */	NdrFcShort( 0x1a ),	/* Offset= 26 (492) */
/* 468 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 470 */	NdrFcShort( 0xfe36 ),	/* Offset= -458 (12) */
/* 472 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 474 */	NdrFcShort( 0xfe32 ),	/* Offset= -462 (12) */
/* 476 */	0x36,		/* FC_POINTER */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 478 */	0x0,		/* 0 */
			NdrFcShort( 0xff25 ),	/* Offset= -219 (260) */
			0x36,		/* FC_POINTER */
/* 482 */	0x36,		/* FC_POINTER */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 484 */	0x0,		/* 0 */
			NdrFcShort( 0xff87 ),	/* Offset= -121 (364) */
			0x8,		/* FC_LONG */
/* 488 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 490 */	0x8,		/* FC_LONG */
			0x5b,		/* FC_END */
/* 492 */	
			0x11, 0x0,	/* FC_RP */
/* 494 */	NdrFcShort( 0xfeae ),	/* Offset= -338 (156) */
/* 496 */	
			0x12, 0x0,	/* FC_UP */
/* 498 */	NdrFcShort( 0xffae ),	/* Offset= -82 (416) */
/* 500 */	
			0x12, 0x0,	/* FC_UP */
/* 502 */	NdrFcShort( 0xffcc ),	/* Offset= -52 (450) */
/* 504 */	
			0x1b,		/* FC_CARRAY */
			0x0,		/* 0 */
/* 506 */	NdrFcShort( 0x1 ),	/* 1 */
/* 508 */	0x9,		/* Corr desc: FC_ULONG */
			0x0,		/*  */
/* 510 */	NdrFcShort( 0xfffc ),	/* -4 */
/* 512 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 514 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 516 */	NdrFcLong( 0x1 ),	/* 1 */
/* 520 */	NdrFcLong( 0x100 ),	/* 256 */
/* 524 */	0x2,		/* FC_CHAR */
			0x5b,		/* FC_END */
/* 526 */	
			0x17,		/* FC_CSTRUCT */
			0x3,		/* 3 */
/* 528 */	NdrFcShort( 0x4 ),	/* 4 */
/* 530 */	NdrFcShort( 0xffe6 ),	/* Offset= -26 (504) */
/* 532 */	0x8,		/* FC_LONG */
			0x5b,		/* FC_END */
/* 534 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x7,		/* 7 */
/* 536 */	NdrFcShort( 0x88 ),	/* 136 */
/* 538 */	NdrFcShort( 0x0 ),	/* 0 */
/* 540 */	NdrFcShort( 0xc ),	/* Offset= 12 (552) */
/* 542 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 544 */	NdrFcShort( 0xfdec ),	/* Offset= -532 (12) */
/* 546 */	0x36,		/* FC_POINTER */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 548 */	0x0,		/* 0 */
			NdrFcShort( 0xffa7 ),	/* Offset= -89 (460) */
			0x5b,		/* FC_END */
/* 552 */	
			0x11, 0x0,	/* FC_RP */
/* 554 */	NdrFcShort( 0xffe4 ),	/* Offset= -28 (526) */
/* 556 */	
			0x15,		/* FC_STRUCT */
			0x7,		/* 7 */
/* 558 */	NdrFcShort( 0x8 ),	/* 8 */
/* 560 */	0xb,		/* FC_HYPER */
			0x5b,		/* FC_END */
/* 562 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x7,		/* 7 */
/* 564 */	NdrFcShort( 0x60 ),	/* 96 */
/* 566 */	NdrFcShort( 0x0 ),	/* 0 */
/* 568 */	NdrFcShort( 0x1a ),	/* Offset= 26 (594) */
/* 570 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 572 */	NdrFcShort( 0xfdd0 ),	/* Offset= -560 (12) */
/* 574 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 576 */	NdrFcShort( 0xfdcc ),	/* Offset= -564 (12) */
/* 578 */	0x36,		/* FC_POINTER */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 580 */	0x0,		/* 0 */
			NdrFcShort( 0xfebf ),	/* Offset= -321 (260) */
			0x36,		/* FC_POINTER */
/* 584 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 586 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 588 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 590 */	NdrFcShort( 0xffde ),	/* Offset= -34 (556) */
/* 592 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 594 */	
			0x11, 0x0,	/* FC_RP */
/* 596 */	NdrFcShort( 0xfe48 ),	/* Offset= -440 (156) */
/* 598 */	
			0x12, 0x0,	/* FC_UP */
/* 600 */	NdrFcShort( 0xff48 ),	/* Offset= -184 (416) */
/* 602 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x7,		/* 7 */
/* 604 */	NdrFcShort( 0xa8 ),	/* 168 */
/* 606 */	NdrFcShort( 0x0 ),	/* 0 */
/* 608 */	NdrFcShort( 0x12 ),	/* Offset= 18 (626) */
/* 610 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 612 */	NdrFcShort( 0xfda8 ),	/* Offset= -600 (12) */
/* 614 */	0x36,		/* FC_POINTER */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 616 */	0x0,		/* 0 */
			NdrFcShort( 0xff63 ),	/* Offset= -157 (460) */
			0x36,		/* FC_POINTER */
/* 620 */	0x36,		/* FC_POINTER */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 622 */	0x0,		/* 0 */
			NdrFcShort( 0xfefd ),	/* Offset= -259 (364) */
			0x5b,		/* FC_END */
/* 626 */	
			0x11, 0x0,	/* FC_RP */
/* 628 */	NdrFcShort( 0xff9a ),	/* Offset= -102 (526) */
/* 630 */	
			0x12, 0x0,	/* FC_UP */
/* 632 */	NdrFcShort( 0xff4a ),	/* Offset= -182 (450) */
/* 634 */	
			0x12, 0x0,	/* FC_UP */
/* 636 */	NdrFcShort( 0xff46 ),	/* Offset= -186 (450) */
/* 638 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x7,		/* 7 */
/* 640 */	NdrFcShort( 0x80 ),	/* 128 */
/* 642 */	NdrFcShort( 0x0 ),	/* 0 */
/* 644 */	NdrFcShort( 0x20 ),	/* Offset= 32 (676) */
/* 646 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 648 */	NdrFcShort( 0xfd84 ),	/* Offset= -636 (12) */
/* 650 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 652 */	NdrFcShort( 0xfd80 ),	/* Offset= -640 (12) */
/* 654 */	0x36,		/* FC_POINTER */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 656 */	0x0,		/* 0 */
			NdrFcShort( 0xfe73 ),	/* Offset= -397 (260) */
			0x36,		/* FC_POINTER */
/* 660 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 662 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 664 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 666 */	NdrFcShort( 0xff92 ),	/* Offset= -110 (556) */
/* 668 */	0x36,		/* FC_POINTER */
			0x36,		/* FC_POINTER */
/* 670 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 672 */	NdrFcShort( 0xfecc ),	/* Offset= -308 (364) */
/* 674 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 676 */	
			0x11, 0x0,	/* FC_RP */
/* 678 */	NdrFcShort( 0xfdf6 ),	/* Offset= -522 (156) */
/* 680 */	
			0x12, 0x0,	/* FC_UP */
/* 682 */	NdrFcShort( 0xfef6 ),	/* Offset= -266 (416) */
/* 684 */	
			0x12, 0x0,	/* FC_UP */
/* 686 */	NdrFcShort( 0xff14 ),	/* Offset= -236 (450) */
/* 688 */	
			0x12, 0x0,	/* FC_UP */
/* 690 */	NdrFcShort( 0xff10 ),	/* Offset= -240 (450) */
/* 692 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x7,		/* 7 */
/* 694 */	NdrFcShort( 0x88 ),	/* 136 */
/* 696 */	NdrFcShort( 0x0 ),	/* 0 */
/* 698 */	NdrFcShort( 0x22 ),	/* Offset= 34 (732) */
/* 700 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 702 */	NdrFcShort( 0xfd4e ),	/* Offset= -690 (12) */
/* 704 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 706 */	NdrFcShort( 0xfd4a ),	/* Offset= -694 (12) */
/* 708 */	0x36,		/* FC_POINTER */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 710 */	0x0,		/* 0 */
			NdrFcShort( 0xfe3d ),	/* Offset= -451 (260) */
			0x36,		/* FC_POINTER */
/* 714 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 716 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 718 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 720 */	NdrFcShort( 0xff5c ),	/* Offset= -164 (556) */
/* 722 */	0x36,		/* FC_POINTER */
			0x36,		/* FC_POINTER */
/* 724 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 726 */	NdrFcShort( 0xfe96 ),	/* Offset= -362 (364) */
/* 728 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 730 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 732 */	
			0x11, 0x0,	/* FC_RP */
/* 734 */	NdrFcShort( 0xfdbe ),	/* Offset= -578 (156) */
/* 736 */	
			0x12, 0x0,	/* FC_UP */
/* 738 */	NdrFcShort( 0xfebe ),	/* Offset= -322 (416) */
/* 740 */	
			0x12, 0x0,	/* FC_UP */
/* 742 */	NdrFcShort( 0xfedc ),	/* Offset= -292 (450) */
/* 744 */	
			0x12, 0x0,	/* FC_UP */
/* 746 */	NdrFcShort( 0xfed8 ),	/* Offset= -296 (450) */
/* 748 */	
			0x11, 0xc,	/* FC_RP [alloced_on_stack] [simple_pointer] */
/* 750 */	0x8,		/* FC_LONG */
			0x5c,		/* FC_PAD */
/* 752 */	
			0x11, 0x0,	/* FC_RP */
/* 754 */	NdrFcShort( 0x2 ),	/* Offset= 2 (756) */
/* 756 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 758 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x54,		/* FC_DEREFERENCE */
/* 760 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 762 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 764 */	0x0 , 
			0x0,		/* 0 */
/* 766 */	NdrFcLong( 0x0 ),	/* 0 */
/* 770 */	NdrFcLong( 0x0 ),	/* 0 */
/* 774 */	NdrFcShort( 0x2 ),	/* Offset= 2 (776) */
/* 776 */	NdrFcShort( 0xa8 ),	/* 168 */
/* 778 */	NdrFcShort( 0x4 ),	/* 4 */
/* 780 */	NdrFcLong( 0x1 ),	/* 1 */
/* 784 */	NdrFcShort( 0x13a ),	/* Offset= 314 (1098) */
/* 786 */	NdrFcLong( 0x2 ),	/* 2 */
/* 790 */	NdrFcShort( 0x18c ),	/* Offset= 396 (1186) */
/* 792 */	NdrFcLong( 0x6 ),	/* 6 */
/* 796 */	NdrFcShort( 0x218 ),	/* Offset= 536 (1332) */
/* 798 */	NdrFcLong( 0x7 ),	/* 7 */
/* 802 */	NdrFcShort( 0x24e ),	/* Offset= 590 (1392) */
/* 804 */	NdrFcShort( 0xffff ),	/* Offset= -1 (803) */
/* 806 */	
			0x1b,		/* FC_CARRAY */
			0x0,		/* 0 */
/* 808 */	NdrFcShort( 0x1 ),	/* 1 */
/* 810 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 812 */	NdrFcShort( 0x0 ),	/* 0 */
/* 814 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 816 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 818 */	NdrFcLong( 0x0 ),	/* 0 */
/* 822 */	NdrFcLong( 0x1900000 ),	/* 26214400 */
/* 826 */	0x2,		/* FC_CHAR */
			0x5b,		/* FC_END */
/* 828 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 830 */	NdrFcShort( 0x10 ),	/* 16 */
/* 832 */	NdrFcShort( 0x0 ),	/* 0 */
/* 834 */	NdrFcShort( 0x6 ),	/* Offset= 6 (840) */
/* 836 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 838 */	0x36,		/* FC_POINTER */
			0x5b,		/* FC_END */
/* 840 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 842 */	NdrFcShort( 0xffdc ),	/* Offset= -36 (806) */
/* 844 */	
			0x21,		/* FC_BOGUS_ARRAY */
			0x3,		/* 3 */
/* 846 */	NdrFcShort( 0x0 ),	/* 0 */
/* 848 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 850 */	NdrFcShort( 0x0 ),	/* 0 */
/* 852 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 854 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 856 */	NdrFcLong( 0x0 ),	/* 0 */
/* 860 */	NdrFcLong( 0xa00000 ),	/* 10485760 */
/* 864 */	NdrFcLong( 0xffffffff ),	/* -1 */
/* 868 */	NdrFcShort( 0x0 ),	/* Corr flags:  */
/* 870 */	0x0 , 
			0x0,		/* 0 */
/* 872 */	NdrFcLong( 0x0 ),	/* 0 */
/* 876 */	NdrFcLong( 0x0 ),	/* 0 */
/* 880 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 882 */	NdrFcShort( 0xffca ),	/* Offset= -54 (828) */
/* 884 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 886 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 888 */	NdrFcShort( 0x10 ),	/* 16 */
/* 890 */	NdrFcShort( 0x0 ),	/* 0 */
/* 892 */	NdrFcShort( 0x6 ),	/* Offset= 6 (898) */
/* 894 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 896 */	0x36,		/* FC_POINTER */
			0x5b,		/* FC_END */
/* 898 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 900 */	NdrFcShort( 0xffc8 ),	/* Offset= -56 (844) */
/* 902 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 904 */	NdrFcShort( 0x18 ),	/* 24 */
/* 906 */	NdrFcShort( 0x0 ),	/* 0 */
/* 908 */	NdrFcShort( 0x0 ),	/* Offset= 0 (908) */
/* 910 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 912 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 914 */	NdrFcShort( 0xffe4 ),	/* Offset= -28 (886) */
/* 916 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 918 */	
			0x21,		/* FC_BOGUS_ARRAY */
			0x3,		/* 3 */
/* 920 */	NdrFcShort( 0x0 ),	/* 0 */
/* 922 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 924 */	NdrFcShort( 0x0 ),	/* 0 */
/* 926 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 928 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 930 */	NdrFcLong( 0x0 ),	/* 0 */
/* 934 */	NdrFcLong( 0x100000 ),	/* 1048576 */
/* 938 */	NdrFcLong( 0xffffffff ),	/* -1 */
/* 942 */	NdrFcShort( 0x0 ),	/* Corr flags:  */
/* 944 */	0x0 , 
			0x0,		/* 0 */
/* 946 */	NdrFcLong( 0x0 ),	/* 0 */
/* 950 */	NdrFcLong( 0x0 ),	/* 0 */
/* 954 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 956 */	NdrFcShort( 0xffca ),	/* Offset= -54 (902) */
/* 958 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 960 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 962 */	NdrFcShort( 0x10 ),	/* 16 */
/* 964 */	NdrFcShort( 0x0 ),	/* 0 */
/* 966 */	NdrFcShort( 0x6 ),	/* Offset= 6 (972) */
/* 968 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 970 */	0x36,		/* FC_POINTER */
			0x5b,		/* FC_END */
/* 972 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 974 */	NdrFcShort( 0xffc8 ),	/* Offset= -56 (918) */
/* 976 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 978 */	NdrFcShort( 0x20 ),	/* 32 */
/* 980 */	NdrFcShort( 0x0 ),	/* 0 */
/* 982 */	NdrFcShort( 0xa ),	/* Offset= 10 (992) */
/* 984 */	0x36,		/* FC_POINTER */
			0x8,		/* FC_LONG */
/* 986 */	0x40,		/* FC_STRUCTPAD4 */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 988 */	0x0,		/* 0 */
			NdrFcShort( 0xffe3 ),	/* Offset= -29 (960) */
			0x5b,		/* FC_END */
/* 992 */	
			0x12, 0x0,	/* FC_UP */
/* 994 */	NdrFcShort( 0xfcba ),	/* Offset= -838 (156) */
/* 996 */	0xb1,		/* FC_FORCED_BOGUS_STRUCT */
			0x7,		/* 7 */
/* 998 */	NdrFcShort( 0x28 ),	/* 40 */
/* 1000 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1002 */	NdrFcShort( 0x0 ),	/* Offset= 0 (1002) */
/* 1004 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 1006 */	0xb,		/* FC_HYPER */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 1008 */	0x0,		/* 0 */
			NdrFcShort( 0xfc1b ),	/* Offset= -997 (12) */
			0xb,		/* FC_HYPER */
/* 1012 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 1014 */	
			0x21,		/* FC_BOGUS_ARRAY */
			0x7,		/* 7 */
/* 1016 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1018 */	0x9,		/* Corr desc: FC_ULONG */
			0x0,		/*  */
/* 1020 */	NdrFcShort( 0xfff8 ),	/* -8 */
/* 1022 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 1024 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 1026 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1030 */	NdrFcLong( 0x100000 ),	/* 1048576 */
/* 1034 */	NdrFcLong( 0xffffffff ),	/* -1 */
/* 1038 */	NdrFcShort( 0x0 ),	/* Corr flags:  */
/* 1040 */	0x0 , 
			0x0,		/* 0 */
/* 1042 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1046 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1050 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 1052 */	NdrFcShort( 0xffc8 ),	/* Offset= -56 (996) */
/* 1054 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 1056 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x7,		/* 7 */
/* 1058 */	NdrFcShort( 0x8 ),	/* 8 */
/* 1060 */	NdrFcShort( 0xffd2 ),	/* Offset= -46 (1014) */
/* 1062 */	NdrFcShort( 0x0 ),	/* Offset= 0 (1062) */
/* 1064 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 1066 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 1068 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 1070 */	NdrFcShort( 0x40 ),	/* 64 */
/* 1072 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1074 */	NdrFcShort( 0xc ),	/* Offset= 12 (1086) */
/* 1076 */	0x36,		/* FC_POINTER */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 1078 */	0x0,		/* 0 */
			NdrFcShort( 0xff99 ),	/* Offset= -103 (976) */
			0x8,		/* FC_LONG */
/* 1082 */	0x40,		/* FC_STRUCTPAD4 */
			0x36,		/* FC_POINTER */
/* 1084 */	0x36,		/* FC_POINTER */
			0x5b,		/* FC_END */
/* 1086 */	
			0x12, 0x0,	/* FC_UP */
/* 1088 */	NdrFcShort( 0xffec ),	/* Offset= -20 (1068) */
/* 1090 */	
			0x12, 0x0,	/* FC_UP */
/* 1092 */	NdrFcShort( 0xfbc8 ),	/* Offset= -1080 (12) */
/* 1094 */	
			0x12, 0x0,	/* FC_UP */
/* 1096 */	NdrFcShort( 0xffd8 ),	/* Offset= -40 (1056) */
/* 1098 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x7,		/* 7 */
/* 1100 */	NdrFcShort( 0x90 ),	/* 144 */
/* 1102 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1104 */	NdrFcShort( 0x20 ),	/* Offset= 32 (1136) */
/* 1106 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 1108 */	NdrFcShort( 0xfbb8 ),	/* Offset= -1096 (12) */
/* 1110 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 1112 */	NdrFcShort( 0xfbb4 ),	/* Offset= -1100 (12) */
/* 1114 */	0x36,		/* FC_POINTER */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 1116 */	0x0,		/* 0 */
			NdrFcShort( 0xfca7 ),	/* Offset= -857 (260) */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 1120 */	0x0,		/* 0 */
			NdrFcShort( 0xfca3 ),	/* Offset= -861 (260) */
			0x36,		/* FC_POINTER */
/* 1124 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 1126 */	NdrFcShort( 0xfd06 ),	/* Offset= -762 (364) */
/* 1128 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 1130 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 1132 */	0x36,		/* FC_POINTER */
			0x8,		/* FC_LONG */
/* 1134 */	0x40,		/* FC_STRUCTPAD4 */
			0x5b,		/* FC_END */
/* 1136 */	
			0x12, 0x0,	/* FC_UP */
/* 1138 */	NdrFcShort( 0xfc2a ),	/* Offset= -982 (156) */
/* 1140 */	
			0x12, 0x0,	/* FC_UP */
/* 1142 */	NdrFcShort( 0xfd2a ),	/* Offset= -726 (416) */
/* 1144 */	
			0x12, 0x0,	/* FC_UP */
/* 1146 */	NdrFcShort( 0xffb2 ),	/* Offset= -78 (1068) */
/* 1148 */	
			0x1b,		/* FC_CARRAY */
			0x0,		/* 0 */
/* 1150 */	NdrFcShort( 0x1 ),	/* 1 */
/* 1152 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 1154 */	NdrFcShort( 0x4 ),	/* 4 */
/* 1156 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 1158 */	0x0 , 
			0x0,		/* 0 */
/* 1160 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1164 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1168 */	0x1,		/* FC_BYTE */
			0x5b,		/* FC_END */
/* 1170 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 1172 */	NdrFcShort( 0x10 ),	/* 16 */
/* 1174 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1176 */	NdrFcShort( 0x6 ),	/* Offset= 6 (1182) */
/* 1178 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 1180 */	0x36,		/* FC_POINTER */
			0x5b,		/* FC_END */
/* 1182 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 1184 */	NdrFcShort( 0xffdc ),	/* Offset= -36 (1148) */
/* 1186 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 1188 */	NdrFcShort( 0x10 ),	/* 16 */
/* 1190 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1192 */	NdrFcShort( 0x0 ),	/* Offset= 0 (1192) */
/* 1194 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 1196 */	NdrFcShort( 0xffe6 ),	/* Offset= -26 (1170) */
/* 1198 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 1200 */	
			0x15,		/* FC_STRUCT */
			0x7,		/* 7 */
/* 1202 */	NdrFcShort( 0x20 ),	/* 32 */
/* 1204 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 1206 */	NdrFcShort( 0xfb56 ),	/* Offset= -1194 (12) */
/* 1208 */	0xb,		/* FC_HYPER */
			0xb,		/* FC_HYPER */
/* 1210 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 1212 */	
			0x1b,		/* FC_CARRAY */
			0x7,		/* 7 */
/* 1214 */	NdrFcShort( 0x20 ),	/* 32 */
/* 1216 */	0x9,		/* Corr desc: FC_ULONG */
			0x0,		/*  */
/* 1218 */	NdrFcShort( 0xfff8 ),	/* -8 */
/* 1220 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 1222 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 1224 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1228 */	NdrFcLong( 0x100000 ),	/* 1048576 */
/* 1232 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 1234 */	NdrFcShort( 0xffde ),	/* Offset= -34 (1200) */
/* 1236 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 1238 */	
			0x17,		/* FC_CSTRUCT */
			0x7,		/* 7 */
/* 1240 */	NdrFcShort( 0x10 ),	/* 16 */
/* 1242 */	NdrFcShort( 0xffe2 ),	/* Offset= -30 (1212) */
/* 1244 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 1246 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 1248 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 1250 */	0xb1,		/* FC_FORCED_BOGUS_STRUCT */
			0x7,		/* 7 */
/* 1252 */	NdrFcShort( 0x30 ),	/* 48 */
/* 1254 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1256 */	NdrFcShort( 0x0 ),	/* Offset= 0 (1256) */
/* 1258 */	0xb,		/* FC_HYPER */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 1260 */	0x0,		/* 0 */
			NdrFcShort( 0xfef7 ),	/* Offset= -265 (996) */
			0x5b,		/* FC_END */
/* 1264 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x7,		/* 7 */
/* 1266 */	NdrFcShort( 0x58 ),	/* 88 */
/* 1268 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1270 */	NdrFcShort( 0x10 ),	/* Offset= 16 (1286) */
/* 1272 */	0x36,		/* FC_POINTER */
			0x8,		/* FC_LONG */
/* 1274 */	0x40,		/* FC_STRUCTPAD4 */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 1276 */	0x0,		/* 0 */
			NdrFcShort( 0xfe3f ),	/* Offset= -449 (828) */
			0x8,		/* FC_LONG */
/* 1280 */	0x40,		/* FC_STRUCTPAD4 */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 1282 */	0x0,		/* 0 */
			NdrFcShort( 0xffdf ),	/* Offset= -33 (1250) */
			0x5b,		/* FC_END */
/* 1286 */	
			0x12, 0x0,	/* FC_UP */
/* 1288 */	NdrFcShort( 0xfb94 ),	/* Offset= -1132 (156) */
/* 1290 */	
			0x21,		/* FC_BOGUS_ARRAY */
			0x7,		/* 7 */
/* 1292 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1294 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 1296 */	NdrFcShort( 0x94 ),	/* 148 */
/* 1298 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 1300 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 1302 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1306 */	NdrFcLong( 0x100000 ),	/* 1048576 */
/* 1310 */	NdrFcLong( 0xffffffff ),	/* -1 */
/* 1314 */	NdrFcShort( 0x0 ),	/* Corr flags:  */
/* 1316 */	0x0 , 
			0x0,		/* 0 */
/* 1318 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1322 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1326 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 1328 */	NdrFcShort( 0xffc0 ),	/* Offset= -64 (1264) */
/* 1330 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 1332 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x7,		/* 7 */
/* 1334 */	NdrFcShort( 0xa8 ),	/* 168 */
/* 1336 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1338 */	NdrFcShort( 0x26 ),	/* Offset= 38 (1376) */
/* 1340 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 1342 */	NdrFcShort( 0xface ),	/* Offset= -1330 (12) */
/* 1344 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 1346 */	NdrFcShort( 0xfaca ),	/* Offset= -1334 (12) */
/* 1348 */	0x36,		/* FC_POINTER */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 1350 */	0x0,		/* 0 */
			NdrFcShort( 0xfbbd ),	/* Offset= -1091 (260) */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 1354 */	0x0,		/* 0 */
			NdrFcShort( 0xfbb9 ),	/* Offset= -1095 (260) */
			0x36,		/* FC_POINTER */
/* 1358 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 1360 */	NdrFcShort( 0xfc1c ),	/* Offset= -996 (364) */
/* 1362 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 1364 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 1366 */	0x36,		/* FC_POINTER */
			0x8,		/* FC_LONG */
/* 1368 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 1370 */	0x8,		/* FC_LONG */
			0x36,		/* FC_POINTER */
/* 1372 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 1374 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 1376 */	
			0x12, 0x0,	/* FC_UP */
/* 1378 */	NdrFcShort( 0xfb3a ),	/* Offset= -1222 (156) */
/* 1380 */	
			0x12, 0x0,	/* FC_UP */
/* 1382 */	NdrFcShort( 0xff70 ),	/* Offset= -144 (1238) */
/* 1384 */	
			0x12, 0x0,	/* FC_UP */
/* 1386 */	NdrFcShort( 0xfec2 ),	/* Offset= -318 (1068) */
/* 1388 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 1390 */	NdrFcShort( 0xff9c ),	/* Offset= -100 (1290) */
/* 1392 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 1394 */	NdrFcShort( 0x18 ),	/* 24 */
/* 1396 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1398 */	NdrFcShort( 0x0 ),	/* Offset= 0 (1398) */
/* 1400 */	0x8,		/* FC_LONG */
			0xd,		/* FC_ENUM16 */
/* 1402 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 1404 */	NdrFcShort( 0xff16 ),	/* Offset= -234 (1170) */
/* 1406 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 1408 */	
			0x11, 0x0,	/* FC_RP */
/* 1410 */	NdrFcShort( 0x2 ),	/* Offset= 2 (1412) */
/* 1412 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 1414 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x0,		/*  */
/* 1416 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 1418 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 1420 */	0x0 , 
			0x0,		/* 0 */
/* 1422 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1426 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1430 */	NdrFcShort( 0x2 ),	/* Offset= 2 (1432) */
/* 1432 */	NdrFcShort( 0x28 ),	/* 40 */
/* 1434 */	NdrFcShort( 0x1 ),	/* 1 */
/* 1436 */	NdrFcLong( 0x1 ),	/* 1 */
/* 1440 */	NdrFcShort( 0x4 ),	/* Offset= 4 (1444) */
/* 1442 */	NdrFcShort( 0xffff ),	/* Offset= -1 (1441) */
/* 1444 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 1446 */	NdrFcShort( 0x28 ),	/* 40 */
/* 1448 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1450 */	NdrFcShort( 0xc ),	/* Offset= 12 (1462) */
/* 1452 */	0x36,		/* FC_POINTER */
			0x36,		/* FC_POINTER */
/* 1454 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 1456 */	NdrFcShort( 0xfa5c ),	/* Offset= -1444 (12) */
/* 1458 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 1460 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 1462 */	
			0x11, 0x0,	/* FC_RP */
/* 1464 */	NdrFcShort( 0xfae4 ),	/* Offset= -1308 (156) */
/* 1466 */	
			0x11, 0x8,	/* FC_RP [simple_pointer] */
/* 1468 */	
			0x22,		/* FC_C_CSTRING */
			0x5c,		/* FC_PAD */
/* 1470 */	
			0x11, 0x0,	/* FC_RP */
/* 1472 */	NdrFcShort( 0x2 ),	/* Offset= 2 (1474) */
/* 1474 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 1476 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x0,		/*  */
/* 1478 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 1480 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 1482 */	0x0 , 
			0x0,		/* 0 */
/* 1484 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1488 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1492 */	NdrFcShort( 0x2 ),	/* Offset= 2 (1494) */
/* 1494 */	NdrFcShort( 0x78 ),	/* 120 */
/* 1496 */	NdrFcShort( 0x2 ),	/* 2 */
/* 1498 */	NdrFcLong( 0x1 ),	/* 1 */
/* 1502 */	NdrFcShort( 0x1a ),	/* Offset= 26 (1528) */
/* 1504 */	NdrFcLong( 0x2 ),	/* 2 */
/* 1508 */	NdrFcShort( 0x2c ),	/* Offset= 44 (1552) */
/* 1510 */	NdrFcShort( 0xffff ),	/* Offset= -1 (1509) */
/* 1512 */	
			0x1d,		/* FC_SMFARRAY */
			0x0,		/* 0 */
/* 1514 */	NdrFcShort( 0x54 ),	/* 84 */
/* 1516 */	0x2,		/* FC_CHAR */
			0x5b,		/* FC_END */
/* 1518 */	
			0x15,		/* FC_STRUCT */
			0x0,		/* 0 */
/* 1520 */	NdrFcShort( 0x54 ),	/* 84 */
/* 1522 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 1524 */	NdrFcShort( 0xfff4 ),	/* Offset= -12 (1512) */
/* 1526 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 1528 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 1530 */	NdrFcShort( 0x68 ),	/* 104 */
/* 1532 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1534 */	NdrFcShort( 0xa ),	/* Offset= 10 (1544) */
/* 1536 */	0x36,		/* FC_POINTER */
			0x36,		/* FC_POINTER */
/* 1538 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 1540 */	NdrFcShort( 0xffea ),	/* Offset= -22 (1518) */
/* 1542 */	0x8,		/* FC_LONG */
			0x5b,		/* FC_END */
/* 1544 */	
			0x11, 0x0,	/* FC_RP */
/* 1546 */	NdrFcShort( 0xfa92 ),	/* Offset= -1390 (156) */
/* 1548 */	
			0x11, 0x8,	/* FC_RP [simple_pointer] */
/* 1550 */	
			0x22,		/* FC_C_CSTRING */
			0x5c,		/* FC_PAD */
/* 1552 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 1554 */	NdrFcShort( 0x78 ),	/* 120 */
/* 1556 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1558 */	NdrFcShort( 0xc ),	/* Offset= 12 (1570) */
/* 1560 */	0x36,		/* FC_POINTER */
			0x36,		/* FC_POINTER */
/* 1562 */	0x36,		/* FC_POINTER */
			0x36,		/* FC_POINTER */
/* 1564 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 1566 */	NdrFcShort( 0xffd0 ),	/* Offset= -48 (1518) */
/* 1568 */	0x8,		/* FC_LONG */
			0x5b,		/* FC_END */
/* 1570 */	
			0x11, 0x0,	/* FC_RP */
/* 1572 */	NdrFcShort( 0xfa78 ),	/* Offset= -1416 (156) */
/* 1574 */	
			0x12, 0x0,	/* FC_UP */
/* 1576 */	NdrFcShort( 0xfa74 ),	/* Offset= -1420 (156) */
/* 1578 */	
			0x12, 0x0,	/* FC_UP */
/* 1580 */	NdrFcShort( 0xfa70 ),	/* Offset= -1424 (156) */
/* 1582 */	
			0x11, 0x8,	/* FC_RP [simple_pointer] */
/* 1584 */	
			0x22,		/* FC_C_CSTRING */
			0x5c,		/* FC_PAD */
/* 1586 */	
			0x11, 0x0,	/* FC_RP */
/* 1588 */	NdrFcShort( 0x2 ),	/* Offset= 2 (1590) */
/* 1590 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 1592 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x0,		/*  */
/* 1594 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 1596 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 1598 */	0x0 , 
			0x0,		/* 0 */
/* 1600 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1604 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1608 */	NdrFcShort( 0x2 ),	/* Offset= 2 (1610) */
/* 1610 */	NdrFcShort( 0x18 ),	/* 24 */
/* 1612 */	NdrFcShort( 0x1 ),	/* 1 */
/* 1614 */	NdrFcLong( 0x1 ),	/* 1 */
/* 1618 */	NdrFcShort( 0x4 ),	/* Offset= 4 (1622) */
/* 1620 */	NdrFcShort( 0xffff ),	/* Offset= -1 (1619) */
/* 1622 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 1624 */	NdrFcShort( 0x18 ),	/* 24 */
/* 1626 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1628 */	NdrFcShort( 0x8 ),	/* Offset= 8 (1636) */
/* 1630 */	0x36,		/* FC_POINTER */
			0x36,		/* FC_POINTER */
/* 1632 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 1634 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 1636 */	
			0x11, 0x0,	/* FC_RP */
/* 1638 */	NdrFcShort( 0xfa36 ),	/* Offset= -1482 (156) */
/* 1640 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 1642 */	
			0x22,		/* FC_C_CSTRING */
			0x5c,		/* FC_PAD */
/* 1644 */	
			0x11, 0x0,	/* FC_RP */
/* 1646 */	NdrFcShort( 0x2 ),	/* Offset= 2 (1648) */
/* 1648 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 1650 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x0,		/*  */
/* 1652 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 1654 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 1656 */	0x0 , 
			0x0,		/* 0 */
/* 1658 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1662 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1666 */	NdrFcShort( 0x2 ),	/* Offset= 2 (1668) */
/* 1668 */	NdrFcShort( 0x80 ),	/* 128 */
/* 1670 */	NdrFcShort( 0x1 ),	/* 1 */
/* 1672 */	NdrFcLong( 0x1 ),	/* 1 */
/* 1676 */	NdrFcShort( 0x4 ),	/* Offset= 4 (1680) */
/* 1678 */	NdrFcShort( 0xffff ),	/* Offset= -1 (1677) */
/* 1680 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 1682 */	NdrFcShort( 0x80 ),	/* 128 */
/* 1684 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1686 */	NdrFcShort( 0x10 ),	/* Offset= 16 (1702) */
/* 1688 */	0x36,		/* FC_POINTER */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 1690 */	0x0,		/* 0 */
			NdrFcShort( 0xf971 ),	/* Offset= -1679 (12) */
			0x36,		/* FC_POINTER */
/* 1694 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 1696 */	NdrFcShort( 0xff4e ),	/* Offset= -178 (1518) */
/* 1698 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 1700 */	0x8,		/* FC_LONG */
			0x5b,		/* FC_END */
/* 1702 */	
			0x11, 0x0,	/* FC_RP */
/* 1704 */	NdrFcShort( 0xf9f4 ),	/* Offset= -1548 (156) */
/* 1706 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 1708 */	
			0x22,		/* FC_C_CSTRING */
			0x5c,		/* FC_PAD */
/* 1710 */	
			0x11, 0x0,	/* FC_RP */
/* 1712 */	NdrFcShort( 0x2 ),	/* Offset= 2 (1714) */
/* 1714 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 1716 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x0,		/*  */
/* 1718 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 1720 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 1722 */	0x0 , 
			0x0,		/* 0 */
/* 1724 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1728 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1732 */	NdrFcShort( 0x2 ),	/* Offset= 2 (1734) */
/* 1734 */	NdrFcShort( 0x30 ),	/* 48 */
/* 1736 */	NdrFcShort( 0x1 ),	/* 1 */
/* 1738 */	NdrFcLong( 0x1 ),	/* 1 */
/* 1742 */	NdrFcShort( 0x2e ),	/* Offset= 46 (1788) */
/* 1744 */	NdrFcShort( 0xffff ),	/* Offset= -1 (1743) */
/* 1746 */	
			0x21,		/* FC_BOGUS_ARRAY */
			0x3,		/* 3 */
/* 1748 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1750 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 1752 */	NdrFcShort( 0x4 ),	/* 4 */
/* 1754 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 1756 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 1758 */	NdrFcLong( 0x1 ),	/* 1 */
/* 1762 */	NdrFcLong( 0x2710 ),	/* 10000 */
/* 1766 */	NdrFcLong( 0xffffffff ),	/* -1 */
/* 1770 */	NdrFcShort( 0x0 ),	/* Corr flags:  */
/* 1772 */	0x0 , 
			0x0,		/* 0 */
/* 1774 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1778 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1782 */	
			0x12, 0x0,	/* FC_UP */
/* 1784 */	NdrFcShort( 0xf9a4 ),	/* Offset= -1628 (156) */
/* 1786 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 1788 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 1790 */	NdrFcShort( 0x30 ),	/* 48 */
/* 1792 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1794 */	NdrFcShort( 0xe ),	/* Offset= 14 (1808) */
/* 1796 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 1798 */	0x36,		/* FC_POINTER */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 1800 */	0x0,		/* 0 */
			NdrFcShort( 0xfcb7 ),	/* Offset= -841 (960) */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 1804 */	0x0,		/* 0 */
			NdrFcShort( 0xfa5f ),	/* Offset= -1441 (364) */
			0x5b,		/* FC_END */
/* 1808 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 1810 */	NdrFcShort( 0xffc0 ),	/* Offset= -64 (1746) */
/* 1812 */	
			0x11, 0x4,	/* FC_RP [alloced_on_stack] */
/* 1814 */	NdrFcShort( 0x2 ),	/* Offset= 2 (1816) */
/* 1816 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 1818 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x54,		/* FC_DEREFERENCE */
/* 1820 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 1822 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 1824 */	0x0 , 
			0x0,		/* 0 */
/* 1826 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1830 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1834 */	NdrFcShort( 0x2 ),	/* Offset= 2 (1836) */
/* 1836 */	NdrFcShort( 0x20 ),	/* 32 */
/* 1838 */	NdrFcShort( 0x1 ),	/* 1 */
/* 1840 */	NdrFcLong( 0x1 ),	/* 1 */
/* 1844 */	NdrFcShort( 0x2e ),	/* Offset= 46 (1890) */
/* 1846 */	NdrFcShort( 0xffff ),	/* Offset= -1 (1845) */
/* 1848 */	
			0x21,		/* FC_BOGUS_ARRAY */
			0x3,		/* 3 */
/* 1850 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1852 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 1854 */	NdrFcShort( 0x4 ),	/* 4 */
/* 1856 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 1858 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 1860 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1864 */	NdrFcLong( 0x2710 ),	/* 10000 */
/* 1868 */	NdrFcLong( 0xffffffff ),	/* -1 */
/* 1872 */	NdrFcShort( 0x0 ),	/* Corr flags:  */
/* 1874 */	0x0 , 
			0x0,		/* 0 */
/* 1876 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1880 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1884 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 1886 */	NdrFcShort( 0xfc72 ),	/* Offset= -910 (976) */
/* 1888 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 1890 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 1892 */	NdrFcShort( 0x20 ),	/* 32 */
/* 1894 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1896 */	NdrFcShort( 0xa ),	/* Offset= 10 (1906) */
/* 1898 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 1900 */	0x36,		/* FC_POINTER */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 1902 */	0x0,		/* 0 */
			NdrFcShort( 0xf9fd ),	/* Offset= -1539 (364) */
			0x5b,		/* FC_END */
/* 1906 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 1908 */	NdrFcShort( 0xffc4 ),	/* Offset= -60 (1848) */
/* 1910 */	
			0x11, 0x0,	/* FC_RP */
/* 1912 */	NdrFcShort( 0x2 ),	/* Offset= 2 (1914) */
/* 1914 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 1916 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x0,		/*  */
/* 1918 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 1920 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 1922 */	0x0 , 
			0x0,		/* 0 */
/* 1924 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1928 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1932 */	NdrFcShort( 0x2 ),	/* Offset= 2 (1934) */
/* 1934 */	NdrFcShort( 0x20 ),	/* 32 */
/* 1936 */	NdrFcShort( 0x1 ),	/* 1 */
/* 1938 */	NdrFcLong( 0x1 ),	/* 1 */
/* 1942 */	NdrFcShort( 0x38 ),	/* Offset= 56 (1998) */
/* 1944 */	NdrFcShort( 0xffff ),	/* Offset= -1 (1943) */
/* 1946 */	0xb7,		/* FC_RANGE */
			0xd,		/* 13 */
/* 1948 */	NdrFcLong( 0x1 ),	/* 1 */
/* 1952 */	NdrFcLong( 0x7 ),	/* 7 */
/* 1956 */	
			0x21,		/* FC_BOGUS_ARRAY */
			0x3,		/* 3 */
/* 1958 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1960 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 1962 */	NdrFcShort( 0x0 ),	/* 0 */
/* 1964 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 1966 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 1968 */	NdrFcLong( 0x1 ),	/* 1 */
/* 1972 */	NdrFcLong( 0x2710 ),	/* 10000 */
/* 1976 */	NdrFcLong( 0xffffffff ),	/* -1 */
/* 1980 */	NdrFcShort( 0x0 ),	/* Corr flags:  */
/* 1982 */	0x0 , 
			0x0,		/* 0 */
/* 1984 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1988 */	NdrFcLong( 0x0 ),	/* 0 */
/* 1992 */	
			0x12, 0x0,	/* FC_UP */
/* 1994 */	NdrFcShort( 0xf8d2 ),	/* Offset= -1838 (156) */
/* 1996 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 1998 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 2000 */	NdrFcShort( 0x20 ),	/* 32 */
/* 2002 */	NdrFcShort( 0x0 ),	/* 0 */
/* 2004 */	NdrFcShort( 0xc ),	/* Offset= 12 (2016) */
/* 2006 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 2008 */	0x36,		/* FC_POINTER */
			0x8,		/* FC_LONG */
/* 2010 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 2012 */	NdrFcShort( 0xffbe ),	/* Offset= -66 (1946) */
/* 2014 */	0x36,		/* FC_POINTER */
			0x5b,		/* FC_END */
/* 2016 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 2018 */	NdrFcShort( 0xffc2 ),	/* Offset= -62 (1956) */
/* 2020 */	
			0x12, 0x0,	/* FC_UP */
/* 2022 */	NdrFcShort( 0xf8b6 ),	/* Offset= -1866 (156) */
/* 2024 */	
			0x11, 0x4,	/* FC_RP [alloced_on_stack] */
/* 2026 */	NdrFcShort( 0x2 ),	/* Offset= 2 (2028) */
/* 2028 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 2030 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x54,		/* FC_DEREFERENCE */
/* 2032 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 2034 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 2036 */	0x0 , 
			0x0,		/* 0 */
/* 2038 */	NdrFcLong( 0x0 ),	/* 0 */
/* 2042 */	NdrFcLong( 0x0 ),	/* 0 */
/* 2046 */	NdrFcShort( 0x2 ),	/* Offset= 2 (2048) */
/* 2048 */	NdrFcShort( 0x28 ),	/* 40 */
/* 2050 */	NdrFcShort( 0x1 ),	/* 1 */
/* 2052 */	NdrFcLong( 0x1 ),	/* 1 */
/* 2056 */	NdrFcShort( 0x6e ),	/* Offset= 110 (2166) */
/* 2058 */	NdrFcShort( 0xffff ),	/* Offset= -1 (2057) */
/* 2060 */	
			0x21,		/* FC_BOGUS_ARRAY */
			0x3,		/* 3 */
/* 2062 */	NdrFcShort( 0x0 ),	/* 0 */
/* 2064 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 2066 */	NdrFcShort( 0x4 ),	/* 4 */
/* 2068 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 2070 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 2072 */	NdrFcLong( 0x0 ),	/* 0 */
/* 2076 */	NdrFcLong( 0x2710 ),	/* 10000 */
/* 2080 */	NdrFcLong( 0xffffffff ),	/* -1 */
/* 2084 */	NdrFcShort( 0x0 ),	/* Corr flags:  */
/* 2086 */	0x0 , 
			0x0,		/* 0 */
/* 2088 */	NdrFcLong( 0x0 ),	/* 0 */
/* 2092 */	NdrFcLong( 0x0 ),	/* 0 */
/* 2096 */	
			0x12, 0x0,	/* FC_UP */
/* 2098 */	NdrFcShort( 0xf86a ),	/* Offset= -1942 (156) */
/* 2100 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 2102 */	
			0x1b,		/* FC_CARRAY */
			0x3,		/* 3 */
/* 2104 */	NdrFcShort( 0x4 ),	/* 4 */
/* 2106 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 2108 */	NdrFcShort( 0x4 ),	/* 4 */
/* 2110 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 2112 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 2114 */	NdrFcLong( 0x0 ),	/* 0 */
/* 2118 */	NdrFcLong( 0x2710 ),	/* 10000 */
/* 2122 */	0x8,		/* FC_LONG */
			0x5b,		/* FC_END */
/* 2124 */	
			0x21,		/* FC_BOGUS_ARRAY */
			0x3,		/* 3 */
/* 2126 */	NdrFcShort( 0x0 ),	/* 0 */
/* 2128 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 2130 */	NdrFcShort( 0x8 ),	/* 8 */
/* 2132 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 2134 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 2136 */	NdrFcLong( 0x0 ),	/* 0 */
/* 2140 */	NdrFcLong( 0x2710 ),	/* 10000 */
/* 2144 */	NdrFcLong( 0xffffffff ),	/* -1 */
/* 2148 */	NdrFcShort( 0x0 ),	/* Corr flags:  */
/* 2150 */	0x0 , 
			0x0,		/* 0 */
/* 2152 */	NdrFcLong( 0x0 ),	/* 0 */
/* 2156 */	NdrFcLong( 0x0 ),	/* 0 */
/* 2160 */	
			0x12, 0x0,	/* FC_UP */
/* 2162 */	NdrFcShort( 0xf80a ),	/* Offset= -2038 (124) */
/* 2164 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 2166 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 2168 */	NdrFcShort( 0x28 ),	/* 40 */
/* 2170 */	NdrFcShort( 0x0 ),	/* 0 */
/* 2172 */	NdrFcShort( 0xa ),	/* Offset= 10 (2182) */
/* 2174 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 2176 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 2178 */	0x36,		/* FC_POINTER */
			0x36,		/* FC_POINTER */
/* 2180 */	0x36,		/* FC_POINTER */
			0x5b,		/* FC_END */
/* 2182 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 2184 */	NdrFcShort( 0xff84 ),	/* Offset= -124 (2060) */
/* 2186 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 2188 */	NdrFcShort( 0xffaa ),	/* Offset= -86 (2102) */
/* 2190 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 2192 */	NdrFcShort( 0xffbc ),	/* Offset= -68 (2124) */
/* 2194 */	
			0x11, 0x0,	/* FC_RP */
/* 2196 */	NdrFcShort( 0x2 ),	/* Offset= 2 (2198) */
/* 2198 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 2200 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x0,		/*  */
/* 2202 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 2204 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 2206 */	0x0 , 
			0x0,		/* 0 */
/* 2208 */	NdrFcLong( 0x0 ),	/* 0 */
/* 2212 */	NdrFcLong( 0x0 ),	/* 0 */
/* 2216 */	NdrFcShort( 0x2 ),	/* Offset= 2 (2218) */
/* 2218 */	NdrFcShort( 0x40 ),	/* 64 */
/* 2220 */	NdrFcShort( 0x2 ),	/* 2 */
/* 2222 */	NdrFcLong( 0x1 ),	/* 1 */
/* 2226 */	NdrFcShort( 0xa ),	/* Offset= 10 (2236) */
/* 2228 */	NdrFcLong( 0x2 ),	/* 2 */
/* 2232 */	NdrFcShort( 0x6c ),	/* Offset= 108 (2340) */
/* 2234 */	NdrFcShort( 0xffff ),	/* Offset= -1 (2233) */
/* 2236 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 2238 */	NdrFcShort( 0x30 ),	/* 48 */
/* 2240 */	NdrFcShort( 0x0 ),	/* 0 */
/* 2242 */	NdrFcShort( 0xc ),	/* Offset= 12 (2254) */
/* 2244 */	0x36,		/* FC_POINTER */
			0x36,		/* FC_POINTER */
/* 2246 */	0x36,		/* FC_POINTER */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 2248 */	0x0,		/* 0 */
			NdrFcShort( 0xf8a3 ),	/* Offset= -1885 (364) */
			0x8,		/* FC_LONG */
/* 2252 */	0x40,		/* FC_STRUCTPAD4 */
			0x5b,		/* FC_END */
/* 2254 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 2256 */	0x2,		/* FC_CHAR */
			0x5c,		/* FC_PAD */
/* 2258 */	
			0x12, 0x0,	/* FC_UP */
/* 2260 */	NdrFcShort( 0xfafc ),	/* Offset= -1284 (976) */
/* 2262 */	
			0x12, 0x0,	/* FC_UP */
/* 2264 */	NdrFcShort( 0xf734 ),	/* Offset= -2252 (12) */
/* 2266 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 2268 */	NdrFcShort( 0x10 ),	/* 16 */
/* 2270 */	NdrFcShort( 0x0 ),	/* 0 */
/* 2272 */	NdrFcShort( 0x6 ),	/* Offset= 6 (2278) */
/* 2274 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 2276 */	0x36,		/* FC_POINTER */
			0x5b,		/* FC_END */
/* 2278 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 2280 */	NdrFcShort( 0xf824 ),	/* Offset= -2012 (268) */
/* 2282 */	
			0x21,		/* FC_BOGUS_ARRAY */
			0x3,		/* 3 */
/* 2284 */	NdrFcShort( 0x0 ),	/* 0 */
/* 2286 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 2288 */	NdrFcShort( 0x4 ),	/* 4 */
/* 2290 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 2292 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 2294 */	NdrFcLong( 0x0 ),	/* 0 */
/* 2298 */	NdrFcLong( 0x2710 ),	/* 10000 */
/* 2302 */	NdrFcLong( 0xffffffff ),	/* -1 */
/* 2306 */	NdrFcShort( 0x0 ),	/* Corr flags:  */
/* 2308 */	0x0 , 
			0x0,		/* 0 */
/* 2310 */	NdrFcLong( 0x0 ),	/* 0 */
/* 2314 */	NdrFcLong( 0x0 ),	/* 0 */
/* 2318 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 2320 */	NdrFcShort( 0xffca ),	/* Offset= -54 (2266) */
/* 2322 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 2324 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 2326 */	NdrFcShort( 0x10 ),	/* 16 */
/* 2328 */	NdrFcShort( 0x0 ),	/* 0 */
/* 2330 */	NdrFcShort( 0x6 ),	/* Offset= 6 (2336) */
/* 2332 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 2334 */	0x36,		/* FC_POINTER */
			0x5b,		/* FC_END */
/* 2336 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 2338 */	NdrFcShort( 0xffc8 ),	/* Offset= -56 (2282) */
/* 2340 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 2342 */	NdrFcShort( 0x40 ),	/* 64 */
/* 2344 */	NdrFcShort( 0x0 ),	/* 0 */
/* 2346 */	NdrFcShort( 0xe ),	/* Offset= 14 (2360) */
/* 2348 */	0x36,		/* FC_POINTER */
			0x36,		/* FC_POINTER */
/* 2350 */	0x36,		/* FC_POINTER */
			0x36,		/* FC_POINTER */
/* 2352 */	0x36,		/* FC_POINTER */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 2354 */	0x0,		/* 0 */
			NdrFcShort( 0xf839 ),	/* Offset= -1991 (364) */
			0x8,		/* FC_LONG */
/* 2358 */	0x40,		/* FC_STRUCTPAD4 */
			0x5b,		/* FC_END */
/* 2360 */	
			0x12, 0x0,	/* FC_UP */
/* 2362 */	NdrFcShort( 0xf762 ),	/* Offset= -2206 (156) */
/* 2364 */	
			0x12, 0x0,	/* FC_UP */
/* 2366 */	NdrFcShort( 0xfa92 ),	/* Offset= -1390 (976) */
/* 2368 */	
			0x12, 0x0,	/* FC_UP */
/* 2370 */	NdrFcShort( 0xf75a ),	/* Offset= -2214 (156) */
/* 2372 */	
			0x12, 0x0,	/* FC_UP */
/* 2374 */	NdrFcShort( 0xf756 ),	/* Offset= -2218 (156) */
/* 2376 */	
			0x12, 0x0,	/* FC_UP */
/* 2378 */	NdrFcShort( 0xffca ),	/* Offset= -54 (2324) */
/* 2380 */	
			0x11, 0x4,	/* FC_RP [alloced_on_stack] */
/* 2382 */	NdrFcShort( 0x2 ),	/* Offset= 2 (2384) */
/* 2384 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 2386 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x54,		/* FC_DEREFERENCE */
/* 2388 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 2390 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 2392 */	0x0 , 
			0x0,		/* 0 */
/* 2394 */	NdrFcLong( 0x0 ),	/* 0 */
/* 2398 */	NdrFcLong( 0x0 ),	/* 0 */
/* 2402 */	NdrFcShort( 0x2 ),	/* Offset= 2 (2404) */
/* 2404 */	NdrFcShort( 0x20 ),	/* 32 */
/* 2406 */	NdrFcShort( 0x2 ),	/* 2 */
/* 2408 */	NdrFcLong( 0x1 ),	/* 1 */
/* 2412 */	NdrFcShort( 0xe ),	/* Offset= 14 (2426) */
/* 2414 */	NdrFcLong( 0x2 ),	/* 2 */
/* 2418 */	NdrFcShort( 0x20 ),	/* Offset= 32 (2450) */
/* 2420 */	NdrFcShort( 0xffff ),	/* Offset= -1 (2419) */
/* 2422 */	
			0x12, 0x0,	/* FC_UP */
/* 2424 */	NdrFcShort( 0xfa58 ),	/* Offset= -1448 (976) */
/* 2426 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 2428 */	NdrFcShort( 0x20 ),	/* 32 */
/* 2430 */	NdrFcShort( 0x0 ),	/* 0 */
/* 2432 */	NdrFcShort( 0xa ),	/* Offset= 10 (2442) */
/* 2434 */	0x36,		/* FC_POINTER */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 2436 */	0x0,		/* 0 */
			NdrFcShort( 0xf7e7 ),	/* Offset= -2073 (364) */
			0x36,		/* FC_POINTER */
/* 2440 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 2442 */	
			0x12, 0x10,	/* FC_UP [pointer_deref] */
/* 2444 */	NdrFcShort( 0xffea ),	/* Offset= -22 (2422) */
/* 2446 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 2448 */	0x8,		/* FC_LONG */
			0x5c,		/* FC_PAD */
/* 2450 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 2452 */	NdrFcShort( 0x10 ),	/* 16 */
/* 2454 */	NdrFcShort( 0x0 ),	/* 0 */
/* 2456 */	NdrFcShort( 0x6 ),	/* Offset= 6 (2462) */
/* 2458 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 2460 */	0x36,		/* FC_POINTER */
			0x5b,		/* FC_END */
/* 2462 */	
			0x12, 0x0,	/* FC_UP */
/* 2464 */	NdrFcShort( 0xf6fc ),	/* Offset= -2308 (156) */
/* 2466 */	
			0x11, 0x0,	/* FC_RP */
/* 2468 */	NdrFcShort( 0x2 ),	/* Offset= 2 (2470) */
/* 2470 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 2472 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x0,		/*  */
/* 2474 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 2476 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 2478 */	0x0 , 
			0x0,		/* 0 */
/* 2480 */	NdrFcLong( 0x0 ),	/* 0 */
/* 2484 */	NdrFcLong( 0x0 ),	/* 0 */
/* 2488 */	NdrFcShort( 0x2 ),	/* Offset= 2 (2490) */
/* 2490 */	NdrFcShort( 0x18 ),	/* 24 */
/* 2492 */	NdrFcShort( 0x1 ),	/* 1 */
/* 2494 */	NdrFcLong( 0x1 ),	/* 1 */
/* 2498 */	NdrFcShort( 0x1a ),	/* Offset= 26 (2524) */
/* 2500 */	NdrFcShort( 0xffff ),	/* Offset= -1 (2499) */
/* 2502 */	
			0x1b,		/* FC_CARRAY */
			0x0,		/* 0 */
/* 2504 */	NdrFcShort( 0x1 ),	/* 1 */
/* 2506 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 2508 */	NdrFcShort( 0x8 ),	/* 8 */
/* 2510 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 2512 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 2514 */	NdrFcLong( 0x0 ),	/* 0 */
/* 2518 */	NdrFcLong( 0xa00000 ),	/* 10485760 */
/* 2522 */	0x1,		/* FC_BYTE */
			0x5b,		/* FC_END */
/* 2524 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 2526 */	NdrFcShort( 0x18 ),	/* 24 */
/* 2528 */	NdrFcShort( 0x0 ),	/* 0 */
/* 2530 */	NdrFcShort( 0x8 ),	/* Offset= 8 (2538) */
/* 2532 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 2534 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 2536 */	0x36,		/* FC_POINTER */
			0x5b,		/* FC_END */
/* 2538 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 2540 */	NdrFcShort( 0xffda ),	/* Offset= -38 (2502) */
/* 2542 */	
			0x11, 0x0,	/* FC_RP */
/* 2544 */	NdrFcShort( 0x2 ),	/* Offset= 2 (2546) */
/* 2546 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 2548 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x54,		/* FC_DEREFERENCE */
/* 2550 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 2552 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 2554 */	0x0 , 
			0x0,		/* 0 */
/* 2556 */	NdrFcLong( 0x0 ),	/* 0 */
/* 2560 */	NdrFcLong( 0x0 ),	/* 0 */
/* 2564 */	NdrFcShort( 0x2 ),	/* Offset= 2 (2566) */
/* 2566 */	NdrFcShort( 0x50 ),	/* 80 */
/* 2568 */	NdrFcShort( 0x1 ),	/* 1 */
/* 2570 */	NdrFcLong( 0x1 ),	/* 1 */
/* 2574 */	NdrFcShort( 0x4e ),	/* Offset= 78 (2652) */
/* 2576 */	NdrFcShort( 0xffff ),	/* Offset= -1 (2575) */
/* 2578 */	
			0x15,		/* FC_STRUCT */
			0x7,		/* 7 */
/* 2580 */	NdrFcShort( 0x30 ),	/* 48 */
/* 2582 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 2584 */	NdrFcShort( 0xf814 ),	/* Offset= -2028 (556) */
/* 2586 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 2588 */	NdrFcShort( 0xf810 ),	/* Offset= -2032 (556) */
/* 2590 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 2592 */	NdrFcShort( 0xf80c ),	/* Offset= -2036 (556) */
/* 2594 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 2596 */	NdrFcShort( 0xf808 ),	/* Offset= -2040 (556) */
/* 2598 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 2600 */	NdrFcShort( 0xf804 ),	/* Offset= -2044 (556) */
/* 2602 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 2604 */	NdrFcShort( 0xf800 ),	/* Offset= -2048 (556) */
/* 2606 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 2608 */	
			0x1b,		/* FC_CARRAY */
			0x0,		/* 0 */
/* 2610 */	NdrFcShort( 0x1 ),	/* 1 */
/* 2612 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 2614 */	NdrFcShort( 0x0 ),	/* 0 */
/* 2616 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 2618 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 2620 */	NdrFcLong( 0x0 ),	/* 0 */
/* 2624 */	NdrFcLong( 0xa00000 ),	/* 10485760 */
/* 2628 */	0x1,		/* FC_BYTE */
			0x5b,		/* FC_END */
/* 2630 */	
			0x1b,		/* FC_CARRAY */
			0x0,		/* 0 */
/* 2632 */	NdrFcShort( 0x1 ),	/* 1 */
/* 2634 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 2636 */	NdrFcShort( 0x4 ),	/* 4 */
/* 2638 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 2640 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 2642 */	NdrFcLong( 0x0 ),	/* 0 */
/* 2646 */	NdrFcLong( 0xa00000 ),	/* 10485760 */
/* 2650 */	0x1,		/* FC_BYTE */
			0x5b,		/* FC_END */
/* 2652 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x7,		/* 7 */
/* 2654 */	NdrFcShort( 0x50 ),	/* 80 */
/* 2656 */	NdrFcShort( 0x0 ),	/* 0 */
/* 2658 */	NdrFcShort( 0xe ),	/* Offset= 14 (2672) */
/* 2660 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 2662 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 2664 */	NdrFcShort( 0xffaa ),	/* Offset= -86 (2578) */
/* 2666 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 2668 */	0x36,		/* FC_POINTER */
			0x36,		/* FC_POINTER */
/* 2670 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 2672 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 2674 */	NdrFcShort( 0xffbe ),	/* Offset= -66 (2608) */
/* 2676 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 2678 */	NdrFcShort( 0xffd0 ),	/* Offset= -48 (2630) */
/* 2680 */	
			0x11, 0x0,	/* FC_RP */
/* 2682 */	NdrFcShort( 0x2 ),	/* Offset= 2 (2684) */
/* 2684 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 2686 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x0,		/*  */
/* 2688 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 2690 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 2692 */	0x0 , 
			0x0,		/* 0 */
/* 2694 */	NdrFcLong( 0x0 ),	/* 0 */
/* 2698 */	NdrFcLong( 0x0 ),	/* 0 */
/* 2702 */	NdrFcShort( 0x2 ),	/* Offset= 2 (2704) */
/* 2704 */	NdrFcShort( 0x20 ),	/* 32 */
/* 2706 */	NdrFcShort( 0x1 ),	/* 1 */
/* 2708 */	NdrFcLong( 0x1 ),	/* 1 */
/* 2712 */	NdrFcShort( 0x2e ),	/* Offset= 46 (2758) */
/* 2714 */	NdrFcShort( 0xffff ),	/* Offset= -1 (2713) */
/* 2716 */	
			0x21,		/* FC_BOGUS_ARRAY */
			0x3,		/* 3 */
/* 2718 */	NdrFcShort( 0x0 ),	/* 0 */
/* 2720 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 2722 */	NdrFcShort( 0x14 ),	/* 20 */
/* 2724 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 2726 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 2728 */	NdrFcLong( 0x1 ),	/* 1 */
/* 2732 */	NdrFcLong( 0x2710 ),	/* 10000 */
/* 2736 */	NdrFcLong( 0xffffffff ),	/* -1 */
/* 2740 */	NdrFcShort( 0x0 ),	/* Corr flags:  */
/* 2742 */	0x0 , 
			0x0,		/* 0 */
/* 2744 */	NdrFcLong( 0x0 ),	/* 0 */
/* 2748 */	NdrFcLong( 0x0 ),	/* 0 */
/* 2752 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 2754 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 2756 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 2758 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 2760 */	NdrFcShort( 0x20 ),	/* 32 */
/* 2762 */	NdrFcShort( 0x0 ),	/* 0 */
/* 2764 */	NdrFcShort( 0xa ),	/* Offset= 10 (2774) */
/* 2766 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 2768 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 2770 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 2772 */	0x36,		/* FC_POINTER */
			0x5b,		/* FC_END */
/* 2774 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 2776 */	NdrFcShort( 0xffc4 ),	/* Offset= -60 (2716) */
/* 2778 */	
			0x11, 0x4,	/* FC_RP [alloced_on_stack] */
/* 2780 */	NdrFcShort( 0x2 ),	/* Offset= 2 (2782) */
/* 2782 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 2784 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x54,		/* FC_DEREFERENCE */
/* 2786 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 2788 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 2790 */	0x0 , 
			0x0,		/* 0 */
/* 2792 */	NdrFcLong( 0x0 ),	/* 0 */
/* 2796 */	NdrFcLong( 0x0 ),	/* 0 */
/* 2800 */	NdrFcShort( 0x2 ),	/* Offset= 2 (2802) */
/* 2802 */	NdrFcShort( 0x8 ),	/* 8 */
/* 2804 */	NdrFcShort( 0x1 ),	/* 1 */
/* 2806 */	NdrFcLong( 0x1 ),	/* 1 */
/* 2810 */	NdrFcShort( 0x54 ),	/* Offset= 84 (2894) */
/* 2812 */	NdrFcShort( 0xffff ),	/* Offset= -1 (2811) */
/* 2814 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 2816 */	NdrFcShort( 0x18 ),	/* 24 */
/* 2818 */	NdrFcShort( 0x0 ),	/* 0 */
/* 2820 */	NdrFcShort( 0x8 ),	/* Offset= 8 (2828) */
/* 2822 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 2824 */	0x36,		/* FC_POINTER */
			0x36,		/* FC_POINTER */
/* 2826 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 2828 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 2830 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 2832 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 2834 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 2836 */	
			0x21,		/* FC_BOGUS_ARRAY */
			0x3,		/* 3 */
/* 2838 */	NdrFcShort( 0x0 ),	/* 0 */
/* 2840 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 2842 */	NdrFcShort( 0x0 ),	/* 0 */
/* 2844 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 2846 */	0x0 , 
			0x0,		/* 0 */
/* 2848 */	NdrFcLong( 0x0 ),	/* 0 */
/* 2852 */	NdrFcLong( 0x0 ),	/* 0 */
/* 2856 */	NdrFcLong( 0xffffffff ),	/* -1 */
/* 2860 */	NdrFcShort( 0x0 ),	/* Corr flags:  */
/* 2862 */	0x0 , 
			0x0,		/* 0 */
/* 2864 */	NdrFcLong( 0x0 ),	/* 0 */
/* 2868 */	NdrFcLong( 0x0 ),	/* 0 */
/* 2872 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 2874 */	NdrFcShort( 0xffc4 ),	/* Offset= -60 (2814) */
/* 2876 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 2878 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 2880 */	NdrFcShort( 0x10 ),	/* 16 */
/* 2882 */	NdrFcShort( 0x0 ),	/* 0 */
/* 2884 */	NdrFcShort( 0x6 ),	/* Offset= 6 (2890) */
/* 2886 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 2888 */	0x36,		/* FC_POINTER */
			0x5b,		/* FC_END */
/* 2890 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 2892 */	NdrFcShort( 0xffc8 ),	/* Offset= -56 (2836) */
/* 2894 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 2896 */	NdrFcShort( 0x8 ),	/* 8 */
/* 2898 */	NdrFcShort( 0x0 ),	/* 0 */
/* 2900 */	NdrFcShort( 0x4 ),	/* Offset= 4 (2904) */
/* 2902 */	0x36,		/* FC_POINTER */
			0x5b,		/* FC_END */
/* 2904 */	
			0x12, 0x0,	/* FC_UP */
/* 2906 */	NdrFcShort( 0xffe4 ),	/* Offset= -28 (2878) */
/* 2908 */	
			0x11, 0x0,	/* FC_RP */
/* 2910 */	NdrFcShort( 0x2 ),	/* Offset= 2 (2912) */
/* 2912 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 2914 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x0,		/*  */
/* 2916 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 2918 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 2920 */	0x0 , 
			0x0,		/* 0 */
/* 2922 */	NdrFcLong( 0x0 ),	/* 0 */
/* 2926 */	NdrFcLong( 0x0 ),	/* 0 */
/* 2930 */	NdrFcShort( 0x2 ),	/* Offset= 2 (2932) */
/* 2932 */	NdrFcShort( 0x20 ),	/* 32 */
/* 2934 */	NdrFcShort( 0x1 ),	/* 1 */
/* 2936 */	NdrFcLong( 0x1 ),	/* 1 */
/* 2940 */	NdrFcShort( 0x2e ),	/* Offset= 46 (2986) */
/* 2942 */	NdrFcShort( 0xffff ),	/* Offset= -1 (2941) */
/* 2944 */	
			0x21,		/* FC_BOGUS_ARRAY */
			0x3,		/* 3 */
/* 2946 */	NdrFcShort( 0x0 ),	/* 0 */
/* 2948 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 2950 */	NdrFcShort( 0x10 ),	/* 16 */
/* 2952 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 2954 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 2956 */	NdrFcLong( 0x0 ),	/* 0 */
/* 2960 */	NdrFcLong( 0x2710 ),	/* 10000 */
/* 2964 */	NdrFcLong( 0xffffffff ),	/* -1 */
/* 2968 */	NdrFcShort( 0x0 ),	/* Corr flags:  */
/* 2970 */	0x0 , 
			0x0,		/* 0 */
/* 2972 */	NdrFcLong( 0x0 ),	/* 0 */
/* 2976 */	NdrFcLong( 0x0 ),	/* 0 */
/* 2980 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 2982 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 2984 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 2986 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 2988 */	NdrFcShort( 0x20 ),	/* 32 */
/* 2990 */	NdrFcShort( 0x0 ),	/* 0 */
/* 2992 */	NdrFcShort( 0xa ),	/* Offset= 10 (3002) */
/* 2994 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 2996 */	0x36,		/* FC_POINTER */
			0x8,		/* FC_LONG */
/* 2998 */	0x40,		/* FC_STRUCTPAD4 */
			0x36,		/* FC_POINTER */
/* 3000 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 3002 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 3004 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 3006 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 3008 */	NdrFcShort( 0xffc0 ),	/* Offset= -64 (2944) */
/* 3010 */	
			0x11, 0x4,	/* FC_RP [alloced_on_stack] */
/* 3012 */	NdrFcShort( 0x2 ),	/* Offset= 2 (3014) */
/* 3014 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 3016 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x54,		/* FC_DEREFERENCE */
/* 3018 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 3020 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 3022 */	0x0 , 
			0x0,		/* 0 */
/* 3024 */	NdrFcLong( 0x0 ),	/* 0 */
/* 3028 */	NdrFcLong( 0x0 ),	/* 0 */
/* 3032 */	NdrFcShort( 0x2 ),	/* Offset= 2 (3034) */
/* 3034 */	NdrFcShort( 0x4 ),	/* 4 */
/* 3036 */	NdrFcShort( 0x1 ),	/* 1 */
/* 3038 */	NdrFcLong( 0x1 ),	/* 1 */
/* 3042 */	NdrFcShort( 0x4 ),	/* Offset= 4 (3046) */
/* 3044 */	NdrFcShort( 0xffff ),	/* Offset= -1 (3043) */
/* 3046 */	
			0x15,		/* FC_STRUCT */
			0x3,		/* 3 */
/* 3048 */	NdrFcShort( 0x4 ),	/* 4 */
/* 3050 */	0x8,		/* FC_LONG */
			0x5b,		/* FC_END */
/* 3052 */	
			0x11, 0x0,	/* FC_RP */
/* 3054 */	NdrFcShort( 0x2 ),	/* Offset= 2 (3056) */
/* 3056 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 3058 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x0,		/*  */
/* 3060 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 3062 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 3064 */	0x0 , 
			0x0,		/* 0 */
/* 3066 */	NdrFcLong( 0x0 ),	/* 0 */
/* 3070 */	NdrFcLong( 0x0 ),	/* 0 */
/* 3074 */	NdrFcShort( 0x2 ),	/* Offset= 2 (3076) */
/* 3076 */	NdrFcShort( 0x18 ),	/* 24 */
/* 3078 */	NdrFcShort( 0x1 ),	/* 1 */
/* 3080 */	NdrFcLong( 0x1 ),	/* 1 */
/* 3084 */	NdrFcShort( 0x4 ),	/* Offset= 4 (3088) */
/* 3086 */	NdrFcShort( 0xffff ),	/* Offset= -1 (3085) */
/* 3088 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 3090 */	NdrFcShort( 0x18 ),	/* 24 */
/* 3092 */	NdrFcShort( 0x0 ),	/* 0 */
/* 3094 */	NdrFcShort( 0x8 ),	/* Offset= 8 (3102) */
/* 3096 */	0x36,		/* FC_POINTER */
			0x36,		/* FC_POINTER */
/* 3098 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 3100 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 3102 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 3104 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 3106 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 3108 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 3110 */	
			0x11, 0x4,	/* FC_RP [alloced_on_stack] */
/* 3112 */	NdrFcShort( 0x2 ),	/* Offset= 2 (3114) */
/* 3114 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 3116 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x54,		/* FC_DEREFERENCE */
/* 3118 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 3120 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 3122 */	0x0 , 
			0x0,		/* 0 */
/* 3124 */	NdrFcLong( 0x0 ),	/* 0 */
/* 3128 */	NdrFcLong( 0x0 ),	/* 0 */
/* 3132 */	NdrFcShort( 0x2 ),	/* Offset= 2 (3134) */
/* 3134 */	NdrFcShort( 0x4 ),	/* 4 */
/* 3136 */	NdrFcShort( 0x1 ),	/* 1 */
/* 3138 */	NdrFcLong( 0x1 ),	/* 1 */
/* 3142 */	NdrFcShort( 0xffa0 ),	/* Offset= -96 (3046) */
/* 3144 */	NdrFcShort( 0xffff ),	/* Offset= -1 (3143) */
/* 3146 */	
			0x11, 0x0,	/* FC_RP */
/* 3148 */	NdrFcShort( 0x2 ),	/* Offset= 2 (3150) */
/* 3150 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 3152 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x0,		/*  */
/* 3154 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 3156 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 3158 */	0x0 , 
			0x0,		/* 0 */
/* 3160 */	NdrFcLong( 0x0 ),	/* 0 */
/* 3164 */	NdrFcLong( 0x0 ),	/* 0 */
/* 3168 */	NdrFcShort( 0x2 ),	/* Offset= 2 (3170) */
/* 3170 */	NdrFcShort( 0x8 ),	/* 8 */
/* 3172 */	NdrFcShort( 0x1 ),	/* 1 */
/* 3174 */	NdrFcLong( 0x1 ),	/* 1 */
/* 3178 */	NdrFcShort( 0x4 ),	/* Offset= 4 (3182) */
/* 3180 */	NdrFcShort( 0xffff ),	/* Offset= -1 (3179) */
/* 3182 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 3184 */	NdrFcShort( 0x8 ),	/* 8 */
/* 3186 */	NdrFcShort( 0x0 ),	/* 0 */
/* 3188 */	NdrFcShort( 0x4 ),	/* Offset= 4 (3192) */
/* 3190 */	0x36,		/* FC_POINTER */
			0x5b,		/* FC_END */
/* 3192 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 3194 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 3196 */	
			0x11, 0x4,	/* FC_RP [alloced_on_stack] */
/* 3198 */	NdrFcShort( 0x2 ),	/* Offset= 2 (3200) */
/* 3200 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 3202 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x54,		/* FC_DEREFERENCE */
/* 3204 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 3206 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 3208 */	0x0 , 
			0x0,		/* 0 */
/* 3210 */	NdrFcLong( 0x0 ),	/* 0 */
/* 3214 */	NdrFcLong( 0x0 ),	/* 0 */
/* 3218 */	NdrFcShort( 0x2 ),	/* Offset= 2 (3220) */
/* 3220 */	NdrFcShort( 0x4 ),	/* 4 */
/* 3222 */	NdrFcShort( 0x1 ),	/* 1 */
/* 3224 */	NdrFcLong( 0x1 ),	/* 1 */
/* 3228 */	NdrFcShort( 0xff4a ),	/* Offset= -182 (3046) */
/* 3230 */	NdrFcShort( 0xffff ),	/* Offset= -1 (3229) */
/* 3232 */	
			0x11, 0x0,	/* FC_RP */
/* 3234 */	NdrFcShort( 0x2 ),	/* Offset= 2 (3236) */
/* 3236 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 3238 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x0,		/*  */
/* 3240 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 3242 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 3244 */	0x0 , 
			0x0,		/* 0 */
/* 3246 */	NdrFcLong( 0x0 ),	/* 0 */
/* 3250 */	NdrFcLong( 0x0 ),	/* 0 */
/* 3254 */	NdrFcShort( 0x2 ),	/* Offset= 2 (3256) */
/* 3256 */	NdrFcShort( 0x10 ),	/* 16 */
/* 3258 */	NdrFcShort( 0x1 ),	/* 1 */
/* 3260 */	NdrFcLong( 0x1 ),	/* 1 */
/* 3264 */	NdrFcShort( 0x4 ),	/* Offset= 4 (3268) */
/* 3266 */	NdrFcShort( 0xffff ),	/* Offset= -1 (3265) */
/* 3268 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 3270 */	NdrFcShort( 0x10 ),	/* 16 */
/* 3272 */	NdrFcShort( 0x0 ),	/* 0 */
/* 3274 */	NdrFcShort( 0x6 ),	/* Offset= 6 (3280) */
/* 3276 */	0x36,		/* FC_POINTER */
			0x8,		/* FC_LONG */
/* 3278 */	0x40,		/* FC_STRUCTPAD4 */
			0x5b,		/* FC_END */
/* 3280 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 3282 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 3284 */	
			0x11, 0x4,	/* FC_RP [alloced_on_stack] */
/* 3286 */	NdrFcShort( 0x2 ),	/* Offset= 2 (3288) */
/* 3288 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 3290 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x54,		/* FC_DEREFERENCE */
/* 3292 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 3294 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 3296 */	0x0 , 
			0x0,		/* 0 */
/* 3298 */	NdrFcLong( 0x0 ),	/* 0 */
/* 3302 */	NdrFcLong( 0x0 ),	/* 0 */
/* 3306 */	NdrFcShort( 0x2 ),	/* Offset= 2 (3308) */
/* 3308 */	NdrFcShort( 0x10 ),	/* 16 */
/* 3310 */	NdrFcShort( 0x4 ),	/* 4 */
/* 3312 */	NdrFcLong( 0x1 ),	/* 1 */
/* 3316 */	NdrFcShort( 0x64 ),	/* Offset= 100 (3416) */
/* 3318 */	NdrFcLong( 0x2 ),	/* 2 */
/* 3322 */	NdrFcShort( 0xd8 ),	/* Offset= 216 (3538) */
/* 3324 */	NdrFcLong( 0x3 ),	/* 3 */
/* 3328 */	NdrFcShort( 0x14c ),	/* Offset= 332 (3660) */
/* 3330 */	NdrFcLong( 0xffffffff ),	/* -1 */
/* 3334 */	NdrFcShort( 0x194 ),	/* Offset= 404 (3738) */
/* 3336 */	NdrFcShort( 0xffff ),	/* Offset= -1 (3335) */
/* 3338 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 3340 */	NdrFcShort( 0x30 ),	/* 48 */
/* 3342 */	NdrFcShort( 0x0 ),	/* 0 */
/* 3344 */	NdrFcShort( 0xa ),	/* Offset= 10 (3354) */
/* 3346 */	0x36,		/* FC_POINTER */
			0x36,		/* FC_POINTER */
/* 3348 */	0x36,		/* FC_POINTER */
			0x36,		/* FC_POINTER */
/* 3350 */	0x36,		/* FC_POINTER */
			0x8,		/* FC_LONG */
/* 3352 */	0x8,		/* FC_LONG */
			0x5b,		/* FC_END */
/* 3354 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 3356 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 3358 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 3360 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 3362 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 3364 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 3366 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 3368 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 3370 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 3372 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 3374 */	
			0x21,		/* FC_BOGUS_ARRAY */
			0x3,		/* 3 */
/* 3376 */	NdrFcShort( 0x0 ),	/* 0 */
/* 3378 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 3380 */	NdrFcShort( 0x0 ),	/* 0 */
/* 3382 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 3384 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 3386 */	NdrFcLong( 0x0 ),	/* 0 */
/* 3390 */	NdrFcLong( 0x2710 ),	/* 10000 */
/* 3394 */	NdrFcLong( 0xffffffff ),	/* -1 */
/* 3398 */	NdrFcShort( 0x0 ),	/* Corr flags:  */
/* 3400 */	0x0 , 
			0x0,		/* 0 */
/* 3402 */	NdrFcLong( 0x0 ),	/* 0 */
/* 3406 */	NdrFcLong( 0x0 ),	/* 0 */
/* 3410 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 3412 */	NdrFcShort( 0xffb6 ),	/* Offset= -74 (3338) */
/* 3414 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 3416 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 3418 */	NdrFcShort( 0x10 ),	/* 16 */
/* 3420 */	NdrFcShort( 0x0 ),	/* 0 */
/* 3422 */	NdrFcShort( 0x6 ),	/* Offset= 6 (3428) */
/* 3424 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 3426 */	0x36,		/* FC_POINTER */
			0x5b,		/* FC_END */
/* 3428 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 3430 */	NdrFcShort( 0xffc8 ),	/* Offset= -56 (3374) */
/* 3432 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 3434 */	NdrFcShort( 0x88 ),	/* 136 */
/* 3436 */	NdrFcShort( 0x0 ),	/* 0 */
/* 3438 */	NdrFcShort( 0x1e ),	/* Offset= 30 (3468) */
/* 3440 */	0x36,		/* FC_POINTER */
			0x36,		/* FC_POINTER */
/* 3442 */	0x36,		/* FC_POINTER */
			0x36,		/* FC_POINTER */
/* 3444 */	0x36,		/* FC_POINTER */
			0x36,		/* FC_POINTER */
/* 3446 */	0x36,		/* FC_POINTER */
			0x8,		/* FC_LONG */
/* 3448 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 3450 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 3452 */	NdrFcShort( 0xf290 ),	/* Offset= -3440 (12) */
/* 3454 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 3456 */	NdrFcShort( 0xf28c ),	/* Offset= -3444 (12) */
/* 3458 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 3460 */	NdrFcShort( 0xf288 ),	/* Offset= -3448 (12) */
/* 3462 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 3464 */	NdrFcShort( 0xf284 ),	/* Offset= -3452 (12) */
/* 3466 */	0x40,		/* FC_STRUCTPAD4 */
			0x5b,		/* FC_END */
/* 3468 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 3470 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 3472 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 3474 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 3476 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 3478 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 3480 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 3482 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 3484 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 3486 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 3488 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 3490 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 3492 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 3494 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 3496 */	
			0x21,		/* FC_BOGUS_ARRAY */
			0x3,		/* 3 */
/* 3498 */	NdrFcShort( 0x0 ),	/* 0 */
/* 3500 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 3502 */	NdrFcShort( 0x0 ),	/* 0 */
/* 3504 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 3506 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 3508 */	NdrFcLong( 0x0 ),	/* 0 */
/* 3512 */	NdrFcLong( 0x2710 ),	/* 10000 */
/* 3516 */	NdrFcLong( 0xffffffff ),	/* -1 */
/* 3520 */	NdrFcShort( 0x0 ),	/* Corr flags:  */
/* 3522 */	0x0 , 
			0x0,		/* 0 */
/* 3524 */	NdrFcLong( 0x0 ),	/* 0 */
/* 3528 */	NdrFcLong( 0x0 ),	/* 0 */
/* 3532 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 3534 */	NdrFcShort( 0xff9a ),	/* Offset= -102 (3432) */
/* 3536 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 3538 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 3540 */	NdrFcShort( 0x10 ),	/* 16 */
/* 3542 */	NdrFcShort( 0x0 ),	/* 0 */
/* 3544 */	NdrFcShort( 0x6 ),	/* Offset= 6 (3550) */
/* 3546 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 3548 */	0x36,		/* FC_POINTER */
			0x5b,		/* FC_END */
/* 3550 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 3552 */	NdrFcShort( 0xffc8 ),	/* Offset= -56 (3496) */
/* 3554 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 3556 */	NdrFcShort( 0x88 ),	/* 136 */
/* 3558 */	NdrFcShort( 0x0 ),	/* 0 */
/* 3560 */	NdrFcShort( 0x1e ),	/* Offset= 30 (3590) */
/* 3562 */	0x36,		/* FC_POINTER */
			0x36,		/* FC_POINTER */
/* 3564 */	0x36,		/* FC_POINTER */
			0x36,		/* FC_POINTER */
/* 3566 */	0x36,		/* FC_POINTER */
			0x36,		/* FC_POINTER */
/* 3568 */	0x36,		/* FC_POINTER */
			0x8,		/* FC_LONG */
/* 3570 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 3572 */	0x8,		/* FC_LONG */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 3574 */	0x0,		/* 0 */
			NdrFcShort( 0xf215 ),	/* Offset= -3563 (12) */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 3578 */	0x0,		/* 0 */
			NdrFcShort( 0xf211 ),	/* Offset= -3567 (12) */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 3582 */	0x0,		/* 0 */
			NdrFcShort( 0xf20d ),	/* Offset= -3571 (12) */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 3586 */	0x0,		/* 0 */
			NdrFcShort( 0xf209 ),	/* Offset= -3575 (12) */
			0x5b,		/* FC_END */
/* 3590 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 3592 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 3594 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 3596 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 3598 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 3600 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 3602 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 3604 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 3606 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 3608 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 3610 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 3612 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 3614 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 3616 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 3618 */	
			0x21,		/* FC_BOGUS_ARRAY */
			0x3,		/* 3 */
/* 3620 */	NdrFcShort( 0x0 ),	/* 0 */
/* 3622 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 3624 */	NdrFcShort( 0x0 ),	/* 0 */
/* 3626 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 3628 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 3630 */	NdrFcLong( 0x0 ),	/* 0 */
/* 3634 */	NdrFcLong( 0x2710 ),	/* 10000 */
/* 3638 */	NdrFcLong( 0xffffffff ),	/* -1 */
/* 3642 */	NdrFcShort( 0x0 ),	/* Corr flags:  */
/* 3644 */	0x0 , 
			0x0,		/* 0 */
/* 3646 */	NdrFcLong( 0x0 ),	/* 0 */
/* 3650 */	NdrFcLong( 0x0 ),	/* 0 */
/* 3654 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 3656 */	NdrFcShort( 0xff9a ),	/* Offset= -102 (3554) */
/* 3658 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 3660 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 3662 */	NdrFcShort( 0x10 ),	/* 16 */
/* 3664 */	NdrFcShort( 0x0 ),	/* 0 */
/* 3666 */	NdrFcShort( 0x6 ),	/* Offset= 6 (3672) */
/* 3668 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 3670 */	0x36,		/* FC_POINTER */
			0x5b,		/* FC_END */
/* 3672 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 3674 */	NdrFcShort( 0xffc8 ),	/* Offset= -56 (3618) */
/* 3676 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 3678 */	NdrFcShort( 0x20 ),	/* 32 */
/* 3680 */	NdrFcShort( 0x0 ),	/* 0 */
/* 3682 */	NdrFcShort( 0xa ),	/* Offset= 10 (3692) */
/* 3684 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 3686 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 3688 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 3690 */	0x36,		/* FC_POINTER */
			0x5b,		/* FC_END */
/* 3692 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 3694 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 3696 */	
			0x21,		/* FC_BOGUS_ARRAY */
			0x3,		/* 3 */
/* 3698 */	NdrFcShort( 0x0 ),	/* 0 */
/* 3700 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 3702 */	NdrFcShort( 0x0 ),	/* 0 */
/* 3704 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 3706 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 3708 */	NdrFcLong( 0x0 ),	/* 0 */
/* 3712 */	NdrFcLong( 0x2710 ),	/* 10000 */
/* 3716 */	NdrFcLong( 0xffffffff ),	/* -1 */
/* 3720 */	NdrFcShort( 0x0 ),	/* Corr flags:  */
/* 3722 */	0x0 , 
			0x0,		/* 0 */
/* 3724 */	NdrFcLong( 0x0 ),	/* 0 */
/* 3728 */	NdrFcLong( 0x0 ),	/* 0 */
/* 3732 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 3734 */	NdrFcShort( 0xffc6 ),	/* Offset= -58 (3676) */
/* 3736 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 3738 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 3740 */	NdrFcShort( 0x10 ),	/* 16 */
/* 3742 */	NdrFcShort( 0x0 ),	/* 0 */
/* 3744 */	NdrFcShort( 0x6 ),	/* Offset= 6 (3750) */
/* 3746 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 3748 */	0x36,		/* FC_POINTER */
			0x5b,		/* FC_END */
/* 3750 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 3752 */	NdrFcShort( 0xffc8 ),	/* Offset= -56 (3696) */
/* 3754 */	
			0x11, 0x0,	/* FC_RP */
/* 3756 */	NdrFcShort( 0x2 ),	/* Offset= 2 (3758) */
/* 3758 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 3760 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x0,		/*  */
/* 3762 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 3764 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 3766 */	0x0 , 
			0x0,		/* 0 */
/* 3768 */	NdrFcLong( 0x0 ),	/* 0 */
/* 3772 */	NdrFcLong( 0x0 ),	/* 0 */
/* 3776 */	NdrFcShort( 0x2 ),	/* Offset= 2 (3778) */
/* 3778 */	NdrFcShort( 0x30 ),	/* 48 */
/* 3780 */	NdrFcShort( 0x3 ),	/* 3 */
/* 3782 */	NdrFcLong( 0x1 ),	/* 1 */
/* 3786 */	NdrFcShort( 0x10 ),	/* Offset= 16 (3802) */
/* 3788 */	NdrFcLong( 0x2 ),	/* 2 */
/* 3792 */	NdrFcShort( 0x2e ),	/* Offset= 46 (3838) */
/* 3794 */	NdrFcLong( 0x3 ),	/* 3 */
/* 3798 */	NdrFcShort( 0x36 ),	/* Offset= 54 (3852) */
/* 3800 */	NdrFcShort( 0xffff ),	/* Offset= -1 (3799) */
/* 3802 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 3804 */	NdrFcShort( 0x18 ),	/* 24 */
/* 3806 */	NdrFcShort( 0x0 ),	/* 0 */
/* 3808 */	NdrFcShort( 0x8 ),	/* Offset= 8 (3816) */
/* 3810 */	0x36,		/* FC_POINTER */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 3812 */	0x0,		/* 0 */
			NdrFcShort( 0xf4db ),	/* Offset= -2853 (960) */
			0x5b,		/* FC_END */
/* 3816 */	
			0x11, 0x0,	/* FC_RP */
/* 3818 */	NdrFcShort( 0xf1b2 ),	/* Offset= -3662 (156) */
/* 3820 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 3822 */	NdrFcShort( 0x28 ),	/* 40 */
/* 3824 */	NdrFcShort( 0x0 ),	/* 0 */
/* 3826 */	NdrFcShort( 0x8 ),	/* Offset= 8 (3834) */
/* 3828 */	0x36,		/* FC_POINTER */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 3830 */	0x0,		/* 0 */
			NdrFcShort( 0xf4d9 ),	/* Offset= -2855 (976) */
			0x5b,		/* FC_END */
/* 3834 */	
			0x12, 0x0,	/* FC_UP */
/* 3836 */	NdrFcShort( 0xfff0 ),	/* Offset= -16 (3820) */
/* 3838 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 3840 */	NdrFcShort( 0x28 ),	/* 40 */
/* 3842 */	NdrFcShort( 0x0 ),	/* 0 */
/* 3844 */	NdrFcShort( 0x0 ),	/* Offset= 0 (3844) */
/* 3846 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 3848 */	NdrFcShort( 0xffe4 ),	/* Offset= -28 (3820) */
/* 3850 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 3852 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 3854 */	NdrFcShort( 0x30 ),	/* 48 */
/* 3856 */	NdrFcShort( 0x0 ),	/* 0 */
/* 3858 */	NdrFcShort( 0x8 ),	/* Offset= 8 (3866) */
/* 3860 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 3862 */	NdrFcShort( 0xffd6 ),	/* Offset= -42 (3820) */
/* 3864 */	0x36,		/* FC_POINTER */
			0x5b,		/* FC_END */
/* 3866 */	
			0x12, 0x0,	/* FC_UP */
/* 3868 */	NdrFcShort( 0xf9f8 ),	/* Offset= -1544 (2324) */
/* 3870 */	
			0x11, 0x0,	/* FC_RP */
/* 3872 */	NdrFcShort( 0x2 ),	/* Offset= 2 (3874) */
/* 3874 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 3876 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x54,		/* FC_DEREFERENCE */
/* 3878 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 3880 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 3882 */	0x0 , 
			0x0,		/* 0 */
/* 3884 */	NdrFcLong( 0x0 ),	/* 0 */
/* 3888 */	NdrFcLong( 0x0 ),	/* 0 */
/* 3892 */	NdrFcShort( 0x2 ),	/* Offset= 2 (3894) */
/* 3894 */	NdrFcShort( 0x40 ),	/* 64 */
/* 3896 */	NdrFcShort( 0x3 ),	/* 3 */
/* 3898 */	NdrFcLong( 0x1 ),	/* 1 */
/* 3902 */	NdrFcShort( 0x10 ),	/* Offset= 16 (3918) */
/* 3904 */	NdrFcLong( 0x2 ),	/* 2 */
/* 3908 */	NdrFcShort( 0x5a ),	/* Offset= 90 (3998) */
/* 3910 */	NdrFcLong( 0x3 ),	/* 3 */
/* 3914 */	NdrFcShort( 0x1f2 ),	/* Offset= 498 (4412) */
/* 3916 */	NdrFcShort( 0xffff ),	/* Offset= -1 (3915) */
/* 3918 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 3920 */	NdrFcShort( 0x40 ),	/* 64 */
/* 3922 */	NdrFcShort( 0x0 ),	/* 0 */
/* 3924 */	NdrFcShort( 0x0 ),	/* Offset= 0 (3924) */
/* 3926 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 3928 */	NdrFcShort( 0xf0b4 ),	/* Offset= -3916 (12) */
/* 3930 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 3932 */	NdrFcShort( 0xf120 ),	/* Offset= -3808 (124) */
/* 3934 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 3936 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 3938 */	0x6,		/* FC_SHORT */
			0x3e,		/* FC_STRUCTPAD2 */
/* 3940 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 3942 */	
			0x15,		/* FC_STRUCT */
			0x3,		/* 3 */
/* 3944 */	NdrFcShort( 0x2c ),	/* 44 */
/* 3946 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 3948 */	NdrFcShort( 0xf0a0 ),	/* Offset= -3936 (12) */
/* 3950 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 3952 */	NdrFcShort( 0xf10c ),	/* Offset= -3828 (124) */
/* 3954 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 3956 */	
			0x21,		/* FC_BOGUS_ARRAY */
			0x3,		/* 3 */
/* 3958 */	NdrFcShort( 0x0 ),	/* 0 */
/* 3960 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 3962 */	NdrFcShort( 0x1c ),	/* 28 */
/* 3964 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 3966 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 3968 */	NdrFcLong( 0x0 ),	/* 0 */
/* 3972 */	NdrFcLong( 0x2710 ),	/* 10000 */
/* 3976 */	NdrFcLong( 0xffffffff ),	/* -1 */
/* 3980 */	NdrFcShort( 0x0 ),	/* Corr flags:  */
/* 3982 */	0x0 , 
			0x0,		/* 0 */
/* 3984 */	NdrFcLong( 0x0 ),	/* 0 */
/* 3988 */	NdrFcLong( 0x0 ),	/* 0 */
/* 3992 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 3994 */	NdrFcShort( 0xffcc ),	/* Offset= -52 (3942) */
/* 3996 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 3998 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 4000 */	NdrFcShort( 0x28 ),	/* 40 */
/* 4002 */	NdrFcShort( 0x0 ),	/* 0 */
/* 4004 */	NdrFcShort( 0xc ),	/* Offset= 12 (4016) */
/* 4006 */	0x36,		/* FC_POINTER */
			0x8,		/* FC_LONG */
/* 4008 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 4010 */	0x8,		/* FC_LONG */
			0x6,		/* FC_SHORT */
/* 4012 */	0x3e,		/* FC_STRUCTPAD2 */
			0x8,		/* FC_LONG */
/* 4014 */	0x36,		/* FC_POINTER */
			0x5b,		/* FC_END */
/* 4016 */	
			0x12, 0x0,	/* FC_UP */
/* 4018 */	NdrFcShort( 0xf0ea ),	/* Offset= -3862 (156) */
/* 4020 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 4022 */	NdrFcShort( 0xffbe ),	/* Offset= -66 (3956) */
/* 4024 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 4026 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 4028 */	NdrFcShort( 0x8 ),	/* 8 */
/* 4030 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 4032 */	0x0 , 
			0x0,		/* 0 */
/* 4034 */	NdrFcLong( 0x0 ),	/* 0 */
/* 4038 */	NdrFcLong( 0x0 ),	/* 0 */
/* 4042 */	NdrFcShort( 0x2 ),	/* Offset= 2 (4044) */
/* 4044 */	NdrFcShort( 0x10 ),	/* 16 */
/* 4046 */	NdrFcShort( 0x1 ),	/* 1 */
/* 4048 */	NdrFcLong( 0x1 ),	/* 1 */
/* 4052 */	NdrFcShort( 0x12e ),	/* Offset= 302 (4354) */
/* 4054 */	NdrFcShort( 0xffff ),	/* Offset= -1 (4053) */
/* 4056 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 4058 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 4060 */	NdrFcShort( 0x4 ),	/* 4 */
/* 4062 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 4064 */	0x0 , 
			0x0,		/* 0 */
/* 4066 */	NdrFcLong( 0x0 ),	/* 0 */
/* 4070 */	NdrFcLong( 0x0 ),	/* 0 */
/* 4074 */	NdrFcShort( 0x2 ),	/* Offset= 2 (4076) */
/* 4076 */	NdrFcShort( 0x40 ),	/* 64 */
/* 4078 */	NdrFcShort( 0x7 ),	/* 7 */
/* 4080 */	NdrFcLong( 0x1 ),	/* 1 */
/* 4084 */	NdrFcShort( 0x4e ),	/* Offset= 78 (4162) */
/* 4086 */	NdrFcLong( 0x2 ),	/* 2 */
/* 4090 */	NdrFcShort( 0x5c ),	/* Offset= 92 (4182) */
/* 4092 */	NdrFcLong( 0x3 ),	/* 3 */
/* 4096 */	NdrFcShort( 0xe2 ),	/* Offset= 226 (4322) */
/* 4098 */	NdrFcLong( 0x4 ),	/* 4 */
/* 4102 */	NdrFcShort( 0xee ),	/* Offset= 238 (4340) */
/* 4104 */	NdrFcLong( 0x5 ),	/* 5 */
/* 4108 */	NdrFcShort( 0xe8 ),	/* Offset= 232 (4340) */
/* 4110 */	NdrFcLong( 0x6 ),	/* 6 */
/* 4114 */	NdrFcShort( 0xe2 ),	/* Offset= 226 (4340) */
/* 4116 */	NdrFcLong( 0x7 ),	/* 7 */
/* 4120 */	NdrFcShort( 0xdc ),	/* Offset= 220 (4340) */
/* 4122 */	NdrFcShort( 0xffff ),	/* Offset= -1 (4121) */
/* 4124 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 4126 */	NdrFcShort( 0x28 ),	/* 40 */
/* 4128 */	NdrFcShort( 0x0 ),	/* 0 */
/* 4130 */	NdrFcShort( 0x0 ),	/* Offset= 0 (4130) */
/* 4132 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 4134 */	0x8,		/* FC_LONG */
			0x6,		/* FC_SHORT */
/* 4136 */	0x3e,		/* FC_STRUCTPAD2 */
			0x8,		/* FC_LONG */
/* 4138 */	0x8,		/* FC_LONG */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 4140 */	0x0,		/* 0 */
			NdrFcShort( 0xf30f ),	/* Offset= -3313 (828) */
			0x5b,		/* FC_END */
/* 4144 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 4146 */	NdrFcShort( 0x30 ),	/* 48 */
/* 4148 */	NdrFcShort( 0x0 ),	/* 0 */
/* 4150 */	NdrFcShort( 0x8 ),	/* Offset= 8 (4158) */
/* 4152 */	0x36,		/* FC_POINTER */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 4154 */	0x0,		/* 0 */
			NdrFcShort( 0xffe1 ),	/* Offset= -31 (4124) */
			0x5b,		/* FC_END */
/* 4158 */	
			0x12, 0x0,	/* FC_UP */
/* 4160 */	NdrFcShort( 0xfff0 ),	/* Offset= -16 (4144) */
/* 4162 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 4164 */	NdrFcShort( 0x40 ),	/* 64 */
/* 4166 */	NdrFcShort( 0x0 ),	/* 0 */
/* 4168 */	NdrFcShort( 0xa ),	/* Offset= 10 (4178) */
/* 4170 */	0x36,		/* FC_POINTER */
			0x8,		/* FC_LONG */
/* 4172 */	0x40,		/* FC_STRUCTPAD4 */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 4174 */	0x0,		/* 0 */
			NdrFcShort( 0xffe1 ),	/* Offset= -31 (4144) */
			0x5b,		/* FC_END */
/* 4178 */	
			0x12, 0x0,	/* FC_UP */
/* 4180 */	NdrFcShort( 0xf048 ),	/* Offset= -4024 (156) */
/* 4182 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 4184 */	NdrFcShort( 0x18 ),	/* 24 */
/* 4186 */	NdrFcShort( 0x0 ),	/* 0 */
/* 4188 */	NdrFcShort( 0xa ),	/* Offset= 10 (4198) */
/* 4190 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 4192 */	0x8,		/* FC_LONG */
			0x6,		/* FC_SHORT */
/* 4194 */	0x3e,		/* FC_STRUCTPAD2 */
			0x36,		/* FC_POINTER */
/* 4196 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 4198 */	
			0x12, 0x0,	/* FC_UP */
/* 4200 */	NdrFcShort( 0xf034 ),	/* Offset= -4044 (156) */
/* 4202 */	
			0x15,		/* FC_STRUCT */
			0x1,		/* 1 */
/* 4204 */	NdrFcShort( 0x4 ),	/* 4 */
/* 4206 */	0x2,		/* FC_CHAR */
			0x2,		/* FC_CHAR */
/* 4208 */	0x6,		/* FC_SHORT */
			0x5b,		/* FC_END */
/* 4210 */	
			0x1c,		/* FC_CVARRAY */
			0x1,		/* 1 */
/* 4212 */	NdrFcShort( 0x2 ),	/* 2 */
/* 4214 */	0x17,		/* Corr desc:  field pointer, FC_USHORT */
			0x55,		/* FC_DIV_2 */
/* 4216 */	NdrFcShort( 0x2 ),	/* 2 */
/* 4218 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 4220 */	0x0 , 
			0x0,		/* 0 */
/* 4222 */	NdrFcLong( 0x0 ),	/* 0 */
/* 4226 */	NdrFcLong( 0x0 ),	/* 0 */
/* 4230 */	0x17,		/* Corr desc:  field pointer, FC_USHORT */
			0x55,		/* FC_DIV_2 */
/* 4232 */	NdrFcShort( 0x0 ),	/* 0 */
/* 4234 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 4236 */	0x0 , 
			0x0,		/* 0 */
/* 4238 */	NdrFcLong( 0x0 ),	/* 0 */
/* 4242 */	NdrFcLong( 0x0 ),	/* 0 */
/* 4246 */	0x5,		/* FC_WCHAR */
			0x5b,		/* FC_END */
/* 4248 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 4250 */	NdrFcShort( 0x10 ),	/* 16 */
/* 4252 */	NdrFcShort( 0x0 ),	/* 0 */
/* 4254 */	NdrFcShort( 0x8 ),	/* Offset= 8 (4262) */
/* 4256 */	0x6,		/* FC_SHORT */
			0x6,		/* FC_SHORT */
/* 4258 */	0x40,		/* FC_STRUCTPAD4 */
			0x36,		/* FC_POINTER */
/* 4260 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 4262 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 4264 */	NdrFcShort( 0xffca ),	/* Offset= -54 (4210) */
/* 4266 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 4268 */	NdrFcShort( 0x10 ),	/* 16 */
/* 4270 */	NdrFcShort( 0x0 ),	/* 0 */
/* 4272 */	NdrFcShort( 0x6 ),	/* Offset= 6 (4278) */
/* 4274 */	0x36,		/* FC_POINTER */
			0x36,		/* FC_POINTER */
/* 4276 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 4278 */	
			0x12, 0x0,	/* FC_UP */
/* 4280 */	NdrFcShort( 0xfff2 ),	/* Offset= -14 (4266) */
/* 4282 */	
			0x12, 0x0,	/* FC_UP */
/* 4284 */	NdrFcShort( 0xffdc ),	/* Offset= -36 (4248) */
/* 4286 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 4288 */	NdrFcShort( 0x30 ),	/* 48 */
/* 4290 */	NdrFcShort( 0x0 ),	/* 0 */
/* 4292 */	NdrFcShort( 0x12 ),	/* Offset= 18 (4310) */
/* 4294 */	0x36,		/* FC_POINTER */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 4296 */	0x0,		/* 0 */
			NdrFcShort( 0xffa1 ),	/* Offset= -95 (4202) */
			0x6,		/* FC_SHORT */
/* 4300 */	0x6,		/* FC_SHORT */
			0x6,		/* FC_SHORT */
/* 4302 */	0x6,		/* FC_SHORT */
			0x40,		/* FC_STRUCTPAD4 */
/* 4304 */	0x36,		/* FC_POINTER */
			0x36,		/* FC_POINTER */
/* 4306 */	0x8,		/* FC_LONG */
			0x2,		/* FC_CHAR */
/* 4308 */	0x3f,		/* FC_STRUCTPAD3 */
			0x5b,		/* FC_END */
/* 4310 */	
			0x12, 0x0,	/* FC_UP */
/* 4312 */	NdrFcShort( 0xefc4 ),	/* Offset= -4156 (156) */
/* 4314 */	
			0x12, 0x0,	/* FC_UP */
/* 4316 */	NdrFcShort( 0xffce ),	/* Offset= -50 (4266) */
/* 4318 */	
			0x12, 0x0,	/* FC_UP */
/* 4320 */	NdrFcShort( 0xffde ),	/* Offset= -34 (4286) */
/* 4322 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 4324 */	NdrFcShort( 0x40 ),	/* 64 */
/* 4326 */	NdrFcShort( 0x0 ),	/* 0 */
/* 4328 */	NdrFcShort( 0x0 ),	/* Offset= 0 (4328) */
/* 4330 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 4332 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 4334 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 4336 */	NdrFcShort( 0xffce ),	/* Offset= -50 (4286) */
/* 4338 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 4340 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 4342 */	NdrFcShort( 0x10 ),	/* 16 */
/* 4344 */	NdrFcShort( 0x0 ),	/* 0 */
/* 4346 */	NdrFcShort( 0x0 ),	/* Offset= 0 (4346) */
/* 4348 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 4350 */	0x8,		/* FC_LONG */
			0x6,		/* FC_SHORT */
/* 4352 */	0x3e,		/* FC_STRUCTPAD2 */
			0x5b,		/* FC_END */
/* 4354 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 4356 */	NdrFcShort( 0x10 ),	/* 16 */
/* 4358 */	NdrFcShort( 0x0 ),	/* 0 */
/* 4360 */	NdrFcShort( 0x6 ),	/* Offset= 6 (4366) */
/* 4362 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 4364 */	0x36,		/* FC_POINTER */
			0x5b,		/* FC_END */
/* 4366 */	
			0x12, 0x0,	/* FC_UP */
/* 4368 */	NdrFcShort( 0xfec8 ),	/* Offset= -312 (4056) */
/* 4370 */	
			0x21,		/* FC_BOGUS_ARRAY */
			0x3,		/* 3 */
/* 4372 */	NdrFcShort( 0x0 ),	/* 0 */
/* 4374 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 4376 */	NdrFcShort( 0x18 ),	/* 24 */
/* 4378 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 4380 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 4382 */	NdrFcLong( 0x0 ),	/* 0 */
/* 4386 */	NdrFcLong( 0x2710 ),	/* 10000 */
/* 4390 */	NdrFcLong( 0xffffffff ),	/* -1 */
/* 4394 */	NdrFcShort( 0x0 ),	/* Corr flags:  */
/* 4396 */	0x0 , 
			0x0,		/* 0 */
/* 4398 */	NdrFcLong( 0x0 ),	/* 0 */
/* 4402 */	NdrFcLong( 0x0 ),	/* 0 */
/* 4406 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 4408 */	NdrFcShort( 0xfe2e ),	/* Offset= -466 (3942) */
/* 4410 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 4412 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 4414 */	NdrFcShort( 0x28 ),	/* 40 */
/* 4416 */	NdrFcShort( 0x0 ),	/* 0 */
/* 4418 */	NdrFcShort( 0xa ),	/* Offset= 10 (4428) */
/* 4420 */	0x36,		/* FC_POINTER */
			0x8,		/* FC_LONG */
/* 4422 */	0x40,		/* FC_STRUCTPAD4 */
			0x36,		/* FC_POINTER */
/* 4424 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 4426 */	0x36,		/* FC_POINTER */
			0x5b,		/* FC_END */
/* 4428 */	
			0x12, 0x0,	/* FC_UP */
/* 4430 */	NdrFcShort( 0xef4e ),	/* Offset= -4274 (156) */
/* 4432 */	
			0x12, 0x0,	/* FC_UP */
/* 4434 */	NdrFcShort( 0xfe66 ),	/* Offset= -410 (4024) */
/* 4436 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 4438 */	NdrFcShort( 0xffbc ),	/* Offset= -68 (4370) */
/* 4440 */	
			0x11, 0x0,	/* FC_RP */
/* 4442 */	NdrFcShort( 0x2 ),	/* Offset= 2 (4444) */
/* 4444 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 4446 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x0,		/*  */
/* 4448 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 4450 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 4452 */	0x0 , 
			0x0,		/* 0 */
/* 4454 */	NdrFcLong( 0x0 ),	/* 0 */
/* 4458 */	NdrFcLong( 0x0 ),	/* 0 */
/* 4462 */	NdrFcShort( 0x2 ),	/* Offset= 2 (4464) */
/* 4464 */	NdrFcShort( 0x8 ),	/* 8 */
/* 4466 */	NdrFcShort( 0x1 ),	/* 1 */
/* 4468 */	NdrFcLong( 0x1 ),	/* 1 */
/* 4472 */	NdrFcShort( 0x4 ),	/* Offset= 4 (4476) */
/* 4474 */	NdrFcShort( 0xffff ),	/* Offset= -1 (4473) */
/* 4476 */	
			0x15,		/* FC_STRUCT */
			0x3,		/* 3 */
/* 4478 */	NdrFcShort( 0x8 ),	/* 8 */
/* 4480 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 4482 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 4484 */	
			0x11, 0x0,	/* FC_RP */
/* 4486 */	NdrFcShort( 0x2 ),	/* Offset= 2 (4488) */
/* 4488 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 4490 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x0,		/*  */
/* 4492 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 4494 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 4496 */	0x0 , 
			0x0,		/* 0 */
/* 4498 */	NdrFcLong( 0x0 ),	/* 0 */
/* 4502 */	NdrFcLong( 0x0 ),	/* 0 */
/* 4506 */	NdrFcShort( 0x2 ),	/* Offset= 2 (4508) */
/* 4508 */	NdrFcShort( 0x40 ),	/* 64 */
/* 4510 */	NdrFcShort( 0x2 ),	/* 2 */
/* 4512 */	NdrFcLong( 0x1 ),	/* 1 */
/* 4516 */	NdrFcShort( 0xa ),	/* Offset= 10 (4526) */
/* 4518 */	NdrFcLong( 0x2 ),	/* 2 */
/* 4522 */	NdrFcShort( 0x18 ),	/* Offset= 24 (4546) */
/* 4524 */	NdrFcShort( 0xffff ),	/* Offset= -1 (4523) */
/* 4526 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 4528 */	NdrFcShort( 0x20 ),	/* 32 */
/* 4530 */	NdrFcShort( 0x0 ),	/* 0 */
/* 4532 */	NdrFcShort( 0xa ),	/* Offset= 10 (4542) */
/* 4534 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 4536 */	0x36,		/* FC_POINTER */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 4538 */	0x0,		/* 0 */
			NdrFcShort( 0xee51 ),	/* Offset= -4527 (12) */
			0x5b,		/* FC_END */
/* 4542 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 4544 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 4546 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 4548 */	NdrFcShort( 0x40 ),	/* 64 */
/* 4550 */	NdrFcShort( 0x0 ),	/* 0 */
/* 4552 */	NdrFcShort( 0x10 ),	/* Offset= 16 (4568) */
/* 4554 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 4556 */	0x36,		/* FC_POINTER */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 4558 */	0x0,		/* 0 */
			NdrFcShort( 0xee3d ),	/* Offset= -4547 (12) */
			0x8,		/* FC_LONG */
/* 4562 */	0x40,		/* FC_STRUCTPAD4 */
			0x36,		/* FC_POINTER */
/* 4564 */	0x36,		/* FC_POINTER */
			0x8,		/* FC_LONG */
/* 4566 */	0x40,		/* FC_STRUCTPAD4 */
			0x5b,		/* FC_END */
/* 4568 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 4570 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 4572 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 4574 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 4576 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 4578 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 4580 */	
			0x11, 0x4,	/* FC_RP [alloced_on_stack] */
/* 4582 */	NdrFcShort( 0x2 ),	/* Offset= 2 (4584) */
/* 4584 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 4586 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x54,		/* FC_DEREFERENCE */
/* 4588 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 4590 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 4592 */	0x0 , 
			0x0,		/* 0 */
/* 4594 */	NdrFcLong( 0x0 ),	/* 0 */
/* 4598 */	NdrFcLong( 0x0 ),	/* 0 */
/* 4602 */	NdrFcShort( 0x2 ),	/* Offset= 2 (4604) */
/* 4604 */	NdrFcShort( 0x8 ),	/* 8 */
/* 4606 */	NdrFcShort( 0xf ),	/* 15 */
/* 4608 */	NdrFcLong( 0x0 ),	/* 0 */
/* 4612 */	NdrFcShort( 0x58 ),	/* Offset= 88 (4700) */
/* 4614 */	NdrFcLong( 0x1 ),	/* 1 */
/* 4618 */	NdrFcShort( 0xc8 ),	/* Offset= 200 (4818) */
/* 4620 */	NdrFcLong( 0x2 ),	/* 2 */
/* 4624 */	NdrFcShort( 0xea ),	/* Offset= 234 (4858) */
/* 4626 */	NdrFcLong( 0x3 ),	/* 3 */
/* 4630 */	NdrFcShort( 0x138 ),	/* Offset= 312 (4942) */
/* 4632 */	NdrFcLong( 0x4 ),	/* 4 */
/* 4636 */	NdrFcShort( 0x132 ),	/* Offset= 306 (4942) */
/* 4638 */	NdrFcLong( 0x5 ),	/* 5 */
/* 4642 */	NdrFcShort( 0x17e ),	/* Offset= 382 (5024) */
/* 4644 */	NdrFcLong( 0x6 ),	/* 6 */
/* 4648 */	NdrFcShort( 0x1de ),	/* Offset= 478 (5126) */
/* 4650 */	NdrFcLong( 0x7 ),	/* 7 */
/* 4654 */	NdrFcShort( 0x256 ),	/* Offset= 598 (5252) */
/* 4656 */	NdrFcLong( 0x8 ),	/* 8 */
/* 4660 */	NdrFcShort( 0x286 ),	/* Offset= 646 (5306) */
/* 4662 */	NdrFcLong( 0x9 ),	/* 9 */
/* 4666 */	NdrFcShort( 0x2d2 ),	/* Offset= 722 (5388) */
/* 4668 */	NdrFcLong( 0xa ),	/* 10 */
/* 4672 */	NdrFcShort( 0x326 ),	/* Offset= 806 (5478) */
/* 4674 */	NdrFcLong( 0xfffffffa ),	/* -6 */
/* 4678 */	NdrFcShort( 0x38e ),	/* Offset= 910 (5588) */
/* 4680 */	NdrFcLong( 0xfffffffb ),	/* -5 */
/* 4684 */	NdrFcShort( 0x3da ),	/* Offset= 986 (5670) */
/* 4686 */	NdrFcLong( 0xfffffffc ),	/* -4 */
/* 4690 */	NdrFcShort( 0x3d8 ),	/* Offset= 984 (5674) */
/* 4692 */	NdrFcLong( 0xfffffffe ),	/* -2 */
/* 4696 */	NdrFcShort( 0x4 ),	/* Offset= 4 (4700) */
/* 4698 */	NdrFcShort( 0xffff ),	/* Offset= -1 (4697) */
/* 4700 */	
			0x12, 0x0,	/* FC_UP */
/* 4702 */	NdrFcShort( 0x68 ),	/* Offset= 104 (4806) */
/* 4704 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x7,		/* 7 */
/* 4706 */	NdrFcShort( 0x90 ),	/* 144 */
/* 4708 */	NdrFcShort( 0x0 ),	/* 0 */
/* 4710 */	NdrFcShort( 0x26 ),	/* Offset= 38 (4748) */
/* 4712 */	0x36,		/* FC_POINTER */
			0x36,		/* FC_POINTER */
/* 4714 */	0x36,		/* FC_POINTER */
			0x36,		/* FC_POINTER */
/* 4716 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 4718 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 4720 */	NdrFcShort( 0xed9c ),	/* Offset= -4708 (12) */
/* 4722 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 4724 */	NdrFcShort( 0xed98 ),	/* Offset= -4712 (12) */
/* 4726 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 4728 */	NdrFcShort( 0xed94 ),	/* Offset= -4716 (12) */
/* 4730 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 4732 */	NdrFcShort( 0xed90 ),	/* Offset= -4720 (12) */
/* 4734 */	0xb,		/* FC_HYPER */
			0xb,		/* FC_HYPER */
/* 4736 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 4738 */	NdrFcShort( 0xfefa ),	/* Offset= -262 (4476) */
/* 4740 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 4742 */	NdrFcShort( 0xfef6 ),	/* Offset= -266 (4476) */
/* 4744 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 4746 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 4748 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 4750 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 4752 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 4754 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 4756 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 4758 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 4760 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 4762 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 4764 */	
			0x21,		/* FC_BOGUS_ARRAY */
			0x7,		/* 7 */
/* 4766 */	NdrFcShort( 0x0 ),	/* 0 */
/* 4768 */	0x9,		/* Corr desc: FC_ULONG */
			0x0,		/*  */
/* 4770 */	NdrFcShort( 0xfff8 ),	/* -8 */
/* 4772 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 4774 */	0x0 , 
			0x0,		/* 0 */
/* 4776 */	NdrFcLong( 0x0 ),	/* 0 */
/* 4780 */	NdrFcLong( 0x0 ),	/* 0 */
/* 4784 */	NdrFcLong( 0xffffffff ),	/* -1 */
/* 4788 */	NdrFcShort( 0x0 ),	/* Corr flags:  */
/* 4790 */	0x0 , 
			0x0,		/* 0 */
/* 4792 */	NdrFcLong( 0x0 ),	/* 0 */
/* 4796 */	NdrFcLong( 0x0 ),	/* 0 */
/* 4800 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 4802 */	NdrFcShort( 0xff9e ),	/* Offset= -98 (4704) */
/* 4804 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 4806 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x7,		/* 7 */
/* 4808 */	NdrFcShort( 0x8 ),	/* 8 */
/* 4810 */	NdrFcShort( 0xffd2 ),	/* Offset= -46 (4764) */
/* 4812 */	NdrFcShort( 0x0 ),	/* Offset= 0 (4812) */
/* 4814 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 4816 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 4818 */	
			0x12, 0x0,	/* FC_UP */
/* 4820 */	NdrFcShort( 0x1c ),	/* Offset= 28 (4848) */
/* 4822 */	
			0x1b,		/* FC_CARRAY */
			0x7,		/* 7 */
/* 4824 */	NdrFcShort( 0x18 ),	/* 24 */
/* 4826 */	0x9,		/* Corr desc: FC_ULONG */
			0x0,		/*  */
/* 4828 */	NdrFcShort( 0xfff8 ),	/* -8 */
/* 4830 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 4832 */	0x0 , 
			0x0,		/* 0 */
/* 4834 */	NdrFcLong( 0x0 ),	/* 0 */
/* 4838 */	NdrFcLong( 0x0 ),	/* 0 */
/* 4842 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 4844 */	NdrFcShort( 0xee90 ),	/* Offset= -4464 (380) */
/* 4846 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 4848 */	
			0x17,		/* FC_CSTRUCT */
			0x7,		/* 7 */
/* 4850 */	NdrFcShort( 0x8 ),	/* 8 */
/* 4852 */	NdrFcShort( 0xffe2 ),	/* Offset= -30 (4822) */
/* 4854 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 4856 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 4858 */	
			0x12, 0x0,	/* FC_UP */
/* 4860 */	NdrFcShort( 0x46 ),	/* Offset= 70 (4930) */
/* 4862 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x7,		/* 7 */
/* 4864 */	NdrFcShort( 0x38 ),	/* 56 */
/* 4866 */	NdrFcShort( 0x0 ),	/* 0 */
/* 4868 */	NdrFcShort( 0x10 ),	/* Offset= 16 (4884) */
/* 4870 */	0x36,		/* FC_POINTER */
			0x8,		/* FC_LONG */
/* 4872 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 4874 */	NdrFcShort( 0xfe72 ),	/* Offset= -398 (4476) */
/* 4876 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 4878 */	NdrFcShort( 0xecfe ),	/* Offset= -4866 (12) */
/* 4880 */	0x40,		/* FC_STRUCTPAD4 */
			0xb,		/* FC_HYPER */
/* 4882 */	0xb,		/* FC_HYPER */
			0x5b,		/* FC_END */
/* 4884 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 4886 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 4888 */	
			0x21,		/* FC_BOGUS_ARRAY */
			0x7,		/* 7 */
/* 4890 */	NdrFcShort( 0x0 ),	/* 0 */
/* 4892 */	0x9,		/* Corr desc: FC_ULONG */
			0x0,		/*  */
/* 4894 */	NdrFcShort( 0xfff8 ),	/* -8 */
/* 4896 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 4898 */	0x0 , 
			0x0,		/* 0 */
/* 4900 */	NdrFcLong( 0x0 ),	/* 0 */
/* 4904 */	NdrFcLong( 0x0 ),	/* 0 */
/* 4908 */	NdrFcLong( 0xffffffff ),	/* -1 */
/* 4912 */	NdrFcShort( 0x0 ),	/* Corr flags:  */
/* 4914 */	0x0 , 
			0x0,		/* 0 */
/* 4916 */	NdrFcLong( 0x0 ),	/* 0 */
/* 4920 */	NdrFcLong( 0x0 ),	/* 0 */
/* 4924 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 4926 */	NdrFcShort( 0xffc0 ),	/* Offset= -64 (4862) */
/* 4928 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 4930 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x7,		/* 7 */
/* 4932 */	NdrFcShort( 0x8 ),	/* 8 */
/* 4934 */	NdrFcShort( 0xffd2 ),	/* Offset= -46 (4888) */
/* 4936 */	NdrFcShort( 0x0 ),	/* Offset= 0 (4936) */
/* 4938 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 4940 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 4942 */	
			0x12, 0x0,	/* FC_UP */
/* 4944 */	NdrFcShort( 0x44 ),	/* Offset= 68 (5012) */
/* 4946 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 4948 */	NdrFcShort( 0x28 ),	/* 40 */
/* 4950 */	NdrFcShort( 0x0 ),	/* 0 */
/* 4952 */	NdrFcShort( 0xe ),	/* Offset= 14 (4966) */
/* 4954 */	0x36,		/* FC_POINTER */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 4956 */	0x0,		/* 0 */
			NdrFcShort( 0xecaf ),	/* Offset= -4945 (12) */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 4960 */	0x0,		/* 0 */
			NdrFcShort( 0xfe1b ),	/* Offset= -485 (4476) */
			0x8,		/* FC_LONG */
/* 4964 */	0x8,		/* FC_LONG */
			0x5b,		/* FC_END */
/* 4966 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 4968 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 4970 */	
			0x21,		/* FC_BOGUS_ARRAY */
			0x3,		/* 3 */
/* 4972 */	NdrFcShort( 0x0 ),	/* 0 */
/* 4974 */	0x9,		/* Corr desc: FC_ULONG */
			0x0,		/*  */
/* 4976 */	NdrFcShort( 0xfff8 ),	/* -8 */
/* 4978 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 4980 */	0x0 , 
			0x0,		/* 0 */
/* 4982 */	NdrFcLong( 0x0 ),	/* 0 */
/* 4986 */	NdrFcLong( 0x0 ),	/* 0 */
/* 4990 */	NdrFcLong( 0xffffffff ),	/* -1 */
/* 4994 */	NdrFcShort( 0x0 ),	/* Corr flags:  */
/* 4996 */	0x0 , 
			0x0,		/* 0 */
/* 4998 */	NdrFcLong( 0x0 ),	/* 0 */
/* 5002 */	NdrFcLong( 0x0 ),	/* 0 */
/* 5006 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 5008 */	NdrFcShort( 0xffc2 ),	/* Offset= -62 (4946) */
/* 5010 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 5012 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 5014 */	NdrFcShort( 0x8 ),	/* 8 */
/* 5016 */	NdrFcShort( 0xffd2 ),	/* Offset= -46 (4970) */
/* 5018 */	NdrFcShort( 0x0 ),	/* Offset= 0 (5018) */
/* 5020 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 5022 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 5024 */	
			0x12, 0x0,	/* FC_UP */
/* 5026 */	NdrFcShort( 0x54 ),	/* Offset= 84 (5110) */
/* 5028 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 5030 */	NdrFcShort( 0x50 ),	/* 80 */
/* 5032 */	NdrFcShort( 0x0 ),	/* 0 */
/* 5034 */	NdrFcShort( 0x16 ),	/* Offset= 22 (5056) */
/* 5036 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 5038 */	NdrFcShort( 0xfdce ),	/* Offset= -562 (4476) */
/* 5040 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 5042 */	0xd,		/* FC_ENUM16 */
			0x8,		/* FC_LONG */
/* 5044 */	0x36,		/* FC_POINTER */
			0x36,		/* FC_POINTER */
/* 5046 */	0x36,		/* FC_POINTER */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 5048 */	0x0,		/* 0 */
			NdrFcShort( 0xec53 ),	/* Offset= -5037 (12) */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 5052 */	0x0,		/* 0 */
			NdrFcShort( 0xec4f ),	/* Offset= -5041 (12) */
			0x5b,		/* FC_END */
/* 5056 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 5058 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 5060 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 5062 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 5064 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 5066 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 5068 */	
			0x21,		/* FC_BOGUS_ARRAY */
			0x3,		/* 3 */
/* 5070 */	NdrFcShort( 0x0 ),	/* 0 */
/* 5072 */	0x9,		/* Corr desc: FC_ULONG */
			0x0,		/*  */
/* 5074 */	NdrFcShort( 0xfff8 ),	/* -8 */
/* 5076 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 5078 */	0x0 , 
			0x0,		/* 0 */
/* 5080 */	NdrFcLong( 0x0 ),	/* 0 */
/* 5084 */	NdrFcLong( 0x0 ),	/* 0 */
/* 5088 */	NdrFcLong( 0xffffffff ),	/* -1 */
/* 5092 */	NdrFcShort( 0x0 ),	/* Corr flags:  */
/* 5094 */	0x0 , 
			0x0,		/* 0 */
/* 5096 */	NdrFcLong( 0x0 ),	/* 0 */
/* 5100 */	NdrFcLong( 0x0 ),	/* 0 */
/* 5104 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 5106 */	NdrFcShort( 0xffb2 ),	/* Offset= -78 (5028) */
/* 5108 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 5110 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 5112 */	NdrFcShort( 0x10 ),	/* 16 */
/* 5114 */	NdrFcShort( 0xffd2 ),	/* Offset= -46 (5068) */
/* 5116 */	NdrFcShort( 0x0 ),	/* Offset= 0 (5116) */
/* 5118 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 5120 */	NdrFcShort( 0xfd7c ),	/* Offset= -644 (4476) */
/* 5122 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 5124 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 5126 */	
			0x12, 0x0,	/* FC_UP */
/* 5128 */	NdrFcShort( 0x70 ),	/* Offset= 112 (5240) */
/* 5130 */	
			0x1b,		/* FC_CARRAY */
			0x0,		/* 0 */
/* 5132 */	NdrFcShort( 0x1 ),	/* 1 */
/* 5134 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 5136 */	NdrFcShort( 0x10 ),	/* 16 */
/* 5138 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 5140 */	0x0 , 
			0x0,		/* 0 */
/* 5142 */	NdrFcLong( 0x0 ),	/* 0 */
/* 5146 */	NdrFcLong( 0x0 ),	/* 0 */
/* 5150 */	0x1,		/* FC_BYTE */
			0x5b,		/* FC_END */
/* 5152 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x7,		/* 7 */
/* 5154 */	NdrFcShort( 0x60 ),	/* 96 */
/* 5156 */	NdrFcShort( 0x0 ),	/* 0 */
/* 5158 */	NdrFcShort( 0x1c ),	/* Offset= 28 (5186) */
/* 5160 */	0x36,		/* FC_POINTER */
			0x36,		/* FC_POINTER */
/* 5162 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 5164 */	0x36,		/* FC_POINTER */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 5166 */	0x0,		/* 0 */
			NdrFcShort( 0xfd4d ),	/* Offset= -691 (4476) */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 5170 */	0x0,		/* 0 */
			NdrFcShort( 0xfd49 ),	/* Offset= -695 (4476) */
			0x8,		/* FC_LONG */
/* 5174 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 5176 */	NdrFcShort( 0xfd44 ),	/* Offset= -700 (4476) */
/* 5178 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 5180 */	NdrFcShort( 0xebd0 ),	/* Offset= -5168 (12) */
/* 5182 */	0x40,		/* FC_STRUCTPAD4 */
			0xb,		/* FC_HYPER */
/* 5184 */	0xb,		/* FC_HYPER */
			0x5b,		/* FC_END */
/* 5186 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 5188 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 5190 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 5192 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 5194 */	
			0x14, 0x20,	/* FC_FP [maybenull_sizeis] */
/* 5196 */	NdrFcShort( 0xffbe ),	/* Offset= -66 (5130) */
/* 5198 */	
			0x21,		/* FC_BOGUS_ARRAY */
			0x7,		/* 7 */
/* 5200 */	NdrFcShort( 0x0 ),	/* 0 */
/* 5202 */	0x9,		/* Corr desc: FC_ULONG */
			0x0,		/*  */
/* 5204 */	NdrFcShort( 0xfff8 ),	/* -8 */
/* 5206 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 5208 */	0x0 , 
			0x0,		/* 0 */
/* 5210 */	NdrFcLong( 0x0 ),	/* 0 */
/* 5214 */	NdrFcLong( 0x0 ),	/* 0 */
/* 5218 */	NdrFcLong( 0xffffffff ),	/* -1 */
/* 5222 */	NdrFcShort( 0x0 ),	/* Corr flags:  */
/* 5224 */	0x0 , 
			0x0,		/* 0 */
/* 5226 */	NdrFcLong( 0x0 ),	/* 0 */
/* 5230 */	NdrFcLong( 0x0 ),	/* 0 */
/* 5234 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 5236 */	NdrFcShort( 0xffac ),	/* Offset= -84 (5152) */
/* 5238 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 5240 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x7,		/* 7 */
/* 5242 */	NdrFcShort( 0x8 ),	/* 8 */
/* 5244 */	NdrFcShort( 0xffd2 ),	/* Offset= -46 (5198) */
/* 5246 */	NdrFcShort( 0x0 ),	/* Offset= 0 (5246) */
/* 5248 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 5250 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 5252 */	
			0x12, 0x0,	/* FC_UP */
/* 5254 */	NdrFcShort( 0x2a ),	/* Offset= 42 (5296) */
/* 5256 */	
			0x15,		/* FC_STRUCT */
			0x7,		/* 7 */
/* 5258 */	NdrFcShort( 0x20 ),	/* 32 */
/* 5260 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 5262 */	NdrFcShort( 0xeb7e ),	/* Offset= -5250 (12) */
/* 5264 */	0xb,		/* FC_HYPER */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 5266 */	0x0,		/* 0 */
			NdrFcShort( 0xfce9 ),	/* Offset= -791 (4476) */
			0x5b,		/* FC_END */
/* 5270 */	
			0x1b,		/* FC_CARRAY */
			0x7,		/* 7 */
/* 5272 */	NdrFcShort( 0x20 ),	/* 32 */
/* 5274 */	0x9,		/* Corr desc: FC_ULONG */
			0x0,		/*  */
/* 5276 */	NdrFcShort( 0xfff8 ),	/* -8 */
/* 5278 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 5280 */	0x0 , 
			0x0,		/* 0 */
/* 5282 */	NdrFcLong( 0x0 ),	/* 0 */
/* 5286 */	NdrFcLong( 0x0 ),	/* 0 */
/* 5290 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 5292 */	NdrFcShort( 0xffdc ),	/* Offset= -36 (5256) */
/* 5294 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 5296 */	
			0x17,		/* FC_CSTRUCT */
			0x7,		/* 7 */
/* 5298 */	NdrFcShort( 0x8 ),	/* 8 */
/* 5300 */	NdrFcShort( 0xffe2 ),	/* Offset= -30 (5270) */
/* 5302 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 5304 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 5306 */	
			0x12, 0x0,	/* FC_UP */
/* 5308 */	NdrFcShort( 0x44 ),	/* Offset= 68 (5376) */
/* 5310 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x7,		/* 7 */
/* 5312 */	NdrFcShort( 0x28 ),	/* 40 */
/* 5314 */	NdrFcShort( 0x0 ),	/* 0 */
/* 5316 */	NdrFcShort( 0xe ),	/* Offset= 14 (5330) */
/* 5318 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 5320 */	NdrFcShort( 0xeb44 ),	/* Offset= -5308 (12) */
/* 5322 */	0xb,		/* FC_HYPER */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 5324 */	0x0,		/* 0 */
			NdrFcShort( 0xfcaf ),	/* Offset= -849 (4476) */
			0x36,		/* FC_POINTER */
/* 5328 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 5330 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 5332 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 5334 */	
			0x21,		/* FC_BOGUS_ARRAY */
			0x7,		/* 7 */
/* 5336 */	NdrFcShort( 0x0 ),	/* 0 */
/* 5338 */	0x9,		/* Corr desc: FC_ULONG */
			0x0,		/*  */
/* 5340 */	NdrFcShort( 0xfff8 ),	/* -8 */
/* 5342 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 5344 */	0x0 , 
			0x0,		/* 0 */
/* 5346 */	NdrFcLong( 0x0 ),	/* 0 */
/* 5350 */	NdrFcLong( 0x0 ),	/* 0 */
/* 5354 */	NdrFcLong( 0xffffffff ),	/* -1 */
/* 5358 */	NdrFcShort( 0x0 ),	/* Corr flags:  */
/* 5360 */	0x0 , 
			0x0,		/* 0 */
/* 5362 */	NdrFcLong( 0x0 ),	/* 0 */
/* 5366 */	NdrFcLong( 0x0 ),	/* 0 */
/* 5370 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 5372 */	NdrFcShort( 0xffc2 ),	/* Offset= -62 (5310) */
/* 5374 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 5376 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x7,		/* 7 */
/* 5378 */	NdrFcShort( 0x8 ),	/* 8 */
/* 5380 */	NdrFcShort( 0xffd2 ),	/* Offset= -46 (5334) */
/* 5382 */	NdrFcShort( 0x0 ),	/* Offset= 0 (5382) */
/* 5384 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 5386 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 5388 */	
			0x12, 0x0,	/* FC_UP */
/* 5390 */	NdrFcShort( 0x4c ),	/* Offset= 76 (5466) */
/* 5392 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x7,		/* 7 */
/* 5394 */	NdrFcShort( 0x40 ),	/* 64 */
/* 5396 */	NdrFcShort( 0x0 ),	/* 0 */
/* 5398 */	NdrFcShort( 0x12 ),	/* Offset= 18 (5416) */
/* 5400 */	0x36,		/* FC_POINTER */
			0x8,		/* FC_LONG */
/* 5402 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 5404 */	NdrFcShort( 0xfc60 ),	/* Offset= -928 (4476) */
/* 5406 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 5408 */	NdrFcShort( 0xeaec ),	/* Offset= -5396 (12) */
/* 5410 */	0x40,		/* FC_STRUCTPAD4 */
			0xb,		/* FC_HYPER */
/* 5412 */	0xb,		/* FC_HYPER */
			0x36,		/* FC_POINTER */
/* 5414 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 5416 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 5418 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 5420 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 5422 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 5424 */	
			0x21,		/* FC_BOGUS_ARRAY */
			0x7,		/* 7 */
/* 5426 */	NdrFcShort( 0x0 ),	/* 0 */
/* 5428 */	0x9,		/* Corr desc: FC_ULONG */
			0x0,		/*  */
/* 5430 */	NdrFcShort( 0xfff8 ),	/* -8 */
/* 5432 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 5434 */	0x0 , 
			0x0,		/* 0 */
/* 5436 */	NdrFcLong( 0x0 ),	/* 0 */
/* 5440 */	NdrFcLong( 0x0 ),	/* 0 */
/* 5444 */	NdrFcLong( 0xffffffff ),	/* -1 */
/* 5448 */	NdrFcShort( 0x0 ),	/* Corr flags:  */
/* 5450 */	0x0 , 
			0x0,		/* 0 */
/* 5452 */	NdrFcLong( 0x0 ),	/* 0 */
/* 5456 */	NdrFcLong( 0x0 ),	/* 0 */
/* 5460 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 5462 */	NdrFcShort( 0xffba ),	/* Offset= -70 (5392) */
/* 5464 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 5466 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x7,		/* 7 */
/* 5468 */	NdrFcShort( 0x8 ),	/* 8 */
/* 5470 */	NdrFcShort( 0xffd2 ),	/* Offset= -46 (5424) */
/* 5472 */	NdrFcShort( 0x0 ),	/* Offset= 0 (5472) */
/* 5474 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 5476 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 5478 */	
			0x12, 0x0,	/* FC_UP */
/* 5480 */	NdrFcShort( 0x60 ),	/* Offset= 96 (5576) */
/* 5482 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x7,		/* 7 */
/* 5484 */	NdrFcShort( 0x68 ),	/* 104 */
/* 5486 */	NdrFcShort( 0x0 ),	/* 0 */
/* 5488 */	NdrFcShort( 0x1e ),	/* Offset= 30 (5518) */
/* 5490 */	0x36,		/* FC_POINTER */
			0x36,		/* FC_POINTER */
/* 5492 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 5494 */	0x36,		/* FC_POINTER */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 5496 */	0x0,		/* 0 */
			NdrFcShort( 0xfc03 ),	/* Offset= -1021 (4476) */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 5500 */	0x0,		/* 0 */
			NdrFcShort( 0xfbff ),	/* Offset= -1025 (4476) */
			0x8,		/* FC_LONG */
/* 5504 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 5506 */	NdrFcShort( 0xfbfa ),	/* Offset= -1030 (4476) */
/* 5508 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 5510 */	NdrFcShort( 0xea86 ),	/* Offset= -5498 (12) */
/* 5512 */	0x40,		/* FC_STRUCTPAD4 */
			0xb,		/* FC_HYPER */
/* 5514 */	0xb,		/* FC_HYPER */
			0x36,		/* FC_POINTER */
/* 5516 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 5518 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 5520 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 5522 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 5524 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 5526 */	
			0x14, 0x20,	/* FC_FP [maybenull_sizeis] */
/* 5528 */	NdrFcShort( 0xfe72 ),	/* Offset= -398 (5130) */
/* 5530 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 5532 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 5534 */	
			0x21,		/* FC_BOGUS_ARRAY */
			0x7,		/* 7 */
/* 5536 */	NdrFcShort( 0x0 ),	/* 0 */
/* 5538 */	0x9,		/* Corr desc: FC_ULONG */
			0x0,		/*  */
/* 5540 */	NdrFcShort( 0xfff8 ),	/* -8 */
/* 5542 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 5544 */	0x0 , 
			0x0,		/* 0 */
/* 5546 */	NdrFcLong( 0x0 ),	/* 0 */
/* 5550 */	NdrFcLong( 0x0 ),	/* 0 */
/* 5554 */	NdrFcLong( 0xffffffff ),	/* -1 */
/* 5558 */	NdrFcShort( 0x0 ),	/* Corr flags:  */
/* 5560 */	0x0 , 
			0x0,		/* 0 */
/* 5562 */	NdrFcLong( 0x0 ),	/* 0 */
/* 5566 */	NdrFcLong( 0x0 ),	/* 0 */
/* 5570 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 5572 */	NdrFcShort( 0xffa6 ),	/* Offset= -90 (5482) */
/* 5574 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 5576 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x7,		/* 7 */
/* 5578 */	NdrFcShort( 0x8 ),	/* 8 */
/* 5580 */	NdrFcShort( 0xffd2 ),	/* Offset= -46 (5534) */
/* 5582 */	NdrFcShort( 0x0 ),	/* Offset= 0 (5582) */
/* 5584 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 5586 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 5588 */	
			0x12, 0x0,	/* FC_UP */
/* 5590 */	NdrFcShort( 0x44 ),	/* Offset= 68 (5658) */
/* 5592 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x7,		/* 7 */
/* 5594 */	NdrFcShort( 0x30 ),	/* 48 */
/* 5596 */	NdrFcShort( 0x0 ),	/* 0 */
/* 5598 */	NdrFcShort( 0xe ),	/* Offset= 14 (5612) */
/* 5600 */	0x36,		/* FC_POINTER */
			0x8,		/* FC_LONG */
/* 5602 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 5604 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 5606 */	0x40,		/* FC_STRUCTPAD4 */
			0xb,		/* FC_HYPER */
/* 5608 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 5610 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 5612 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 5614 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 5616 */	
			0x21,		/* FC_BOGUS_ARRAY */
			0x7,		/* 7 */
/* 5618 */	NdrFcShort( 0x0 ),	/* 0 */
/* 5620 */	0x9,		/* Corr desc: FC_ULONG */
			0x0,		/*  */
/* 5622 */	NdrFcShort( 0xfff8 ),	/* -8 */
/* 5624 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 5626 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 5628 */	NdrFcLong( 0x0 ),	/* 0 */
/* 5632 */	NdrFcLong( 0x100 ),	/* 256 */
/* 5636 */	NdrFcLong( 0xffffffff ),	/* -1 */
/* 5640 */	NdrFcShort( 0x0 ),	/* Corr flags:  */
/* 5642 */	0x0 , 
			0x0,		/* 0 */
/* 5644 */	NdrFcLong( 0x0 ),	/* 0 */
/* 5648 */	NdrFcLong( 0x0 ),	/* 0 */
/* 5652 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 5654 */	NdrFcShort( 0xffc2 ),	/* Offset= -62 (5592) */
/* 5656 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 5658 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x7,		/* 7 */
/* 5660 */	NdrFcShort( 0x8 ),	/* 8 */
/* 5662 */	NdrFcShort( 0xffd2 ),	/* Offset= -46 (5616) */
/* 5664 */	NdrFcShort( 0x0 ),	/* Offset= 0 (5664) */
/* 5666 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 5668 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 5670 */	
			0x12, 0x0,	/* FC_UP */
/* 5672 */	NdrFcShort( 0xeb78 ),	/* Offset= -5256 (416) */
/* 5674 */	
			0x12, 0x0,	/* FC_UP */
/* 5676 */	NdrFcShort( 0x2c ),	/* Offset= 44 (5720) */
/* 5678 */	
			0x15,		/* FC_STRUCT */
			0x7,		/* 7 */
/* 5680 */	NdrFcShort( 0x30 ),	/* 48 */
/* 5682 */	0xb,		/* FC_HYPER */
			0x8,		/* FC_LONG */
/* 5684 */	0x8,		/* FC_LONG */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 5686 */	0x0,		/* 0 */
			NdrFcShort( 0xe9d5 ),	/* Offset= -5675 (12) */
			0xb,		/* FC_HYPER */
/* 5690 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 5692 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 5694 */	
			0x1b,		/* FC_CARRAY */
			0x7,		/* 7 */
/* 5696 */	NdrFcShort( 0x30 ),	/* 48 */
/* 5698 */	0x9,		/* Corr desc: FC_ULONG */
			0x0,		/*  */
/* 5700 */	NdrFcShort( 0xfff8 ),	/* -8 */
/* 5702 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 5704 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 5706 */	NdrFcLong( 0x0 ),	/* 0 */
/* 5710 */	NdrFcLong( 0x2710 ),	/* 10000 */
/* 5714 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 5716 */	NdrFcShort( 0xffda ),	/* Offset= -38 (5678) */
/* 5718 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 5720 */	
			0x17,		/* FC_CSTRUCT */
			0x7,		/* 7 */
/* 5722 */	NdrFcShort( 0x8 ),	/* 8 */
/* 5724 */	NdrFcShort( 0xffe2 ),	/* Offset= -30 (5694) */
/* 5726 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 5728 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 5730 */	
			0x11, 0x0,	/* FC_RP */
/* 5732 */	NdrFcShort( 0x2 ),	/* Offset= 2 (5734) */
/* 5734 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 5736 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x0,		/*  */
/* 5738 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 5740 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 5742 */	0x0 , 
			0x0,		/* 0 */
/* 5744 */	NdrFcLong( 0x0 ),	/* 0 */
/* 5748 */	NdrFcLong( 0x0 ),	/* 0 */
/* 5752 */	NdrFcShort( 0x2 ),	/* Offset= 2 (5754) */
/* 5754 */	NdrFcShort( 0x60 ),	/* 96 */
/* 5756 */	NdrFcShort( 0x1 ),	/* 1 */
/* 5758 */	NdrFcLong( 0x1 ),	/* 1 */
/* 5762 */	NdrFcShort( 0x46 ),	/* Offset= 70 (5832) */
/* 5764 */	NdrFcShort( 0xffff ),	/* Offset= -1 (5763) */
/* 5766 */	
			0x1b,		/* FC_CARRAY */
			0x1,		/* 1 */
/* 5768 */	NdrFcShort( 0x2 ),	/* 2 */
/* 5770 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 5772 */	NdrFcShort( 0x20 ),	/* 32 */
/* 5774 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 5776 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 5778 */	NdrFcLong( 0x0 ),	/* 0 */
/* 5782 */	NdrFcLong( 0x100 ),	/* 256 */
/* 5786 */	0x5,		/* FC_WCHAR */
			0x5b,		/* FC_END */
/* 5788 */	
			0x1b,		/* FC_CARRAY */
			0x1,		/* 1 */
/* 5790 */	NdrFcShort( 0x2 ),	/* 2 */
/* 5792 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 5794 */	NdrFcShort( 0x30 ),	/* 48 */
/* 5796 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 5798 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 5800 */	NdrFcLong( 0x0 ),	/* 0 */
/* 5804 */	NdrFcLong( 0x100 ),	/* 256 */
/* 5808 */	0x5,		/* FC_WCHAR */
			0x5b,		/* FC_END */
/* 5810 */	
			0x1b,		/* FC_CARRAY */
			0x1,		/* 1 */
/* 5812 */	NdrFcShort( 0x2 ),	/* 2 */
/* 5814 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 5816 */	NdrFcShort( 0x40 ),	/* 64 */
/* 5818 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 5820 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 5822 */	NdrFcLong( 0x0 ),	/* 0 */
/* 5826 */	NdrFcLong( 0x100 ),	/* 256 */
/* 5830 */	0x5,		/* FC_WCHAR */
			0x5b,		/* FC_END */
/* 5832 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 5834 */	NdrFcShort( 0x60 ),	/* 96 */
/* 5836 */	NdrFcShort( 0x0 ),	/* 0 */
/* 5838 */	NdrFcShort( 0x14 ),	/* Offset= 20 (5858) */
/* 5840 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 5842 */	0x36,		/* FC_POINTER */
			0x36,		/* FC_POINTER */
/* 5844 */	0x36,		/* FC_POINTER */
			0x8,		/* FC_LONG */
/* 5846 */	0x40,		/* FC_STRUCTPAD4 */
			0x36,		/* FC_POINTER */
/* 5848 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 5850 */	0x36,		/* FC_POINTER */
			0x8,		/* FC_LONG */
/* 5852 */	0x40,		/* FC_STRUCTPAD4 */
			0x36,		/* FC_POINTER */
/* 5854 */	0x36,		/* FC_POINTER */
			0x36,		/* FC_POINTER */
/* 5856 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 5858 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 5860 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 5862 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 5864 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 5866 */	
			0x14, 0x8,	/* FC_FP [simple_pointer] */
/* 5868 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 5870 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 5872 */	NdrFcShort( 0xff96 ),	/* Offset= -106 (5766) */
/* 5874 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 5876 */	NdrFcShort( 0xffa8 ),	/* Offset= -88 (5788) */
/* 5878 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 5880 */	NdrFcShort( 0xffba ),	/* Offset= -70 (5810) */
/* 5882 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 5884 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 5886 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 5888 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 5890 */	
			0x11, 0x4,	/* FC_RP [alloced_on_stack] */
/* 5892 */	NdrFcShort( 0x2 ),	/* Offset= 2 (5894) */
/* 5894 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 5896 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x54,		/* FC_DEREFERENCE */
/* 5898 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 5900 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 5902 */	0x0 , 
			0x0,		/* 0 */
/* 5904 */	NdrFcLong( 0x0 ),	/* 0 */
/* 5908 */	NdrFcLong( 0x0 ),	/* 0 */
/* 5912 */	NdrFcShort( 0x2 ),	/* Offset= 2 (5914) */
/* 5914 */	NdrFcShort( 0x4 ),	/* 4 */
/* 5916 */	NdrFcShort( 0x1 ),	/* 1 */
/* 5918 */	NdrFcLong( 0x1 ),	/* 1 */
/* 5922 */	NdrFcShort( 0xf4c4 ),	/* Offset= -2876 (3046) */
/* 5924 */	NdrFcShort( 0xffff ),	/* Offset= -1 (5923) */
/* 5926 */	
			0x11, 0x0,	/* FC_RP */
/* 5928 */	NdrFcShort( 0x2 ),	/* Offset= 2 (5930) */
/* 5930 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 5932 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x0,		/*  */
/* 5934 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 5936 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 5938 */	0x0 , 
			0x0,		/* 0 */
/* 5940 */	NdrFcLong( 0x0 ),	/* 0 */
/* 5944 */	NdrFcLong( 0x0 ),	/* 0 */
/* 5948 */	NdrFcShort( 0x2 ),	/* Offset= 2 (5950) */
/* 5950 */	NdrFcShort( 0x10 ),	/* 16 */
/* 5952 */	NdrFcShort( 0x1 ),	/* 1 */
/* 5954 */	NdrFcLong( 0x1 ),	/* 1 */
/* 5958 */	NdrFcShort( 0x2e ),	/* Offset= 46 (6004) */
/* 5960 */	NdrFcShort( 0xffff ),	/* Offset= -1 (5959) */
/* 5962 */	
			0x21,		/* FC_BOGUS_ARRAY */
			0x3,		/* 3 */
/* 5964 */	NdrFcShort( 0x0 ),	/* 0 */
/* 5966 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 5968 */	NdrFcShort( 0x0 ),	/* 0 */
/* 5970 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 5972 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 5974 */	NdrFcLong( 0x1 ),	/* 1 */
/* 5978 */	NdrFcLong( 0x2710 ),	/* 10000 */
/* 5982 */	NdrFcLong( 0xffffffff ),	/* -1 */
/* 5986 */	NdrFcShort( 0x0 ),	/* Corr flags:  */
/* 5988 */	0x0 , 
			0x0,		/* 0 */
/* 5990 */	NdrFcLong( 0x0 ),	/* 0 */
/* 5994 */	NdrFcLong( 0x0 ),	/* 0 */
/* 5998 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 6000 */	NdrFcShort( 0xf05e ),	/* Offset= -4002 (1998) */
/* 6002 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 6004 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 6006 */	NdrFcShort( 0x10 ),	/* 16 */
/* 6008 */	NdrFcShort( 0x0 ),	/* 0 */
/* 6010 */	NdrFcShort( 0x6 ),	/* Offset= 6 (6016) */
/* 6012 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 6014 */	0x36,		/* FC_POINTER */
			0x5b,		/* FC_END */
/* 6016 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 6018 */	NdrFcShort( 0xffc8 ),	/* Offset= -56 (5962) */
/* 6020 */	
			0x11, 0x4,	/* FC_RP [alloced_on_stack] */
/* 6022 */	NdrFcShort( 0x2 ),	/* Offset= 2 (6024) */
/* 6024 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 6026 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x54,		/* FC_DEREFERENCE */
/* 6028 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 6030 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 6032 */	0x0 , 
			0x0,		/* 0 */
/* 6034 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6038 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6042 */	NdrFcShort( 0x2 ),	/* Offset= 2 (6044) */
/* 6044 */	NdrFcShort( 0x10 ),	/* 16 */
/* 6046 */	NdrFcShort( 0x1 ),	/* 1 */
/* 6048 */	NdrFcLong( 0x1 ),	/* 1 */
/* 6052 */	NdrFcShort( 0x2e ),	/* Offset= 46 (6098) */
/* 6054 */	NdrFcShort( 0xffff ),	/* Offset= -1 (6053) */
/* 6056 */	
			0x21,		/* FC_BOGUS_ARRAY */
			0x3,		/* 3 */
/* 6058 */	NdrFcShort( 0x0 ),	/* 0 */
/* 6060 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 6062 */	NdrFcShort( 0x0 ),	/* 0 */
/* 6064 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 6066 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 6068 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6072 */	NdrFcLong( 0x2710 ),	/* 10000 */
/* 6076 */	NdrFcLong( 0xffffffff ),	/* -1 */
/* 6080 */	NdrFcShort( 0x0 ),	/* Corr flags:  */
/* 6082 */	0x0 , 
			0x0,		/* 0 */
/* 6084 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6088 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6092 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 6094 */	NdrFcShort( 0xf0a8 ),	/* Offset= -3928 (2166) */
/* 6096 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 6098 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 6100 */	NdrFcShort( 0x10 ),	/* 16 */
/* 6102 */	NdrFcShort( 0x0 ),	/* 0 */
/* 6104 */	NdrFcShort( 0x6 ),	/* Offset= 6 (6110) */
/* 6106 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 6108 */	0x36,		/* FC_POINTER */
			0x5b,		/* FC_END */
/* 6110 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 6112 */	NdrFcShort( 0xffc8 ),	/* Offset= -56 (6056) */
/* 6114 */	
			0x11, 0x0,	/* FC_RP */
/* 6116 */	NdrFcShort( 0x2 ),	/* Offset= 2 (6118) */
/* 6118 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 6120 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x0,		/*  */
/* 6122 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 6124 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 6126 */	0x0 , 
			0x0,		/* 0 */
/* 6128 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6132 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6136 */	NdrFcShort( 0x2 ),	/* Offset= 2 (6138) */
/* 6138 */	NdrFcShort( 0x20 ),	/* 32 */
/* 6140 */	NdrFcShort( 0x1 ),	/* 1 */
/* 6142 */	NdrFcLong( 0x1 ),	/* 1 */
/* 6146 */	NdrFcShort( 0x4 ),	/* Offset= 4 (6150) */
/* 6148 */	NdrFcShort( 0xffff ),	/* Offset= -1 (6147) */
/* 6150 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 6152 */	NdrFcShort( 0x20 ),	/* 32 */
/* 6154 */	NdrFcShort( 0x0 ),	/* 0 */
/* 6156 */	NdrFcShort( 0xa ),	/* Offset= 10 (6166) */
/* 6158 */	0x36,		/* FC_POINTER */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 6160 */	0x0,		/* 0 */
			NdrFcShort( 0xe7fb ),	/* Offset= -6149 (12) */
			0x8,		/* FC_LONG */
/* 6164 */	0x40,		/* FC_STRUCTPAD4 */
			0x5b,		/* FC_END */
/* 6166 */	
			0x11, 0x0,	/* FC_RP */
/* 6168 */	NdrFcShort( 0xe884 ),	/* Offset= -6012 (156) */
/* 6170 */	
			0x11, 0x0,	/* FC_RP */
/* 6172 */	NdrFcShort( 0x2 ),	/* Offset= 2 (6174) */
/* 6174 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 6176 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x0,		/*  */
/* 6178 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 6180 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 6182 */	0x0 , 
			0x0,		/* 0 */
/* 6184 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6188 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6192 */	NdrFcShort( 0x2 ),	/* Offset= 2 (6194) */
/* 6194 */	NdrFcShort( 0x38 ),	/* 56 */
/* 6196 */	NdrFcShort( 0x1 ),	/* 1 */
/* 6198 */	NdrFcLong( 0x1 ),	/* 1 */
/* 6202 */	NdrFcShort( 0xa ),	/* Offset= 10 (6212) */
/* 6204 */	NdrFcShort( 0xffff ),	/* Offset= -1 (6203) */
/* 6206 */	
			0x1d,		/* FC_SMFARRAY */
			0x0,		/* 0 */
/* 6208 */	NdrFcShort( 0x10 ),	/* 16 */
/* 6210 */	0x2,		/* FC_CHAR */
			0x5b,		/* FC_END */
/* 6212 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 6214 */	NdrFcShort( 0x38 ),	/* 56 */
/* 6216 */	NdrFcShort( 0x0 ),	/* 0 */
/* 6218 */	NdrFcShort( 0x10 ),	/* Offset= 16 (6234) */
/* 6220 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 6222 */	NdrFcShort( 0xe7be ),	/* Offset= -6210 (12) */
/* 6224 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 6226 */	0x36,		/* FC_POINTER */
			0x36,		/* FC_POINTER */
/* 6228 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 6230 */	NdrFcShort( 0xffe8 ),	/* Offset= -24 (6206) */
/* 6232 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 6234 */	
			0x12, 0x0,	/* FC_UP */
/* 6236 */	NdrFcShort( 0xe840 ),	/* Offset= -6080 (156) */
/* 6238 */	
			0x12, 0x0,	/* FC_UP */
/* 6240 */	NdrFcShort( 0xe940 ),	/* Offset= -5824 (416) */
/* 6242 */	
			0x11, 0x4,	/* FC_RP [alloced_on_stack] */
/* 6244 */	NdrFcShort( 0x2 ),	/* Offset= 2 (6246) */
/* 6246 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 6248 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x54,		/* FC_DEREFERENCE */
/* 6250 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 6252 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 6254 */	0x0 , 
			0x0,		/* 0 */
/* 6256 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6260 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6264 */	NdrFcShort( 0x2 ),	/* Offset= 2 (6266) */
/* 6266 */	NdrFcShort( 0x10 ),	/* 16 */
/* 6268 */	NdrFcShort( 0x1 ),	/* 1 */
/* 6270 */	NdrFcLong( 0x1 ),	/* 1 */
/* 6274 */	NdrFcShort( 0x2e ),	/* Offset= 46 (6320) */
/* 6276 */	NdrFcShort( 0xffff ),	/* Offset= -1 (6275) */
/* 6278 */	
			0x21,		/* FC_BOGUS_ARRAY */
			0x3,		/* 3 */
/* 6280 */	NdrFcShort( 0x0 ),	/* 0 */
/* 6282 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 6284 */	NdrFcShort( 0x4 ),	/* 4 */
/* 6286 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 6288 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 6290 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6294 */	NdrFcLong( 0xa00000 ),	/* 10485760 */
/* 6298 */	NdrFcLong( 0xffffffff ),	/* -1 */
/* 6302 */	NdrFcShort( 0x0 ),	/* Corr flags:  */
/* 6304 */	0x0 , 
			0x0,		/* 0 */
/* 6306 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6310 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6314 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 6316 */	NdrFcShort( 0xe760 ),	/* Offset= -6304 (12) */
/* 6318 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 6320 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 6322 */	NdrFcShort( 0x10 ),	/* 16 */
/* 6324 */	NdrFcShort( 0x0 ),	/* 0 */
/* 6326 */	NdrFcShort( 0x6 ),	/* Offset= 6 (6332) */
/* 6328 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 6330 */	0x36,		/* FC_POINTER */
			0x5b,		/* FC_END */
/* 6332 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 6334 */	NdrFcShort( 0xffc8 ),	/* Offset= -56 (6278) */
/* 6336 */	
			0x11, 0x0,	/* FC_RP */
/* 6338 */	NdrFcShort( 0x2 ),	/* Offset= 2 (6340) */
/* 6340 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 6342 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x0,		/*  */
/* 6344 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 6346 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 6348 */	0x0 , 
			0x0,		/* 0 */
/* 6350 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6354 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6358 */	NdrFcShort( 0x2 ),	/* Offset= 2 (6360) */
/* 6360 */	NdrFcShort( 0x20 ),	/* 32 */
/* 6362 */	NdrFcShort( 0x1 ),	/* 1 */
/* 6364 */	NdrFcLong( 0x1 ),	/* 1 */
/* 6368 */	NdrFcShort( 0x2e ),	/* Offset= 46 (6414) */
/* 6370 */	NdrFcShort( 0xffff ),	/* Offset= -1 (6369) */
/* 6372 */	
			0x21,		/* FC_BOGUS_ARRAY */
			0x3,		/* 3 */
/* 6374 */	NdrFcShort( 0x0 ),	/* 0 */
/* 6376 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 6378 */	NdrFcShort( 0x8 ),	/* 8 */
/* 6380 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 6382 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 6384 */	NdrFcLong( 0x1 ),	/* 1 */
/* 6388 */	NdrFcLong( 0x2710 ),	/* 10000 */
/* 6392 */	NdrFcLong( 0xffffffff ),	/* -1 */
/* 6396 */	NdrFcShort( 0x0 ),	/* Corr flags:  */
/* 6398 */	0x0 , 
			0x0,		/* 0 */
/* 6400 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6404 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6408 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 6410 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 6412 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 6414 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 6416 */	NdrFcShort( 0x20 ),	/* 32 */
/* 6418 */	NdrFcShort( 0x0 ),	/* 0 */
/* 6420 */	NdrFcShort( 0xa ),	/* Offset= 10 (6430) */
/* 6422 */	0x36,		/* FC_POINTER */
			0x8,		/* FC_LONG */
/* 6424 */	0x40,		/* FC_STRUCTPAD4 */
			0x36,		/* FC_POINTER */
/* 6426 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 6428 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 6430 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 6432 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 6434 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 6436 */	NdrFcShort( 0xffc0 ),	/* Offset= -64 (6372) */
/* 6438 */	
			0x11, 0x4,	/* FC_RP [alloced_on_stack] */
/* 6440 */	NdrFcShort( 0x2 ),	/* Offset= 2 (6442) */
/* 6442 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 6444 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x54,		/* FC_DEREFERENCE */
/* 6446 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 6448 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 6450 */	0x0 , 
			0x0,		/* 0 */
/* 6452 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6456 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6460 */	NdrFcShort( 0x2 ),	/* Offset= 2 (6462) */
/* 6462 */	NdrFcShort( 0x18 ),	/* 24 */
/* 6464 */	NdrFcShort( 0x1 ),	/* 1 */
/* 6466 */	NdrFcLong( 0x1 ),	/* 1 */
/* 6470 */	NdrFcShort( 0x2e ),	/* Offset= 46 (6516) */
/* 6472 */	NdrFcShort( 0xffff ),	/* Offset= -1 (6471) */
/* 6474 */	
			0x21,		/* FC_BOGUS_ARRAY */
			0x3,		/* 3 */
/* 6476 */	NdrFcShort( 0x0 ),	/* 0 */
/* 6478 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 6480 */	NdrFcShort( 0x0 ),	/* 0 */
/* 6482 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 6484 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 6486 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6490 */	NdrFcLong( 0x2710 ),	/* 10000 */
/* 6494 */	NdrFcLong( 0xffffffff ),	/* -1 */
/* 6498 */	NdrFcShort( 0x0 ),	/* Corr flags:  */
/* 6500 */	0x0 , 
			0x0,		/* 0 */
/* 6502 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6506 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6510 */	0x4c,		/* FC_EMBEDDED_COMPLEX */
			0x0,		/* 0 */
/* 6512 */	NdrFcShort( 0xf80c ),	/* Offset= -2036 (4476) */
/* 6514 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 6516 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 6518 */	NdrFcShort( 0x18 ),	/* 24 */
/* 6520 */	NdrFcShort( 0x0 ),	/* 0 */
/* 6522 */	NdrFcShort( 0x8 ),	/* Offset= 8 (6530) */
/* 6524 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 6526 */	0x36,		/* FC_POINTER */
			0x8,		/* FC_LONG */
/* 6528 */	0x40,		/* FC_STRUCTPAD4 */
			0x5b,		/* FC_END */
/* 6530 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 6532 */	NdrFcShort( 0xffc6 ),	/* Offset= -58 (6474) */
/* 6534 */	
			0x11, 0x0,	/* FC_RP */
/* 6536 */	NdrFcShort( 0x2 ),	/* Offset= 2 (6538) */
/* 6538 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 6540 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x0,		/*  */
/* 6542 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 6544 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 6546 */	0x0 , 
			0x0,		/* 0 */
/* 6548 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6552 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6556 */	NdrFcShort( 0x2 ),	/* Offset= 2 (6558) */
/* 6558 */	NdrFcShort( 0x4 ),	/* 4 */
/* 6560 */	NdrFcShort( 0x1 ),	/* 1 */
/* 6562 */	NdrFcLong( 0x1 ),	/* 1 */
/* 6566 */	NdrFcShort( 0xf240 ),	/* Offset= -3520 (3046) */
/* 6568 */	NdrFcShort( 0xffff ),	/* Offset= -1 (6567) */
/* 6570 */	
			0x11, 0x4,	/* FC_RP [alloced_on_stack] */
/* 6572 */	NdrFcShort( 0x2 ),	/* Offset= 2 (6574) */
/* 6574 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 6576 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x54,		/* FC_DEREFERENCE */
/* 6578 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 6580 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 6582 */	0x0 , 
			0x0,		/* 0 */
/* 6584 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6588 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6592 */	NdrFcShort( 0x2 ),	/* Offset= 2 (6594) */
/* 6594 */	NdrFcShort( 0x4 ),	/* 4 */
/* 6596 */	NdrFcShort( 0x1 ),	/* 1 */
/* 6598 */	NdrFcLong( 0x1 ),	/* 1 */
/* 6602 */	NdrFcShort( 0xf21c ),	/* Offset= -3556 (3046) */
/* 6604 */	NdrFcShort( 0xffff ),	/* Offset= -1 (6603) */
/* 6606 */	
			0x11, 0x0,	/* FC_RP */
/* 6608 */	NdrFcShort( 0x2 ),	/* Offset= 2 (6610) */
/* 6610 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 6612 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x0,		/*  */
/* 6614 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 6616 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 6618 */	0x0 , 
			0x0,		/* 0 */
/* 6620 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6624 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6628 */	NdrFcShort( 0x2 ),	/* Offset= 2 (6630) */
/* 6630 */	NdrFcShort( 0x20 ),	/* 32 */
/* 6632 */	NdrFcShort( 0x1 ),	/* 1 */
/* 6634 */	NdrFcLong( 0x1 ),	/* 1 */
/* 6638 */	NdrFcShort( 0x4 ),	/* Offset= 4 (6642) */
/* 6640 */	NdrFcShort( 0xffff ),	/* Offset= -1 (6639) */
/* 6642 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 6644 */	NdrFcShort( 0x20 ),	/* 32 */
/* 6646 */	NdrFcShort( 0x0 ),	/* 0 */
/* 6648 */	NdrFcShort( 0xa ),	/* Offset= 10 (6658) */
/* 6650 */	0x8,		/* FC_LONG */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 6652 */	0x0,		/* 0 */
			NdrFcShort( 0xe60f ),	/* Offset= -6641 (12) */
			0x40,		/* FC_STRUCTPAD4 */
/* 6656 */	0x36,		/* FC_POINTER */
			0x5b,		/* FC_END */
/* 6658 */	
			0x11, 0x0,	/* FC_RP */
/* 6660 */	NdrFcShort( 0xe698 ),	/* Offset= -6504 (156) */
/* 6662 */	
			0x11, 0x4,	/* FC_RP [alloced_on_stack] */
/* 6664 */	NdrFcShort( 0x2 ),	/* Offset= 2 (6666) */
/* 6666 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 6668 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x54,		/* FC_DEREFERENCE */
/* 6670 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 6672 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 6674 */	0x0 , 
			0x0,		/* 0 */
/* 6676 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6680 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6684 */	NdrFcShort( 0x2 ),	/* Offset= 2 (6686) */
/* 6686 */	NdrFcShort( 0x4 ),	/* 4 */
/* 6688 */	NdrFcShort( 0x1 ),	/* 1 */
/* 6690 */	NdrFcLong( 0x1 ),	/* 1 */
/* 6694 */	NdrFcShort( 0xf1c0 ),	/* Offset= -3648 (3046) */
/* 6696 */	NdrFcShort( 0xffff ),	/* Offset= -1 (6695) */
/* 6698 */	
			0x11, 0x0,	/* FC_RP */
/* 6700 */	NdrFcShort( 0x2 ),	/* Offset= 2 (6702) */
/* 6702 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 6704 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x0,		/*  */
/* 6706 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 6708 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 6710 */	0x0 , 
			0x0,		/* 0 */
/* 6712 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6716 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6720 */	NdrFcShort( 0x2 ),	/* Offset= 2 (6722) */
/* 6722 */	NdrFcShort( 0x20 ),	/* 32 */
/* 6724 */	NdrFcShort( 0x1 ),	/* 1 */
/* 6726 */	NdrFcLong( 0x1 ),	/* 1 */
/* 6730 */	NdrFcShort( 0x4 ),	/* Offset= 4 (6734) */
/* 6732 */	NdrFcShort( 0xffff ),	/* Offset= -1 (6731) */
/* 6734 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 6736 */	NdrFcShort( 0x20 ),	/* 32 */
/* 6738 */	NdrFcShort( 0x0 ),	/* 0 */
/* 6740 */	NdrFcShort( 0xa ),	/* Offset= 10 (6750) */
/* 6742 */	0x8,		/* FC_LONG */
			0x4c,		/* FC_EMBEDDED_COMPLEX */
/* 6744 */	0x0,		/* 0 */
			NdrFcShort( 0xe5b3 ),	/* Offset= -6733 (12) */
			0x40,		/* FC_STRUCTPAD4 */
/* 6748 */	0x36,		/* FC_POINTER */
			0x5b,		/* FC_END */
/* 6750 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 6752 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 6754 */	
			0x11, 0x4,	/* FC_RP [alloced_on_stack] */
/* 6756 */	NdrFcShort( 0x2 ),	/* Offset= 2 (6758) */
/* 6758 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 6760 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x54,		/* FC_DEREFERENCE */
/* 6762 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 6764 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 6766 */	0x0 , 
			0x0,		/* 0 */
/* 6768 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6772 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6776 */	NdrFcShort( 0x2 ),	/* Offset= 2 (6778) */
/* 6778 */	NdrFcShort( 0xc ),	/* 12 */
/* 6780 */	NdrFcShort( 0x1 ),	/* 1 */
/* 6782 */	NdrFcLong( 0x1 ),	/* 1 */
/* 6786 */	NdrFcShort( 0x4 ),	/* Offset= 4 (6790) */
/* 6788 */	NdrFcShort( 0xffff ),	/* Offset= -1 (6787) */
/* 6790 */	
			0x15,		/* FC_STRUCT */
			0x3,		/* 3 */
/* 6792 */	NdrFcShort( 0xc ),	/* 12 */
/* 6794 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 6796 */	0x8,		/* FC_LONG */
			0x5b,		/* FC_END */
/* 6798 */	
			0x11, 0x0,	/* FC_RP */
/* 6800 */	NdrFcShort( 0x2 ),	/* Offset= 2 (6802) */
/* 6802 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 6804 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x0,		/*  */
/* 6806 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 6808 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 6810 */	0x0 , 
			0x0,		/* 0 */
/* 6812 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6816 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6820 */	NdrFcShort( 0x2 ),	/* Offset= 2 (6822) */
/* 6822 */	NdrFcShort( 0x10 ),	/* 16 */
/* 6824 */	NdrFcShort( 0x1 ),	/* 1 */
/* 6826 */	NdrFcLong( 0x1 ),	/* 1 */
/* 6830 */	NdrFcShort( 0x4 ),	/* Offset= 4 (6834) */
/* 6832 */	NdrFcShort( 0xffff ),	/* Offset= -1 (6831) */
/* 6834 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 6836 */	NdrFcShort( 0x10 ),	/* 16 */
/* 6838 */	NdrFcShort( 0x0 ),	/* 0 */
/* 6840 */	NdrFcShort( 0x6 ),	/* Offset= 6 (6846) */
/* 6842 */	0x36,		/* FC_POINTER */
			0x36,		/* FC_POINTER */
/* 6844 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 6846 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 6848 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 6850 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 6852 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 6854 */	
			0x11, 0x4,	/* FC_RP [alloced_on_stack] */
/* 6856 */	NdrFcShort( 0x2 ),	/* Offset= 2 (6858) */
/* 6858 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 6860 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x54,		/* FC_DEREFERENCE */
/* 6862 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 6864 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 6866 */	0x0 , 
			0x0,		/* 0 */
/* 6868 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6872 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6876 */	NdrFcShort( 0x2 ),	/* Offset= 2 (6878) */
/* 6878 */	NdrFcShort( 0x20 ),	/* 32 */
/* 6880 */	NdrFcShort( 0x1 ),	/* 1 */
/* 6882 */	NdrFcLong( 0x1 ),	/* 1 */
/* 6886 */	NdrFcShort( 0x1a ),	/* Offset= 26 (6912) */
/* 6888 */	NdrFcShort( 0xffff ),	/* Offset= -1 (6887) */
/* 6890 */	
			0x1b,		/* FC_CARRAY */
			0x1,		/* 1 */
/* 6892 */	NdrFcShort( 0x2 ),	/* 2 */
/* 6894 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 6896 */	NdrFcShort( 0x10 ),	/* 16 */
/* 6898 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 6900 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 6902 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6906 */	NdrFcLong( 0x400 ),	/* 1024 */
/* 6910 */	0x5,		/* FC_WCHAR */
			0x5b,		/* FC_END */
/* 6912 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 6914 */	NdrFcShort( 0x20 ),	/* 32 */
/* 6916 */	NdrFcShort( 0x0 ),	/* 0 */
/* 6918 */	NdrFcShort( 0x8 ),	/* Offset= 8 (6926) */
/* 6920 */	0x36,		/* FC_POINTER */
			0x36,		/* FC_POINTER */
/* 6922 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 6924 */	0x36,		/* FC_POINTER */
			0x5b,		/* FC_END */
/* 6926 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 6928 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 6930 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 6932 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 6934 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 6936 */	NdrFcShort( 0xffd2 ),	/* Offset= -46 (6890) */
/* 6938 */	
			0x11, 0x0,	/* FC_RP */
/* 6940 */	NdrFcShort( 0x2 ),	/* Offset= 2 (6942) */
/* 6942 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 6944 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x0,		/*  */
/* 6946 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 6948 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 6950 */	0x0 , 
			0x0,		/* 0 */
/* 6952 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6956 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6960 */	NdrFcShort( 0x2 ),	/* Offset= 2 (6962) */
/* 6962 */	NdrFcShort( 0x4 ),	/* 4 */
/* 6964 */	NdrFcShort( 0x1 ),	/* 1 */
/* 6966 */	NdrFcLong( 0x1 ),	/* 1 */
/* 6970 */	NdrFcShort( 0xf0ac ),	/* Offset= -3924 (3046) */
/* 6972 */	NdrFcShort( 0xffff ),	/* Offset= -1 (6971) */
/* 6974 */	
			0x11, 0x0,	/* FC_RP */
/* 6976 */	NdrFcShort( 0x2 ),	/* Offset= 2 (6978) */
/* 6978 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 6980 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x54,		/* FC_DEREFERENCE */
/* 6982 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 6984 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 6986 */	0x0 , 
			0x0,		/* 0 */
/* 6988 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6992 */	NdrFcLong( 0x0 ),	/* 0 */
/* 6996 */	NdrFcShort( 0x2 ),	/* Offset= 2 (6998) */
/* 6998 */	NdrFcShort( 0x40 ),	/* 64 */
/* 7000 */	NdrFcShort( 0x1 ),	/* 1 */
/* 7002 */	NdrFcLong( 0x1 ),	/* 1 */
/* 7006 */	NdrFcShort( 0x46 ),	/* Offset= 70 (7076) */
/* 7008 */	NdrFcShort( 0xffff ),	/* Offset= -1 (7007) */
/* 7010 */	
			0x1b,		/* FC_CARRAY */
			0x0,		/* 0 */
/* 7012 */	NdrFcShort( 0x1 ),	/* 1 */
/* 7014 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 7016 */	NdrFcShort( 0x10 ),	/* 16 */
/* 7018 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 7020 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 7022 */	NdrFcLong( 0x0 ),	/* 0 */
/* 7026 */	NdrFcLong( 0x400 ),	/* 1024 */
/* 7030 */	0x1,		/* FC_BYTE */
			0x5b,		/* FC_END */
/* 7032 */	
			0x1b,		/* FC_CARRAY */
			0x0,		/* 0 */
/* 7034 */	NdrFcShort( 0x1 ),	/* 1 */
/* 7036 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 7038 */	NdrFcShort( 0x20 ),	/* 32 */
/* 7040 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 7042 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 7044 */	NdrFcLong( 0x0 ),	/* 0 */
/* 7048 */	NdrFcLong( 0xa00000 ),	/* 10485760 */
/* 7052 */	0x1,		/* FC_BYTE */
			0x5b,		/* FC_END */
/* 7054 */	
			0x1b,		/* FC_CARRAY */
			0x0,		/* 0 */
/* 7056 */	NdrFcShort( 0x1 ),	/* 1 */
/* 7058 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 7060 */	NdrFcShort( 0x30 ),	/* 48 */
/* 7062 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 7064 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 7066 */	NdrFcLong( 0x0 ),	/* 0 */
/* 7070 */	NdrFcLong( 0xa00000 ),	/* 10485760 */
/* 7074 */	0x1,		/* FC_BYTE */
			0x5b,		/* FC_END */
/* 7076 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 7078 */	NdrFcShort( 0x40 ),	/* 64 */
/* 7080 */	NdrFcShort( 0x0 ),	/* 0 */
/* 7082 */	NdrFcShort( 0x10 ),	/* Offset= 16 (7098) */
/* 7084 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 7086 */	0x36,		/* FC_POINTER */
			0x8,		/* FC_LONG */
/* 7088 */	0x40,		/* FC_STRUCTPAD4 */
			0x36,		/* FC_POINTER */
/* 7090 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 7092 */	0x36,		/* FC_POINTER */
			0x8,		/* FC_LONG */
/* 7094 */	0x40,		/* FC_STRUCTPAD4 */
			0x36,		/* FC_POINTER */
/* 7096 */	0x5c,		/* FC_PAD */
			0x5b,		/* FC_END */
/* 7098 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 7100 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */
/* 7102 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 7104 */	NdrFcShort( 0xffa2 ),	/* Offset= -94 (7010) */
/* 7106 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 7108 */	NdrFcShort( 0xffb4 ),	/* Offset= -76 (7032) */
/* 7110 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 7112 */	NdrFcShort( 0xffc6 ),	/* Offset= -58 (7054) */
/* 7114 */	
			0x11, 0x0,	/* FC_RP */
/* 7116 */	NdrFcShort( 0x2 ),	/* Offset= 2 (7118) */
/* 7118 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 7120 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x0,		/*  */
/* 7122 */	NdrFcShort( 0x8 ),	/* X64 Stack size/offset = 8 */
/* 7124 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 7126 */	0x0 , 
			0x0,		/* 0 */
/* 7128 */	NdrFcLong( 0x0 ),	/* 0 */
/* 7132 */	NdrFcLong( 0x0 ),	/* 0 */
/* 7136 */	NdrFcShort( 0x2 ),	/* Offset= 2 (7138) */
/* 7138 */	NdrFcShort( 0x10 ),	/* 16 */
/* 7140 */	NdrFcShort( 0x1 ),	/* 1 */
/* 7142 */	NdrFcLong( 0x1 ),	/* 1 */
/* 7146 */	NdrFcShort( 0x1a ),	/* Offset= 26 (7172) */
/* 7148 */	NdrFcShort( 0xffff ),	/* Offset= -1 (7147) */
/* 7150 */	
			0x1b,		/* FC_CARRAY */
			0x0,		/* 0 */
/* 7152 */	NdrFcShort( 0x1 ),	/* 1 */
/* 7154 */	0x19,		/* Corr desc:  field pointer, FC_ULONG */
			0x0,		/*  */
/* 7156 */	NdrFcShort( 0x4 ),	/* 4 */
/* 7158 */	NdrFcShort( 0x11 ),	/* Corr flags:  early, */
/* 7160 */	0x1 , /* correlation range */
			0x0,		/* 0 */
/* 7162 */	NdrFcLong( 0x1 ),	/* 1 */
/* 7166 */	NdrFcLong( 0x400 ),	/* 1024 */
/* 7170 */	0x1,		/* FC_BYTE */
			0x5b,		/* FC_END */
/* 7172 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 7174 */	NdrFcShort( 0x10 ),	/* 16 */
/* 7176 */	NdrFcShort( 0x0 ),	/* 0 */
/* 7178 */	NdrFcShort( 0x6 ),	/* Offset= 6 (7184) */
/* 7180 */	0x8,		/* FC_LONG */
			0x8,		/* FC_LONG */
/* 7182 */	0x36,		/* FC_POINTER */
			0x5b,		/* FC_END */
/* 7184 */	
			0x12, 0x20,	/* FC_UP [maybenull_sizeis] */
/* 7186 */	NdrFcShort( 0xffdc ),	/* Offset= -36 (7150) */
/* 7188 */	
			0x11, 0x4,	/* FC_RP [alloced_on_stack] */
/* 7190 */	NdrFcShort( 0x2 ),	/* Offset= 2 (7192) */
/* 7192 */	
			0x2b,		/* FC_NON_ENCAPSULATED_UNION */
			0x9,		/* FC_ULONG */
/* 7194 */	0x29,		/* Corr desc:  parameter, FC_ULONG */
			0x54,		/* FC_DEREFERENCE */
/* 7196 */	NdrFcShort( 0x18 ),	/* X64 Stack size/offset = 24 */
/* 7198 */	NdrFcShort( 0x1 ),	/* Corr flags:  early, */
/* 7200 */	0x0 , 
			0x0,		/* 0 */
/* 7202 */	NdrFcLong( 0x0 ),	/* 0 */
/* 7206 */	NdrFcLong( 0x0 ),	/* 0 */
/* 7210 */	NdrFcShort( 0x2 ),	/* Offset= 2 (7212) */
/* 7212 */	NdrFcShort( 0x10 ),	/* 16 */
/* 7214 */	NdrFcShort( 0x1 ),	/* 1 */
/* 7216 */	NdrFcLong( 0x1 ),	/* 1 */
/* 7220 */	NdrFcShort( 0x4 ),	/* Offset= 4 (7224) */
/* 7222 */	NdrFcShort( 0xffff ),	/* Offset= -1 (7221) */
/* 7224 */	
			0x1a,		/* FC_BOGUS_STRUCT */
			0x3,		/* 3 */
/* 7226 */	NdrFcShort( 0x10 ),	/* 16 */
/* 7228 */	NdrFcShort( 0x0 ),	/* 0 */
/* 7230 */	NdrFcShort( 0x6 ),	/* Offset= 6 (7236) */
/* 7232 */	0x8,		/* FC_LONG */
			0x40,		/* FC_STRUCTPAD4 */
/* 7234 */	0x36,		/* FC_POINTER */
			0x5b,		/* FC_END */
/* 7236 */	
			0x12, 0x8,	/* FC_UP [simple_pointer] */
/* 7238 */	
			0x25,		/* FC_C_WSTRING */
			0x5c,		/* FC_PAD */

			0x0
        }
    };

static const unsigned short drsuapi_FormatStringOffsetTable[] =
    {
    0,
    60,
    104,
    160,
    228,
    284,
    340,
    396,
    452,
    520,
    588,
    656,
    724,
    792,
    860,
    928,
    996,
    1064,
    1132,
    1188,
    1256,
    1324,
    1392,
    1448,
    1516,
    1584,
    1652,
    1720,
    1788
    };


static const MIDL_STUB_DESC drsuapi_StubDesc = 
    {
    (void *)& drsuapi___RpcClientInterface,
    MIDL_user_allocate,
    MIDL_user_free,
    &drsuapi__MIDL_AutoBindHandle,
    0,
    0,
    0,
    0,
    drsr__MIDL_TypeFormatString.Format,
    1, /* -error bounds_check flag */
    0x60001, /* Ndr library version */
    0,
    0x800025b, /* MIDL Version 8.0.603 */
    0,
    0,
    0,  /* notify & notify_flag routine table */
    0x1, /* MIDL flag */
    0, /* cs routines */
    0,   /* proxy/server info */
    0
    };

static const unsigned short dsaop_FormatStringOffsetTable[] =
    {
    1856,
    1916
    };


static const MIDL_STUB_DESC dsaop_StubDesc = 
    {
    (void *)& dsaop___RpcClientInterface,
    MIDL_user_allocate,
    MIDL_user_free,
    &dsaop__MIDL_AutoBindHandle,
    0,
    0,
    0,
    0,
    drsr__MIDL_TypeFormatString.Format,
    1, /* -error bounds_check flag */
    0x60001, /* Ndr library version */
    0,
    0x800025b, /* MIDL Version 8.0.603 */
    0,
    0,
    0,  /* notify & notify_flag routine table */
    0x1, /* MIDL flag */
    0, /* cs routines */
    0,   /* proxy/server info */
    0
    };
#if _MSC_VER >= 1200
#pragma warning(pop)
#endif


#endif /* defined(_M_AMD64)*/

