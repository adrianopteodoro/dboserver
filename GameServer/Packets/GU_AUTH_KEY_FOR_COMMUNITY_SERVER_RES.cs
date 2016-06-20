using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseLib;
using BaseLib.Packets;
using BaseLib.Structs;

namespace GameServer.Packets
{
    class GU_AUTH_KEY_FOR_COMMUNITY_SERVER_RES : Packet
    {
        public GU_AUTH_KEY_FOR_COMMUNITY_SERVER_RES()
        {
            this.Opcode = (ushort)PacketOpcodes.GU_AUTH_KEY_FOR_COMMUNITY_SERVER_RES;
            //this.ResultCode = 500;
        }
        // dlugosc 2
        public ushort ResultCode
        {
            get { return this.GetShort(4); }
            set { this.SetShort(4, value); }
        }
        public byte[] AuthKey
        {
            get { return this.GetBytes(6, 16); }
            set { this.SetBytes(6, value, 16); }
        }
    }
}
