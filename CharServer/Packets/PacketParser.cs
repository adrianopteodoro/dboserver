using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseLib;
using BaseLib.Helpers;
using BaseLib.Packets;
using BaseLib.Network;
using CharServer.Network;
using CharServer.Database;
using CharServer.Configs;

namespace CharServer.Packets
{
    enum PacketOpcodes
    {
        // SYSTEM OPCODES
        SYS_ALIVE = 1,
        SYS_PING,
        SYS_HANDSHAKE_REQ,
        SYS_HANDSHAKE_RES,
        SYS_HANSAHAKE_NFY = 16,

        // USER TO CHAR OPCODES
        UC_LOGIN_REQ = 2001,
        UC_CHARACTER_SERVERLIST_REQ,
        UC_CHARACTER_SERVERLIST_ONE_REQ,
        UC_CHARACTER_ADD_REQ,
        UC_CHARACTER_DEL_REQ,
        UC_CHARACTER_SELECT_REQ,
        UC_CHARACTER_EXIT_REQ,
        UC_CHARACTER_LOAD_REQ,
        UC_CHARACTER_DEL_CANCEL_REQ,
        UC_CONNECT_WAIT_CHECK_REQ,
        UC_CONNECT_WAIT_CANCEL_REQ,
        UC_CHARACTER_RENAME_REQ,
        UC_CASHITEM_HLSHOP_REFRESH_REQ,
        UC_CASHITEM_BUY_REQ,
        UC_CHAR_SERVERLIST_REQ,

        // CHAR TO USER OPCODES
        CU_HEARTBEAT = 3001,
        CU_SERVER_FARM_INFO,
        CU_SERVER_CHANNEL_INFO,
        CU_LOGIN_RES,
        CU_CHARACTER_SERVERLIST_RES,
        CU_CHARACTER_SERVERLIST_ONE_RES,
        CU_CHARACTER_ADD_RES,
        CU_CHARACTER_DEL_RES,
        CU_CHARACTER_SELECT_RES,
        CU_CHARACTER_INFO,
        CU_CHARACTER_LOAD_RES,
        CU_CHARACTER_EXIT_RES,
        CU_CHARACTER_DEL_CANCEL_RES,
        CU_DISCONNECTED_NFY,
        CU_SERVER_FARM_INFO_REFRESHED_NFY,
        CU_SERVER_CHANNEL_INFO_REFRESHED_NFY,
        CU_CONNECT_WAIT_CHECK_RES,
        CU_CONNECT_WAIT_COUNT_NFY,
        CU_CONNECT_WAIT_CANCEL_RES,
        CU_CONNECT_WAIT_CANCEL_NFY,
        CU_NETMARBLEMEMBERIP_NFY,
        CU_CHARACTER_DEL_NFY,
        CU_CHARACTER_RENAME_RES,
        CU_CASHITEM_HLSHOP_REFRESH_RES,
        CU_CASHITEM_BUY_RES,
        CU_PREMIUM_SLOT_COUNT_NFY,
        CU_CHAR_SERVERLIST_RES,
        CU_SERVER_FARM_INFO_NFY,
    };

    public class PacketParser
    {
        Packet pkt;
        CharClient client;

        public PacketParser() { }

        public void CheckPacket(byte[] data, CharClient client)
        {
            this.pkt = new Packet();
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
                default:
                    SysCons.WriteLine("Recv Unknow Packet Len({0}) Enc({1}) Opcode({2})", pkt.Lenght, pkt.Encrypt, pkt.Opcode);
                    SysCons.SavePacket(pkt.Data, pkt.Opcode);
                    break;
            }
        }
    }
}
