using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonAPI
{
    public static class FightFunctions
    {
        static float Effectivness(int attacker, int defender)
        {
            return attacker / defender;
        }
    }
}
