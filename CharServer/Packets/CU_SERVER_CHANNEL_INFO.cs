using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseLib;
using BaseLib.Packets;
using BaseLib.Structs;
using CharServer.Configs;

namespace CharServer.Packets
{
    class CU_SERVER_CHANNEL_INFO : Packet
    {
        public CU_SERVER_CHANNEL_INFO()
        {
            this.Opcode = (ushort)PacketOpcodes.CU_SERVER_CHANNEL_INFO;
            this.ChannelCount = 0;
        }

        public byte ChannelCount
        {
            get { return this.GetByte(4); }
            set { this.SetByte(4, value); }
        }

        public void BuildChannelList(int ServerID)
        {
            this.ChannelCount = (byte)CharConfig.Instance.GetGameServerChannelCount(ServerID);
            for(int i = 0; i < this.ChannelCount; ++i)
            {
                int channid = i + 1;
                // Server ID
                this.SetByte(5 + (i * 119), (byte)ServerID);
                // Channel ID
                this.SetByte(6 + (i * 119), (byte)channid);
                // Boolean is Visible
                this.SetByte(7 + (i * 119), 1);
                // Server Status
                this.SetByte(8 + (i * 119), 0);
                // MaxLoad
                this.SetInt(9 + (i * 119), 100);
                // Load
                this.SetInt(13 + (i * 119), 0);
                // Is Scramble
                this.SetByte(17 + (i * 119), 1);
            }
        }
    }
}
