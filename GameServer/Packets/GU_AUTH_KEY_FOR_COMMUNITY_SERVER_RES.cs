using BaseLib.Helpers;
using BaseLib.Packets;
using BaseLib.Structs;
using System;

namespace GameServer.Packets
{
    class GU_AUTH_KEY_FOR_COMMUNITY_SERVER_RES : Packet
    {
        public GU_AUTH_KEY_FOR_COMMUNITY_SERVER_RES()
        {
            Opcode = (ushort)PacketOpcodes.GU_AUTH_KEY_FOR_COMMUNITY_SERVER_RES;
            ResultCode = (ushort)ResultCodes.GAME_SUCCESS;
        }

        public ushort ResultCode
        {
            get { return GetShort(4); }
            set { SetShort(4, value); }
        }

        public string AuthKey
        {
            get { return GetAsciiString(6, 16); }
            set { SetAsciiString(6, value); }
        }
    }
}
