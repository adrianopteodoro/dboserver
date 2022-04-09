using System;
using System.Collections.Generic;
using BaseLib;
using BaseLib.Network;
using BaseLib.Entities;
using CharServer.Packets;
using CharServer.Configs;
using CharServer.Database;
using BaseLib.Structs;

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
        public uint CharID;
        public byte ServerID;
        public byte ChannelID;
        public string AuthKey;
        public string Username;
        public string Password;
        public List<Character> Chars;
        
		public CharClient(IClient client)
		{
			Client = client;
		}

        public void SendHandShakeRes()
        {
            byte[] rawData = { 0x22, 0x00, 0x10, 0x00, 0x49, 0xD1, 0xF1, 0x1C, 0x6D, 0x58, 0xF9, 0xC5, 0x30, 0x26, 0xA4, 0x7B,
			            0xB2, 0xD8, 0x2C, 0x86, 0x58, 0x60, 0x7B, 0xDD, 0xF0, 0x77, 0xCF, 0x25, 0x48, 0xB3, 0x65, 0x45,
			            0x38, 0x80, 0x14, 0x72 };
            Client.Send(rawData);
        }

        public void SendLoginResponse(byte[] data)
        {
            var iPkt = new UC_LOGIN_REQ();
            iPkt.SetData(data);
            SysCons.LogInfo("UC_LOGIN_REQ AuthKey({0}) AccountID({1}) LastServerID({2})", iPkt.AuthKey, iPkt.AccountID, iPkt.ServerID);

            AccountID = iPkt.AccountID;
            ServerID = iPkt.ServerID;
            AuthKey = iPkt.AuthKey;

            CharDB.SetLastServerID(AccountID, ServerID);

            using (var oPkt = new CU_LOGIN_RES())
            {
                oPkt.LastServerID = ServerID;
                oPkt.BuildPacket();
                Client.Send(oPkt.Data);
            }
        }

        public void SendServerList(bool isOnlyOne)
        {
            SysCons.LogInfo("CU_SERVER_FARM_INFO Sending {0} server(s) information", CharConfig.Instance.GameServerCount);
            for (int i = 0; i < CharConfig.Instance.GameServerCount; ++i)
            {
                using (var oPkt = new CU_SERVER_FARM_INFO())
                {
                    int srvid = i + 1;
                    oPkt.ServerID = (byte)srvid;
                    oPkt.MaxLoad = 100;
                    oPkt.Load = 0;
                    oPkt.ServerStatus = 0;
                    oPkt.ServerName = CharConfig.Instance.GetGameServerName(srvid);
                    oPkt.BuildPacket();
                    Client.Send(oPkt.Data);
                }
            }

            if (isOnlyOne)
            {
                using (var oPkt = new CU_CHARACTER_SERVERLIST_ONE_RES())
                {
                    oPkt.BuildPacket();
                    Client.Send(oPkt.Data);
                }
            }
            else
            {
                using (var oPkt = new CU_CHARACTER_SERVERLIST_RES())
                {
                    oPkt.BuildPacket();
                    Client.Send(oPkt.Data);
                }
            }
        }

        public void SendCharacterLoad(byte[] data)
        {
            var iPkt = new UC_CHARACTER_LOAD_REQ();
            iPkt.SetData(data);
            SysCons.LogInfo("UC_CHARACTER_LOAD_REQ AccountID({0}) LastServerID({1})", iPkt.AccountID, iPkt.ServerID);

            AccountID = iPkt.AccountID;
            ServerID = iPkt.ServerID;

            CharDB.SetLastServerID(AccountID, ServerID);

            using (var oPkt = new CU_SERVER_CHANNEL_INFO())
            {
                oPkt.BuildChannelList(iPkt.ServerID);
                oPkt.BuildPacket();
                Client.Send(oPkt.Data);
            }

            using (var oPkt = new CU_CHARACTER_INFO())
            {
                oPkt.BuildCharList(AccountID, ServerID);
                oPkt.BuildPacket();
                SysCons.SavePacket(oPkt);
                Client.Send(oPkt.Data);
            }

            using (var oPkt = new CU_CHARACTER_LOAD_RES())
            {
                oPkt.ServerID = iPkt.ServerID;
                oPkt.BuildPacket();
                Client.Send(oPkt.Data);
            }
        }

        public void SendCharacterCreate(byte[] data)
        {
            var iPkt = new UC_CHARACTER_ADD_REQ();
            iPkt.SetData(data);
            SysCons.LogInfo(
                "UC_CHARACTER_ADD_REQ Name({0}) Race({1}) Class({2}) Gender({3})",
                iPkt.Name,
                ((CharRaces)iPkt.Race).ToString(),
                ((CharClasses)iPkt.Class).ToString(),
                ((CharGenders)iPkt.Gender).ToString()
            );

            using (var oPkt = new CU_CHARACTER_ADD_RES())
            {
                oPkt.ResultCode = (ushort)ResultCodes.CHARACTER_SUCCESS;
                oPkt.CharID = (uint)(new Random().Next());
                oPkt.Race = iPkt.Race;
                oPkt.Class = iPkt.Class;
                oPkt.Gender = iPkt.Gender;
                oPkt.Name = iPkt.Name;
                oPkt.Face = iPkt.Face;
                oPkt.Hair = iPkt.Hair;
                oPkt.HairColor = iPkt.HairColor;
                oPkt.SkinColor = iPkt.SkinColor;
                oPkt.Level = 1;
                oPkt.WorldId = 1;
                oPkt.WorldTblIndex = 1;
                oPkt.PositionX = 2902.0f;
                oPkt.PositionY = 0.0f;
                oPkt.PositionZ = -2370.0f;
                oPkt.Zenny = 0;
                oPkt.ZennyBank = 0;
                oPkt.BuildCharEquipaments(oPkt.CharID);
                oPkt.BuildPacket();
                Client.Send(oPkt.Data);
            }
        }

        public void SendDisconnect(byte[] data)
        {
            var iPkt = new UC_CHARACTER_EXIT_REQ();
            iPkt.SetData(data);

            SysCons.LogInfo("UC_CHARACTER_EXIT_REQ IsGameMove({0})", iPkt.IsGameMove);

            using (var oPkt = new CU_CHARACTER_EXIT_RES())
            {
                oPkt.ResultCode = (ushort)ResultCodes.CHARACTER_SUCCESS;
                oPkt.BuildPacket();
                Client.Send(oPkt.Data);
            }
        }

        public void SendConnectWaitCheckResult(byte[] data)
        {
            var iPkt = new UC_CONNECT_WAIT_CHECK_REQ();
            iPkt.SetData(data);
            SysCons.LogInfo("UC_CONNECT_WAIT_CHECK_REQ ChannelID({0})", iPkt.ChannelID);

            ChannelID = iPkt.ChannelID;

            CharDB.SetLastChannelID(AccountID, ChannelID);

            using (var oPkt = new CU_CONNECT_WAIT_CHECK_RES())
            {
                oPkt.ResultCode = (ushort)ResultCodes.CHARACTER_SUCCESS;
                oPkt.BuildPacket();
                Client.Send(oPkt.Data);
            }
        }

        public void SendConnectWaitCancelResult(byte[] data)
        {
            var iPkt = new UC_CONNECT_WAIT_CANCEL_REQ();
            iPkt.SetData(data);
            SysCons.LogInfo("UC_CONNECT_WAIT_CANCEL_REQ ChannelID({0})", iPkt.ChannelID);

            ChannelID = iPkt.ChannelID;

            CharDB.SetLastChannelID(AccountID, ChannelID);

            using (var oPkt = new CU_CONNECT_WAIT_CANCEL_RES())
            {
                oPkt.ResultCode = (ushort)ResultCodes.CHARACTER_SUCCESS;
                oPkt.BuildPacket();
                Client.Send(oPkt.Data);
            }
        }

        public void SendCharacterSelectResult(byte[] data)
        {
            var iPkt = new UC_CHARACTER_SELECT_REQ();
            iPkt.SetData(data);
            SysCons.LogInfo("UC_CHARACTER_SELECT_REQ ServerID({0}) CharID({1})", iPkt.ServerID, iPkt.CharID);
            ServerID = iPkt.ServerID;
            CharID = iPkt.CharID;

            using (var oPkt = new CU_CHARACTER_SELECT_RES())
            {
                oPkt.ResultCode = (ushort)ResultCodes.CHARACTER_SUCCESS;
                oPkt.CharID = CharID;
                oPkt.AuthKey = AuthKey;
                oPkt.GameServerIP = CharConfig.Instance.GetGameServerIP(ServerID, ChannelID);
                oPkt.GameServerPort = CharConfig.Instance.GetGameServerPort(ServerID, ChannelID);
                oPkt.BuildPacket();
                Client.Send(oPkt.Data);
            }
        }
    }
}
