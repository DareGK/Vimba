using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NetworkInterface
{
    public class AdapterInformation
    {
        private IP_ADAPTER_INFO _adapterInfo;

        //--------------------------------------------------------------------------------
        //PINVOKE Const Condtitions
        private const int MAX_ADAPTER_DESCRIPTION_LENGTH = 128;
        private const int ERROR_BUFFER_OVERFLOW = 111;
        private const int MAX_ADAPTER_NAME_LENGTH = 256;
        private const int MAX_ADAPTER_ADDRESS_LENGTH = 8;
        private const int MIB_IF_TYPE_OTHER = 1;
        private const int MIB_IF_TYPE_ETHERNET = 6;
        private const int MIB_IF_TYPE_TOKENRING = 9;
        private const int MIB_IF_TYPE_FDDI = 15;
        private const int MIB_IF_TYPE_PPP = 23;
        private const int MIB_IF_TYPE_LOOPBACK = 24;
        private const int MIB_IF_TYPE_SLIP = 28;

        //--------------------------------------------------------------------------------
        //PINVOKE STRUCTURES

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        private struct IP_ADAPTER_INFO
        {
            public IntPtr Next;
            public Int32 ComboIndex;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ADAPTER_NAME_LENGTH + 4)]
            public string AdapterName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ADAPTER_DESCRIPTION_LENGTH + 4)]
            public string AdapterDescription;
            public UInt32 AddressLength;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_ADAPTER_ADDRESS_LENGTH)]
            public byte[] Address;
            public Int32 Index;
            public UInt32 Type;
            public UInt32 DhcpEnabled;
            public IntPtr CurrentIpAddress;
            public IP_ADDR_STRING IpAddressList;
            public IP_ADDR_STRING GatewayList;
            public IP_ADDR_STRING DhcpServer;
            public bool HaveWins;
            public IP_ADDR_STRING PrimaryWinsServer;
            public IP_ADDR_STRING SecondaryWinsServer;
            public Int32 LeaseObtained;
            public Int32 LeaseExpires;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        private struct IP_ADDR_STRING
        {
            public IntPtr Next;
            public IP_ADDRESS_STRING IpAddress;
            public IP_ADDRESS_STRING IpMask;
            public Int32 Context;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        private struct IP_ADDRESS_STRING
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string Address;
        }

        //--------------------------------------------------------------------------------

        public AdapterInformation()
        {
            
        }

        public void GetAdapters()
        {
            long structSize = Marshal.SizeOf(typeof (IP_ADAPTER_INFO));
            IntPtr pAdapter = Marshal.AllocHGlobal(new IntPtr(structSize));

            int ret = GetAdaptersInfo(pAdapter, ref structSize);

            if (ret == ERROR_BUFFER_OVERFLOW) //Allocated Size was not big enough
            {
                pAdapter = Marshal.ReAllocHGlobal(pAdapter, new IntPtr(structSize));
                ret      = GetAdaptersInfo(pAdapter, ref structSize);
            }

            if (ret == 0)
            {
                _adapterInfo = (IP_ADAPTER_INFO)Marshal.PtrToStructure(pAdapter, typeof(IP_ADAPTER_INFO));
            }
        }

        public string[] GetAdaptersNames()
        {
            List<string> AdapterName = new List<string>();
            IP_ADAPTER_INFO tempAdapter = _adapterInfo;
            IntPtr nextAdapterPtr = Marshal.AllocHGlobal(Marshal.SizeOf(_adapterInfo));
            Marshal.StructureToPtr(_adapterInfo, nextAdapterPtr, true);


            do
            {
                tempAdapter = (IP_ADAPTER_INFO)Marshal.PtrToStructure(nextAdapterPtr, typeof(IP_ADAPTER_INFO));
                AdapterName.Add(tempAdapter.AdapterName);

                nextAdapterPtr = tempAdapter.Next;
            } while (nextAdapterPtr != IntPtr.Zero);


            return AdapterName.ToArray();
        }

        public string[] GetAdaptersMacs()
        {
            List<string> AdapterMac = new List<string>();
            IP_ADAPTER_INFO tempAdapter = _adapterInfo;
            IntPtr nextAdapterPtr = Marshal.AllocHGlobal(Marshal.SizeOf(_adapterInfo));
            Marshal.StructureToPtr(_adapterInfo, nextAdapterPtr, true);


            do
            {
                tempAdapter = (IP_ADAPTER_INFO)Marshal.PtrToStructure(nextAdapterPtr, typeof(IP_ADAPTER_INFO));
                AdapterMac.Add(BitConverter.ToString(tempAdapter.Address));

                nextAdapterPtr = tempAdapter.Next;
            } while (nextAdapterPtr != IntPtr.Zero);


            return AdapterMac.ToArray();
        }


        //--------------------------------------------------------------------------------
        //PINVOKE Functions
        [DllImport("iphlpapi.dll", CharSet = CharSet.Ansi)]
        public static extern int GetAdaptersInfo(IntPtr pAdapterInfo, ref Int64 pBufOutLen);
    }
}
