using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//가방매니저
public class InvenManager : Singleton<InvenManager>
{
    public static int LastInventoryIndex = 0; //얘가 인벤토리 고유번호를 부여하기 위함
    Dictionary<int, Inventory> AllInventoryDic = new Dictionary<int, Inventory>(); //모든 인벤토리를 딕셔너리로 가지고 있음

    /// <summary>
    /// 호출시 인벤토리 생성과, 해당 인벤토리의 고유번호를 반환해줌...
    /// </summary>
    /// <param name="kind"></param>
    /// <param name="invencount"></param>
    /// <returns></returns>
    public int CreateInven(AllEnum.InventoryKind kind, int invencount)
    {
        LastInventoryIndex++; //인벤토리 고유번호
        AllInventoryDic.Add(LastInventoryIndex, new Inventory()); 
        AllInventoryDic[LastInventoryIndex].Init(kind, invencount, LastInventoryIndex);
        return LastInventoryIndex;
    }

    public Inventory GetInven(int invenidx)
    {
        if (AllInventoryDic.ContainsKey(invenidx))
        {
            return AllInventoryDic[invenidx];
        }
        else
        {
            throw new System.Exception($"{invenidx}는 없는 인벤토리 번호");
        }
    }

    public Item GetItemInfo(int invenidx, int slotidx)
    {
        if (AllInventoryDic.ContainsKey(invenidx))
        {
            if (AllInventoryDic[invenidx].InvenCount > slotidx)
            {
                return AllInventoryDic[invenidx].GetItemInfo(slotidx);
            }
        }
        return new Item();
    }

    public void AddItemSimple(int invenidx, Item item)
    {
        AllInventoryDic[invenidx].AddSimple(item);
    }

    public void SubItemSlot(int invenidx, int slotnum)
    {
        AllInventoryDic[invenidx].SubSlot(slotnum);
    }

    /// <summary>
    /// 슬롯간에 바꾸기 했을때 진행할 것들...
    /// </summary>
    public void SwapSlot(int Start_InventoryIdx, int Start_Slotnum, int End_Inventoryidx, int End_slotnum )
    {
        if (Start_InventoryIdx == End_Inventoryidx && Start_Slotnum == End_slotnum)
        {
            Debug.Log("같은 위치");
            return;
        }
        Item startitem = AllInventoryDic[Start_InventoryIdx].SubSlot(Start_Slotnum); //시작슬롯의 아이템을 빼기
        Item resultitem = AllInventoryDic[End_Inventoryidx].AddSlot(End_slotnum, startitem); //시작슬롯의 아이템을 끝 슬롯에다가 넣기                             
        AllInventoryDic[Start_InventoryIdx].AddSlot(Start_Slotnum, resultitem); //끝슬롯에다 넣은 부산물을 시작슬롯에 넣기.

        //Item tempitem = new Item(GetItemInfo(Start_InventoryIdx, Start_Slotnum)); //시작점의 데이터
        //AllInventoryDic[Start_InventoryIdx].SetItemInfoForce(Start_Slotnum, GetItemInfo(End_Inventoryidx, End_slotnum));
        ////시작슬롯에 해당되는 아이템 정보에, 끝 슬롯에 해당되는 데이터를 집어넣음. (대체해버림)
        //AllInventoryDic[End_Inventoryidx].SetItemInfoForce(End_slotnum, tempitem);
    }
}
