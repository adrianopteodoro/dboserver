using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseLib;
using BaseLib.Packets;
using BaseLib.Structs;

namespace CharServer.Packets
{
    class CU_CHARACTER_SERVERLIST_RES : Packet
    {
        public CU_CHARACTER_SERVERLIST_RES()
        {
            this.Opcode = (ushort)PacketOpcodes.CU_CHARACTER_SERVERLIST_RES;
            this.ResultCode = 200;
        }

        public ushort ResultCode
        {
            get { return this.GetShort(4); }
            set { this.SetShort(4, value); }
        }
    }
}
