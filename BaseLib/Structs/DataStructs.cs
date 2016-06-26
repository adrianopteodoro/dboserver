using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace BaseLib.Structs
{
    public enum CharRaces : byte
    {
        HUMAN,
        NAMEK,
        MAJIN
    }

    public enum CharGenders : byte
    {
        MALE,
        FEMALE,
        SEXLESS
    }

    public enum CharClasses : byte
    {
        HUMAN_FIGHTER,
        HUMAN_MYSTIC,
        HUMAN_ENGINEER,
        NAMEK_FIGHTER,
        NAMEK_MYSTIC,
        MIGHTY_MAJIN,
        WONDER_MAJIN,
        STREET_FIGHTER,
        SWORD_MASTER,
        CRANE_ROSHI,
        TURTLE_ROSHI,
        GUN_MANIA,
        MECH_MANIA,
        DARK_WARRIOR,
        SHADOW_KNIGHT,
        DENDEN_HEALER,
        POCO_SUMMONER,
        ULTI_MA,
        GRAND_MA,
        PLAS_MA,
        KAR_MA
    }

    enum EquipSlots
    {
        HAND,
        SUB_WEAPON,
        JACKET,
        PANTS,
        BOOTS,
        SCOUTER,
        COSTUME,
        NECKLACE,
        EARRING_1,
        EARRING_2,
        RING_1,
        RING_2,
        DOGI,
        HAIR,
        ACCESSORY_1,
        ACCESSORY_2,
        ACCESSORY_3,

        COUNT,
        UNKNOWN = 0xFF,

        FIRST = HAND,
        LAST = COUNT - 1,
    };

    public enum DBOCommon : uint
    {
	   DBO_LOGIN_LOGIN_LEN = 16,
	   DBO_LOGIN_PASSWORD_LEN = 16,
       DBO_LOGIN_PASSHASH_LEN = 255,
       DBO_LOGIN_EMAIL_LEN = 255,

        DBO_MAC_ADDR_LEN = 6,
        DBO_IP_ADDR_LEN = 64,

        DBO_AUTHKEY_LEN = 16,

        DBO_CHAR_NAME_LEN = 16,
        DBO_CHAR_SERVER_MAX = 10,
        DBO_CHAR_SERVER_NAME_LEN = 32,
        DBO_CHAR_COUNT_MAX = 8,
        DBO_CHAR_MODEL_LEN = 32,
        DBO_CHAR_SKILL_MAX = 120,
        DBO_CHAR_HTB_MAX = 30,

        DBO_GAME_SERVER_CHANNEL_MAX = 10,
        DBO_GAME_SERVER_NAME_LEN = 32,

        DBO_CHAT_MSG_LEN = 256,

        DBO_ITEM_OPTION_MAX = 2,

        DBO_INVENTORY_ITEM_MAX = 128,


    	DBO_NEWBIE_ITEM_MAX = 10,
    	DBO_NEWBIE_SKILL_MAX = 7,


        DBO_NEWBIE_QUICKSLOT_MAX = 5,

        DBO_DOJO_IN_WORLD_MAX = 7,

        DBO_WORLD_NAME_LEN = 32,
        DBO_WORLD_SPAWN_TABLE_LEN = 64,
        DBO_WORLD_RESOURCE_FOLDER_LEN = 64,
        DBO_WORLD_RESOURCE_FLASH_LEN = 32,
    };


    /* A bunch of "invalid" values */
    public enum InvalidIDs : uint
    {
        LOGINID_INVALID = 0xffffffff,
        CHARID_INVALID = 0xffffffff,
        HANDLEID_INVALID = 0xffffffff,
        GUILDID_INVALID = 0xffffffff,
        BYTE_INVALID = 0xff,
        DBOID_INVALID = 0xffffffff,
    };


    public struct sVECTOR3
    {
        public float x, y, z;

        public sVECTOR3(float p1, float p2, float p3)
        {
            x = p1;
            y = p2;
            z = p3;
        }
    }

    public struct sWORLD_INFO
    {
        uint WorldID; //WorldID
        uint tblidx; // Table index
        uint hTriggerObjectOffset; //Start objects at offset

        sGAME_RULE_INFO sRuleInfo;
    }

    public struct sGAME_RULE_INFO
    {
        Byte byRuleType; // eGAMERULE_TYPE

      //union
	  //{
	   	 //sTIMEQUEST_RULE_INFO sTimeQuestRuleInfo;
         //sRANKBATTLE_RULE_INFO		sRankBattleRuleInfo;
         //sBUDOKAI_RULE_INFO	sBudokaiRuleInfo;
         //sDOJO_RULE_INFO		sDojoRuleInfo;
      //};
    }

    public struct sDBO_DOJO_BRIEF
    {
        uint guildId;
        uint  dojoTblidx;
        Byte byLevel;
        sDBO_GUILD_MARK sMark;
    };

    struct sDBO_GUILD_MARK
    {
        bool IsIntialized()
        {
            if (0xff == byMarkMain && 0xff == byMarkMainColor && 0xff == byMarkInLine &&
                0xff == byMarkOutLine && 0xff == byMarkOutColor)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        Byte byMarkMain;
        Byte byMarkMainColor;
        Byte byMarkInLine;
        Byte byMarkInColor;
        Byte byMarkOutLine;
        Byte byMarkOutColor;

    };

}
