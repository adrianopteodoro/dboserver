using BaseLib.Packets;
using CharServer.Network;

namespace CharServer.Packets
{
    public class PacketParser
    {
        Packet pkt;
        CharClient client;

        public PacketParser() { }

        public void CheckPacket(byte[] data, CharClient client)
        {
            pkt = new Packet();
            pkt.SetData(data);
            this.client = client;

            switch ((PacketOpcodes)pkt.Opcode)
            {
                case PacketOpcodes.SYS_ALIVE: /* TO SKIP LOGGING THIS PACKET */ break;
                case PacketOpcodes.SYS_HANDSHAKE_RES: client.SendHandShakeRes(); break;
                case PacketOpcodes.UC_LOGIN_REQ: client.SendLoginResponse(data); break;
                case PacketOpcodes.UC_CHARACTER_SERVERLIST_REQ: client.SendServerList(false); break;
                case PacketOpcodes.UC_CHARACTER_SERVERLIST_ONE_REQ: client.SendServerList(true); break;
                case PacketOpcodes.UC_CHARACTER_LOAD_REQ: client.SendCharacterLoad(data); break;
                case PacketOpcodes.UC_CHARACTER_ADD_REQ: client.SendCharacterCreate(data); break;
                case PacketOpcodes.UC_CHARACTER_EXIT_REQ: client.SendDisconnect(data); break;
                case PacketOpcodes.UC_CONNECT_WAIT_CANCEL_REQ: client.SendConnectWaitCancelResult(data); break;
                case PacketOpcodes.UC_CONNECT_WAIT_CHECK_REQ: client.SendConnectWaitCheckResult(data); break;
                case PacketOpcodes.UC_CHARACTER_SELECT_REQ: client.SendCharacterSelectResult(data); break;
                default: PacketDefinitions.LogPacketData(pkt); break;
            }
        }
    }
}
