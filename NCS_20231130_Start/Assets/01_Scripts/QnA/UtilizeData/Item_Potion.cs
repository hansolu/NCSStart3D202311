using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Potion : Item
{
    //�ʿ� ������ �ڽ� Ŭ�������� �����־���.
    public Item_Potion() : base()
    {
    }

    public Item_Potion(int index, int count, int maxCount) : base(index, count, maxCount)
    {        
    }
        

    //�길 �������Ϥ��� Ư�� ��...

    public override void Use(PlayerForInven player)
    {
        base.Use(player); //�θ𿡰Լ� �����ص� Use�� �����~~~
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
        
        //�길�� �ؾ�����...
    }
}
