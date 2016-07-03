using BaseLib.Packets;

namespace AuthServer.Packets
{
    class AU_LOGIN_DISCONNECT_RES : Packet
    {
        public AU_LOGIN_DISCONNECT_RES()
        {
            Opcode = (ushort)PacketOpcodes.AU_LOGIN_DISCONNECT_RES;
        }
    }
}
