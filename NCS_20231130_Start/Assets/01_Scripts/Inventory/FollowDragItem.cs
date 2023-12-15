using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowDragItem : MonoBehaviour
{
    Image img;
    Text txt;
    public void SetSprite(Sprite spr)
    {
        if (img == null)
        {
            img = GetComponent<Image>();
        }
        img.sprite = spr;
    }
    public void SetText(string count)
    {
        if (txt == null)
        {
            txt = transform.GetChild(0).GetComponent<Text>();
        }
        txt.text = count;
    }
}
