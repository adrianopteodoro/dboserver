using BaseLib;
using BaseLib.Network;
using AuthServer.Packets;
using AuthServer.Database;

namespace AuthServer.Network
{
    /// <summary>
    /// Description of AuthClient.
    /// </summary>
    public class AuthClient : IUser
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
        
		public AuthClient(IClient client)
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

        public void SendLoginDisconnectResponse()
        {
            using (var oPkt = new AU_LOGIN_DISCONNECT_RES())
            {
                oPkt.BuildPacket();
                Client.Send(oPkt.Data);
            }
        }

        public void SendLoginResponse(byte[] data)
        {
            var iPkt = new UA_LOGIN_REQ();
            iPkt.SetData(data);
            SysCons.LogInfo("UA_LOGIN_REQ UserID({0}) CodePage({1}) Version({2}.{3})", iPkt.UserID, iPkt.CodePage, iPkt.MajorVer, iPkt.MinorVer);
            Username = iPkt.UserID;
            Password = iPkt.UserPW;
            AccountID = (uint)AuthDB.GetAccountID(Username);

            using (var oPkt = new AU_COMMERCIAL_SETTING_NFY())
            {
                oPkt.BuildPacket();
                Client.Send(oPkt.Data);
            }

            using (var oPkt = new AU_LOGIN_RES())
            {
                oPkt.UserID = iPkt.UserID;
                oPkt.AccountID = AccountID;
                oPkt.AllowedFunctionForDeveloper = 65535;
                oPkt.AuthKey = "SE@WASDE#$RFWD@D";
                oPkt.ResultCode = (ushort)AuthDB.CheckAccount(this.Username, this.Password);
                oPkt.lastServerID = 255;
                oPkt.lastChannelID = 255;
                oPkt.BuildCharServerList();
                oPkt.BuildPacket();
                Client.Send(oPkt.Data);
            }
        }
	}
}
