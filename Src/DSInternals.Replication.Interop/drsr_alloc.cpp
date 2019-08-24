#include "stdafx.h"
#include "drsr_alloc.h"

template<>
midl_ptr<DRS_EXTENSIONS_INT> make_midl_ptr()
{
	auto extensions = midl_ptr<DRS_EXTENSIONS_INT>((DRS_EXTENSIONS_INT*)midl_user_allocate(sizeof(DRS_EXTENSIONS_INT)));
	// size excluding the actual count
	extensions->cb = sizeof(DRS_EXTENSIONS_INT) - sizeof(DWORD);
	return extensions;
}

template<>
midl_ptr<DSNAME> make_midl_ptr(ULONG numChars)
{
	ULONG totalSize = sizeof(DSNAME) + numChars * sizeof(WCHAR);
	auto dsName = midl_ptr<DSNAME>((DSNAME*)midl_user_allocate(totalSize));
	dsName->NameLen = numChars;
	dsName->structLen = totalSize;
	return dsName;
}

template<>
midl_ptr<UPTODATE_VECTOR_V1_EXT> make_midl_ptr(ULONG numCursors)
{
	size_t totalSize = sizeof(UPTODATE_VECTOR_V1_EXT) + numCursors * sizeof(UPTODATE_CURSOR_V1);
	auto vector = midl_ptr<UPTODATE_VECTOR_V1_EXT>((UPTODATE_VECTOR_V1_EXT*)midl_user_allocate(totalSize));
	vector->cNumCursors = numCursors;
	vector->dwVersion = 1;
	return vector;
}

template<>
midl_ptr<PARTIAL_ATTR_VECTOR_V1_EXT> make_midl_ptr(ULONG numAttributes)
{
	auto totalSize = sizeof(PARTIAL_ATTR_VECTOR_V1_EXT) + numAttributes * sizeof(ATTRTYP);
	auto pas = midl_ptr<PARTIAL_ATTR_VECTOR_V1_EXT>((PARTIAL_ATTR_VECTOR_V1_EXT*)midl_user_allocate(totalSize));
	pas->cAttrs = numAttributes;
	pas->dwVersion = 1;
	return pas;
}

template<>
midl_ptr<DRS_MSG_CRACKREQ_V1> make_midl_ptr(ULONG numNames)
{
	auto request = midl_ptr<DRS_MSG_CRACKREQ_V1>((DRS_MSG_CRACKREQ_V1*)midl_user_allocate(sizeof(DRS_MSG_CRACKREQ_V1)));
	request->cNames = numNames;
	request->rpNames = (WCHAR * *)midl_user_allocate(numNames * sizeof(WCHAR*));
	return request;
}

template<>
void midl_delete<DRS_MSG_GETCHGREQ_V5>::operator()(DRS_MSG_GETCHGREQ_V5* request) const
{
	if (request != nullptr)
	{
		// Perform deep free
		midl_user_free(request->pNC);
		midl_user_free(request->pUpToDateVecDestV1);

		// Now free the encapsulating object:
		midl_user_free(request);
	}
}

template<>
void midl_delete<DRS_MSG_GETCHGREQ_V8>::operator()(DRS_MSG_GETCHGREQ_V8* request) const
{
	if (request == nullptr)
	{
		return;
	}

	// Perform deep free
	midl_user_free(request->pPartialAttrSet);
	midl_user_free(request->pPartialAttrSetEx);

	// Free all SCHEMA_PREFIX_TABLE entries
	for (DWORD i = 0; i < request->PrefixTableDest.PrefixCount; i++)
	{
		midl_user_free(request->PrefixTableDest.pPrefixEntry[i].prefix.elements);
	}
	midl_user_free(request->PrefixTableDest.pPrefixEntry);

	// The DRS_MSG_GETCHGREQ_V8 message is a superset of DRS_MSG_GETCHGREQ_V5.
	auto requestV5 = midl_ptr<DRS_MSG_GETCHGREQ_V5>((DRS_MSG_GETCHGREQ_V5*)request);
}

template<>
void midl_delete<DRS_MSG_GETCHGREQ_V10>::operator()(DRS_MSG_GETCHGREQ_V10* request) const
{
	if (request != nullptr)
	{
		// The DRS_MSG_GETCHGREQ_V10 message is a superset of DRS_MSG_GETCHGREQ_V8.
		auto requestV8 = midl_ptr<DRS_MSG_GETCHGREQ_V8>((DRS_MSG_GETCHGREQ_V8*)request);
	}
}

template<>
void midl_delete<DRS_MSG_CRACKREQ_V1>::operator()(DRS_MSG_CRACKREQ_V1* request) const
{
	if (request == nullptr)
	{
		return;
	}

	midl_user_free(request->rpNames);
	midl_user_free(request);
}

template<>
void midl_delete<DRS_MSG_CRACKREPLY_V1>::operator()(DRS_MSG_CRACKREPLY_V1* reply) const
{
	if (reply == nullptr)
	{
		return;
	}

	if (reply->pResult != nullptr)
	{
		// Perform deep free
		auto numItems = reply->pResult->cItems;
		for (DWORD i = 0; i < numItems; i++)
		{
			auto currentItem = reply->pResult->rItems[i];
			midl_user_free(currentItem.pDomain);
			midl_user_free(currentItem.pName);
		}
		midl_user_free(reply->pResult->rItems);
		midl_user_free(reply->pResult);
	}

	// Free the wrapping object itself
	midl_user_free(reply);
}

template<>
void midl_delete<DRS_MSG_GETCHGREPLY_V1>::operator()(DRS_MSG_GETCHGREPLY_V1* reply) const
{
	if (reply == nullptr)
	{
		return;
	}

	// Perform deep free
	midl_user_free(reply->pNC);
	midl_user_free(reply->pUpToDateVecSrcV1);

	// Free the prefix table:
	int numPrefixes = reply->PrefixTableSrc.PrefixCount;
	for (int i = 0; i < numPrefixes; i++)
	{
		midl_user_free(reply->PrefixTableSrc.pPrefixEntry[i].prefix.elements);
	}
	midl_user_free(reply->PrefixTableSrc.pPrefixEntry);

	// Free the replicated objects:
	auto currentObject = reply->pObjects;
	while (currentObject != nullptr)
	{
		midl_user_free(currentObject->Entinf.pName);
		midl_user_free(currentObject->pMetaDataExt);
		midl_user_free(currentObject->pParentGuid);

		// Free all object attributes:
		ULONG numAttributes = currentObject->Entinf.AttrBlock.attrCount;
		for (ULONG i = 0; i < numAttributes; i++)
		{
			auto currentAttribute = currentObject->Entinf.AttrBlock.pAttr[i];
			// Free all attribute values:
			ULONG numValues = currentAttribute.AttrVal.valCount;
			for (ULONG j = 0; j < numValues; j++)
			{
				auto currentValue = currentAttribute.AttrVal.pAVal[j];
				midl_user_free(currentValue.pVal);
			}
			// Free the value list:
			midl_user_free(currentAttribute.AttrVal.pAVal);
		}
		// Free the attribute list:
		midl_user_free(currentObject->Entinf.AttrBlock.pAttr);

		// Free the current object and go to next:
		auto nextObject = currentObject->pNextEntInf;
		midl_user_free(currentObject);
		currentObject = nextObject;
	}

	// Finally, free the encapsulating object:
	midl_user_free(reply);
}

template<>
void midl_delete<DRS_MSG_GETCHGREPLY_V6>::operator()(DRS_MSG_GETCHGREPLY_V6* reply) const
{
	if (reply != nullptr)
	{
		// Free the linked values (REPLVALINF_V1):
		for (DWORD i = 0; i < reply->cNumValues; i++)
		{
			auto currentValue = reply->rgValues[i];
			midl_user_free(currentValue.pObject);
			midl_user_free(currentValue.Aval.pVal);
		}

		// Free the encapsulating array. It does not matter whether it is of type REPLVALINF_V1 or REPLVALINF_V3.
		midl_user_free(reply->rgValues);

		// The DRS_MSG_GETCHGREPLY_V6 message is a superset of DRS_MSG_GETCHGREPLY_V1.
		auto replyV1 = midl_ptr<DRS_MSG_GETCHGREPLY_V1>((DRS_MSG_GETCHGREPLY_V1*)reply);
	}
}

template<>
void midl_delete<DRS_MSG_GETCHGREPLY_V9>::operator()(DRS_MSG_GETCHGREPLY_V9* reply) const
{
	if (reply != nullptr)
	{
		// Free the linked values (REPLVALINF_V3):
		for (DWORD i = 0; i < reply->cNumValues; i++)
		{
			auto currentValue = reply->rgValues[i];
			midl_user_free(currentValue.pObject);
			midl_user_free(currentValue.Aval.pVal);
		}

		/* The DRS_MSG_GETCHGREPLY_V6 deleter should not go through these values,
		   because it would interpret them as REPLVALINF_V1 instead of REPLVALINF_V3.
		*/
		reply->cNumValues = 0;

		// The DRS_MSG_GETCHGREPLY_V9 message is a superset of DRS_MSG_GETCHGREPLY_V6.
		auto replyV6 = midl_ptr<DRS_MSG_GETCHGREPLY_V6>((DRS_MSG_GETCHGREPLY_V6*)reply);
	}
}

template<>
void midl_delete<DRS_MSG_GETREPLINFO_REQ_V1>::operator()(DRS_MSG_GETREPLINFO_REQ_V1* request) const
{
	if (request == nullptr)
	{
		return;
	}

	// Free the DN string
	midl_user_free(request->pszObjectDN);

	// Free the encapsulating object:
	midl_user_free(request);
}

template<>
void midl_delete<DRS_MSG_WRITENGCKEYREQ_V1>::operator()(DRS_MSG_WRITENGCKEYREQ_V1* request) const
{
	if (request == nullptr)
	{
		return;
	}

	// Free the key
	midl_user_free(request->pNgcKey);

	// Free the DN string
	midl_user_free((void*)request->pwszAccount);

	// Free the encapsulating object:
	midl_user_free(request);
}
