using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Armor : Item
{
    //�ʿ� ������ �ڽ� Ŭ�������� �����־���.
    public Item_Armor() : base()
    {
    }

    public Item_Armor(int index, int count, int maxCount) : base(index, count, maxCount)
    {
    }

    //�길 �������Ϥ��� Ư�� ��...

    public override void Use(PlayerForInven player)
    {
        base.Use(player); //�θ𿡰Լ� �����ص� Use�� �����~~~

        //�길�� �ؾ�����...
    }
}
