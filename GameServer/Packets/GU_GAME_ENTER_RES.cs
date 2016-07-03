using BaseLib.Helpers;
using BaseLib.Packets;
using BaseLib.Structs;
using System;

namespace GameServer.Packets
{
    class GU_GAME_ENTER_RES : Packet
    {
        public GU_GAME_ENTER_RES()
        {
            Opcode = (ushort)PacketOpcodes.GU_GAME_ENTER_RES;
            ResultCode = (ushort)ResultCodes.GAME_SUCCESS;
            CommunityServerIP = "127.0.0.1";
            CommunityServerPort = 50700;
            GameEnterTime = Utils.GetTimestamp(DateTime.Now);
        }

        public ushort ResultCode
        {
            get { return GetShort(4); }
            set { SetShort(4, value); }
        }

        public string CommunityServerIP
        {
            get { return GetAsciiString(6, 65); }
            set { SetAsciiString(6, value); }
        }

        public ushort CommunityServerPort
        {
            get { return GetShort(71); }
            set { SetShort(71, value); }
        }

        public ulong GameEnterTime
        {
            get { return GetLong(73); }
            set { SetLong(73, value); }
        }
    }
}
