using BaseLib.Packets;
using BaseLib.Structs;

namespace GameServer.Packets
{
    class GU_ENTER_WORLD_RES : Packet
    {
        public GU_ENTER_WORLD_RES()
        {
            Opcode = (ushort)PacketOpcodes.GU_ENTER_WORLD_RES;
            ResultCode = (ushort)ResultCodes.GAME_SUCCESS;
        }

        public ushort ResultCode
        {
            get { return GetShort(4); }
            set { SetShort(4, value); }
        }
    }
}
