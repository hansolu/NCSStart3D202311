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
            //�ؽ�Ʈ�� �����ϱ�..
        }
    }

    public void SetItemInfo(Item item)
    {
        Debug.Log("��ũ��Ʈ�� ������ ���� ���� �����ϱ�");
        //infos[0].text = item.�������̸�;
        //infos[1].text = item.������ ���... ����...;
    }
}
