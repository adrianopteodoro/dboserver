using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseLib;
using BaseLib.Helpers;
using BaseLib.Packets;
using BaseLib.Network;
using AuthServer.Network;
using AuthServer.Database;
using AuthServer.Configs;

namespace AuthServer.Packets
{
    enum PacketOpcodes
    {
        // SYSTEM OPCODES
        SYS_ALIVE = 1,
        SYS_PING,
        SYS_HANDSHAKE_REQ,
        SYS_HANDSHAKE_RES,
        SYS_HANSAHAKE_NFY = 16,

        // USER TO LOGIN OPCODES
        UA_LOGIN_REQ = 103,
        UA_LOGIN_HK_REQ,
        UA_LOGIN_CREATEUSER_REQ,
        UA_LOGIN_DISCONNECT_REQ
    };

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
                case PacketOpcodes.UA_LOGIN_REQ: client.SendLoginResponse(data); break;
                case PacketOpcodes.UA_LOGIN_DISCONNECT_REQ: client.SendLoginDisconnectResponse(); break;
                default:
                    SysCons.WriteLine("Recv Unknow Packet Len({0}) Enc({1}) Opcode({2})", pkt.Lenght, pkt.Encrypt, pkt.Opcode);
                    SysCons.SavePacket(pkt.Data, pkt.Opcode);
                    break;
            }
        }
    }
}
