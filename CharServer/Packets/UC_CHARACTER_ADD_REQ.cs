using BaseLib.Packets;

namespace CharServer.Packets
{
    class UC_CHARACTER_ADD_REQ : Packet
    {
        public string Name
        {
            get { return this.GetString(4, 34); }
        }

        public byte Race
        {
            get { return this.GetByte(38); }
        }

        public byte Class
        {
            get { return this.GetByte(39); }
        }

        public byte Gender
        {
            get { return this.GetByte(40); }
        }

        public byte Face
        {
            get { return this.GetByte(41); }
        }

        public byte Hair
        {
            get { return this.GetByte(42); }
        }

        public byte HairColor
        {
            get { return this.GetByte(43); }
        }

        public byte SkinColor
        {
            get { return this.GetByte(44); }
        }

        public byte Blood
        {
            get { return this.GetByte(45); }
        }
    }
}
