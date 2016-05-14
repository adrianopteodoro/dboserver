using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseLib;
using BaseLib.Packets;
using BaseLib.Structs;

namespace CharServer.Packets
{
    class UC_CHARACTER_LOAD_REQ : Packet
    {
        public uint AccountID
        {
            get { return this.GetInt(4); }
        }

        public byte ServerID
        {
            get { return this.GetByte(8); }
        }
    }
}
