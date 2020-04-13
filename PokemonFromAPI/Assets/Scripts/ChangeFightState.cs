using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFightState : MonoBehaviour
{
    public Fight fightScript;
    public int stateIndex;
    public int selectIndex;

    public void ChangeState() //call on button click
    {
        fightScript.nextState = stateIndex; //change state in fight
        fightScript.atkIndex = selectIndex; //select what move to use 
    }
}
