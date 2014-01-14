// TestAPP.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <ctime>
#include <cstdlib>

static const char alphanum [] =
"0123456789"
"ABCDEFGHIJKLMNOPQRSTUVWXYZ";
int ArrayLength = sizeof(alphanum) -1;

char GenerateRandomCharacter()
{
	return alphanum[rand() % ArrayLength];
}

DWORD GetPhysicalDriveSerialNumber(UINT nDriveNumber IN, CString& strSerialNumber OUT)
{
	DWORD dwRet = NO_ERROR;
	strSerialNumber.Empty();

	// Format physical drive path (may be '\\.\PhysicalDrive0', '\\.\PhysicalDrive1' and so on).
	CString strDrivePath;
	strDrivePath.Format(_T("\\\\.\\PhysicalDrive%u"), nDriveNumber);

	// Get a handle to physical drive
	HANDLE hDevice = ::CreateFile(strDrivePath, 0, FILE_SHARE_READ | FILE_SHARE_WRITE,
		NULL, OPEN_EXISTING, 0, NULL);

	if (INVALID_HANDLE_VALUE == hDevice)
		return ::GetLastError();

	// Set the input data structure
	STORAGE_PROPERTY_QUERY storagePropertyQuery;
	ZeroMemory(&storagePropertyQuery, sizeof(STORAGE_PROPERTY_QUERY));
	storagePropertyQuery.PropertyId = StorageDeviceProperty;
	storagePropertyQuery.QueryType = PropertyStandardQuery;

	// Get the necessary output buffer size
	STORAGE_DESCRIPTOR_HEADER storageDescriptorHeader = { 0 };
	DWORD dwBytesReturned = 0;
	if (!::DeviceIoControl(hDevice, IOCTL_STORAGE_QUERY_PROPERTY,
		&storagePropertyQuery, sizeof(STORAGE_PROPERTY_QUERY),
		&storageDescriptorHeader, sizeof(STORAGE_DESCRIPTOR_HEADER),
		&dwBytesReturned, NULL))
	{
		dwRet = ::GetLastError();
		::CloseHandle(hDevice);
		return dwRet;
	}

	// Alloc the output buffer
	const DWORD dwOutBufferSize = storageDescriptorHeader.Size;
	BYTE* pOutBuffer = new BYTE[dwOutBufferSize];
	ZeroMemory(pOutBuffer, dwOutBufferSize);

	// Get the storage device descriptor
	if (!::DeviceIoControl(hDevice, IOCTL_STORAGE_QUERY_PROPERTY,
		&storagePropertyQuery, sizeof(STORAGE_PROPERTY_QUERY),
		pOutBuffer, dwOutBufferSize,
		&dwBytesReturned, NULL))
	{
		dwRet = ::GetLastError();
		delete[]pOutBuffer;
		::CloseHandle(hDevice);
		return dwRet;
	}
	//00cb5b90
	// Now, the output buffer points to a STORAGE_DEVICE_DESCRIPTOR structure
	// followed by additional info like vendor ID, product ID, serial number, and so on.
	STORAGE_DEVICE_DESCRIPTOR* pDeviceDescriptor = (STORAGE_DEVICE_DESCRIPTOR*) pOutBuffer;
	const DWORD dwSerialNumberOffset = pDeviceDescriptor->SerialNumberOffset;
	char *serialTest = (char*) (pOutBuffer + dwSerialNumberOffset);
	if (dwSerialNumberOffset != 0)
	{
		// Finally, get the serial number
		strSerialNumber = CString(pOutBuffer + dwSerialNumberOffset);
	}

	_tprintf(_T(" Length(no trim): %d Old Serial %s\n\n"), strSerialNumber.GetLength(), strSerialNumber);

	BYTE *SerialStart = (pOutBuffer + dwSerialNumberOffset);
	for (int i = 0; i < strlen(serialTest); i++)
	{
		//SerialStart = (pOutBuffer + (dwSerialNumberOffset+i));
		serialTest[i] = GenerateRandomCharacter();
	}

	strSerialNumber = CString(pOutBuffer + dwSerialNumberOffset);
	_tprintf(_T("New Serial %s new length %d"), strSerialNumber, strSerialNumber.GetLength());


	// Do cleanup and return
	delete[]pOutBuffer;
	::CloseHandle(hDevice);
	return dwRet;
}

int _tmain(int argc, _TCHAR* argv[])
{
	CString strSerialNumber;
	GetPhysicalDriveSerialNumber(0, strSerialNumber);
	GetPhysicalDriveSerialNumber(1, strSerialNumber);
	GetPhysicalDriveSerialNumber(2, strSerialNumber);
	return 0;
}

