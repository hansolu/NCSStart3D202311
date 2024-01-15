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

    //부딪혀서 먹는 경우.
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerForInven player = other.GetComponent<PlayerForInven>();
            if (player!=null)
            {
                player.PickUp(Index);
            }

            //내가 오브젝트 풀에 속한다면, 해당 오브젝트풀로 돌려보내기.

            //오브젝트 풀에 속하지 않는다면 하여간 없애기.
            Destroy(this.gameObject);                               
        }      
    }

    void OnCollisionEnter(Collision collision)
    {
        //트리거와 동일.
    }
}
