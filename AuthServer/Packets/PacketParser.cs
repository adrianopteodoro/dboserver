using BaseLib;
using BaseLib.Packets;
using AuthServer.Network;

namespace AuthServer.Packets
{
    public class PacketParser
    {
        private Packet pkt;
        private AuthClient client;

        public PacketParser() {}

        public void CheckPacket(byte[] data, AuthClient client)
        {
            this.pkt = new Packet();
            pkt.SetData(data);
            this.client = client;

            switch((PacketOpcodes)pkt.Opcode)
            {
                case PacketOpcodes.SYS_ALIVE: /* TO SKIP LOGGING THIS PACKET */ break;
                case PacketOpcodes.SYS_HANDSHAKE_RES: client.SendHandShakeRes(); break;
                case PacketOpcodes.UA_LOGIN_TW_REQ:
                case PacketOpcodes.UA_LOGIN_HK_REQ:
                case PacketOpcodes.UA_LOGIN_NTL_REQ:
                    client.SendLoginResponse(data);
                    break;
                case PacketOpcodes.UA_LOGIN_KR_REQ: /* bypass KR client AUTH (its uses Netmarble SSO) */ break;
                case PacketOpcodes.UA_LOGIN_DISCONNECT_REQ: client.SendLoginDisconnectResponse(); break;
                default:
                    PacketDefinitions.LogPacketData(pkt);
                    break;
            }
        }
    }
}
