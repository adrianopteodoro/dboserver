using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace BaseLib.Structs
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct sSERVER_INFO
    {
        [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = 65, ArraySubType = UnmanagedType.U1)]
        public String szCharacterServerIP;
	    public ushort wCharacterServerPortForClient;
	    public int dwLoad;
    }
}
