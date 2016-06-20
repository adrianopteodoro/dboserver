using System.Collections.Generic;
using BaseLib;
using BaseLib.Network;
using BaseLib.Packets;
using GameServer.Packets;
using BaseLib.Entities;
using System.Text;

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
        public string Username;
        public string Password;
        public List<Character> Chars;
        
		public GameClient(IClient client)
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

        internal void AuthKeyForCommunityServer(byte[] data)
        {
            //GU_AUTH_KEY_FOR_COMMUNITY_SERVER_RES
            SysCons.LogInfo("GU_AUTH_KEY_FOR_COMMUNITY_SERVER_RES");
            GU_AUTH_KEY_FOR_COMMUNITY_SERVER_RES oPkt = new GU_AUTH_KEY_FOR_COMMUNITY_SERVER_RES();
            oPkt.ResultCode = 500;
            oPkt.AuthKey = Encoding.ASCII.GetBytes("SE@WASDE#$RFWD@D");
            oPkt.BuildPacket();
            this.Client.Send(oPkt.Data);
        }

        internal void EnterWolrd(byte[] data)
        {
            // pakiet UG_ENTER_WORLD nie ma nic
            SysCons.LogInfo("UG_ENTER_WORLD");
            //GU_NETMARBLEMEMBERIP_NFY
            // jakis pakiet na surowo
            //GU_AVATAR_WORLD_INFO
            //GU_ENTER_WORLD_COMPLETE
            SysCons.LogInfo("GU_ENTER_WORLD_COMPLETE");
            Packet pkt = new Packet();
            pkt.Opcode = (ushort)PacketOpcodes.GU_ENTER_WORLD_COMPLETE;
            pkt.BuildPacket();
            this.Client.Send(pkt.Data);
        }

        internal void SendGameEnter(byte[] data)
        {
            // to do UG_GAME_ENTER_REQ
            SysCons.LogInfo("UG_GAME_ENTER_REQ");
            GU_GAME_ENTER_RES sPkt = new GU_GAME_ENTER_RES();
            sPkt.ResultCode = 500;
            sPkt.achCommunityServerIP = Encoding.ASCII.GetBytes("192.168.0.3");
            sPkt.wCommunityServerPort = 50200;
            SysCons.LogInfo("GU_GAME_ENTER_RES IPAddress({0}) Port({1})", sPkt.achCommunityServerIP, sPkt.wCommunityServerPort);
            sPkt.BuildPacket();
            this.Client.Send(sPkt.Data);
            // O WCHUJ PACKETOW JEDZIEM zerami
            // GU_AVATAR_CHAR_INFO

            // GU_AVATAR_ITEM_INFO

            //GU_AVATAR_SKILL_INFO

            //GU_AVATAR_HTB_INFO

            //GU_QUICK_SLOT_INFO

            //GU_WAR_FOG_INFO

            //GU_AVATAR_BUFF_INFO

            //GU_AVATAR_INFO_END
            SysCons.LogInfo("GU_AVATAR_INFO_END");
            Packet pkt = new Packet();
            pkt.Opcode = (ushort)PacketOpcodes.GU_AVATAR_INFO_END;
            pkt.BuildPacket();
            this.Client.Send(pkt.Data);
        }
    }
}
