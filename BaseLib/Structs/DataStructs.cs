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
}
