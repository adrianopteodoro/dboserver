using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseLib;
using BaseLib.Packets;
using BaseLib.Structs;

namespace CharServer.Packets
{
    class UC_CONNECT_WAIT_CHECK_REQ : Packet
    {
        public byte byServerChannelIndex
        {
            get { return this.GetByte(4); }
        }
    }
}
