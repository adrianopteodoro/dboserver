using BaseLib.Packets;


namespace CharServer.Packets
{
    class UC_CHARACTER_SELECT_REQ : Packet
    {
        public uint CharID
        {
            get { return GetInt(4); }
        }

        public byte ServerID
        {
            get { return GetByte(8); }
        }
    }
}
