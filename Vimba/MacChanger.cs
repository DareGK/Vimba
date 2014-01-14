using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vimba
{
    class MacChanger
    {
        private string strMacAddress;
        private int adapterIndex;

        public MacChanger(string macAddress, int adapater)
        {
            strMacAddress = macAddress;
            adapterIndex = adapater;
        }

        //-----------------------------------------------------------------------------------------

        public bool ChangeMacAddress()
        {

            return true;
        }
    }
}
