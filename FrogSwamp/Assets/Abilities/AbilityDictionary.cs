using System;
using System.Collections.Generic;

public static class AbilityDictionary 
{
    public static Dictionary<int, Type> abilityDictionary = new Dictionary<int, Type>()
    {
        {0, typeof(HolyLight)},
        {1, typeof(FateScroll)},
        {2, typeof(VampireAura)},
        {3, typeof(KingOfTheHill)},
        {4, typeof(BleedingStrike)},
    };
}
