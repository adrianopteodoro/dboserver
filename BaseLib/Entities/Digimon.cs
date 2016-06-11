using System;
using BaseLib.Helpers;

namespace BaseLib.Entities
{
    public class Digimon
    {
        public uint DigimonID = 0;
        public uint Slot = 0;
        public uint Model = 0;
        public uint CharacterID = 0;
        public string Name = String.Empty;
        public byte Level = 1;
        public uint Specie = 31001;
        public uint CurrentForm = 0;
        public ushort Size = 10000;
        public uint EXP = 0;
        public Position Pos;

        public Digimon() { }

        public uint ProperModel()
        {
            uint pModel = 0;
            pModel += (uint)((CurrentForm * 128) + 16);
            return (pModel << 8);
        }

        public ushort Handle
        {
            get
            {
                byte[] b = new byte[] { (byte)((Model >> 32) & 0xFF), 0x10 };
                return BitConverter.ToUInt16(b, 0);
            }
        }

        public byte byteHandle
        {
            get
            {
                return (byte)((Model >> 32) & 0xFF);
            }
        }
    }
}
