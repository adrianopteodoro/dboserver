using BaseLib.Packets;

namespace CharServer.Packets
{
    class UC_LOGIN_REQ : Packet
    {
        public uint AccountID
        {
            get { return GetInt(4); }
        }

        public string AuthKey
        {
            get { return GetAsciiString(8, 16); }
        }

        public byte ServerID
        {
            get { return GetByte(24); }
        }
    }
}
