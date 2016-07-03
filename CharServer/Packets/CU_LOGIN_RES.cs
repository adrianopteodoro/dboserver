using BaseLib.Packets;

namespace CharServer.Packets
{
    class CU_LOGIN_RES : Packet
    {
        public CU_LOGIN_RES()
        {
            Opcode = (ushort)PacketOpcodes.CU_LOGIN_RES;
            ResultCode = 200;
            LastServerID = 255;
            RaceAllowedFlag = 7;
            LastChannelID = 255;
        }

        public ushort ResultCode
        {
            get { return GetShort(4); }
            set { SetShort(4, value); }
        }

        public byte LastServerID
        {
            get { return GetByte(6); }
            set { SetByte(6, value); }
        }

        public uint RaceAllowedFlag
        {
            get { return GetInt(7); }
            set { SetInt(7, value); }
        }

        public byte LastChannelID
        {
            get { return GetByte(11); }
            set { SetByte(11, value); }
        }
    }
}
