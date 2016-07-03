using BaseLib.Packets;
using BaseLib.Structs;

namespace CharServer.Packets
{
    class CU_CHARACTER_SELECT_RES : Packet
    {
        public CU_CHARACTER_SELECT_RES()
        {
            Opcode = (ushort)PacketOpcodes.CU_CHARACTER_SELECT_RES;
            ResultCode = (ushort)ResultCodes.CHARACTER_SUCCESS;
        }

        public ushort ResultCode
        {
            get { return GetShort(4); }
            set { SetShort(4, value); }
        }

        public uint CharID
        {
            get { return GetInt(6); }
            set { SetInt(6, value); }
        }

        public string AuthKey
        {
            get { return GetAsciiString(10, 16); }
            set { SetAsciiString(10, value); }
        }

        public string GameServerIP
        {
            get { return GetAsciiString(26, 65); }
            set { SetAsciiString(26, value); }
        }

        public ushort GameServerPort
        {
            get { return GetShort(91); }
            set { SetShort(91, value); }
        }
    }
}
