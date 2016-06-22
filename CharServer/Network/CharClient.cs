using System;
using System.Collections.Generic;
using BaseLib;
using BaseLib.Packets;
using BaseLib.Network;
using BaseLib.Entities;
using CharServer.Packets;
using CharServer.Configs;
using BaseLib.Structs;
using System.Text;

namespace CharServer.Network
{
    public class CharClient : IUser
	{
		/// <summary>
        /// TCP connection.
        /// </summary>
        private IClient _Client = null;
        public IClient Client {
        	get {return _Client;}
        	set {_Client = value;}
        }

        public uint AccountID;
        public uint UniqueID;
        public string Username;
        public string Password;
        public List<Character> Chars;
        
		public CharClient(IClient client)
		{
			this.Client = client;
		}

        public void SendHandShakeRes()
        {
            byte[] rawData = { 0x22, 0x00, 0x10, 0x00, 0x49, 0xD1, 0xF1, 0x1C, 0x6D, 0x58, 0xF9, 0xC5, 0x30, 0x26, 0xA4, 0x7B,
			            0xB2, 0xD8, 0x2C, 0x86, 0x58, 0x60, 0x7B, 0xDD, 0xF0, 0x77, 0xCF, 0x25, 0x48, 0xB3, 0x65, 0x45,
			            0x38, 0x80, 0x14, 0x72 };
            this.Client.Send(rawData);
        }

        public void SendLoginResponse(byte[] data)
        {
            UC_LOGIN_REQ iPkt = new UC_LOGIN_REQ();
            iPkt.SetData(data);
            SysCons.LogInfo("UC_LOGIN_REQ AccountID({0}) LastServerID({1})", iPkt.AccountID, iPkt.ServerID);

            CU_LOGIN_RES oPkt = new CU_LOGIN_RES();
            oPkt.LastServerID = iPkt.ServerID;
            oPkt.BuildPacket();
            this.Client.Send(oPkt.Data);
        }

        public void SendServerList(Boolean isOnlyOne)
        {
            SysCons.LogInfo("CU_SERVER_FARM_INFO Sending {0} server(s) information", CharConfig.Instance.GameServerCount);
            for (int i = 0; i < CharConfig.Instance.GameServerCount; ++i)
            {
                var oPkt = new CU_SERVER_FARM_INFO();
                int srvid = i + 1;

                oPkt.ServerID = (byte)srvid;
                oPkt.MaxLoad = 100;
                oPkt.Load = 0;
                oPkt.ServerStatus = 0;
                oPkt.ServerName = CharConfig.Instance.GetGameServerName(srvid);
                oPkt.BuildPacket();
                this.Client.Send(oPkt.Data);
            }

            if (isOnlyOne)
            {
                var oPkt = new CU_CHARACTER_SERVERLIST_ONE_RES();
                oPkt.BuildPacket();
                this.Client.Send(oPkt.Data);
            }
            else
            {
                var oPkt = new CU_CHARACTER_SERVERLIST_RES();
                oPkt.BuildPacket();
                this.Client.Send(oPkt.Data);
            }
        }

        public void SendCharacterLoad(byte[] data)
        {
            var iPkt = new UC_CHARACTER_LOAD_REQ();
            iPkt.SetData(data);
            SysCons.LogInfo("UC_CHARACTER_LOAD_REQ AccountID({0}) LastServerID({1})", iPkt.AccountID, iPkt.ServerID);

            var oPkt = new CU_SERVER_CHANNEL_INFO();
            oPkt.BuildChannelList(iPkt.ServerID);
            oPkt.BuildPacket();
            this.Client.Send(oPkt.Data);

            var osPkt = new CU_CHARACTER_LOAD_RES();
            osPkt.ServerID = iPkt.ServerID;
            osPkt.BuildPacket();
            this.Client.Send(osPkt.Data);
        }

        public void SendCharacterCreate(byte[] data)
        {
            var iPkt = new UC_CHARACTER_ADD_REQ();
            iPkt.SetData(data);
            SysCons.LogInfo(
                "UC_CHARACTER_ADD_REQ Name({0}) Race({1}) Class({2}) Gender({3}) Face({4}) Hair({5}) HairColor({6}) SkinColor({7}) Blood({8})",
                iPkt.Name,
                ((CharRaces)iPkt.Race).ToString(),
                ((CharClasses)iPkt.Class).ToString(),
                ((CharGenders)iPkt.Gender).ToString(),
                iPkt.Face,
                iPkt.Hair,
                iPkt.HairColor,
                iPkt.SkinColor,
                iPkt.Blood
            );
        }

        internal void SendCharacterAdd(byte[] data)
        {
            //TO DO UC_CHARACTER_ADD_REQ
            SysCons.LogInfo("UC_CHARACTER_ADD_REQ");
            UC_CHARACTER_ADD_REQ iPkt = new UC_CHARACTER_ADD_REQ();
            iPkt.SetData(data);
            SysCons.LogInfo(
                "UC_CHARACTER_ADD_REQ Name({0}) Race({1}) Class({2}) Gender({3}) Face({4}) Hair({5}) HairColor({6}) SkinColor({7}) Blood({8})",
                iPkt.Name,
                ((CharRaces)iPkt.Race).ToString(),
                ((CharClasses)iPkt.Class).ToString(),
                ((CharGenders)iPkt.Gender).ToString(),
                iPkt.Face,
                iPkt.Hair,
                iPkt.HairColor,
                iPkt.SkinColor,
                iPkt.Blood
            );
            //dlaczego wczesniej bylo var ?
            CU_CHARACTER_ADD_RES oPkt = new CU_CHARACTER_ADD_RES();
            oPkt.ResultCode = 200;
            oPkt.charID = 1;
            oPkt.Name = iPkt.Name;
            oPkt.Race = iPkt.Race;
            oPkt.Face = iPkt.Face;
            oPkt.Hair = iPkt.Hair;
            oPkt.Gender = iPkt.Gender;
            oPkt.HairColor = iPkt.HairColor;
            oPkt.SkinColor = iPkt.SkinColor;
            oPkt.worldTblidx = 1;
            oPkt.worldId = 1;
            oPkt.BuildPacket();
            this.Client.Send(oPkt.Data);
        }

        internal void SendCharacterSelect(byte[] data)
        {
            UC_CHARACTER_SELECT_REQ iPkt = new UC_CHARACTER_SELECT_REQ();
            iPkt.SetData(data);
            SysCons.LogInfo("UC_CHARACTER_SELECT_REQ charId({0}) ServerChannelIndex({1})", iPkt.charId, iPkt.byServerChannelIndex);

            CU_CHARACTER_SELECT_RES sPkt = new CU_CHARACTER_SELECT_RES();
            sPkt.szGameServerIP = Encoding.ASCII.GetBytes("127.0.0.1");
            sPkt.wGameServerPortForClient = 50400;
            SysCons.LogInfo("CU_CHARACTER_SELECT_RES IPAddress({0}) Port({1})", sPkt.szGameServerIP, sPkt.wGameServerPortForClient);
            sPkt.AuthKey = Encoding.ASCII.GetBytes("SE@WASDE#$RFWD@D");
            sPkt.ResultCode = 200;
            // connecting to GameServer
            sPkt.BuildPacket();
            this.Client.Send(sPkt.Data);
        }

        internal void SendWaitCheck(byte[] data)
        {
            UC_CONNECT_WAIT_CHECK_REQ iPkt = new UC_CONNECT_WAIT_CHECK_REQ();
            iPkt.SetData(data);
            SysCons.LogInfo("UC_CONNECT_WAIT_CHECK_REQ ServerChannelIndex({0})", iPkt.byServerChannelIndex);
            CU_CONNECT_WAIT_CHECK_RES oPkt = new CU_CONNECT_WAIT_CHECK_RES();
            oPkt.BuildPacket();
            this.Client.Send(oPkt.Data);
            CU_CONNECT_WAIT_COUNT_NFY o2Pkt = new CU_CONNECT_WAIT_COUNT_NFY();
            o2Pkt.BuildPacket();
            this.Client.Send(o2Pkt.Data);
        }
        internal void SendCharacterExit(byte[] data)
        {
            SysCons.LogInfo("UC_CHARACTER_EXIT_REQ disconnected  client");
            Packet sPkt = new Packet();
            sPkt.Opcode = (ushort)PacketOpcodes.CU_CHARACTER_EXIT_RES;
            sPkt.BuildPacket();
            this.Client.Send(sPkt.Data);
        }
    }
}
