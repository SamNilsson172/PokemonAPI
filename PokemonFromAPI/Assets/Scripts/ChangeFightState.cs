using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFightState : MonoBehaviour
{
    public Fight fightScript;
    public int stateIndex;

    public void ChangeState()
    {
        fightScript.nextState = stateIndex;
    }
}
