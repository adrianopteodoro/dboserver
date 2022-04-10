using BaseLib.Packets;

namespace GameServer.Packets
{
    class GU_ENTER_WORLD_COMPLETE : Packet
    {
        public GU_ENTER_WORLD_COMPLETE()
        {
            Opcode = (ushort)PacketOpcodes.GU_ENTER_WORLD_COMPLETE;
        }
    }
}
