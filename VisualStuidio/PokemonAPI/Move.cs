using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonAPI
{
    public class Move
    {
        public string Name { get; private set; }
        public int Dmg { get; private set; }
        public int Effect { get; private set; }
        public int Pp { get; private set; }
        public int Type { get; private set; }
        public int LearnAt { get; private set; }

        public Move(string _name, int _dmg, int _effect, int _pp, int _type, int _learnAt)
        {
            Name = _name;
            Dmg = _dmg;
            Effect = _effect;
            Pp = _pp;
            Type = _type;
            LearnAt = _learnAt;
        }
    }

    public class PartyMove : Move //not needed for API server, put in game only?
    {
        int currentPp;

        public PartyMove(Move baseMove) : base(baseMove.Name, baseMove.Dmg, baseMove.Effect, baseMove.Pp, baseMove.Type, baseMove.LearnAt)
        {
            currentPp = baseMove.Pp;
        }

        int? Use()
        {
            if (currentPp > 0)
            {
                currentPp--;
                return Dmg;
            }
            else
            {
                return null;
            }
        }

        void Fill()
        {
            currentPp = Pp;
        }
    }
}
