using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseLib;
using BaseLib.Packets;
using BaseLib.Structs;
namespace CharServer.Packets
{
    class CU_CHARACTER_SELECT_RES : Packet
    {
        public CU_CHARACTER_SELECT_RES()
        {
            this.Opcode = (ushort)PacketOpcodes.CU_CHARACTER_SELECT_RES;
            this.ResultCode = 0;
            this.charId = 1;
            this.AuthKey = new byte[16];
            //TO DO POPRAWIC
            //this.szGameServerIP = "192.168.0.3";
            //this.szGameServerIP= Encoding.ASCII.GetBytes("192.168.0.3");
            //this.wGameServerPortForClient = 50400;

        }
        public ushort ResultCode
        {
            get { return this.GetShort(4); }
            set { this.SetShort(4, value); }
        }
        public uint charId
        {
            get { return this.GetInt(6); }
            set { this.SetInt(6,value);  }
        }
        public byte[] AuthKey
        {
            get { return this.GetBytes(10, 16); }
            set { this.SetBytes(10, value, 16); }
        }
        public byte[] szGameServerIP
        {
            get { return this.GetBytes(26, 65);}
            set { this.SetBytes(26, value, 65); }
        }
        public ushort wGameServerPortForClient
        {
            get { return this.GetShort(91); }
            set { this.SetShort(91, value); }
        }
    }
}
/*
	WORD			wResultCode;
	CHARACTERID		charId;
	BYTE			abyAuthKey[NTL_MAX_SIZE_AUTH_KEY];
	char			szGameServerIP[NTL_MAX_LENGTH_OF_IP + 1];
	WORD			wGameServerPortForClient;
*/