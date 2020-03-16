using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonAPI
{
    public class Pokemon
    {
        public string name; //should be protected, needs
        public int hp;
        public int def;
        public int atk;
        public int type;
        //public Move[] learnableMoves;

        //make public creation method
    }

    public class PartyPokemon : Pokemon //class only needed in game?
    { //need effectiveness method, need effect method, need atk method
        string nickName;
        float currentHp;
        float currentDef;
        float currentAtk;
        int lvl;
        int xp;
        PartyMove[] moves = new PartyMove[4];

        public PartyPokemon(int _lvl, int _xp)
        {
            nickName = name;
            currentHp = hp;
            currentDef = def;
            currentAtk = atk;
            lvl = _lvl;
            xp = _xp;
        }

        void Heal()
        {
            currentHp = hp;
        }
    }
}
