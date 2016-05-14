using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseLib.Packets;

namespace AuthServer.Packets
{
    class UA_LOGIN_REQ : Packet
    {
        public string UserID
        {
            get { return this.GetString(4, 34); }
        }

        public string UserPW
        {
            get { return this.GetString(38, 34); }
        }

        public uint CodePage
        {
            get { return this.GetInt(72); }
        }

        public ushort MajorVer
        {
            get { return this.GetShort(76); }
        }

        public ushort MinorVer
        {
            get { return this.GetShort(78); }
        }

        public byte[] MacAddress
        {
            get { return this.GetBytes(80, 6); }
        }

        public byte Unknow
        {
            get { return this.GetByte(86); }
        }
    }
}
