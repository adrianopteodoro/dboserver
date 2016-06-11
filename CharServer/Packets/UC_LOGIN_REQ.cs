using BaseLib.Packets;

namespace CharServer.Packets
{
    class UC_LOGIN_REQ : Packet
    {
        public uint AccountID
        {
            get { return this.GetInt(4); }
        }

        public byte[] AuthKey
        {
            get { return this.GetBytes(8, 16); }
        }

        public byte ServerID
        {
            get { return this.GetByte(24); }
        }
    }
}
