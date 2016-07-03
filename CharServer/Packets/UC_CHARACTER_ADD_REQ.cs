using BaseLib.Packets;

namespace CharServer.Packets
{
    class UC_CHARACTER_ADD_REQ : Packet
    {
        public string Name
        {
            get { return GetString(4, 34); }
        }

        public byte Race
        {
            get { return GetByte(38); }
        }

        public byte Class
        {
            get { return GetByte(39); }
        }

        public byte Gender
        {
            get { return GetByte(40); }
        }

        public byte Face
        {
            get { return GetByte(41); }
        }

        public byte Hair
        {
            get { return GetByte(42); }
        }

        public byte HairColor
        {
            get { return GetByte(43); }
        }

        public byte SkinColor
        {
            get { return GetByte(44); }
        }

        public byte Blood
        {
            get { return GetByte(45); }
        }
    }
}
