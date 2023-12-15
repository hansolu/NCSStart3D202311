using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//가방
//아이템들 소유하고있음...
//뭔가 내가 가진 아이템들을 검색하거나, 더하거나 빼거나 같은 작업을 여기서 할 것임..
public class Inventory 
{
    public AllEnum.InventoryKind InvenKind { get; private set; }
    //아이템들~
    public int Index { get; private set; } = 0; //인벤의 고유 번호
    public int InvenCount { get; private set; } = 0; //인벤의 칸 개수

    //Item을 어떻게 관리를 하느냐
    // 리스트 / 배열 / 딕셔너리... //리스트 == 중간것이 삭제된다면 가운데가 비어있지않고 뒤에 애가 땡겨짐....==> 자체 자동정렬..
    //배열과 딕셔너리는 중간에 삭제해도 다른애들은 기존칸을 유지함...
    Dictionary<int, Item> ItemDic = new Dictionary<int, Item>(); //슬롯 인덱스와 같아짐..., 아이템 
    //슬롯번호, 아이템 매칭해둔것과 같음....

    public void Init(AllEnum.InventoryKind kind, int invenCount, int index)
    {
        InvenKind = kind;
        InvenCount = invenCount;
        Index = index;

        //제가 한 방식은, 무조건 인벤토리 개수만큼 일단 빈 Item들로 채워둠. 그래서 있는지 없는지는 null체크가 아닌, count ==0으로 체크함..
        for (int i = 0; i < invenCount; i++) 
        {
            ItemDic.Add(i, new Item());
        }
    }

    //기본 기능     
    #region 검색
    //혹시 모르니 아이템 정보 검색용...
    /// <summary>
    /// 내 인벤토리의 모든 아이템들을 리스트로 반환
    /// </summary>
    /// <param name="isAll">true : 비어있는칸 포함하여 전체 보냄 / false : 가진 정보들만 보냄</param>
    /// <returns></returns>
    public List<Item> GetAllItemInfo(bool isAll) //가진 아이템 정보들만 보낼것인지, 비어있는칸 포함해서 전체를 보낼것인지...
    {
        List<Item> returnList = new List<Item>();
        for (int i = 0; i < InvenCount; i++)
        {            
            if (ItemDic[i].Count > 0 )
            {
                returnList.Add(ItemDic[i]);
            }
            else
            {
                if (isAll)
                {
                    returnList.Add(ItemDic[i]);
                }
            }
        }

        return returnList;
    }

    public Item GetItemInfo(int slotnum)
    {
        return ItemDic[slotnum];
    }
    #endregion
    //더하기    
    //그냥 더하기...(그냥 빈칸에 더하기)
    public void AddSimple(Item _item) //단순히 줍는 경우 
    {
        for (int i = 0; i < InvenCount; i++)
        {
            if (ItemDic[i].Index == _item.Index)
            {
                if (ItemDic[i].AbleCount >= _item.Count) //매개변수로 들어온 아이템의 개수보다, 해당칸에 넣을수있는 아이템수가 더 많다면
                {
                    ItemDic[i].AddCount(_item.Count);
                    //#########인벤토리에 변화가 생겼음.
                    UIManager.Instance.SetInvenChange(Index, i, ItemDic[i]);
                    return;
                }
                else //전부다 집어넣을 수는 없고, 넣을수있는만큼만 넣겠음
                {
                    ItemDic[i].AddCount(ItemDic[i].AbleCount); //결과적으로 ItemDic[i] 안의 개수는 MaxCount상태가 될것임
                    _item.SubCount(ItemDic[i].AbleCount); //매개변수의 아이템은 그 개수가 일부 기존 슬롯에 들어갔기떄문에, 그만큼 차감시켜줌.
                    //#########인벤토리에 변화가 생겼음.
                    UIManager.Instance.SetInvenChange(Index, i, ItemDic[i]);
                }
            }
            else if (ItemDic[i].Count ==0) //비었으면
            {
                ItemDic[i].SetItem(_item);
                //#########인벤토리에 변화가 생겼음.
                UIManager.Instance.SetInvenChange(Index, i, ItemDic[i]);
                return;
            }
        }

        if (_item.Count > 0) //인벤에 넣을 수 없는 아이템의 여분이 있음...
        {
            //#############바닥에 버리기...
        }        
    }

    //특정 슬롯에 더하기//특정슬롯에 내가 강제로 넣게되면, Maxcount 내지 아이템의 종류가 달라서, 내가 아이템을 돌려받는 경우가 생김..
    public Item AddSlot(int slotnum, Item _item)
    {                
        if (ItemDic[slotnum].Index == _item.Index) //그 슬롯의 기존 아이템과 나의 인덱스가 같음
        {
            int overnum = ItemDic[slotnum].AddCount(_item.Count);//해당슬롯에 넣고, 넘친 개수를 받아냄...
            _item.SetCount(overnum); //만약에 정상적으로 _item이 다 들어갔다면, overnum이 0상태일것..
            //#########인벤토리에 변화가 생겼음.
            UIManager.Instance.SetInvenChange(Index, slotnum, ItemDic[slotnum]);
        }
        else
        {
            if (ItemDic[slotnum].Count <= 0) //빈거
            {
                ItemDic[slotnum].SetItem(_item);
                _item.Clear();
                //#########인벤토리에 변화가 생겼음.
                UIManager.Instance.SetInvenChange(Index, slotnum, ItemDic[slotnum]);
            }
            else //아예 다른 물건이 차있음..
            {
                Item tempItem = new Item(ItemDic[slotnum]);
                ItemDic[slotnum].SetItem(_item);
                _item.SetItem(tempItem);
                //#########인벤토리에 변화가 생겼음.
                UIManager.Instance.SetInvenChange(Index, slotnum, ItemDic[slotnum]);
            }
        }

        //return new Item( _item);        
        return _item;
    }

    //빼기
    //그냥 빼기...(그냥 있는거 싹다 빼기) ==> 후에 제작.. 같은데 쓰일것
    
    /// <summary>
    /// 해당 index의 물품이 몇개나 존재하는지 검색...
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public int GetIsAbleCount(int index) //
    {
        int ableCount = 0;
        for (int i = 0; i < InvenCount; i++)
        {
            if (ItemDic[i].Index == index)
            {
                ableCount += ItemDic[i].Count;
            }
        }

        return ableCount;
    }

    /// <summary>
    /// index인 아이템을 count개수만큼 빼고싶음
    /// </summary>
    /// <param name="index"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public bool SubSimple(int index, int count)
    {
        int ablecount = GetIsAbleCount(index);
        if (ablecount < count)
        {
            return false;
        }
        else
        {
            for (int i = 0; i < InvenCount; i++)
            {
                if (ItemDic[i].Index == index) //내가 찾는 그아이템임
                {
                    count = ItemDic[i].SubCount(count); //빼고 넘친것에대한처리. 정상적으로 빼기를 했다면 0을 반환함.
                    //#########인벤토리에 변화가 생겼음.
                    UIManager.Instance.SetInvenChange(Index, i, ItemDic[i]);

                    if (count ==0)
                    {
                        return true;
                    }
                }
            }

            return true;
        }
    }

    //특정 슬롯에서 빼기
    public Item SubSlot(int slotnum, int count=0 )
    {
        Item tempItem = new Item(ItemDic[slotnum]);
        if (count > 0) //일부만 빼기
        {
            tempItem.SetCount(count);//돌려줄 아이템은 요구한 count로 세팅.
            ItemDic[slotnum].SubCount(count);//기존칸에서 count만큼 빼고
            //#########인벤토리에 변화가 생겼음.
            UIManager.Instance.SetInvenChange(Index, slotnum, ItemDic[slotnum]);
        }
        else //전체 빼기
        {            
            ItemDic[slotnum].Clear();
            //#########인벤토리에 변화가 생겼음.
            UIManager.Instance.SetInvenChange(Index, slotnum, ItemDic[slotnum]);
        }

        return tempItem;
    }

    //그냥 무작정 해당 슬롯의 아이템정보를 item으로 넣어버림
    public void SetItemInfoForce(int slotnum, Item item)
    {
        ItemDic[slotnum].SetItem(item);
        //#########인벤토리에 변화가 생겼음.
        UIManager.Instance.SetInvenChange(Index, slotnum, ItemDic[slotnum]);
    }
}
