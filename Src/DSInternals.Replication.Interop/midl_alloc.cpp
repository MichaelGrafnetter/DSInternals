#include "stdafx.h"
#include "midl_alloc.h"

/// <summary>
/// The midl_user_allocate function is a function that client and server applications provide to allocate memory.
/// </summary>
/// <param name="size">Specifies the count of bytes to allocate.</param>
/// <returns>If midl_user_allocate fails to allocate memory, it must return a NULL pointer.</returns>
void* __RPC_USER midl_user_allocate(size_t size)
{
	void* address = malloc(size);
	if (address != 0)
	{
		// Zero fill for safety
		memset(address, 0, size);
	}
	return address;
}
 
/// <summary>
/// The midl_user_free function is provided by client and server applications to deallocate dynamically allocated memory.
/// </summary>
/// <param name="p">A pointer to the memory block to be freed.</param>
void __RPC_USER midl_user_free(void* p)
{
	// Free would throw an error on null pointer. If we check it here, user code can kept simpler.
	if (p != nullptr)
	{
		free(p);
	}
}