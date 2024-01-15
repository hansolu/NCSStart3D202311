using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���������̺� ���ִ� ���� ����...
public class ItemInfo 
{
    public int Index;
    public AllEnum.ItemType type;
    //AllEnum.StatType stattype = ;
    public string Name;
    public float Value;

    public ItemInfo(string index, string type, string name, string value)
    {
        this.Index = int.Parse(index);
        this.type = (AllEnum.ItemType)System.Enum.Parse(typeof(AllEnum.ItemType),type); //
        this.Name = name;
        this.Value = float.Parse(value);
    }
}
