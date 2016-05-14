using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseLib.Helpers;

namespace BaseLib.Entities
{
    public enum CharacterModel : int
    {
        NULL = -1,
        Masaru = 80001,
        Tohma,
        Yoshino,
        Ikuto
    }

    public class Character
    {
        public uint CharacterID = 0;
        public uint intHandle = 0;
        public string Name = String.Empty;
        public byte Level = 1;
        public CharacterModel Model = CharacterModel.NULL;
        public Position Pos;
        public List<Digimon> DigimonList;

        public Character() {}

        public Character(string name, int level, int cModel)
        {
            this.Name = name;
            this.Level = (byte)level;
            this.Model = (CharacterModel)cModel;
        }

        public Digimon Partner
        {
            get
            {
                return DigimonList[0];
            }
            set
            {
                DigimonList[0] = value;
            }
        }

        public uint ProperModel
        {
            get
            {
                uint iModel = 0x9C40A0;
                iModel += (((uint)Model - 80001) * 128);
                return (iModel << 8);
            }
        }

        public ushort TamerHandle
        {
            get
            {
                byte[] b = new byte[] { (byte)((intHandle >> 32) & 0xFF), 0x20 };
                return BitConverter.ToUInt16(b, 0);
            }
        }

        public ushort DigimonHandle = 0;
    }
}
