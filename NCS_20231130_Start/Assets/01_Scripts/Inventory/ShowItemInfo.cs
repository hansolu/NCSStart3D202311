using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowItemInfo : MonoBehaviour
{
    public Image explainImg;
    public Text[] infos;
    
    public void SetInit()
    {
        if (explainImg == null)
        {
            explainImg = transform.GetChild(0).GetComponent<Image>();
            //텍스트도 세팅하기..
        }
    }

    public void SetItemInfo(Item item)
    {
        Debug.Log("스크립트로 아이템 설명 정보 세팅하기");
        //infos[0].text = item.아이템이름;
        //infos[1].text = item.아이템 기능... 설명...;
    }
}
