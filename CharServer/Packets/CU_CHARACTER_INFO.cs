using BaseLib.Packets;
using BaseLib.Structs;
using CharServer.Database;
using System;

namespace CharServer.Packets
{
    class CU_CHARACTER_INFO : Packet
    {
        private static readonly int blocksize = 220;

        public CU_CHARACTER_INFO()
        {
            Opcode = (ushort)PacketOpcodes.CU_CHARACTER_INFO;
            CharacterCount = 0;
        }

        private void BuildCharDelInfo(uint AccountID, byte ServerID)
        {
            for(int i = 0; i < Definitions.MAXCHARSLOTS; i++)
            {
                bool found = false;

                // TODO: Get from DB Character that will be deleted

                if (!found) {
                    // Char ID to delete
                    SetInt(4 + (i * 8), Definitions.INVALID_INT);
                    // Tick Count to delete
                    SetInt(8 + (i * 8), Definitions.INVALID_INT);
                }
            }
        }

        public byte CharacterCount
        {
            get { return GetByte(68); }
            set { SetByte(68, value); }
        }

        public void BuildCharList(uint AccountID, byte ServerID)
        {
            // Build Deletion List
            BuildCharDelInfo(AccountID, ServerID);

            var chars = CharDB.UserDataQuery("CALL `getAccountCharacters`('{0}','{1}');", AccountID, ServerID);
            CharacterCount = Convert.ToByte(chars.Count);

            if (CharacterCount > 0)
            {
                byte i = 0;
                foreach(var c in chars)
                {
                    BuildCharEquipaments(Convert.ToUInt32(c["CharacterID"]), i);

                    SetInt(69 + (i * blocksize), Convert.ToUInt32(c["CharacterID"]));
                    SetString(73 + (i * blocksize), Convert.ToString(c["Name"]), 34);
                    SetByte(107 + (i * blocksize), Convert.ToByte(c["RaceID"]));
                    SetByte(108 + (i * blocksize), Convert.ToByte(c["ClassID"]));
                    SetByte(109 + (i * blocksize), Convert.ToByte(c["IsAdult"]));
                    SetByte(110 + (i * blocksize), Convert.ToByte(c["GenderID"]));
                    SetByte(111 + (i * blocksize), Convert.ToByte(c["FaceID"]));
                    SetByte(112 + (i * blocksize), Convert.ToByte(c["HairID"]));
                    SetByte(113 + (i * blocksize), Convert.ToByte(c["HairColorID"]));
                    SetByte(114 + (i * blocksize), Convert.ToByte(c["SkinColorID"]));
                    SetByte(115 + (i * blocksize), Convert.ToByte(c["CurrentLevel"]));
                    SetInt(116 + (i * blocksize), Convert.ToUInt32(c["WorldTableID"]));
                    SetInt(120 + (i * blocksize), Convert.ToUInt32(c["WorldID"]));
                    SetFloat(124 + (i * blocksize), (float)Convert.ToDouble(c["Position_X"]));
                    SetFloat(128 + (i * blocksize), (float)Convert.ToDouble(c["Position_Y"]));
                    SetFloat(132 + (i * blocksize), (float)Convert.ToDouble(c["Position_Z"]));
                    SetInt(136 + (i * blocksize), Convert.ToUInt32(c["ZennyInventory"]));
                    SetInt(140 + (i * blocksize), Convert.ToUInt32(c["ZennyBank"]));
                    SetInt(263 + (i * blocksize), Convert.ToUInt32(c["MapInfoID"]));
                    SetByte(267 + (i * blocksize), Convert.ToByte(c["IsTutorialDone"]));
                    SetInt(268 + (i * blocksize), Convert.ToUInt32(c["Marking"]));
                    SetByte(272 + (i * blocksize), Convert.ToByte(c["IsToRename"]));
                    SetInt(273 + (i * blocksize), Convert.ToUInt32(c["GuildID"]));
                    // TODO Add to DBs
                    SetByte(277 + (i * blocksize), Definitions.INVALID_BYTE); //Guild Type?
                    SetByte(278 + (i * blocksize), Definitions.INVALID_BYTE); //Guild Color
                    SetByte(279 + (i * blocksize), Definitions.INVALID_BYTE); //Dojo Color
                    // Need Discover this!
                    SetBytes(280 + (i * blocksize), new byte[] { 0x00,0x00,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0x00 });
                    i++;
                }
            }
        }

        private void BuildCharEquipaments(uint CharID, byte pos)
        {
            var charEquips = CharDB.UserDataQuery("CALL `getCharacterEquipment`('{0}');", CharID);

            for (int i = 0; i < (int)EquipSlots.COUNT; i++)
            {
                bool found = false;

                // Check if exist any equip data in DB
                if (charEquips.Count > 0)
                {
                    foreach (var e in charEquips)
                    {
                        if (Convert.ToInt32(e["Slot"]) == i)
                        {
                            // If Slot X is found in DB get data and set boolean
                            found = true;
                            SetInt((144 + (i * 7)) + (pos * blocksize), Convert.ToUInt32(e["ItemID"]));
                            SetByte((148 + (i * 7)) + (pos * blocksize), Convert.ToByte(e["Rank"]));
                            SetByte((149 + (i * 7)) + (pos * blocksize), Convert.ToByte(e["Grade"]));
                            SetByte((150 + (i * 7)) + (pos * blocksize), Convert.ToByte(e["BattleAttribute"]));
                        }
                    }
                }

                if (!found)
                {
                    // If Slot X is not found in DB fill with INVALID
                    SetInt((144 + (i * 7)) + (pos * blocksize), Definitions.INVALID_INT);
                    SetByte((148 + (i * 7)) + (pos * blocksize), Definitions.INVALID_BYTE);
                    SetByte((149 + (i * 7)) + (pos * blocksize), Definitions.INVALID_BYTE);
                    SetByte((150 + (i * 7)) + (pos * blocksize), Definitions.INVALID_BYTE);
                }
            }
        }
    }
}
