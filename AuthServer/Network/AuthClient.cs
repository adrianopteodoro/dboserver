using System.Text;
using BaseLib;
using BaseLib.Network;
using BaseLib.Packets;
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
			this.Client = client;
		}

        public void SendHandShakeRes()
        {
            byte[] rawData = { 0x22, 0x00, 0x10, 0x00, 0x49, 0xD1, 0xF1, 0x1C, 0x6D, 0x58, 0xF9, 0xC5, 0x30, 0x26, 0xA4, 0x7B,
			            0xB2, 0xD8, 0x2C, 0x86, 0x58, 0x60, 0x7B, 0xDD, 0xF0, 0x77, 0xCF, 0x25, 0x48, 0xB3, 0x65, 0x45,
			            0x38, 0x80, 0x14, 0x72 };
            this.Client.Send(rawData);
        }

        public void SendLoginDisconnectResponse()
        {
            Packet sPkt = new Packet();
            sPkt.Opcode = 1004;
            sPkt.BuildPacket();
            this.Client.Send(sPkt.Data);
        }

        public void SendLoginResponse(byte[] data)
        {
            Packet oPkt = new Packet();
            oPkt.Opcode = 1005;
            oPkt.BuildPacket();
            this.Client.Send(oPkt.Data);

            UA_LOGIN_REQ inPkt = new UA_LOGIN_REQ();
            inPkt.SetData(data);
            SysCons.LogInfo("UA_LOGIN_REQ {0} CodePage({1}) {2}.{3}", inPkt.UserID, inPkt.CodePage, inPkt.MajorVer, inPkt.MinorVer);
            this.Username = inPkt.UserID;
            this.Password = inPkt.UserPW;
            // ignoring sql connection
            this.AccountID = 1;//(uint)AuthDB.GetAccountID(this.Username);

            AU_LOGIN_RES sPkt = new AU_LOGIN_RES();
            sPkt.UserID = inPkt.UserID;
            sPkt.AccountID = this.AccountID;
            sPkt.AllowedFunctionForDeveloper = 65535;
            sPkt.AuthKey = Encoding.ASCII.GetBytes("SE@WASDE#$RFWD@D");
            // ignoring sql connection
            sPkt.ResultCode = 100;// (ushort)AuthDB.CheckAccount(this.Username, this.Password);
            sPkt.lastServerID = 255;
            sPkt.lastChannelID = 255;
            sPkt.BuildCharServerList();
            sPkt.BuildPacket();
            this.Client.Send(sPkt.Data);
        }
	}
}
