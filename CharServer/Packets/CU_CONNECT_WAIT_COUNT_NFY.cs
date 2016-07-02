using BaseLib.Packets;
using BaseLib.Structs;

namespace CharServer.Packets
{
    class CU_CONNECT_WAIT_COUNT_NFY : Packet
    {
        public CU_CONNECT_WAIT_COUNT_NFY()
        {
            Opcode = (ushort)PacketOpcodes.CU_CONNECT_WAIT_COUNT_NFY;
            WaitCount = 0;
        }

        public uint WaitCount
        {
            get { return GetInt(4); }
            set { SetInt(4, value); }
        }
    }
}
