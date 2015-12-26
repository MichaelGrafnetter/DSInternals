

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


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

#pragma warning( disable: 4049 )  /* more than 64k source lines */


/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 500
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif // __RPCNDR_H_VERSION__


#ifndef __drsr_h__
#define __drsr_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

/* header files for imported files */
#include "wtypesbase.h"
#include "drsr_imports.h"

#ifdef __cplusplus
extern "C"{
#endif 


#ifndef __drsuapi_INTERFACE_DEFINED__
#define __drsuapi_INTERFACE_DEFINED__

/* interface drsuapi */
/* [unique][version][uuid] */ 

typedef struct _RPC_UNICODE_STRING
    {
    unsigned short Length;
    unsigned short MaximumLength;
    /* [length_is][size_is] */ WCHAR *Buffer;
    } 	RPC_UNICODE_STRING;

typedef struct _RPC_UNICODE_STRING *PRPC_UNICODE_STRING;

typedef LONGLONG DSTIME;

typedef /* [context_handle] */ void *DRS_HANDLE;

typedef /* [public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public] */ struct __MIDL_drsuapi_0001
    {
    unsigned char Data[ 28 ];
    } 	NT4SID;

typedef /* [public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public] */ struct __MIDL_drsuapi_0002
    {
    unsigned long structLen;
    unsigned long SidLen;
    GUID Guid;
    NT4SID Sid;
    unsigned long NameLen;
    /* [size_is][range] */ WCHAR StringName[ 1 ];
    } 	DSNAME;

typedef LONGLONG USN;

typedef /* [public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public] */ struct __MIDL_drsuapi_0003
    {
    USN usnHighObjUpdate;
    USN usnReserved;
    USN usnHighPropUpdate;
    } 	USN_VECTOR;

typedef /* [public][public][public][public][public][public][public][public][public] */ struct __MIDL_drsuapi_0004
    {
    UUID uuidDsa;
    USN usnHighPropUpdate;
    } 	UPTODATE_CURSOR_V1;

typedef /* [public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public] */ struct __MIDL_drsuapi_0005
    {
    DWORD dwVersion;
    DWORD dwReserved1;
    /* [range] */ DWORD cNumCursors;
    DWORD dwReserved2;
    /* [size_is] */ UPTODATE_CURSOR_V1 rgCursors[ 1 ];
    } 	UPTODATE_VECTOR_V1_EXT;

typedef /* [public][public][public][public][public][public][public][public][public] */ struct __MIDL_drsuapi_0006
    {
    /* [range] */ unsigned int length;
    /* [size_is] */ BYTE *elements;
    } 	OID_t;

typedef /* [public][public][public][public][public][public][public][public][public] */ struct __MIDL_drsuapi_0007
    {
    unsigned long ndx;
    OID_t prefix;
    } 	PrefixTableEntry;

typedef /* [public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public] */ struct __MIDL_drsuapi_0008
    {
    /* [range] */ DWORD PrefixCount;
    /* [size_is] */ PrefixTableEntry *pPrefixEntry;
    } 	SCHEMA_PREFIX_TABLE;

typedef ULONG ATTRTYP;

typedef /* [public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public] */ struct __MIDL_drsuapi_0009
    {
    DWORD dwVersion;
    DWORD dwReserved1;
    /* [range] */ DWORD cAttrs;
    /* [size_is] */ ATTRTYP rgPartialAttr[ 1 ];
    } 	PARTIAL_ATTR_VECTOR_V1_EXT;

typedef /* [public][public][public][public][public][public][public] */ struct __MIDL_drsuapi_0010
    {
    /* [range] */ unsigned long mtx_namelen;
    /* [size_is] */ unsigned char mtx_name[ 1 ];
    } 	MTX_ADDR;

typedef /* [public][public][public][public][public][public][public][public][public][public][public][public][public][public][public] */ struct __MIDL_drsuapi_0011
    {
    /* [range] */ ULONG valLen;
    /* [size_is] */ UCHAR *pVal;
    } 	ATTRVAL;

typedef /* [public][public][public][public][public][public][public] */ struct __MIDL_drsuapi_0012
    {
    /* [range] */ ULONG valCount;
    /* [size_is] */ ATTRVAL *pAVal;
    } 	ATTRVALBLOCK;

typedef /* [public][public][public][public][public][public][public][public][public][public] */ struct __MIDL_drsuapi_0013
    {
    ATTRTYP attrTyp;
    ATTRVALBLOCK AttrVal;
    } 	ATTR;

typedef /* [public][public][public][public][public][public][public][public][public][public][public][public][public][public][public] */ struct __MIDL_drsuapi_0014
    {
    /* [range] */ ULONG attrCount;
    /* [size_is] */ ATTR *pAttr;
    } 	ATTRBLOCK;

typedef /* [public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public][public] */ struct __MIDL_drsuapi_0015
    {
    DSNAME *pName;
    unsigned long ulFlags;
    ATTRBLOCK AttrBlock;
    } 	ENTINF;

typedef /* [public][public][public][public][public][public][public][public] */ struct __MIDL_drsuapi_0016
    {
    DWORD dwVersion;
    DSTIME timeChanged;
    UUID uuidDsaOriginating;
    USN usnOriginating;
    } 	PROPERTY_META_DATA_EXT;

typedef /* [public][public][public][public] */ struct __MIDL_drsuapi_0017
    {
    /* [range] */ DWORD cNumProps;
    /* [size_is] */ PROPERTY_META_DATA_EXT rgMetaData[ 1 ];
    } 	PROPERTY_META_DATA_EXT_VECTOR;

typedef struct REPLENTINFLIST
    {
    struct REPLENTINFLIST *pNextEntInf;
    ENTINF Entinf;
    BOOL fIsNCPrefix;
    UUID *pParentGuid;
    PROPERTY_META_DATA_EXT_VECTOR *pMetaDataExt;
    } 	REPLENTINFLIST;

typedef /* [public][public][public][public] */ struct __MIDL_drsuapi_0018
    {
    UUID uuidDsa;
    USN usnHighPropUpdate;
    DSTIME timeLastSyncSuccess;
    } 	UPTODATE_CURSOR_V2;

typedef /* [public][public][public][public] */ struct __MIDL_drsuapi_0019
    {
    DWORD dwVersion;
    DWORD dwReserved1;
    /* [range] */ DWORD cNumCursors;
    DWORD dwReserved2;
    /* [size_is] */ UPTODATE_CURSOR_V2 rgCursors[ 1 ];
    } 	UPTODATE_VECTOR_V2_EXT;

typedef /* [public][public][public][public] */ struct __MIDL_drsuapi_0020
    {
    DSTIME timeCreated;
    PROPERTY_META_DATA_EXT MetaData;
    } 	VALUE_META_DATA_EXT_V1;

typedef /* [public][public][public][public] */ struct __MIDL_drsuapi_0021
    {
    DSNAME *pObject;
    ATTRTYP attrTyp;
    ATTRVAL Aval;
    BOOL fIsPresent;
    VALUE_META_DATA_EXT_V1 MetaData;
    } 	REPLVALINF;

typedef /* [public][public][public][public][public][public][public][public][public][public] */ struct __MIDL_drsuapi_0022
    {
    UCHAR rgTimes[ 84 ];
    } 	REPLTIMES;

typedef /* [public][public][public][public] */ struct __MIDL_drsuapi_0023
    {
    DWORD IPAddress;
    DWORD NotificationCount;
    DWORD secTimeConnected;
    DWORD Flags;
    DWORD TotalRequests;
    DWORD Reserved1;
    /* [unique][string] */ WCHAR *UserName;
    } 	DS_DOMAIN_CONTROLLER_INFO_FFFFFFFFW;

typedef struct ENTINFLIST
    {
    struct ENTINFLIST *pNextEntInf;
    ENTINF Entinf;
    } 	ENTINFLIST;

typedef /* [public][public][public][public][public] */ struct __MIDL_drsuapi_0024
    {
    DWORD dsid;
    DWORD extendedErr;
    DWORD extendedData;
    USHORT problem;
    ATTRTYP type;
    BOOL valReturned;
    ATTRVAL Val;
    } 	INTFORMPROB_DRS_WIRE_V1;

typedef struct _PROBLEMLIST_DRS_WIRE_V1
    {
    struct _PROBLEMLIST_DRS_WIRE_V1 *pNextProblem;
    INTFORMPROB_DRS_WIRE_V1 intprob;
    } 	PROBLEMLIST_DRS_WIRE_V1;

typedef /* [public][public][public][public][public][public] */ struct __MIDL_drsuapi_0025
    {
    DSNAME *pObject;
    ULONG count;
    PROBLEMLIST_DRS_WIRE_V1 FirstProblem;
    } 	ATRERR_DRS_WIRE_V1;

typedef /* [public][public][public][public][public][public] */ struct __MIDL_drsuapi_0026
    {
    DWORD dsid;
    DWORD extendedErr;
    DWORD extendedData;
    USHORT problem;
    DSNAME *pMatched;
    } 	NAMERR_DRS_WIRE_V1;

typedef /* [public][public][public][public][public] */ struct __MIDL_drsuapi_0027
    {
    UCHAR nameRes;
    UCHAR unusedPad;
    USHORT nextRDN;
    } 	NAMERESOP_DRS_WIRE_V1;

typedef struct _DSA_ADDRESS_LIST_DRS_WIRE_V1
    {
    struct _DSA_ADDRESS_LIST_DRS_WIRE_V1 *pNextAddress;
    RPC_UNICODE_STRING *pAddress;
    } 	DSA_ADDRESS_LIST_DRS_WIRE_V1;

typedef struct CONTREF_DRS_WIRE_V1
    {
    DSNAME *pTarget;
    NAMERESOP_DRS_WIRE_V1 OpState;
    USHORT aliasRDN;
    USHORT RDNsInternal;
    USHORT refType;
    USHORT count;
    DSA_ADDRESS_LIST_DRS_WIRE_V1 *pDAL;
    struct CONTREF_DRS_WIRE_V1 *pNextContRef;
    BOOL bNewChoice;
    UCHAR choice;
    } 	CONTREF_DRS_WIRE_V1;

typedef /* [public][public][public][public][public][public] */ struct __MIDL_drsuapi_0028
    {
    DWORD dsid;
    DWORD extendedErr;
    DWORD extendedData;
    CONTREF_DRS_WIRE_V1 Refer;
    } 	REFERR_DRS_WIRE_V1;

typedef /* [public][public][public][public][public][public] */ struct __MIDL_drsuapi_0029
    {
    DWORD dsid;
    DWORD extendedErr;
    DWORD extendedData;
    USHORT problem;
    } 	SECERR_DRS_WIRE_V1;

typedef /* [public][public][public][public][public][public] */ struct __MIDL_drsuapi_0030
    {
    DWORD dsid;
    DWORD extendedErr;
    DWORD extendedData;
    USHORT problem;
    } 	SVCERR_DRS_WIRE_V1;

typedef /* [public][public][public][public][public][public] */ struct __MIDL_drsuapi_0031
    {
    DWORD dsid;
    DWORD extendedErr;
    DWORD extendedData;
    USHORT problem;
    } 	UPDERR_DRS_WIRE_V1;

typedef /* [public][public][public][public][public][public] */ struct __MIDL_drsuapi_0032
    {
    DWORD dsid;
    DWORD extendedErr;
    DWORD extendedData;
    USHORT problem;
    } 	SYSERR_DRS_WIRE_V1;

typedef /* [public][public][public][public][public][switch_type] */ union __MIDL_drsuapi_0033
    {
    /* [case()] */ ATRERR_DRS_WIRE_V1 AtrErr;
    /* [case()] */ NAMERR_DRS_WIRE_V1 NamErr;
    /* [case()] */ REFERR_DRS_WIRE_V1 RefErr;
    /* [case()] */ SECERR_DRS_WIRE_V1 SecErr;
    /* [case()] */ SVCERR_DRS_WIRE_V1 SvcErr;
    /* [case()] */ UPDERR_DRS_WIRE_V1 UpdErr;
    /* [case()] */ SYSERR_DRS_WIRE_V1 SysErr;
    } 	DIRERR_DRS_WIRE_V1;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0034
    {
    /* [range] */ DWORD cb;
    /* [size_is] */ BYTE rgb[ 1 ];
    } 	DRS_EXTENSIONS;

typedef /* [public][public][public][public][public][public][public] */ struct __MIDL_drsuapi_0035
    {
    UUID uuidDsaObjDest;
    UUID uuidInvocIdSrc;
    /* [ref] */ DSNAME *pNC;
    USN_VECTOR usnvecFrom;
    /* [unique] */ UPTODATE_VECTOR_V1_EXT *pUpToDateVecDestV1;
    /* [unique] */ PARTIAL_ATTR_VECTOR_V1_EXT *pPartialAttrVecDestV1;
    SCHEMA_PREFIX_TABLE PrefixTableDest;
    ULONG ulFlags;
    ULONG cMaxObjects;
    ULONG cMaxBytes;
    ULONG ulExtendedOp;
    } 	DRS_MSG_GETCHGREQ_V3;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0036
    {
    UUID uuidTransportObj;
    /* [ref] */ MTX_ADDR *pmtxReturnAddress;
    DRS_MSG_GETCHGREQ_V3 V3;
    } 	DRS_MSG_GETCHGREQ_V4;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0037
    {
    UUID uuidTransportObj;
    /* [ref] */ MTX_ADDR *pmtxReturnAddress;
    DRS_MSG_GETCHGREQ_V3 V3;
    /* [unique] */ PARTIAL_ATTR_VECTOR_V1_EXT *pPartialAttrSet;
    /* [unique] */ PARTIAL_ATTR_VECTOR_V1_EXT *pPartialAttrSetEx;
    SCHEMA_PREFIX_TABLE PrefixTableDest;
    } 	DRS_MSG_GETCHGREQ_V7;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0038
    {
    UUID uuidDsaObjSrc;
    UUID uuidInvocIdSrc;
    /* [unique] */ DSNAME *pNC;
    USN_VECTOR usnvecFrom;
    USN_VECTOR usnvecTo;
    /* [unique] */ UPTODATE_VECTOR_V1_EXT *pUpToDateVecSrcV1;
    SCHEMA_PREFIX_TABLE PrefixTableSrc;
    ULONG ulExtendedRet;
    ULONG cNumObjects;
    ULONG cNumBytes;
    /* [unique] */ REPLENTINFLIST *pObjects;
    BOOL fMoreData;
    } 	DRS_MSG_GETCHGREPLY_V1;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0039
    {
    UUID uuidDsaObjSrc;
    UUID uuidInvocIdSrc;
    /* [unique] */ DSNAME *pNC;
    USN_VECTOR usnvecFrom;
    USN_VECTOR usnvecTo;
    /* [unique] */ UPTODATE_VECTOR_V2_EXT *pUpToDateVecSrc;
    SCHEMA_PREFIX_TABLE PrefixTableSrc;
    ULONG ulExtendedRet;
    ULONG cNumObjects;
    ULONG cNumBytes;
    /* [unique] */ REPLENTINFLIST *pObjects;
    BOOL fMoreData;
    ULONG cNumNcSizeObjects;
    ULONG cNumNcSizeValues;
    /* [range] */ DWORD cNumValues;
    /* [size_is] */ REPLVALINF *rgValues;
    DWORD dwDRSError;
    } 	DRS_MSG_GETCHGREPLY_V6;

typedef /* [public][public][public][public][public][public][public] */ struct __MIDL_drsuapi_0040
    {
    DWORD cbUncompressedSize;
    DWORD cbCompressedSize;
    /* [size_is] */ BYTE *pbCompressedData;
    } 	DRS_COMPRESSED_BLOB;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0041
    {
    UUID uuidDsaObjDest;
    UUID uuidInvocIdSrc;
    /* [ref] */ DSNAME *pNC;
    USN_VECTOR usnvecFrom;
    /* [unique] */ UPTODATE_VECTOR_V1_EXT *pUpToDateVecDestV1;
    ULONG ulFlags;
    ULONG cMaxObjects;
    ULONG cMaxBytes;
    ULONG ulExtendedOp;
    ULARGE_INTEGER liFsmoInfo;
    } 	DRS_MSG_GETCHGREQ_V5;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0042
    {
    UUID uuidDsaObjDest;
    UUID uuidInvocIdSrc;
    /* [ref] */ DSNAME *pNC;
    USN_VECTOR usnvecFrom;
    /* [unique] */ UPTODATE_VECTOR_V1_EXT *pUpToDateVecDest;
    ULONG ulFlags;
    ULONG cMaxObjects;
    ULONG cMaxBytes;
    ULONG ulExtendedOp;
    ULARGE_INTEGER liFsmoInfo;
    /* [unique] */ PARTIAL_ATTR_VECTOR_V1_EXT *pPartialAttrSet;
    /* [unique] */ PARTIAL_ATTR_VECTOR_V1_EXT *pPartialAttrSetEx;
    SCHEMA_PREFIX_TABLE PrefixTableDest;
    } 	DRS_MSG_GETCHGREQ_V8;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0043
    {
    UUID uuidDsaObjDest;
    UUID uuidInvocIdSrc;
    /* [ref] */ DSNAME *pNC;
    USN_VECTOR usnvecFrom;
    /* [unique] */ UPTODATE_VECTOR_V1_EXT *pUpToDateVecDest;
    ULONG ulFlags;
    ULONG cMaxObjects;
    ULONG cMaxBytes;
    ULONG ulExtendedOp;
    ULARGE_INTEGER liFsmoInfo;
    /* [unique] */ PARTIAL_ATTR_VECTOR_V1_EXT *pPartialAttrSet;
    /* [unique] */ PARTIAL_ATTR_VECTOR_V1_EXT *pPartialAttrSetEx;
    SCHEMA_PREFIX_TABLE PrefixTableDest;
    ULONG ulMoreFlags;
    } 	DRS_MSG_GETCHGREQ_V10;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0044
    {
    /* [case()] */ DRS_MSG_GETCHGREQ_V4 V4;
    /* [case()] */ DRS_MSG_GETCHGREQ_V5 V5;
    /* [case()] */ DRS_MSG_GETCHGREQ_V7 V7;
    /* [case()] */ DRS_MSG_GETCHGREQ_V8 V8;
    /* [case()] */ DRS_MSG_GETCHGREQ_V10 V10;
    } 	DRS_MSG_GETCHGREQ;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0045
    {
    DRS_COMPRESSED_BLOB CompressedV1;
    } 	DRS_MSG_GETCHGREPLY_V2;

typedef /* [public][public][public][public] */ 
enum __MIDL_drsuapi_0046
    {
        DRS_COMP_ALG_NONE	= 0,
        DRS_COMP_ALG_UNUSED	= 1,
        DRS_COMP_ALG_MSZIP	= 2,
        DRS_COMP_ALG_WIN2K3	= 3
    } 	DRS_COMP_ALG_TYPE;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0047
    {
    DWORD dwCompressedVersion;
    DRS_COMP_ALG_TYPE CompressionAlg;
    DRS_COMPRESSED_BLOB CompressedAny;
    } 	DRS_MSG_GETCHGREPLY_V7;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0048
    {
    /* [case()] */ DRS_MSG_GETCHGREPLY_V1 V1;
    /* [case()] */ DRS_MSG_GETCHGREPLY_V2 V2;
    /* [case()] */ DRS_MSG_GETCHGREPLY_V6 V6;
    /* [case()] */ DRS_MSG_GETCHGREPLY_V7 V7;
    } 	DRS_MSG_GETCHGREPLY;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0049
    {
    /* [ref] */ DSNAME *pNC;
    UUID uuidDsaSrc;
    /* [string][unique] */ unsigned char *pszDsaSrc;
    ULONG ulOptions;
    } 	DRS_MSG_REPSYNC_V1;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0050
    {
    /* [case()] */ DRS_MSG_REPSYNC_V1 V1;
    } 	DRS_MSG_REPSYNC;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0051
    {
    /* [ref] */ DSNAME *pNC;
    /* [string][ref] */ unsigned char *pszDsaDest;
    UUID uuidDsaObjDest;
    ULONG ulOptions;
    } 	DRS_MSG_UPDREFS_V1;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0052
    {
    /* [case()] */ DRS_MSG_UPDREFS_V1 V1;
    } 	DRS_MSG_UPDREFS;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0053
    {
    /* [ref] */ DSNAME *pNC;
    /* [string][ref] */ unsigned char *pszDsaSrc;
    REPLTIMES rtSchedule;
    ULONG ulOptions;
    } 	DRS_MSG_REPADD_V1;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0054
    {
    /* [ref] */ DSNAME *pNC;
    /* [unique] */ DSNAME *pSourceDsaDN;
    /* [unique] */ DSNAME *pTransportDN;
    /* [string][ref] */ unsigned char *pszSourceDsaAddress;
    REPLTIMES rtSchedule;
    ULONG ulOptions;
    } 	DRS_MSG_REPADD_V2;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0055
    {
    /* [case()] */ DRS_MSG_REPADD_V1 V1;
    /* [case()] */ DRS_MSG_REPADD_V2 V2;
    } 	DRS_MSG_REPADD;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0056
    {
    /* [ref] */ DSNAME *pNC;
    /* [string] */ unsigned char *pszDsaSrc;
    ULONG ulOptions;
    } 	DRS_MSG_REPDEL_V1;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0057
    {
    /* [case()] */ DRS_MSG_REPDEL_V1 V1;
    } 	DRS_MSG_REPDEL;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0058
    {
    /* [ref] */ DSNAME *pNC;
    UUID uuidSourceDRA;
    /* [string][unique] */ unsigned char *pszSourceDRA;
    REPLTIMES rtSchedule;
    ULONG ulReplicaFlags;
    ULONG ulModifyFields;
    ULONG ulOptions;
    } 	DRS_MSG_REPMOD_V1;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0059
    {
    /* [case()] */ DRS_MSG_REPMOD_V1 V1;
    } 	DRS_MSG_REPMOD;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0060
    {
    DWORD dwFlags;
    /* [range] */ DWORD cNames;
    /* [size_is] */ DSNAME **rpNames;
    ATTRBLOCK RequiredAttrs;
    SCHEMA_PREFIX_TABLE PrefixTable;
    } 	DRS_MSG_VERIFYREQ_V1;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0061
    {
    /* [case()] */ DRS_MSG_VERIFYREQ_V1 V1;
    } 	DRS_MSG_VERIFYREQ;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0062
    {
    DWORD error;
    /* [range] */ DWORD cNames;
    /* [size_is] */ ENTINF *rpEntInf;
    SCHEMA_PREFIX_TABLE PrefixTable;
    } 	DRS_MSG_VERIFYREPLY_V1;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0063
    {
    /* [case()] */ DRS_MSG_VERIFYREPLY_V1 V1;
    } 	DRS_MSG_VERIFYREPLY;

typedef /* [public][public] */ 
enum __MIDL_drsuapi_0064
    {
        RevMembGetGroupsForUser	= 1,
        RevMembGetAliasMembership	= ( RevMembGetGroupsForUser + 1 ) ,
        RevMembGetAccountGroups	= ( RevMembGetAliasMembership + 1 ) ,
        RevMembGetResourceGroups	= ( RevMembGetAccountGroups + 1 ) ,
        RevMembGetUniversalGroups	= ( RevMembGetResourceGroups + 1 ) ,
        GroupMembersTransitive	= ( RevMembGetUniversalGroups + 1 ) ,
        RevMembGlobalGroupsNonTransitive	= ( GroupMembersTransitive + 1 ) 
    } 	REVERSE_MEMBERSHIP_OPERATION_TYPE;

typedef /* [public][public][public][public][public][public] */ struct __MIDL_drsuapi_0065
    {
    /* [range] */ ULONG cDsNames;
    /* [size_is][size_is] */ DSNAME **ppDsNames;
    DWORD dwFlags;
    /* [range] */ REVERSE_MEMBERSHIP_OPERATION_TYPE OperationType;
    DSNAME *pLimitingDomain;
    } 	DRS_MSG_REVMEMB_REQ_V1;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0066
    {
    /* [case()] */ DRS_MSG_REVMEMB_REQ_V1 V1;
    } 	DRS_MSG_REVMEMB_REQ;

typedef /* [public][public][public][public][public][public] */ struct __MIDL_drsuapi_0067
    {
    ULONG errCode;
    /* [range] */ ULONG cDsNames;
    /* [range] */ ULONG cSidHistory;
    /* [size_is][size_is] */ DSNAME **ppDsNames;
    /* [size_is] */ DWORD *pAttributes;
    /* [size_is][size_is] */ NT4SID **ppSidHistory;
    } 	DRS_MSG_REVMEMB_REPLY_V1;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0068
    {
    /* [case()] */ DRS_MSG_REVMEMB_REPLY_V1 V1;
    } 	DRS_MSG_REVMEMB_REPLY;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0069
    {
    unsigned char *pSourceDSA;
    ENTINF *pObject;
    UUID *pParentUUID;
    SCHEMA_PREFIX_TABLE PrefixTable;
    ULONG ulFlags;
    } 	DRS_MSG_MOVEREQ_V1;

typedef /* [public][public][public][public] */ struct __MIDL_drsuapi_0070
    {
    /* [range] */ unsigned long cbBuffer;
    unsigned long BufferType;
    /* [size_is] */ BYTE *pvBuffer;
    } 	DRS_SecBuffer;

typedef /* [public][public][public][public][public][public][public] */ struct __MIDL_drsuapi_0071
    {
    unsigned long ulVersion;
    /* [range] */ unsigned long cBuffers;
    /* [size_is] */ DRS_SecBuffer *Buffers;
    } 	DRS_SecBufferDesc;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0072
    {
    DSNAME *pSrcDSA;
    ENTINF *pSrcObject;
    DSNAME *pDstName;
    DSNAME *pExpectedTargetNC;
    DRS_SecBufferDesc *pClientCreds;
    SCHEMA_PREFIX_TABLE PrefixTable;
    ULONG ulFlags;
    } 	DRS_MSG_MOVEREQ_V2;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0073
    {
    /* [case()] */ DRS_MSG_MOVEREQ_V1 V1;
    /* [case()] */ DRS_MSG_MOVEREQ_V2 V2;
    } 	DRS_MSG_MOVEREQ;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0074
    {
    ENTINF **ppResult;
    SCHEMA_PREFIX_TABLE PrefixTable;
    ULONG *pError;
    } 	DRS_MSG_MOVEREPLY_V1;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0075
    {
    ULONG win32Error;
    /* [unique] */ DSNAME *pAddedName;
    } 	DRS_MSG_MOVEREPLY_V2;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0076
    {
    /* [case()] */ DRS_MSG_MOVEREPLY_V1 V1;
    /* [case()] */ DRS_MSG_MOVEREPLY_V2 V2;
    } 	DRS_MSG_MOVEREPLY;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0077
    {
    ULONG CodePage;
    ULONG LocaleId;
    DWORD dwFlags;
    DWORD formatOffered;
    DWORD formatDesired;
    /* [range] */ DWORD cNames;
    /* [size_is][string] */ WCHAR **rpNames;
    } 	DRS_MSG_CRACKREQ_V1;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0078
    {
    /* [case()] */ DRS_MSG_CRACKREQ_V1 V1;
    } 	DRS_MSG_CRACKREQ;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0079
    {
    DS_NAME_RESULTW *pResult;
    } 	DRS_MSG_CRACKREPLY_V1;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0080
    {
    /* [case()] */ DRS_MSG_CRACKREPLY_V1 V1;
    } 	DRS_MSG_CRACKREPLY;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0081
    {
    DWORD dwFlags;
    DWORD PreferredMaximumLength;
    /* [range] */ DWORD cbRestart;
    /* [size_is] */ BYTE *pRestart;
    } 	DRS_MSG_NT4_CHGLOG_REQ_V1;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0082
    {
    /* [case()] */ DRS_MSG_NT4_CHGLOG_REQ_V1 V1;
    } 	DRS_MSG_NT4_CHGLOG_REQ;

typedef /* [public][public][public][public] */ struct __MIDL_drsuapi_0083
    {
    LARGE_INTEGER SamSerialNumber;
    LARGE_INTEGER SamCreationTime;
    LARGE_INTEGER BuiltinSerialNumber;
    LARGE_INTEGER BuiltinCreationTime;
    LARGE_INTEGER LsaSerialNumber;
    LARGE_INTEGER LsaCreationTime;
    } 	NT4_REPLICATION_STATE;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0084
    {
    /* [range] */ DWORD cbRestart;
    /* [range] */ DWORD cbLog;
    NT4_REPLICATION_STATE ReplicationState;
    DWORD ActualNtStatus;
    /* [size_is] */ BYTE *pRestart;
    /* [size_is] */ BYTE *pLog;
    } 	DRS_MSG_NT4_CHGLOG_REPLY_V1;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0085
    {
    /* [case()] */ DRS_MSG_NT4_CHGLOG_REPLY_V1 V1;
    } 	DRS_MSG_NT4_CHGLOG_REPLY;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0086
    {
    DWORD operation;
    DWORD flags;
    /* [string] */ const WCHAR *pwszAccount;
    /* [range] */ DWORD cSPN;
    /* [size_is][string] */ const WCHAR **rpwszSPN;
    } 	DRS_MSG_SPNREQ_V1;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0087
    {
    /* [case()] */ DRS_MSG_SPNREQ_V1 V1;
    } 	DRS_MSG_SPNREQ;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0088
    {
    DWORD retVal;
    } 	DRS_MSG_SPNREPLY_V1;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0089
    {
    /* [case()] */ DRS_MSG_SPNREPLY_V1 V1;
    } 	DRS_MSG_SPNREPLY;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0090
    {
    /* [string] */ LPWSTR ServerDN;
    /* [string] */ LPWSTR DomainDN;
    BOOL fCommit;
    } 	DRS_MSG_RMSVRREQ_V1;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0091
    {
    /* [case()] */ DRS_MSG_RMSVRREQ_V1 V1;
    } 	DRS_MSG_RMSVRREQ;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0092
    {
    BOOL fLastDcInDomain;
    } 	DRS_MSG_RMSVRREPLY_V1;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0093
    {
    /* [case()] */ DRS_MSG_RMSVRREPLY_V1 V1;
    } 	DRS_MSG_RMSVRREPLY;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0094
    {
    /* [string] */ LPWSTR DomainDN;
    } 	DRS_MSG_RMDMNREQ_V1;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0095
    {
    /* [case()] */ DRS_MSG_RMDMNREQ_V1 V1;
    } 	DRS_MSG_RMDMNREQ;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0096
    {
    DWORD Reserved;
    } 	DRS_MSG_RMDMNREPLY_V1;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0097
    {
    /* [case()] */ DRS_MSG_RMDMNREPLY_V1 V1;
    } 	DRS_MSG_RMDMNREPLY;

typedef /* [public][public][public][public] */ struct __MIDL_drsuapi_0098
    {
    /* [string] */ WCHAR *Domain;
    DWORD InfoLevel;
    } 	DRS_MSG_DCINFOREQ_V1;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0099
    {
    /* [case()] */ DRS_MSG_DCINFOREQ_V1 V1;
    } 	DRS_MSG_DCINFOREQ;

typedef /* [switch_type] */ union __MIDL_drsuapi_0099 *PDRS_MSG_DCINFOREQ;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0100
    {
    /* [range] */ DWORD cItems;
    /* [size_is] */ DS_DOMAIN_CONTROLLER_INFO_1W *rItems;
    } 	DRS_MSG_DCINFOREPLY_V1;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0101
    {
    /* [range] */ DWORD cItems;
    /* [size_is] */ DS_DOMAIN_CONTROLLER_INFO_2W *rItems;
    } 	DRS_MSG_DCINFOREPLY_V2;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0102
    {
    /* [range] */ DWORD cItems;
    /* [size_is] */ DS_DOMAIN_CONTROLLER_INFO_3W *rItems;
    } 	DRS_MSG_DCINFOREPLY_V3;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0103
    {
    /* [range] */ DWORD cItems;
    /* [size_is] */ DS_DOMAIN_CONTROLLER_INFO_FFFFFFFFW *rItems;
    } 	DRS_MSG_DCINFOREPLY_VFFFFFFFF;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0104
    {
    /* [case()] */ DRS_MSG_DCINFOREPLY_V1 V1;
    /* [case()] */ DRS_MSG_DCINFOREPLY_V2 V2;
    /* [case()] */ DRS_MSG_DCINFOREPLY_V3 V3;
    /* [case()] */ DRS_MSG_DCINFOREPLY_VFFFFFFFF VFFFFFFFF;
    } 	DRS_MSG_DCINFOREPLY;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0105
    {
    /* [ref] */ DSNAME *pObject;
    ATTRBLOCK AttrBlock;
    } 	DRS_MSG_ADDENTRYREQ_V1;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0106
    {
    ENTINFLIST EntInfList;
    } 	DRS_MSG_ADDENTRYREQ_V2;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0107
    {
    ENTINFLIST EntInfList;
    DRS_SecBufferDesc *pClientCreds;
    } 	DRS_MSG_ADDENTRYREQ_V3;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0108
    {
    /* [case()] */ DRS_MSG_ADDENTRYREQ_V1 V1;
    /* [case()] */ DRS_MSG_ADDENTRYREQ_V2 V2;
    /* [case()] */ DRS_MSG_ADDENTRYREQ_V3 V3;
    } 	DRS_MSG_ADDENTRYREQ;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0109
    {
    GUID Guid;
    NT4SID Sid;
    DWORD errCode;
    DWORD dsid;
    DWORD extendedErr;
    DWORD extendedData;
    USHORT problem;
    } 	DRS_MSG_ADDENTRYREPLY_V1;

typedef /* [public][public][public][public][public][public][public] */ struct __MIDL_drsuapi_0110
    {
    GUID objGuid;
    NT4SID objSid;
    } 	ADDENTRY_REPLY_INFO;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0111
    {
    /* [unique] */ DSNAME *pErrorObject;
    DWORD errCode;
    DWORD dsid;
    DWORD extendedErr;
    DWORD extendedData;
    USHORT problem;
    /* [range] */ ULONG cObjectsAdded;
    /* [size_is] */ ADDENTRY_REPLY_INFO *infoList;
    } 	DRS_MSG_ADDENTRYREPLY_V2;

typedef /* [public][public][public][public][public] */ struct __MIDL_drsuapi_0112
    {
    DWORD dwRepError;
    DWORD errCode;
    /* [switch_is] */ DIRERR_DRS_WIRE_V1 *pErrInfo;
    } 	DRS_ERROR_DATA_V1;

typedef /* [public][public][public][public][switch_type] */ union __MIDL_drsuapi_0113
    {
    /* [case()] */ DRS_ERROR_DATA_V1 V1;
    } 	DRS_ERROR_DATA;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0114
    {
    DSNAME *pdsErrObject;
    DWORD dwErrVer;
    /* [switch_is] */ DRS_ERROR_DATA *pErrData;
    /* [range] */ ULONG cObjectsAdded;
    /* [size_is] */ ADDENTRY_REPLY_INFO *infoList;
    } 	DRS_MSG_ADDENTRYREPLY_V3;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0115
    {
    /* [case()] */ DRS_MSG_ADDENTRYREPLY_V1 V1;
    /* [case()] */ DRS_MSG_ADDENTRYREPLY_V2 V2;
    /* [case()] */ DRS_MSG_ADDENTRYREPLY_V3 V3;
    } 	DRS_MSG_ADDENTRYREPLY;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0116
    {
    DWORD dwTaskID;
    DWORD dwFlags;
    } 	DRS_MSG_KCC_EXECUTE_V1;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0117
    {
    /* [case()] */ DRS_MSG_KCC_EXECUTE_V1 V1;
    } 	DRS_MSG_KCC_EXECUTE;

typedef /* [public][public][public][public] */ struct __MIDL_drsuapi_0118
    {
    ULONGLONG hCtx;
    LONG lReferenceCount;
    BOOL fIsBound;
    UUID uuidClient;
    DSTIME timeLastUsed;
    ULONG IPAddr;
    int pid;
    } 	DS_REPL_CLIENT_CONTEXT;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0119
    {
    /* [range] */ DWORD cNumContexts;
    DWORD dwReserved;
    /* [size_is] */ DS_REPL_CLIENT_CONTEXT rgContext[ 1 ];
    } 	DS_REPL_CLIENT_CONTEXTS;

typedef /* [public][public][public][public] */ struct __MIDL_drsuapi_0120
    {
    /* [string] */ LPWSTR pszServerName;
    BOOL fIsHandleBound;
    BOOL fIsHandleFromCache;
    BOOL fIsHandleInCache;
    DWORD dwThreadId;
    DWORD dwBindingTimeoutMins;
    DSTIME dstimeCreated;
    DWORD dwCallType;
    } 	DS_REPL_SERVER_OUTGOING_CALL;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0121
    {
    /* [range] */ DWORD cNumCalls;
    DWORD dwReserved;
    /* [size_is] */ DS_REPL_SERVER_OUTGOING_CALL rgCall[ 1 ];
    } 	DS_REPL_SERVER_OUTGOING_CALLS;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0122
    {
    DWORD InfoType;
    /* [string] */ LPWSTR pszObjectDN;
    UUID uuidSourceDsaObjGuid;
    } 	DRS_MSG_GETREPLINFO_REQ_V1;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0123
    {
    DWORD InfoType;
    /* [string] */ LPWSTR pszObjectDN;
    UUID uuidSourceDsaObjGuid;
    DWORD ulFlags;
    /* [string] */ LPWSTR pszAttributeName;
    /* [string] */ LPWSTR pszValueDN;
    DWORD dwEnumerationContext;
    } 	DRS_MSG_GETREPLINFO_REQ_V2;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0124
    {
    /* [case()] */ DRS_MSG_GETREPLINFO_REQ_V1 V1;
    /* [case()] */ DRS_MSG_GETREPLINFO_REQ_V2 V2;
    } 	DRS_MSG_GETREPLINFO_REQ;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0125
    {
    /* [case()] */ DS_REPL_NEIGHBORSW *pNeighbors;
    /* [case()] */ DS_REPL_CURSORS *pCursors;
    /* [case()] */ DS_REPL_OBJ_META_DATA *pObjMetaData;
    /* [case()] */ DS_REPL_KCC_DSA_FAILURESW *pConnectFailures;
    /* [case()] */ DS_REPL_KCC_DSA_FAILURESW *pLinkFailures;
    /* [case()] */ DS_REPL_PENDING_OPSW *pPendingOps;
    /* [case()] */ DS_REPL_ATTR_VALUE_META_DATA *pAttrValueMetaData;
    /* [case()] */ DS_REPL_CURSORS_2 *pCursors2;
    /* [case()] */ DS_REPL_CURSORS_3W *pCursors3;
    /* [case()] */ DS_REPL_OBJ_META_DATA_2 *pObjMetaData2;
    /* [case()] */ DS_REPL_ATTR_VALUE_META_DATA_2 *pAttrValueMetaData2;
    /* [case()] */ DS_REPL_SERVER_OUTGOING_CALLS *pServerOutgoingCalls;
    /* [case()] */ UPTODATE_VECTOR_V1_EXT *pUpToDateVec;
    /* [case()] */ DS_REPL_CLIENT_CONTEXTS *pClientContexts;
    /* [case()] */ DS_REPL_NEIGHBORSW *pRepsTo;
    } 	DRS_MSG_GETREPLINFO_REPLY;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0126
    {
    DWORD Flags;
    /* [string] */ WCHAR *SrcDomain;
    /* [string] */ WCHAR *SrcPrincipal;
    /* [full][string] */ WCHAR *SrcDomainController;
    /* [range] */ DWORD SrcCredsUserLength;
    /* [size_is] */ WCHAR *SrcCredsUser;
    /* [range] */ DWORD SrcCredsDomainLength;
    /* [size_is] */ WCHAR *SrcCredsDomain;
    /* [range] */ DWORD SrcCredsPasswordLength;
    /* [size_is] */ WCHAR *SrcCredsPassword;
    /* [string] */ WCHAR *DstDomain;
    /* [string] */ WCHAR *DstPrincipal;
    } 	DRS_MSG_ADDSIDREQ_V1;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0127
    {
    /* [case()] */ DRS_MSG_ADDSIDREQ_V1 V1;
    } 	DRS_MSG_ADDSIDREQ;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0128
    {
    DWORD dwWin32Error;
    } 	DRS_MSG_ADDSIDREPLY_V1;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0129
    {
    /* [case()] */ DRS_MSG_ADDSIDREPLY_V1 V1;
    } 	DRS_MSG_ADDSIDREPLY;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0130
    {
    /* [range] */ ULONG Count;
    /* [size_is] */ DRS_MSG_REVMEMB_REQ_V1 *Requests;
    } 	DRS_MSG_GETMEMBERSHIPS2_REQ_V1;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0131
    {
    /* [case()] */ DRS_MSG_GETMEMBERSHIPS2_REQ_V1 V1;
    } 	DRS_MSG_GETMEMBERSHIPS2_REQ;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0132
    {
    /* [range] */ ULONG Count;
    /* [size_is] */ DRS_MSG_REVMEMB_REPLY_V1 *Replies;
    } 	DRS_MSG_GETMEMBERSHIPS2_REPLY_V1;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0133
    {
    /* [case()] */ DRS_MSG_GETMEMBERSHIPS2_REPLY_V1 V1;
    } 	DRS_MSG_GETMEMBERSHIPS2_REPLY;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0134
    {
    /* [ref] */ DSNAME *pNC;
    UUID uuidDsaSrc;
    ULONG ulOptions;
    } 	DRS_MSG_REPVERIFYOBJ_V1;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0135
    {
    /* [case()] */ DRS_MSG_REPVERIFYOBJ_V1 V1;
    } 	DRS_MSG_REPVERIFYOBJ;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0136
    {
    UUID guidStart;
    DWORD cGuids;
    DSNAME *pNC;
    UPTODATE_VECTOR_V1_EXT *pUpToDateVecCommonV1;
    UCHAR Md5Digest[ 16 ];
    } 	DRS_MSG_EXISTREQ_V1;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0137
    {
    /* [case()] */ DRS_MSG_EXISTREQ_V1 V1;
    } 	DRS_MSG_EXISTREQ;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0138
    {
    DWORD dwStatusFlags;
    /* [range] */ DWORD cNumGuids;
    /* [size_is] */ UUID *rgGuids;
    } 	DRS_MSG_EXISTREPLY_V1;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0139
    {
    /* [case()] */ DRS_MSG_EXISTREPLY_V1 V1;
    } 	DRS_MSG_EXISTREPLY;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0140
    {
    /* [string] */ const WCHAR *pwszFromSite;
    /* [range] */ DWORD cToSites;
    /* [size_is][string] */ WCHAR **rgszToSites;
    DWORD dwFlags;
    } 	DRS_MSG_QUERYSITESREQ_V1;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0141
    {
    /* [case()] */ DRS_MSG_QUERYSITESREQ_V1 V1;
    } 	DRS_MSG_QUERYSITESREQ;

typedef /* [public][public][public][public] */ struct __MIDL_drsuapi_0142
    {
    DWORD dwErrorCode;
    DWORD dwCost;
    } 	DRS_MSG_QUERYSITESREPLYELEMENT_V1;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0143
    {
    /* [range] */ DWORD cToSites;
    /* [size_is] */ DRS_MSG_QUERYSITESREPLYELEMENT_V1 *rgCostInfo;
    DWORD dwFlags;
    } 	DRS_MSG_QUERYSITESREPLY_V1;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0144
    {
    /* [case()] */ DRS_MSG_QUERYSITESREPLY_V1 V1;
    } 	DRS_MSG_QUERYSITESREPLY;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0145
    {
    DWORD dwReserved;
    } 	DRS_MSG_INIT_DEMOTIONREQ_V1;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0146
    {
    /* [case()] */ DRS_MSG_INIT_DEMOTIONREQ_V1 V1;
    } 	DRS_MSG_INIT_DEMOTIONREQ;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0147
    {
    DWORD dwOpError;
    } 	DRS_MSG_INIT_DEMOTIONREPLY_V1;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0148
    {
    /* [case()] */ DRS_MSG_INIT_DEMOTIONREPLY_V1 V1;
    } 	DRS_MSG_INIT_DEMOTIONREPLY;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0149
    {
    DWORD dwFlags;
    UUID uuidHelperDest;
    /* [ref] */ DSNAME *pNC;
    } 	DRS_MSG_REPLICA_DEMOTIONREQ_V1;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0150
    {
    /* [case()] */ DRS_MSG_REPLICA_DEMOTIONREQ_V1 V1;
    } 	DRS_MSG_REPLICA_DEMOTIONREQ;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0151
    {
    DWORD dwOpError;
    } 	DRS_MSG_REPLICA_DEMOTIONREPLY_V1;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0152
    {
    /* [case()] */ DRS_MSG_REPLICA_DEMOTIONREPLY_V1 V1;
    } 	DRS_MSG_REPLICA_DEMOTIONREPLY;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0153
    {
    DWORD dwOperations;
    UUID uuidHelperDest;
    /* [string] */ LPWSTR szScriptBase;
    } 	DRS_MSG_FINISH_DEMOTIONREQ_V1;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0154
    {
    /* [case()] */ DRS_MSG_FINISH_DEMOTIONREQ_V1 V1;
    } 	DRS_MSG_FINISH_DEMOTIONREQ;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0155
    {
    DWORD dwOperationsDone;
    DWORD dwOpFailed;
    DWORD dwOpError;
    } 	DRS_MSG_FINISH_DEMOTIONREPLY_V1;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0156
    {
    /* [case()] */ DRS_MSG_FINISH_DEMOTIONREPLY_V1 V1;
    } 	DRS_MSG_FINISH_DEMOTIONREPLY;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0157
    {
    /* [string] */ const WCHAR *pwszCloneDCName;
    /* [string] */ const WCHAR *pwszSite;
    } 	DRS_MSG_ADDCLONEDCREQ_V1;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0158
    {
    /* [case()] */ DRS_MSG_ADDCLONEDCREQ_V1 V1;
    } 	DRS_MSG_ADDCLONEDCREQ;

typedef /* [public][public][public] */ struct __MIDL_drsuapi_0159
    {
    /* [string] */ WCHAR *pwszCloneDCName;
    /* [string] */ WCHAR *pwszSite;
    /* [range] */ DWORD cPasswordLength;
    /* [size_is] */ WCHAR *pwsNewDCAccountPassword;
    } 	DRS_MSG_ADDCLONEDCREPLY_V1;

typedef /* [public][public][switch_type] */ union __MIDL_drsuapi_0160
    {
    /* [case()] */ DRS_MSG_ADDCLONEDCREPLY_V1 V1;
    } 	DRS_MSG_ADDCLONEDCREPLY;

ULONG IDL_DRSBind( 
    /* [in] */ handle_t rpc_handle,
    /* [unique][in] */ UUID *puuidClientDsa,
    /* [unique][in] */ DRS_EXTENSIONS *pextClient,
    /* [out] */ DRS_EXTENSIONS **ppextServer,
    /* [ref][out] */ DRS_HANDLE *phDrs);

ULONG IDL_DRSUnbind( 
    /* [ref][out][in] */ DRS_HANDLE *phDrs);

ULONG IDL_DRSReplicaSync( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwVersion,
    /* [switch_is][ref][in] */ DRS_MSG_REPSYNC *pmsgSync);

ULONG IDL_DRSGetNCChanges( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_GETCHGREQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DRS_MSG_GETCHGREPLY *pmsgOut);

ULONG IDL_DRSUpdateRefs( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwVersion,
    /* [switch_is][ref][in] */ DRS_MSG_UPDREFS *pmsgUpdRefs);

ULONG IDL_DRSReplicaAdd( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwVersion,
    /* [switch_is][ref][in] */ DRS_MSG_REPADD *pmsgAdd);

ULONG IDL_DRSReplicaDel( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwVersion,
    /* [switch_is][ref][in] */ DRS_MSG_REPDEL *pmsgDel);

ULONG IDL_DRSReplicaModify( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwVersion,
    /* [switch_is][ref][in] */ DRS_MSG_REPMOD *pmsgMod);

ULONG IDL_DRSVerifyNames( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_VERIFYREQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DRS_MSG_VERIFYREPLY *pmsgOut);

ULONG IDL_DRSGetMemberships( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_REVMEMB_REQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DRS_MSG_REVMEMB_REPLY *pmsgOut);

ULONG IDL_DRSInterDomainMove( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_MOVEREQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DRS_MSG_MOVEREPLY *pmsgOut);

ULONG IDL_DRSGetNT4ChangeLog( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_NT4_CHGLOG_REQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DRS_MSG_NT4_CHGLOG_REPLY *pmsgOut);

ULONG IDL_DRSCrackNames( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_CRACKREQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DRS_MSG_CRACKREPLY *pmsgOut);

ULONG IDL_DRSWriteSPN( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_SPNREQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DRS_MSG_SPNREPLY *pmsgOut);

ULONG IDL_DRSRemoveDsServer( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_RMSVRREQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DRS_MSG_RMSVRREPLY *pmsgOut);

ULONG IDL_DRSRemoveDsDomain( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_RMDMNREQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DRS_MSG_RMDMNREPLY *pmsgOut);

ULONG IDL_DRSDomainControllerInfo( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_DCINFOREQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DRS_MSG_DCINFOREPLY *pmsgOut);

ULONG IDL_DRSAddEntry( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_ADDENTRYREQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DRS_MSG_ADDENTRYREPLY *pmsgOut);

ULONG IDL_DRSExecuteKCC( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_KCC_EXECUTE *pmsgIn);

ULONG IDL_DRSGetReplInfo( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_GETREPLINFO_REQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DRS_MSG_GETREPLINFO_REPLY *pmsgOut);

ULONG IDL_DRSAddSidHistory( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_ADDSIDREQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DRS_MSG_ADDSIDREPLY *pmsgOut);

ULONG IDL_DRSGetMemberships2( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_GETMEMBERSHIPS2_REQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DRS_MSG_GETMEMBERSHIPS2_REPLY *pmsgOut);

ULONG IDL_DRSReplicaVerifyObjects( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwVersion,
    /* [switch_is][ref][in] */ DRS_MSG_REPVERIFYOBJ *pmsgVerify);

ULONG IDL_DRSGetObjectExistence( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_EXISTREQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DRS_MSG_EXISTREPLY *pmsgOut);

ULONG IDL_DRSQuerySitesByCost( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_QUERYSITESREQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DRS_MSG_QUERYSITESREPLY *pmsgOut);

ULONG IDL_DRSInitDemotion( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_INIT_DEMOTIONREQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DRS_MSG_INIT_DEMOTIONREPLY *pmsgOut);

ULONG IDL_DRSReplicaDemotion( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_REPLICA_DEMOTIONREQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DRS_MSG_REPLICA_DEMOTIONREPLY *pmsgOut);

ULONG IDL_DRSFinishDemotion( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_FINISH_DEMOTIONREQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DRS_MSG_FINISH_DEMOTIONREPLY *pmsgOut);

ULONG IDL_DRSAddCloneDC( 
    /* [ref][in] */ DRS_HANDLE hDrs,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DRS_MSG_ADDCLONEDCREQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DRS_MSG_ADDCLONEDCREPLY *pmsgOut);



extern RPC_IF_HANDLE drsuapi_v4_0_c_ifspec;
extern RPC_IF_HANDLE drsuapi_v4_0_s_ifspec;
#endif /* __drsuapi_INTERFACE_DEFINED__ */

#ifndef __dsaop_INTERFACE_DEFINED__
#define __dsaop_INTERFACE_DEFINED__

/* interface dsaop */
/* [unique][version][uuid] */ 

typedef /* [public][public][public] */ struct __MIDL_dsaop_0001
    {
    DWORD Flags;
    /* [range] */ DWORD cbPassword;
    /* [size_is] */ BYTE *pbPassword;
    } 	DSA_MSG_EXECUTE_SCRIPT_REQ_V1;

typedef /* [public][public][switch_type] */ union __MIDL_dsaop_0002
    {
    /* [case()] */ DSA_MSG_EXECUTE_SCRIPT_REQ_V1 V1;
    } 	DSA_MSG_EXECUTE_SCRIPT_REQ;

typedef /* [public][public][public] */ struct __MIDL_dsaop_0003
    {
    DWORD dwOperationStatus;
    /* [string] */ LPWSTR pwErrMessage;
    } 	DSA_MSG_EXECUTE_SCRIPT_REPLY_V1;

typedef /* [public][public][switch_type] */ union __MIDL_dsaop_0004
    {
    /* [case()] */ DSA_MSG_EXECUTE_SCRIPT_REPLY_V1 V1;
    } 	DSA_MSG_EXECUTE_SCRIPT_REPLY;

typedef /* [public][public][public] */ struct __MIDL_dsaop_0005
    {
    DWORD Reserved;
    } 	DSA_MSG_PREPARE_SCRIPT_REQ_V1;

typedef /* [public][public][switch_type] */ union __MIDL_dsaop_0006
    {
    /* [case()] */ DSA_MSG_PREPARE_SCRIPT_REQ_V1 V1;
    } 	DSA_MSG_PREPARE_SCRIPT_REQ;

typedef /* [public][public][public] */ struct __MIDL_dsaop_0007
    {
    DWORD dwOperationStatus;
    /* [string] */ LPWSTR pwErrMessage;
    /* [range] */ DWORD cbPassword;
    /* [size_is] */ BYTE *pbPassword;
    /* [range] */ DWORD cbHashBody;
    /* [size_is] */ BYTE *pbHashBody;
    /* [range] */ DWORD cbHashSignature;
    /* [size_is] */ BYTE *pbHashSignature;
    } 	DSA_MSG_PREPARE_SCRIPT_REPLY_V1;

typedef /* [public][public][switch_type] */ union __MIDL_dsaop_0008
    {
    /* [case()] */ DSA_MSG_PREPARE_SCRIPT_REPLY_V1 V1;
    } 	DSA_MSG_PREPARE_SCRIPT_REPLY;

ULONG IDL_DSAPrepareScript( 
    /* [in] */ handle_t hRpc,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DSA_MSG_PREPARE_SCRIPT_REQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DSA_MSG_PREPARE_SCRIPT_REPLY *pmsgOut);

ULONG IDL_DSAExecuteScript( 
    /* [in] */ handle_t hRpc,
    /* [in] */ DWORD dwInVersion,
    /* [switch_is][ref][in] */ DSA_MSG_EXECUTE_SCRIPT_REQ *pmsgIn,
    /* [ref][out] */ DWORD *pdwOutVersion,
    /* [switch_is][ref][out] */ DSA_MSG_EXECUTE_SCRIPT_REPLY *pmsgOut);



extern RPC_IF_HANDLE dsaop_v1_0_c_ifspec;
extern RPC_IF_HANDLE dsaop_v1_0_s_ifspec;
#endif /* __dsaop_INTERFACE_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

void __RPC_USER DRS_HANDLE_rundown( DRS_HANDLE );

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


