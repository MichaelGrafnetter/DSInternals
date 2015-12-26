#pragma once

#include <rpc.h>
#include <memory>
#include <string>

#ifdef __cplusplus 
extern "C"{
#endif

void* __RPC_USER midl_user_allocate(size_t size);
void __RPC_USER midl_user_free(void* p);

#ifdef __cplusplus 
}
#endif

/// <summary>
/// Custom deleter for unique_ptr, that calls midl_user_free instead of delete.
/// </summary>
template <typename T>
struct midl_delete {
	// Empty constructor
	midl_delete();
	// Deletion functor
	void operator()(T* ptr) const;
};

template <typename T>
midl_delete<T>::midl_delete() {}

template<typename T>
void midl_delete<T>::operator()(T* ptr) const
{
	midl_user_free(ptr);
}

// Define the midl_ptr as parametrized unique_ptr.
template <typename T>
using midl_ptr = std::unique_ptr<T, midl_delete<T>>;

// Allocators:

template <typename T>
midl_ptr<T> make_midl_ptr()
{
	return midl_ptr<T>((T*)midl_user_allocate(sizeof(T)));
}

template <typename T>
midl_ptr<T> make_midl_ptr(ULONG count)
{
	size_t totalSize = count * sizeof(T);
	return midl_ptr<T>((T*)midl_user_allocate(totalSize));
}