using BaseLib.Packets;

namespace GameServer.Packets
{
    class GU_AVATAR_INFO_END : Packet
    {
        public GU_AVATAR_INFO_END()
        {
            Opcode = (ushort)PacketOpcodes.GU_AVATAR_INFO_END;
        }
    }
}
