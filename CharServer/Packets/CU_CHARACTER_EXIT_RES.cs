using BaseLib.Packets;
using BaseLib.Structs;

namespace CharServer.Packets
{
    class CU_CHARACTER_EXIT_RES : Packet
    {
        public CU_CHARACTER_EXIT_RES()
        {
            Opcode = (ushort)PacketOpcodes.CU_CHARACTER_EXIT_RES;
            ResultCode = (ushort)ResultCodes.CHARACTER_SUCCESS;
        }

        public ushort ResultCode
        {
            get { return GetShort(4); }
            set { SetShort(4, value); }
        }
    }
}
