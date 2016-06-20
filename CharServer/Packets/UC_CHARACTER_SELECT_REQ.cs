using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseLib;
using BaseLib.Packets;
using BaseLib.Structs;

namespace CharServer.Packets
{
    class UC_CHARACTER_SELECT_REQ : Packet
    {
        public uint charId
        {
            get { return this.GetInt(4); }
        }

        public byte byServerChannelIndex
        {
            get { return this.GetByte(8); }
        }

    }
}
