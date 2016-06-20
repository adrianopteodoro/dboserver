using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseLib;
using BaseLib.Packets;
using BaseLib.Structs;


namespace GameServer.Packets
{
    class GU_GAME_ENTER_RES :Packet
    {
        public GU_GAME_ENTER_RES()
        {
            this.Opcode = (ushort)PacketOpcodes.GU_GAME_ENTER_RES;
            //this.ResultCode = 500;
            //this.achCommunityServerIP = Encoding.ASCII.GetBytes("192.168.0.3");
            //this.wCommunityServerPort = 50200;
            this.timeDBOEnter = 0;
        }
        // długość 2
        public ushort ResultCode
        {
            get { return this.GetShort(4); }
            set { this.SetShort(4, value); }
        }
        public byte[] achCommunityServerIP
        {
            get { return this.GetBytes(6, 65); }
            set { this.SetBytes(6, value, 65); }
        }
        public ushort wCommunityServerPort
        {
            get { return this.GetShort(71); }
            set { this.SetShort(71, value); }
        }
        public ulong timeDBOEnter
        {
            get { return this.GetLong(73); }
            set { this.SetLong(72, value); }
        }
    }
}

/*
BEGIN_PROTOCOL(GU_GAME_ENTER_RES)
	WORD			wResultCode;
	char			achCommunityServerIP[NTL_MAX_LENGTH_OF_IP + 1];
	WORD			wCommunityServerPort;
	DBOTIME			timeDBOEnter;
END_PROTOCOL()
*/