using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseLib;
using BaseLib.Packets;
using BaseLib.Structs;

namespace CharServer.Packets
{
    class CU_SERVER_FARM_INFO : Packet
    {
        public CU_SERVER_FARM_INFO()
        {
            this.Opcode = (ushort)PacketOpcodes.CU_SERVER_FARM_INFO;
            this.ServerID = 0;
            this.ServerName = "";
            this.ServerStatus = 0;
            this.MaxLoad = 0;
            this.Load = 0;
        }

        public byte ServerID
        {
            get { return this.GetByte(4); }
            set { this.SetByte(4, value); }
        }

        public string ServerName
        {
            get { return this.GetString(5, 66); }
            set { this.SetString(5, value, 66); }
        }

        public byte ServerStatus
        {
            get { return this.GetByte(71); }
            set { this.SetByte(71, value); }
        }

        public uint MaxLoad
        {
            get { return this.GetInt(72); }
            set { this.SetInt(72, value); }
        }

        public uint Load
        {
            get { return this.GetInt(76); }
            set { this.SetInt(76, value); }
        }
    }
}
