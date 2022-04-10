using BaseLib.Packets;
using BaseLib.Structs;

namespace CommunityServer.Packets
{
    class TU_ENTER_CHAT_RES : Packet
    {
        public TU_ENTER_CHAT_RES()
        {
            Opcode = (ushort)PacketOpcodes.TU_ENTER_CHAT_RES;
            ResultCode = (ushort)ResultCodes.CHAT_SUCCESS;
        }

        public ushort ResultCode
        {
            get { return GetShort(4); }
            set { SetShort(4, value); }
        }
    }
}
