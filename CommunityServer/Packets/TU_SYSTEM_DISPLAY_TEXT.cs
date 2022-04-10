using BaseLib.Packets;

namespace CommunityServer.Packets
{
    class TU_SYSTEM_DISPLAY_TEXT : Packet
    {
        public TU_SYSTEM_DISPLAY_TEXT()
        {
            Opcode = (ushort)PacketOpcodes.TU_SYSTEM_DISPLAY_TEXT;
            GmCharName = "Server";
            DisplayType = (byte)0;
            Message = "Welcome to DBO!";
            MessageLenght = (ushort)Message.Length;
        }

        public string GmCharName
        {
            get { return GetAsciiString(4, 16); }
            set { SetAsciiString(4, value); }
        }

        public byte DisplayType
        { 
            get { return GetByte(20); }
            set { SetByte(20, value); }
        }

        public ushort MessageLenght
        {
            get { return GetShort(21); }
            set { SetShort(21, value); }
        }

        public string Message
        {
            get { return GetString(23, 256); }
            set { SetString(23, value, 256); }
        }
    }
}
