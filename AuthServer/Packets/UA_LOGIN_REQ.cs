using BaseLib.Packets;

namespace AuthServer.Packets
{
    class UA_LOGIN_REQ : Packet
    {
        public string UserID
        {
            get { return GetString(4, 34); }
        }

        public string UserPW
        {
            get { return GetString(38, 34); }
        }

        public uint CodePage
        {
            get { return GetInt(72); }
        }

        public ushort MajorVer
        {
            get { return GetShort(76); }
        }

        public ushort MinorVer
        {
            get { return GetShort(78); }
        }

        public byte[] MacAddress
        {
            get { return GetBytes(80, 6); }
        }

        public byte Unknow
        {
            get { return GetByte(86); }
        }
    }
}
