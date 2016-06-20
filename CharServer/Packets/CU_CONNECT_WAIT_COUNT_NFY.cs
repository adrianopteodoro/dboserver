using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseLib;
using BaseLib.Packets;
using BaseLib.Structs;

namespace CharServer.Packets
{
    class CU_CONNECT_WAIT_COUNT_NFY : Packet
    {
        public CU_CONNECT_WAIT_COUNT_NFY()
        {
            this.Opcode = (ushort)PacketOpcodes.CU_CONNECT_WAIT_COUNT_NFY;
            this.CountWaiting = 0;

        }

        public uint CountWaiting
        {
            get { return this.GetShort(4); }
            set { this.SetInt(4, value); }
        }
    }
}
