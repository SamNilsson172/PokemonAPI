using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonAPI
{
    public class Move
    {
        public string name; //need public for serialization, should be protected otherwise
        public int dmg;
        public int effect;
        public int pp;
        public int type;
        public int learnAt;
        public bool learnt;

        public void Create(string _name, int _dmg, int _effect, int _pp, int _type, int _learnAt)
        {
            name = _name;
            dmg = _dmg;
            effect = _effect;
            pp = _pp;
            type = _type;
            learnAt = _learnAt;
            learnt = false;
        }

        public string GetName()
        {
            return name;
        }
        public int GetDmg()
        {
            return dmg;
        }
        public int GetEffect()
        {
            return effect;
        }
        public int GetPp()
        {
            return pp;
        }
        public int GetMoveType()
        {
            return type;
        }

        public int GetLearnAt()
        {
            return learnAt;
        }

        public bool GetLearnt()
        {
            return learnt;
        }

        public void SetLearnt(bool set)
        {
            learnt = set;
        }
    }
}
