using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour
{
    public Text pName, oName, pHp, oHp;
    public Image pImg, oImg;

    public void UIUpdate(PartyPokemon pp, PartyPokemon op)
    {
        pName.text = pp.nickName;
        oName.text = op.nickName;
        pHp.text = pp.CurrentHp + "/" + pp.GetHp();
        oHp.text = op.CurrentHp + "/" + op.GetHp();

        Texture2D bmp = new Texture2D(240, 240, TextureFormat.ARGB32, false); //https://forum.unity.com/threads/image-sprite-from-byte-array.486667/
        bmp.LoadImage(pp.GetImageBack()); //https://forum.unity.com/threads/solved-issue-loading-sprite-from-byte-into-ui-image-component.329102/
        bmp.Apply();
        pImg.sprite = Sprite.Create(bmp, new Rect(0, 0, bmp.width, bmp.height), new Vector2(.5f, .5f));

        bmp = new Texture2D(240, 240, TextureFormat.ARGB32, false);
        bmp.LoadImage(op.GetImageFront());
        bmp.Apply();
        oImg.sprite = Sprite.Create(bmp, new Rect(0, 0, bmp.width, bmp.height), new Vector2(.5f, .5f));
    }
}
