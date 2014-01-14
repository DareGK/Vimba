// EasyVMT
//
// Easy Virtual Method Table manipulation
//
// 10-26-2009
// Updated to added optional calls to VirtualProtect
//
// Credits: zoomgod
//
#include <Windows.h>

class EasyVMT
{
public:
	DWORD* pVMT;

	static VOID *GetVTableAddress(DWORD *address, UINT vTableIndex);

	UINT CountFunctions(void);
	DWORD GetVMT(void);
	void Freeup();
	bool SetVMT(DWORD* pNewVMT, bool bUseProtect = true);
	bool NewVMT(bool bUseProtect = true);
	DWORD SetFunction(UINT FnIndex, DWORD NewFnAddress, bool bUseProtect = true);
	DWORD GetFunctionAddress(UINT FnIndex);
};
