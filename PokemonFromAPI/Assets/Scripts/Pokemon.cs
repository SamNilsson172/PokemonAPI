using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon
{
    public string name; //should be protected, needs
    public int hp;
    public int def;
    public int atk;
    public int type;
    public Move[] learnableMoves;

    //make public creation method
}

public class PartyPokemon : Pokemon
{ //need effectiveness method, need effect method, need atk method
    //use prorperties for private set
    public string nickName;
    public float currentHp; //cant go below 0 in property
    float currentDef; //cant go below /2
    float currentAtk;
    public int lvl;
    int xp;
    public List<PartyMove> moves = new List<PartyMove>(); //do method that checks length in array, move != null

    public PartyPokemon(int _lvl, int _xp)
    {
        nickName = name;
        currentHp = hp;
        currentDef = def;
        currentAtk = atk;
        lvl = _lvl;
        xp = _xp;
    }

    public void Heal()
    {
        currentHp = hp;
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
            defender.currentHp -= (float)dmg / defender.currentDef;
            return true;
        }
        else return false;

    }
}
