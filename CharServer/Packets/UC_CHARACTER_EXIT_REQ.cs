using System;
using BaseLib.Packets;


namespace CharServer.Packets
{
    class UC_CHARACTER_EXIT_REQ : Packet
    {
        public bool IsGameMove
        {
            get { return Convert.ToBoolean(GetByte(4)); }
        }
    }
}
