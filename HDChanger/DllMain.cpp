#include <Windows.h>
#include <iostream>
#include "EasyVMT.h"
#include <detours.h>
#include <string.h>
#include <fstream>

#include <stdlib.h>

#include "atlbase.h"
#include "atlstr.h"
#include "comutil.h"

using namespace std;

static const char alphanum[] =
"0123456789"
"ABCDEFGHIJKLMNOPQRSTUVWXYZ";
int ArrayLength = sizeof(alphanum) -1;
static bool psMsg = true;
static bool ChkDrive = false;
char GenerateRandomCharacter()
{
	return alphanum[rand() % ArrayLength];
}


typedef BOOL (WINAPI* DeviceIoControl_t)(HANDLE hDevice,DWORD dwIoControlCode,LPVOID lpInBuffer,DWORD nInBufferSize, 
	LPVOID lpOutBuffer, DWORD nOutBufferSize, LPDWORD lpBytesReturned, LPOVERLAPPED lpOverlapped);
typedef WINBASEAPI HANDLE (WINAPI* CreateFile_t)(LPCWSTR lpFileName, DWORD dwDesiredAccess, 
										         DWORD dwShareMode, LPSECURITY_ATTRIBUTES lpSecurityAttributes, 
												 DWORD dwCreationDisposition, DWORD dwFlagsAndAttributes, 
												 HANDLE hTemplateFile);


DeviceIoControl_t pDeviceIoControl;
CreateFile_t pCreateFile;

void ChangeThings(PBYTE DeviceInfo, DWORD BytesReturned)
{
	STORAGE_DEVICE_DESCRIPTOR *pDeviceDescriptor = (STORAGE_DEVICE_DESCRIPTOR*) DeviceInfo;
	DWORD dwSerialOffset;
	if (BytesReturned > 20)
	{

		pDeviceDescriptor = (STORAGE_DEVICE_DESCRIPTOR*) DeviceInfo;

		if (pDeviceDescriptor != NULL)
		{
			ofstream myfile;
			myfile.open("Log.txt");

			dwSerialOffset = pDeviceDescriptor->SerialNumberOffset;
			if (dwSerialOffset != 0)
			{
				char *HDSerial = (char*) (DeviceInfo + dwSerialOffset);
				for (int i = 0; i < strlen(HDSerial); i++)
				{
					HDSerial[i] = GenerateRandomCharacter();
				}
				myfile << HDSerial << endl;
				ChkDrive = false;
			}


			myfile.close();
		}
	}
}

BOOL WINAPI DeviceIoControlHK(HANDLE hDevice, DWORD ICCode, LPVOID ICBuffer, DWORD ICSize,
	PBYTE DeviceInfo, DWORD DeviceInfoSize, LPDWORD bytesReturned,
	LPOVERLAPPED lpOverlapped)
{
	bool State = pDeviceIoControl(hDevice, ICCode, ICBuffer, ICSize,
		DeviceInfo, DeviceInfoSize, bytesReturned,
		lpOverlapped);

	if (ChkDrive == true && bytesReturned[0] > 50)
	{
		ChangeThings(DeviceInfo, bytesReturned[0]);
	}

	return State;

}

HANDLE WINAPI CreateFileHK(LPCWSTR lpFileName, DWORD dwDesiredAccess,
	DWORD dwShareMode, LPSECURITY_ATTRIBUTES lpSecurityAttributes,
	DWORD dwCreationDisposition, DWORD dwFlagsAndAttributes,
	HANDLE hTemplateFile)
{
	CString *fileName = new CString(lpFileName);

	if(fileName->Find(_T("PhysicalDrive")) != -1)
	{
		ChkDrive = true;
	}

	return pCreateFile(lpFileName, dwDesiredAccess, dwShareMode, lpSecurityAttributes, dwCreationDisposition, dwFlagsAndAttributes, hTemplateFile);
}

DWORD WINAPI StartHack()
{
	pDeviceIoControl = (DeviceIoControl_t) DetourFunction((BYTE*) DeviceIoControl, (BYTE*) DeviceIoControlHK);
	pCreateFile = (CreateFile_t) DetourFunction((PBYTE) CreateFileW, (PBYTE) CreateFileHK);
	return 0;
}

BOOL WINAPI DllMain(HMODULE hModule, DWORD dwReason, LPVOID lpReserved)
{

	if (dwReason == DLL_PROCESS_ATTACH)
	{
		CreateThread(0, 0, (LPTHREAD_START_ROUTINE) StartHack, 0, 0, 0);
	}

	return TRUE;
}