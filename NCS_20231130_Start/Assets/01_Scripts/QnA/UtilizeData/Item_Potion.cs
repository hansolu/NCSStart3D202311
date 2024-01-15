using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Potion : Item
{
    //필요 생성자 자식 클래스에도 만들어둬야함.
    public Item_Potion() : base()
    {
    }

    public Item_Potion(int index, int count, int maxCount) : base(index, count, maxCount)
    {        
    }
        

    //얘만 가져야하ㅑㄹ 특정 값...

    public override void Use(PlayerForInven player)
    {
        base.Use(player); //부모에게서 구현해둔 Use를 사용함~~~
        if (Index == 1)
        {
            player.SetPlayerStat(
                /*ResourceManager.Instance.AllItemInfos[Index].stattype*/
                AllEnum.StatType.HP,
                ResourceManager.Instance.AllItemInfos[Index].Value
                );
        }
        else
        {
            player.SetPlayerStat(
                /*ResourceManager.Instance.AllItemInfos[Index].stattype*/
                AllEnum.StatType.Speed,
                ResourceManager.Instance.AllItemInfos[Index].Value
                );
        }
        
        //얘만이 해야할일...
    }
}
