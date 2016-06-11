using BaseLib.Packets;

namespace CharServer.Packets
{
    class CU_CHARACTER_SERVERLIST_ONE_RES : Packet
    {
        public CU_CHARACTER_SERVERLIST_ONE_RES()
        {
            this.Opcode = (ushort)PacketOpcodes.CU_CHARACTER_SERVERLIST_ONE_RES;
            this.ResultCode = 200;
        }

        public ushort ResultCode
        {
            get { return this.GetShort(4); }
            set { this.SetShort(4, value); }
        }
    }
}
