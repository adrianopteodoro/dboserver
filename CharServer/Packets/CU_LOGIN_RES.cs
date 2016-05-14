using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseLib;
using BaseLib.Packets;
using BaseLib.Structs;

namespace CharServer.Packets
{
    class CU_LOGIN_RES : Packet
    {
        public CU_LOGIN_RES()
        {
            this.Opcode = (ushort)PacketOpcodes.CU_LOGIN_RES;
            this.ResultCode = 200;
            this.LastServerID = 255;
            this.RaceAllowedFlag = 7;
            this.LastChannelID = 255;
        }

        public ushort ResultCode
        {
            get { return this.GetShort(4); }
            set { this.SetShort(4, value); }
        }

        public byte LastServerID
        {
            get { return this.GetByte(6); }
            set { this.SetByte(6, value); }
        }

        public uint RaceAllowedFlag
        {
            get { return this.GetInt(7); }
            set { this.SetInt(7, value); }
        }

        public byte LastChannelID
        {
            get { return this.GetByte(11); }
            set { this.SetByte(11, value); }
        }
    }
}
