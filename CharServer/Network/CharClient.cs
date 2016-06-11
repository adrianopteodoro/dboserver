using System;
using System.Collections.Generic;
using BaseLib;
using BaseLib.Network;
using BaseLib.Entities;
using CharServer.Packets;
using CharServer.Configs;

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
    }
}
