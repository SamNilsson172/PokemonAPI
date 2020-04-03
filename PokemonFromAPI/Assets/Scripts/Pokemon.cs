public class Pokemon
{
    public string name; //needs to be public for serialization
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

public class PartyPokemon : Pokemon
{ //need effectiveness method, need effect method, need lvl ethod
    //use prorperties for private set
    public string nickName;
    float currentHp;
    public float CurrentHp //cant go below 0
    {
        get
        {
            return currentHp;
        }
        set
        {
            if (value < 0)
                currentHp = 0;
            else
                currentHp = value;
        }
    }
    float currentDef;
    float CurrentDef //cant go below /2
    {
        get
        {
            return currentDef;
        }
        set
        {
            if (value < GetDef() / 2)
                currentDef = GetDef() / 2;
            else
                currentDef = value;
        }
    }
    float currentAtk;
    float CurrentAtk //cant go below /2
    {
        get
        {
            return currentAtk;
        }
        set
        {
            if (value < GetAtk() / 2)
                currentAtk = GetAtk() / 2;
            else
                currentAtk = value;
        }
    }
    public int lvl;
    int xp;
    public PartyMove[] moves = new PartyMove[4]; //do method that checks length in array, move != null

    public PartyPokemon(int _lvl, int _xp, Pokemon p)
    {
        atk = p.GetAtk();
        def = p.GetDef();
        hp = p.GetHp();
        name = p.GetName();
        learnableMoves = p.GetLearnableMoves();
        type = p.GetPokeType();

        nickName = name;
        CurrentHp = hp;
        CurrentDef = def;
        CurrentAtk = atk;
        lvl = _lvl;
        xp = _xp;

        LearnNewMove();
    }

    public void LearnNewMove()
    {
        for (int i = 0; i < learnableMoves.Length; i++)//loop throug all learnable moves
        {
            if (learnableMoves[i].GetLearnAt() <= lvl && !learnableMoves[i].GetLearnt()) //if your highenough level to learn the move and haven't leant it
            {
                for (int ii = 0; ii < 4; ii++) //find empty slot in moves
                {
                    if (moves[ii] == null)
                    {
                        moves[ii] = new PartyMove(learnableMoves[i]);
                        learnableMoves[i].SetLearnt(true);
                    }
                }// add outcome for if all slots are full
            }
        }
    }

    public void Heal()
    {
        CurrentHp = hp;
        foreach (PartyMove m in moves)
        {
            m.Fill();
        }
    }

    public bool Attack(int move, PartyPokemon defender)
    {
        int? dmg = moves[move].Use();
        if (dmg != null)
        {
            defender.CurrentHp -= (float)dmg + CurrentAtk / defender.CurrentDef;
            return true;
        }
        else return false;
    }
}

public class AllPokemon
{
    public static Pokemon[] pokemon;
}
