using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseLib;
using BaseLib.Helpers;
using BaseLib.Packets;
using BaseLib.Network;
using GameServer.Network;
using GameServer.Database;
using GameServer.Configs;

namespace GameServer.Packets
{
    enum PacketOpcodes
    {
        // SYSTEM OPCODES
        SYS_ALIVE = 1,
        SYS_PING,
        SYS_HANDSHAKE_REQ,
        SYS_HANDSHAKE_RES,
        SYS_HANSAHAKE_NFY = 16,
    };

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
                default:
                    SysCons.WriteLine("Recv Unknow Packet Len({0}) Enc({1}) Opcode({2})", pkt.Lenght, pkt.Encrypt, pkt.Opcode);
                    SysCons.SavePacket(pkt.Data, pkt.Opcode);
                    break;
            }
        }
    }
}
