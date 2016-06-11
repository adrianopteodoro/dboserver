using BaseLib.Packets;

namespace CharServer.Packets
{
    class UC_CHARACTER_LOAD_REQ : Packet
    {
        public uint AccountID
        {
            get { return this.GetInt(4); }
        }

        public byte ServerID
        {
            get { return this.GetByte(8); }
        }
    }
}
