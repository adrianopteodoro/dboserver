using BaseLib.Packets;
using AuthServer.Configs;

namespace AuthServer.Packets
{
    class AU_LOGIN_RES : Packet
    {
        public AU_LOGIN_RES()
        {
            this.Opcode = (ushort)PacketOpcodes.AU_LOGIN_RES;
            this.ResultCode = 0;
            this.UserID = "";
            this.AuthKey = new byte[16];
            this.AccountID = 0;
            this.lastServerID = 255;
            this.lastChannelID = 255;
            this.AllowedFunctionForDeveloper = 0;
            this.CharServerCount = 0;
        }
        public ushort ResultCode
        {
            get { return this.GetShort(4); }
            set { this.SetShort(4, value); }
        }

        public string UserID
        {
            get { return this.GetString(6, 34); }
            set { this.SetString(6, value, 34); }
        }

        public byte[] AuthKey
        {
            get { return this.GetBytes(40, 16); }
            set { this.SetBytes(40, value, 16); }
        }

        public uint AccountID
        {
            get { return this.GetInt(56); }
            set { this.SetInt(56, value); }
        }

        public byte lastServerID
        {
            get { return this.GetByte(60); }
            set { this.SetByte(60, value); }
        }

        public byte lastChannelID
        {
            get { return this.GetByte(61); }
            set { this.SetByte(61, value); }
        }

        public uint AllowedFunctionForDeveloper
        {
            get { return this.GetInt(62); }
            set { this.SetInt(62, value); }
        }

        public byte CharServerCount
        {
            get { return this.GetByte(66); }
            set { this.SetByte(66, value); }
        }

        public void BuildCharServerList()
        {
            this.CharServerCount = (byte)AuthConfig.Instance.CharServerCount;
            for (int i = 0; i < this.CharServerCount; i++)
            {
                int srvid = i + 1;
                var dip = AuthConfig.Instance.GetCharServerAddress(srvid);

                this.SetAsciiString(67 + (i * 73), dip.Value);
                // Char Server Port
                this.SetShort(132 + (i * 73), (ushort)dip.Key);
                // Char Server Load
                this.SetInt(134 + (i * 73), 0);
                // Unknow
                this.SetShort(138 + (i * 73), 65535);
            }
        }
    }
}
