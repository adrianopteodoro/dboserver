using BaseLib.Packets;


namespace CharServer.Packets
{
    class UC_CONNECT_WAIT_CANCEL_REQ : Packet
    {
        public byte ChannelID
        {
            get { return GetByte(4); }
        }
    }
}
