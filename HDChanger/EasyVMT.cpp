// EasyVMT
//
// Easy Virtual Method Table manipulation
//
// 10-26-2009
// Updated to added optional calls to VirtualProtect
//
// Credits: zoomgod
//

#include "EasyVMT.h"

//
// Count virtual methods in table
//
UINT EasyVMT::CountFunctions(void)
{

	DWORD dwFunction = 0;

	while (pVMT)
	{

		if (IsBadReadPtr((void*) &pVMT[dwFunction], 4))
			break;

		if (pVMT[dwFunction] == NULL || pVMT[dwFunction] == 0)
			break;

		if (!pVMT[dwFunction] || IsBadCodePtr((FARPROC) pVMT[dwFunction]))
			break;

		dwFunction++;

	}

	return dwFunction;

}

//
// Get the VMT address
//
DWORD EasyVMT::GetVMT(void)
{
	return (DWORD) &this->pVMT[0];
}

//
// Set VMT to another address
//
bool EasyVMT::SetVMT(DWORD* pNewVMT, bool bUseProtect)
{

	DWORD dwOldProtection;

	if (bUseProtect &&
		!VirtualProtect((void*)this->pVMT, 4, PAGE_EXECUTE_READWRITE, &dwOldProtection))
		return false;

	this->pVMT = pNewVMT;

	if (bUseProtect)
		VirtualProtect((void*)this->pVMT, 4, dwOldProtection, NULL);

	return true;

}

//
// Creates a new VMT, copies existing table to it and replaces pointer
//
bool EasyVMT::NewVMT(bool bUseProtect)
{

	DWORD dwOldProtection;

	UINT uiCount = this->CountFunctions();
	if (!uiCount) return false;

	UINT uiBytes = uiCount * 4;
	DWORD* pNewVMT = (DWORD*) malloc(uiBytes);
	if (!pNewVMT) return false;

	memcpy(pNewVMT, (void*)this->pVMT, uiBytes);

	if (bUseProtect &&
		!VirtualProtect((void*)this->pVMT, 4, PAGE_EXECUTE_READWRITE, &dwOldProtection))
	{
		free(pNewVMT);
		return false;
	}

	this->pVMT = pNewVMT;

	if (bUseProtect)
		VirtualProtect((void*)this->pVMT, 4, dwOldProtection, NULL);

	return true;

}

//
// Easy VMT hooking
//
DWORD EasyVMT::SetFunction(UINT FnIndex, DWORD NewFnAddress, bool bUseProtect)
{

	DWORD dwOldProtection;

	if (FnIndex > CountFunctions() - 1)
		return 0;

	if ((this->pVMT[FnIndex] == NewFnAddress))
		return 0;

	if (bUseProtect &&
		!VirtualProtect((void*) (this->pVMT[FnIndex]), 4, PAGE_EXECUTE_READWRITE, &dwOldProtection))
		return 0;

	DWORD dwCurrent = this->pVMT[FnIndex];
	this->pVMT[FnIndex] = NewFnAddress;

	if (bUseProtect)
		VirtualProtect((void*) (this->pVMT[FnIndex]), 4, dwOldProtection, NULL);

	return dwCurrent;

}

void EasyVMT::Freeup()
{
	free(pVMT);
}

//
// Get virtual function address
//
DWORD EasyVMT::GetFunctionAddress(UINT FnIndex)
{
	return this->pVMT[FnIndex];
}


VOID *EasyVMT::GetVTableAddress(DWORD *address, UINT vTableIndex)
{
	if (IsBadReadPtr((void*) &address[vTableIndex], 4))
	{
		return 0;
	}

	return &address[vTableIndex];
}