using System.Collections.Generic;
using BaseLib;
using BaseLib.Network;
using BaseLib.Entities;
using CommunityServer.Packets;

namespace CommunityServer.Network
{
    /// <summary>
    /// Description of AuthClient.
    /// </summary>
    public class CommClient : IUser
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
        
		public CommClient(IClient client)
		{
			Client = client;
		}

        public void SendEnterChatResponse(byte[] data)
        {
            var iPkt = new UT_ENTER_CHAT();
            iPkt.SetData(data);
            SysCons.LogInfo("UT_ENTER_CHAT AuthKey({0}) AccountID({1}) OnChannelBitFlag({2})", iPkt.AuthKey, iPkt.AccountID, iPkt.OnChannelBitFlag);

            using (var oPkt = new TU_SYSTEM_DISPLAY_TEXT())
            {
                oPkt.BuildPacket();
                Client.Send(oPkt.Data);
            }

            using (var oPkt = new TU_ENTER_CHAT_RES())
            {
                oPkt.BuildPacket();
                Client.Send(oPkt.Data);
            }
        }

        public void SendHandShakeRes()
        {
            byte[] rawData = { 0x22, 0x00, 0x10, 0x00, 0x49, 0xD1, 0xF1, 0x1C, 0x6D, 0x58, 0xF9, 0xC5, 0x30, 0x26, 0xA4, 0x7B,
			            0xB2, 0xD8, 0x2C, 0x86, 0x58, 0x60, 0x7B, 0xDD, 0xF0, 0x77, 0xCF, 0x25, 0x48, 0xB3, 0x65, 0x45,
			            0x38, 0x80, 0x14, 0x72 };
            Client.Send(rawData);
        }
	}
}
