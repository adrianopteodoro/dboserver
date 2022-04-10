﻿using BaseLib;
using BaseLib.Packets;
using GameServer.Network;

namespace GameServer.Packets
{
    public class PacketParser
    {
        Packet pkt;
        GameClient client;

        public PacketParser() {}

        public void CheckPacket(byte[] data, GameClient client)
        {
            this.pkt = new Packet();
            pkt.SetData(data);
            this.client = client;

            switch ((PacketOpcodes)pkt.Opcode)
            {
                case PacketOpcodes.SYS_ALIVE: /* TO SKIP LOGGING THIS PACKET */ break;
                case PacketOpcodes.SYS_HANDSHAKE_RES: client.SendHandShakeRes(); break;
                case PacketOpcodes.UG_GAME_ENTER_REQ: client.SendGameEnterResult(data); break;
                case PacketOpcodes.UG_AUTH_KEY_FOR_COMMUNITY_SERVER_REQ: client.SendAuthKeyCommunityServer(data); break;
                case PacketOpcodes.UG_ENTER_WORLD: client.SendEnterWorldComplete(data); break;
                default:
                    PacketDefinitions.LogPacketData(pkt);
                    break;
            }
        }
    }
}
