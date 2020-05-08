using System;

namespace dotnet_rpg.Enums
{
    public enum RpgClass
    {
        None,
        Knight,
        Mage,
        Cleric
    }

    public static class RpgClassParser {
        public static RpgClass Parse(string input)
        {
            try {
                return (RpgClass) Enum.Parse(typeof(RpgClass), input);
            } catch (Exception) {
                return RpgClass.None;
            }
        }
    }
}