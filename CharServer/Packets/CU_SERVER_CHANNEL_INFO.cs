using BaseLib.Packets;
using CharServer.Configs;

namespace CharServer.Packets
{
    class CU_SERVER_CHANNEL_INFO : Packet
    {
        public CU_SERVER_CHANNEL_INFO()
        {
            Opcode = (ushort)PacketOpcodes.CU_SERVER_CHANNEL_INFO;
            ChannelCount = 0;
        }

        public byte ChannelCount
        {
            get { return GetByte(4); }
            set { SetByte(4, value); }
        }

        public void BuildChannelList(int ServerID)
        {
            ChannelCount = (byte)CharConfig.Instance.GetGameServerChannelCount(ServerID);
            for(int i = 0; i < ChannelCount; ++i)
            {
                int channid = i + 1;
                // Server ID
                SetByte(5 + (i * 119), (byte)ServerID);
                // Channel ID
                SetByte(6 + (i * 119), (byte)channid);
                // Boolean is Visible
                SetByte(7 + (i * 119), 1);
                // Server Status
                SetByte(8 + (i * 119), 0);
                // MaxLoad
                SetInt(9 + (i * 119), 100);
                // Load
                SetInt(13 + (i * 119), 0);
                // Is Scramble (0 = NO | 1 = YES)
                SetByte(17 + (i * 119), 0);
            }
        }
    }
}
