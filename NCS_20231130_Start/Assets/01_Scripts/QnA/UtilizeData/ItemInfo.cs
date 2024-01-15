using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//아이템테이블에 들어가있던 정보 원형...
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
