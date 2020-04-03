using System.Xml.Serialization;
using System.IO;
using System.Text;

namespace PokemonAPI
{
    public class Pokemon
    {
        public string name; //need public for serialization, want protected
        public int hp;
        public int def;
        public int atk;
        public int type;
        public Move[] learnableMoves;

        public void Create(string _name, int _hp, int _def, int _atk, int _type, Move[] _learnableMoves) //can't have constructor for xml serialization
        {
            name = _name;
            hp = _hp;
            def = _def;
            atk = _atk;
            type = _type;
            learnableMoves = _learnableMoves;
        }

        public string GetName()
        {
            return name;
        }
        public int GetHp()
        {
            return hp;
        }
        public int GetDef()
        {
            return def;
        }
        public int GetAtk()
        {
            return atk;
        }
        public int GetPokeType()
        {
            return type;
        }
        public Move[] GetLearnableMoves()
        {
            return learnableMoves;
        }
    }
}
