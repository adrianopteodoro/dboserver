using BaseLib.Packets;
using BaseLib.Structs;

namespace CharServer.Packets
{
    class CU_CHARACTER_LOAD_RES : Packet
    {
        public CU_CHARACTER_LOAD_RES()
        {
            Opcode = (ushort)PacketOpcodes.CU_CHARACTER_LOAD_RES;
            ResultCode = (ushort)ResultCodes.CHARACTER_SUCCESS;
            ServerID = 255;
            OpenCharSlots = 8;
            VIPCharSlots = 0;
        }

        public ushort ResultCode
        {
            get { return GetShort(4); }
            set { SetShort(4, value); }
        }

        public byte ServerID
        {
            get { return GetByte(6); }
            set { SetByte(6, value); }
        }

        public byte OpenCharSlots
        {
            get { return GetByte(7); }
            set { SetByte(7, value); }
        }

        public byte VIPCharSlots
        {
            get { return GetByte(8); }
            set { SetByte(8, value); }
        }
    }
}
