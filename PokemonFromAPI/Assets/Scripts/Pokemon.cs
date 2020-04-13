public class Pokemon
{
    public string name; //need public for serialization, want protected
    public int hp;
    public int def;
    public int atk;
    public int type;
    public Move[] learnableMoves;
    public byte[] imageFront; //byte array for images
    public byte[] imageBack;


    public void Create(string _name, int _hp, int _def, int _atk, int _type, Move[] _learnableMoves, byte[] _imageBack, byte[] _imageFront) //can't have constructor for xml serialization
    {
        name = _name;
        hp = _hp;
        def = _def;
        atk = _atk;
        type = _type;
        learnableMoves = _learnableMoves;
        imageBack = _imageBack;
        imageFront = _imageFront;
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
    public byte[] GetImageFront()
    {
        return imageFront;
    }
    public byte[] GetImageBack()
    {
        return imageBack;
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
        imageFront = p.GetImageFront();
        imageBack = p.GetImageBack();

        nickName = name;
        CurrentHp = hp;
        CurrentDef = def;
        CurrentAtk = atk;
        lvl = _lvl;
        xp = _xp;

        LearnNewMove();
    }

    bool[] learnt = new bool[1000];
    public void LearnNewMove()
    {
        for (int i = 0; i < learnableMoves.Length; i++)//loop through all learnable moves
        {
            if (learnableMoves[i].GetLearnAt() <= lvl && !learnt[i]) //if your highenough level to learn the move and haven't leant it
            {
                for (int ii = 0; ii < 4; ii++) //find empty slot in moves
                {
                    if (moves[ii] == null)
                    {
                        moves[ii] = new PartyMove(learnableMoves[i]); //set slot to move
                        learnt[i] = true;
                        break; //don't fill all slots
                    }
                }// add outcome for if all slots are full
            }
        }
    }

    public int KnownMoves() //gets lenth of moves excluding null
    {
        int move = 0;
        foreach (Move m in moves)
        {
            if (m != null)
            {
                move++;
            }
        }
        return move;
    }

    public void Heal()
    {
        CurrentHp = hp;
        foreach (PartyMove m in moves)
        {
            m.Fill();
        }
    }

    public bool Attack(int move, PartyPokemon defender, float effectivness)
    {
        int? dmg = moves[move].Use();
        if (dmg != null) //if all the moves pp is used up
        {
            defender.CurrentHp -= ((float)dmg + CurrentAtk / defender.CurrentDef) * effectivness;
            return true;
        }
        else return false;
    }
}

public class AllPokemon
{
    public static Pokemon[] pokemon; //store all pokemon
}
