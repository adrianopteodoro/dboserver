using BaseLib.Packets;
using System.Numerics;

namespace GameServer.Packets
{
    class GU_AVATAR_WORLD_INFO : Packet
    {
        public GU_AVATAR_WORLD_INFO()
        {
            Opcode = (ushort)PacketOpcodes.GU_AVATAR_WORLD_INFO;
            CurLoc = new Vector3(15, 20, 30);
            CurDir = new Vector3(5, 2, 1);
            worldID = 1;
            tblIdx = 1;
            triggerObjOffset = (uint)0xFFFFFFFF;
            ruleType = 0;
            timeQuestTblidx = (uint)0xFFFFFFFF;
            timeQuestStartHour = 0xFF;
            timeQuestStartMin = 0xFF;
            timeQuestMode = 0xFF;
            timeQuestDifficulty = 0xFF;
            timeQuestCountDown = false;
            timeQuestGameState = 0xFF;
            timeQuestStageNumber = 0xFF;
            battleDungeonEpRegen = false;
            battleDungeonExpAdd = false;
            battleDungeonItemDrop = false;
            battleDungeonItemUse = false;
            battleDungeonLpRegen = false;
            dojoCount = 0;
            buildDojoData();
        }

        public Vector3 CurLoc
        {
            get { return GetVector3(4);  }
            set { SetVector3(4, value); }
        }
        public Vector3 CurDir
        {
            get { return GetVector3(16); }
            set { SetVector3(16, value); }
        }

        public uint worldID
        {
            get { return GetInt(28); }
            set { SetInt(28, value); }
        }

        public uint tblIdx
        {
            get { return GetInt(32); }
            set { SetInt(32, value); }
        }

        public uint triggerObjOffset
        {
            get { return GetInt(36); }
            set { SetInt(36, value); }
        }

        public byte ruleType
        {
            get { return GetByte(40); }
            set { SetByte(40, value); }
        }

        public uint timeQuestTblidx
        {
            get { return GetInt(41); }
            set { SetInt(41, value); }
        }
        public byte timeQuestStartHour
        {
            get { return GetByte(45); }
            set { SetByte(45, value); }
        }
        public byte timeQuestStartMin
        {
            get { return GetByte(46); }
            set { SetByte(46, value); }
        }
        public byte timeQuestMode
        {
            get { return GetByte(47); }
            set { SetByte(47, value); }
        }
        public byte timeQuestDifficulty
        {
            get { return GetByte(48); }
            set { SetByte(48, value); }
        }
        public bool timeQuestCountDown
        {
            get { return GetBool(49); }
            set { SetBool(49, value); }
        }
        public byte timeQuestGameState
        {
            get { return GetByte(50); }
            set { SetByte(50, value); }
        }
        public byte timeQuestStageNumber
        {
            get { return GetByte(51); }
            set { SetByte(51, value); }
        }

        public bool battleDungeonLpRegen
        {
            get { return GetBool(52); }
            set { SetBool(52, value); }
        }
        public bool battleDungeonEpRegen
        {
            get { return GetBool(53); }
            set { SetBool(53, value); }
        }
        public bool battleDungeonItemDrop
        {
            get { return GetBool(54); }
            set { SetBool(54, value); }
        }
        public bool battleDungeonItemUse
        {
            get { return GetBool(55); }
            set { SetBool(55, value); }
        }
        public bool battleDungeonExpAdd
        {
            get { return GetBool(56); }
            set { SetBool(56, value); }
        }

        public byte dojoCount
        {
            get { return GetByte(57); }
            set { SetByte(57, value); }
        }

        public void buildDojoData()
        {
            int start_offset = 58;
            for (int i = 0; i < 7; i++)
            {
                //uint guildId (len 4)
                //uint dojoTblidx (len 4)
                //byte dojoLevel (len 1)
                //bool dojoMarkIsIntialized (len 1)
                //byte dojoMarkMain (len 1)
                //byte dojoMarkMainColor (len 1)
                //byte dojoMarkInLine (len 1)
                //byte dojoMarkInColor (len 1)
                //byte dojoMarkOutLine (len 1)
                //byte dojoMarkOutColor (len 1)
                int position = start_offset + (16 * i);
                SetInt(position, (uint)0xFFFFFFFF);
                SetInt(position + 4, (uint)0xFFFFFFFF);
                SetByte(position + 8, (byte)0);
                SetBool(position + 9, (bool)false);
                SetByte(position + 10, 0xFF);
                SetByte(position + 11, 0xFF);
                SetByte(position + 12, 0xFF);
                SetByte(position + 13, 0xFF);
                SetByte(position + 14, 0xFF);
                SetByte(position + 15, 0xFF);
            }
        }
    }
}
