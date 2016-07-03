
namespace BaseLib.Structs
{
    public class Definitions
    {
        public static readonly byte MAXCHARSLOTS = 8;
        public static readonly uint INVALID_INT = 0xFFFFFFFF;
        public static readonly ushort INVALID_SHORT = 0xFFFF;
        public static readonly byte INVALID_BYTE = 0xFF;
    }

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

    public enum EquipSlots
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
