// ShavedIceNative.cpp : DLL アプリケーション用にエクスポートされる関数を定義します。
//

#include "stdafx.h"

using namespace 

#define DLL_FUNCTION __declspec(dllexport)

DLL_FUNCTION BOOL GetElements()
{
	HMODULE hmodule = NULL;
	hmodule = LoadLibrary(L"TEST.dll");
	if (hmodule == NULL)
	{
		return FALSE;
	}
	MAKEINTRESOURCEW(hmodule, 1);
	return TRUE;
}
