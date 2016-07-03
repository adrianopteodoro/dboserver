using BaseLib.Packets;

namespace CharServer.Packets
{
    class CU_SERVER_FARM_INFO : Packet
    {
        public CU_SERVER_FARM_INFO()
        {
            Opcode = (ushort)PacketOpcodes.CU_SERVER_FARM_INFO;
            ServerID = 0;
            ServerName = "";
            ServerStatus = 0;
            MaxLoad = 0;
            Load = 0;
        }

        public byte ServerID
        {
            get { return GetByte(4); }
            set { SetByte(4, value); }
        }

        public string ServerName
        {
            get { return GetString(5, 66); }
            set { SetString(5, value, 66); }
        }

        public byte ServerStatus
        {
            get { return GetByte(71); }
            set { SetByte(71, value); }
        }

        public uint MaxLoad
        {
            get { return GetInt(72); }
            set { SetInt(72, value); }
        }

        public uint Load
        {
            get { return GetInt(76); }
            set { SetInt(76, value); }
        }
    }
}
