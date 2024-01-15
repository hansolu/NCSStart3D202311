using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public int Index { get; private set; }

    SpriteRenderer spriteren;

    public void SetInfo(int index)
    {
        this.Index = index;

        if (spriteren == null)
        {
            spriteren = GetComponent<SpriteRenderer>();
        }
        spriteren.sprite = ResourceManager.Instance.AllItemSprites[index];
    }

    //�ε����� �Դ� ���.
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerForInven player = other.GetComponent<PlayerForInven>();
            if (player!=null)
            {
                player.PickUp(Index);
            }

            //���� ������Ʈ Ǯ�� ���Ѵٸ�, �ش� ������ƮǮ�� ����������.

            //������Ʈ Ǯ�� ������ �ʴ´ٸ� �Ͽ��� ���ֱ�.
            Destroy(this.gameObject);                               
        }      
    }

    void OnCollisionEnter(Collision collision)
    {
        //Ʈ���ſ� ����.
    }
}
