using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour
{
    public Text pName, oName, pHp, oHp;

    public void UIUpdate(PartyPokemon pp, PartyPokemon op)
    {
        pName.text = pp.nickName;
        oName.text = op.nickName;
        pHp.text = pp.CurrentHp + "/" + pp.GetHp();
        oHp.text = op.CurrentHp + "/" + op.GetHp();
    }
}
