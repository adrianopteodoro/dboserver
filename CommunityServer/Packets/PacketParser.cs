using BaseLib.Packets;
using CommunityServer.Network;

namespace CommunityServer.Packets
{
    public class PacketParser
    {
        Packet pkt;
        CommClient client;

        public PacketParser() {}

        public void CheckPacket(byte[] data, CommClient client)
        {
            this.pkt = new Packet();
            pkt.SetData(data);
            this.client = client;

            switch ((PacketOpcodes)pkt.Opcode)
            {
                case PacketOpcodes.SYS_ALIVE: /* TO SKIP LOGGING THIS PACKET */ break;
                case PacketOpcodes.SYS_HANDSHAKE_RES: client.SendHandShakeRes(); break;
                case PacketOpcodes.UT_CHAT_MESSAGE_SAY: client.ChatSendMessageSay(data); break;
                //case PacketOpcodes.UT_GUILD_LEAVE_REQ:
                default:
                    PacketDefinitions.LogPacketData(pkt);
                    break;
            }
        }
    }
}
