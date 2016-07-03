using BaseLib.Packets;
using BaseLib.Structs;
using CharServer.Database;
using System;

namespace CharServer.Packets
{
    class CU_CHARACTER_ADD_RES : Packet
    {
        public CU_CHARACTER_ADD_RES()
        {
            Opcode = (ushort)PacketOpcodes.CU_CHARACTER_ADD_RES;
            ResultCode = (ushort)ResultCodes.CHARACTER_SUCCESS;
            GuildID = MarkingCode = MapInfoIndex = Definitions.INVALID_INT;
            DogiType = GuildColor = DojoColor = Unknow3 = Unknow4 = Unknow5 = Unknow6 = Unknow7 = Unknow8 = Definitions.INVALID_BYTE;
            Unknow1 = Unknow2 = Unknow9 = 0;
        }

        public ushort ResultCode
        {
            get { return GetShort(4); }
            set { SetShort(4, value); }
        }

        public uint CharID
        {
            get { return GetInt(6); }
            set { SetInt(6, value); }
        }

        public string Name
        {
            get { return GetString(10, 34); }
            set { SetString(10, value, 34); }
        }

        public byte Race
        {
            get { return GetByte(44); }
            set { SetByte(44, value); }
        }

        public byte Class
        {
            get { return GetByte(45); }
            set { SetByte(45, value); }
        }

        public bool IsAdult
        {
            get { return GetBool(46); }
            set { SetBool(46, value); }
        }

        public byte Gender
        {
            get { return GetByte(47); }
            set { SetByte(47, value); }
        }

        public byte Face
        {
            get { return GetByte(48); }
            set { SetByte(48, value); }
        }

        public byte Hair
        {
            get { return GetByte(49); }
            set { SetByte(49, value); }
        }

        public byte HairColor
        {
            get { return GetByte(50); }
            set { SetByte(50, value); }
        }

        public byte SkinColor
        {
            get { return GetByte(51); }
            set { SetByte(51, value); }
        }

        public byte Level
        {
            get { return GetByte(52); }
            set { SetByte(52, value); }
        }

        public uint WorldTblIndex
        {
            get { return GetInt(53); }
            set { SetInt(53, value); }
        }

        public uint WorldId
        {
            get { return GetInt(57); }
            set { SetInt(57, value); }
        }

        public float PositionX
        {
            get { return GetFloat(61); }
            set { SetFloat(61, value); }
        }

        public float PositionY
        {
            get { return GetFloat(65); }
            set { SetFloat(65, value); }
        }

        public float PositionZ
        {
            get { return GetFloat(69); }
            set { SetFloat(69, value); }
        }

        public uint Zenny
        {
            get { return GetInt(73); }
            set { SetInt(73, value); }
        }

        public uint ZennyBank
        {
            get { return GetInt(77); }
            set { SetInt(77, value); }
        }

        public void BuildCharEquipaments(uint CharID)
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
                            SetInt(81 + (i * 7), Convert.ToUInt32(e["ItemID"]));
                            SetByte(85 + (i * 7), Convert.ToByte(e["Rank"]));
                            SetByte(86 + (i * 7), Convert.ToByte(e["Grade"]));
                            SetByte(87 + (i * 7), Convert.ToByte(e["BattleAttribute"]));
                        }
                    }
                }

                if (!found)
                {
                    // If Slot X is not found in DB fill with INVALID
                    SetInt(81 + (i * 7), Definitions.INVALID_INT);
                    SetByte(85 + (i * 7), Definitions.INVALID_BYTE);
                    SetByte(86 + (i * 7), Definitions.INVALID_BYTE);
                    SetByte(87 + (i * 7), Definitions.INVALID_BYTE);
                }
            }
        }

        public uint MapInfoIndex
        {
            get { return GetInt(200); }
            set { SetInt(200, value); }
        }

        public bool IsTutorial
        {
            get { return GetBool(204); }
            set { SetBool(204, value); }
        }

        public uint MarkingCode
        {
            get { return GetInt(205); }
            set { SetInt(205, value); }
        }

        public bool IsNeedNameChange
        {
            get { return GetBool(209); }
            set { SetBool(209, value); }
        }

        public uint GuildID
        {
            get { return GetInt(210); }
            set { SetInt(210, value); }
        }

        public byte DogiType
        {
            get { return GetByte(214); }
            set { SetByte(214, value); }
        }

        public byte GuildColor
        {
            get { return GetByte(215); }
            set { SetByte(215, value); }
        }

        public byte DojoColor
        {
            get { return GetByte(216); }
            set { SetByte(216, value); }
        }

        public byte Unknow1
        {
            get { return GetByte(217); }
            set { SetByte(217, value); }
        }

        public byte Unknow2
        {
            get { return GetByte(218); }
            set { SetByte(218, value); }
        }

        public byte Unknow3
        {
            get { return GetByte(219); }
            set { SetByte(219, value); }
        }

        public byte Unknow4
        {
            get { return GetByte(220); }
            set { SetByte(220, value); }
        }

        public byte Unknow5
        {
            get { return GetByte(221); }
            set { SetByte(221, value); }
        }

        public byte Unknow6
        {
            get { return GetByte(222); }
            set { SetByte(222, value); }
        }

        public byte Unknow7
        {
            get { return GetByte(223); }
            set { SetByte(223, value); }
        }

        public byte Unknow8
        {
            get { return GetByte(224); }
            set { SetByte(224, value); }
        }

        public byte Unknow9
        {
            get { return GetByte(225); }
            set { SetByte(225, value); }
        }
    }
}
