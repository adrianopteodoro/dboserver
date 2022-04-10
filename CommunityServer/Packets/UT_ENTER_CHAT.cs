using BaseLib.Packets;

namespace CommunityServer.Packets
{
    class UT_ENTER_CHAT : Packet
    {
        public uint AccountID
        {
            get { return GetInt(4); }
        }

        public string AuthKey
        {
            get { return GetAsciiString(8, 16); }
        }

        public ulong OnChannelBitFlag
        {
            get { return GetLong(24); }
        }
    }
}
