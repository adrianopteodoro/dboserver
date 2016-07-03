using BaseLib.Packets;
using AuthServer.Configs;
using BaseLib.Structs;

namespace AuthServer.Packets
{
    class AU_LOGIN_RES : Packet
    {
        public AU_LOGIN_RES()
        {
            Opcode = (ushort)PacketOpcodes.AU_LOGIN_RES;
            ResultCode = (ushort)ResultCodes.AUTH_SUCCESS;
            UserID = "";
            AuthKey = "0123456789ABCDEF";
            AccountID = 0;
            lastServerID = 255;
            lastChannelID = 255;
            AllowedFunctionForDeveloper = 0;
            CharServerCount = 0;
        }
        public ushort ResultCode
        {
            get { return GetShort(4); }
            set { SetShort(4, value); }
        }

        public string UserID
        {
            get { return GetString(6, 34); }
            set { SetString(6, value, 34); }
        }

        public string AuthKey
        {
            get { return GetAsciiString(40, 16); }
            set { SetAsciiString(40, value); }
        }

        public uint AccountID
        {
            get { return GetInt(56); }
            set { SetInt(56, value); }
        }

        public byte lastServerID
        {
            get { return GetByte(60); }
            set { SetByte(60, value); }
        }

        public byte lastChannelID
        {
            get { return GetByte(61); }
            set { SetByte(61, value); }
        }

        public uint AllowedFunctionForDeveloper
        {
            get { return GetInt(62); }
            set { SetInt(62, value); }
        }

        public byte CharServerCount
        {
            get { return GetByte(66); }
            set { SetByte(66, value); }
        }

        public void BuildCharServerList()
        {
            CharServerCount = (byte)AuthConfig.Instance.CharServerCount;
            for (int i = 0; i < CharServerCount; i++)
            {
                int srvid = i + 1;
                var dip = AuthConfig.Instance.GetCharServerAddress(srvid);

                SetAsciiString(67 + (i * 73), dip.Value);
                // Char Server Port
                SetShort(132 + (i * 73), (ushort)dip.Key);
                // Char Server Load
                SetInt(134 + (i * 73), 0);
                // Unknow
                SetShort(138 + (i * 73), 65535);
            }
        }
    }
}
