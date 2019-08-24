#pragma once

#include "midl_alloc.h"
#include "drsr.h"
#include "drsr_addons.h"

// Specialized allocators and deleters:

template<>
void midl_delete<DRS_MSG_GETCHGREPLY_V1>::operator()(DRS_MSG_GETCHGREPLY_V1* reply) const;

template<>
void midl_delete<DRS_MSG_GETCHGREPLY_V6>::operator()(DRS_MSG_GETCHGREPLY_V6* reply) const;

template<>
void midl_delete<DRS_MSG_GETCHGREPLY_V9>::operator()(DRS_MSG_GETCHGREPLY_V9* reply) const;

template<>
void midl_delete<DRS_MSG_GETCHGREQ_V5>::operator()(DRS_MSG_GETCHGREQ_V5* request) const;

template<>
void midl_delete<DRS_MSG_GETCHGREQ_V8>::operator()(DRS_MSG_GETCHGREQ_V8* request) const;

template<>
void midl_delete<DRS_MSG_GETCHGREQ_V10>::operator()(DRS_MSG_GETCHGREQ_V10* request) const;

template<>
void midl_delete<DRS_MSG_CRACKREQ_V1>::operator()(DRS_MSG_CRACKREQ_V1* request) const;

template<>
void midl_delete<DRS_MSG_CRACKREPLY_V1>::operator()(DRS_MSG_CRACKREPLY_V1* request) const;

template<>
void midl_delete<DRS_MSG_GETREPLINFO_REQ_V1>::operator()(DRS_MSG_GETREPLINFO_REQ_V1* request) const;

template<>
void midl_delete<DRS_MSG_WRITENGCKEYREQ_V1>::operator()(DRS_MSG_WRITENGCKEYREQ_V1* request) const;

template<>
midl_ptr<DRS_EXTENSIONS_INT> make_midl_ptr();

template<>
midl_ptr<DSNAME> make_midl_ptr(ULONG numChars);

template<>
midl_ptr<UPTODATE_VECTOR_V1_EXT> make_midl_ptr(ULONG numCursors);

template<>
midl_ptr<PARTIAL_ATTR_VECTOR_V1_EXT> make_midl_ptr(ULONG numAttributes);

template<>
midl_ptr<DRS_MSG_CRACKREQ_V1> make_midl_ptr(ULONG numNames);
