using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseLib;
using BaseLib.Packets;
using BaseLib.Structs;

namespace CharServer.Packets
{
    class CU_CHARACTER_ADD_RES : Packet
    {
        public CU_CHARACTER_ADD_RES()
        {
            this.Opcode = (ushort)PacketOpcodes.CU_CHARACTER_ADD_RES;
            this.ResultCode = 200;
            this.charID = 1;
            this.Name = "";
            this.Race = 1;
            this.Class = 4;
            this.Gender = 1;
            this.Face = 2;
            this.Hair = 3;
            this.HairColor = 4;
            this.SkinColor = 5;
            this.Level = 8;
            this.Money = 8888;

        }
        // Lenght 2
        public ushort ResultCode
        {
            get { return this.GetShort(4); }
            set { this.SetShort(4, value); }
        }
        // Lenght 4
        public uint charID
        {
            get { return this.GetInt(6); }
            set { this.SetInt(6, value); }
        }
        // Lenght 17
        public string Name
        {
            get { return this.GetString(10, 34); }
            set { this.SetString(10, value, 34); }
        }
        // Lenght 1
        public byte Race
        {
            get { return this.GetByte(44); }
            set { this.SetByte(44, value); }
        }
        // Lenght 1
        public byte Class
        {
            get { return this.GetByte(45); }
            set { this.SetByte(45, value); }
        }
        // xD
        /* public bool isAdult
         {
             get
             {
                 ushort ret = this.GetShort(46);
                 ret = Convert.ToUInt16(((ret >> 15) & 0x7FFF));
                 return Convert.ToBoolean(ret);
             }
             set
             {
                 ushort ret = this.GetShort(46);
                 ret = Convert.ToUInt16(((ret & (~0x7FFF << 15)) | (Convert.ToUInt16(value) & 0x7FFF) << 15));
                 this.SetShort(46 ret);
             }
         } */
        // Lenght 1
        public byte Gender
        {
            get { return this.GetByte(47); }
            set { this.SetByte(47, value); }
        }
        // Lenght 1
        public byte Face
        {
            get { return this.GetByte(48); }
            set { this.SetByte(48, value); }
        }
        // Lenght 1
        public byte Hair
        {
            get { return this.GetByte(49); }
            set { this.SetByte(49, value); }
        }
        // Lenght 1
        public byte HairColor
        {
            get { return this.GetByte(50); }
            set { this.SetByte(50, value); }
        }
        // Lenght 1
        public byte SkinColor
        {
            get { return this.GetByte(51); }
            set { this.SetByte(51, value); }
        }
        // Lenght 1
        public byte Level
        {
            get { return this.GetByte(52); }
            set { this.SetByte(52, value); }
        }
        // Lenght 4
        public uint worldTblidx
        {
            get { return this.GetInt(53); }
            set { this.SetInt(53, value); }
        }
        // Lenght 4
        public uint worldId
        {
            get { return this.GetInt(57); }
            set { this.SetInt(57, value); }
        }
        // Lenght 4
        public float fPositionX
        {
            get { return this.GetFloat(61); }
            set { this.SetFloat(61, value); }
        }
        // Lenght 4
        public float fPositionY
        {
            get { return this.GetFloat(65); }
            set { this.SetFloat(65, value); }
        }
        // Lenght 4
        public float fPositionZ
        {
            get { return this.GetFloat(69); }
            set { this.SetFloat(69, value); }
        }
        // Lenght 4
        public uint Money
        {
            get { return this.GetInt(73); }
            set { this.SetInt(73, value); }
        }
    }
}
/*
BEGIN_PROTOCOL(CU_CHARACTER_ADD_RES)
    WORD wResultCode;
sPC_SUMMARY sPcDataSummary;
END_PROTOCOL()

/*struct sPC_SUMMARY
{
CHARACTERID		charId;
WCHAR			awchName[NTL_MAX_SIZE_CHAR_NAME_UNICODE + 1];
BYTE			byRace;
BYTE			byClass;
bool			bIsAdult;
BYTE			byGender;
BYTE			byFace;
BYTE			byHair;
BYTE			byHairColor;
BYTE			bySkinColor;
BYTE			byLevel;
TBLIDX			worldTblidx;
WORLDID			worldId;
float			fPositionX;
float			fPositionY;
float			fPositionZ;
DWORD			dwMoney;
DWORD			dwMoneyBank;
sITEM_SUMMARY	sItem[EQUIP_SLOT_TYPE_COUNT]; // 장착 아이템 정보
DWORD			dwMapInfoIndex;
bool			bTutorialFlag;

sMARKING		sMarking;
bool			bNeedNameChange;
sDBO_DOGI_DATA	sDogi;
WORD			wUnknow1; // 0x0000
BYTE			abyUnknow2[6];
};
 */