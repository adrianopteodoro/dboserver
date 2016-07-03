using System;
using System.Collections.Generic;
using BaseLib.Network;
using BaseLib.Entities;
using GameServer.Packets;
using BaseLib;
using BaseLib.Structs;
using GameServer.Configs;
using BaseLib.Helpers;

namespace GameServer.Network
{
    /// <summary>
    /// Description of AuthClient.
    /// </summary>
    public class GameClient : IUser
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
        
		public GameClient(IClient client)
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

        public void SendGameEnterResult(byte[] data)
        {
            var iPkt = new UG_GAME_ENTER_REQ();
            iPkt.SetData(data);
            SysCons.LogInfo("UG_GAME_ENTER_REQ AccountID({0}) CharID({1}) AuthKey({2}) IsTutorialMode({3})",
                iPkt.AccountID, iPkt.CharID, iPkt.AuthKey, iPkt.IsTutorialMode);
            AccountID = iPkt.AccountID;
            CharID = iPkt.CharID;
            AuthKey = iPkt.AuthKey;

            using (var oPkt = new GU_GAME_ENTER_RES())
            {
                oPkt.ResultCode = (ushort)ResultCodes.GAME_SUCCESS;
                oPkt.CommunityServerIP = GameConfig.Instance.CommunityServerIP;
                oPkt.CommunityServerPort = GameConfig.Instance.CommunityServerPort;
                oPkt.GameEnterTime = Utils.GetTimestamp(DateTime.Now);
                oPkt.BuildPacket();
                Client.Send(oPkt.Data);
                SysCons.SavePacket(oPkt);
            }
        }
	}
}
