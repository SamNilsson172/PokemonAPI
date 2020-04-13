using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour
{
    public Text pName, oName, pHp, oHp;
    public Button[] moveButts;
    public Image pImg, oImg;

    public void UIUpdate(PartyPokemon pp, PartyPokemon op) //function to update all UI
    {
        pName.text = pp.nickName; //write out names and hp
        oName.text = op.nickName;
        pHp.text = pp.CurrentHp + "/" + pp.GetHp();
        oHp.text = op.CurrentHp + "/" + op.GetHp();

        Texture2D bmp = new Texture2D(240, 240, TextureFormat.ARGB32, false); //https://forum.unity.com/threads/image-sprite-from-byte-array.486667/
        bmp.LoadImage(pp.GetImageBack()); //https://forum.unity.com/threads/solved-issue-loading-sprite-from-byte-into-ui-image-component.329102/
        bmp.Apply();
        pImg.sprite = Sprite.Create(bmp, new Rect(0, 0, bmp.width, bmp.height), new Vector2(.5f, .5f)); //get sprite from byte[]

        bmp = new Texture2D(240, 240, TextureFormat.ARGB32, false);
        bmp.LoadImage(op.GetImageFront());
        bmp.Apply();
        oImg.sprite = Sprite.Create(bmp, new Rect(0, 0, bmp.width, bmp.height), new Vector2(.5f, .5f));

        for (int i = 0; i < 4; i++) //loop 4 times for all moves in moves
        {
            if (i < pp.KnownMoves()) //if current move exists
                moveButts[i].GetComponentInChildren<Text>().text = pp.moves[i].GetName(); //write out its name
            else
            {
                moveButts[i].GetComponentInChildren<Text>().text = "Empty"; //say it's empty
                moveButts[i].interactable = false; //make it unclickable
            }
        }
    }
}
