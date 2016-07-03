
using BaseLib.Packets;

namespace AuthServer.Packets
{
    class AU_COMMERCIAL_SETTING_NFY : Packet
    {
        public AU_COMMERCIAL_SETTING_NFY()
        {
            Opcode = (ushort)PacketOpcodes.AU_COMMERCIAL_SETTING_NFY;
        }
    }
}
