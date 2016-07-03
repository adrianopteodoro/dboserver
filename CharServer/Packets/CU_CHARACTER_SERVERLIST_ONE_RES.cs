using BaseLib.Packets;

namespace CharServer.Packets
{
    class CU_CHARACTER_SERVERLIST_ONE_RES : Packet
    {
        public CU_CHARACTER_SERVERLIST_ONE_RES()
        {
            Opcode = (ushort)PacketOpcodes.CU_CHARACTER_SERVERLIST_ONE_RES;
            ResultCode = 200;
        }

        public ushort ResultCode
        {
            get { return GetShort(4); }
            set { SetShort(4, value); }
        }
    }
}
