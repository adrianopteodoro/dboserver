using BaseLib.Packets;

namespace GameServer.Packets
{
    class UG_GAME_ENTER_REQ : Packet
    {
        public uint AccountID
        {
            get { return GetInt(4); }
        }

        public uint CharID
        {
            get { return GetInt(8); }
        }

        public string AuthKey
        {
            get { return GetAsciiString(12, 16); }
        }

        public bool IsTutorialMode
        {
            get { return GetBool(28); }
        }
    }
}
