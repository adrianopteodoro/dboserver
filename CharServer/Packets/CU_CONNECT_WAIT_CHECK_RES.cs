using BaseLib.Packets;
using BaseLib.Structs;

namespace CharServer.Packets
{
    class CU_CONNECT_WAIT_CHECK_RES : Packet
    {
        public CU_CONNECT_WAIT_CHECK_RES()
        {
            Opcode = (ushort)PacketOpcodes.CU_CONNECT_WAIT_CHECK_RES;
            ResultCode = (ushort)ResultCodes.CHARACTER_SUCCESS;
        }

        public ushort ResultCode
        {
            get { return this.GetShort(4); }
            set { this.SetShort(4, value); }
        }
    }
}
