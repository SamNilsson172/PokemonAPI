using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
    public string name;
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

public class PartyMove : Move
{
    int currentPp;

    public PartyMove(Move m)
    {
        name = m.GetName();
        dmg = m.GetDmg();
        effect = m.GetEffect();
        pp = m.GetPp();
        type = m.GetMoveType();
        learnAt = m.GetLearnAt();

        currentPp = pp;
    }

    public int? Use()
    {
        if (currentPp > 0)
        {
            currentPp--;
            return dmg;
        }
        else
        {
            return null;
        }
    }

    public void Fill()
    {
        currentPp = pp;
    }
}
