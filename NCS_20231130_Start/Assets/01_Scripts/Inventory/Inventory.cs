using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//����
//�����۵� �����ϰ�����...
//���� ���� ���� �����۵��� �˻��ϰų�, ���ϰų� ���ų� ���� �۾��� ���⼭ �� ����..
public class Inventory 
{
    public AllEnum.InventoryKind InvenKind { get; private set; }
    //�����۵�~
    public int Index { get; private set; } = 0; //�κ��� ���� ��ȣ
    public int InvenCount { get; private set; } = 0; //�κ��� ĭ ����

    //Item�� ��� ������ �ϴ���
    // ����Ʈ / �迭 / ��ųʸ�... //����Ʈ == �߰����� �����ȴٸ� ����� ��������ʰ� �ڿ� �ְ� ������....==> ��ü �ڵ�����..
    //�迭�� ��ųʸ��� �߰��� �����ص� �ٸ��ֵ��� ����ĭ�� ������...
    Dictionary<int, Item> ItemDic = new Dictionary<int, Item>(); //���� �ε����� ������..., ������ 
    //���Թ�ȣ, ������ ��Ī�صаͰ� ����....

    public void Init(AllEnum.InventoryKind kind, int invenCount, int index)
    {
        InvenKind = kind;
        InvenCount = invenCount;
        Index = index;

        //���� �� �����, ������ �κ��丮 ������ŭ �ϴ� �� Item��� ä����. �׷��� �ִ��� �������� nullüũ�� �ƴ�, count ==0���� üũ��..
        for (int i = 0; i < invenCount; i++) 
        {
            ItemDic.Add(i, new Item());
        }
    }

    //�⺻ ���     
    #region �˻�
    //Ȥ�� �𸣴� ������ ���� �˻���...
    /// <summary>
    /// �� �κ��丮�� ��� �����۵��� ����Ʈ�� ��ȯ
    /// </summary>
    /// <param name="isAll">true : ����ִ�ĭ �����Ͽ� ��ü ���� / false : ���� �����鸸 ����</param>
    /// <returns></returns>
    public List<Item> GetAllItemInfo(bool isAll) //���� ������ �����鸸 ����������, ����ִ�ĭ �����ؼ� ��ü�� ����������...
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
    //���ϱ�    
    //�׳� ���ϱ�...(�׳� ��ĭ�� ���ϱ�)
    public void AddSimple(Item _item) //�ܼ��� �ݴ� ��� 
    {
        for (int i = 0; i < InvenCount; i++)
        {
            if (ItemDic[i].Index == _item.Index)
            {
                if (ItemDic[i].AbleCount >= _item.Count) //�Ű������� ���� �������� ��������, �ش�ĭ�� �������ִ� �����ۼ��� �� ���ٸ�
                {
                    ItemDic[i].AddCount(_item.Count);
                    //#########�κ��丮�� ��ȭ�� ������.
                    UIManager.Instance.SetInvenChange(Index, i, ItemDic[i]);
                    return;
                }
                else //���δ� ������� ���� ����, �������ִ¸�ŭ�� �ְ���
                {
                    ItemDic[i].AddCount(ItemDic[i].AbleCount); //��������� ItemDic[i] ���� ������ MaxCount���°� �ɰ���
                    _item.SubCount(ItemDic[i].AbleCount); //�Ű������� �������� �� ������ �Ϻ� ���� ���Կ� ���⋚����, �׸�ŭ ����������.
                    //#########�κ��丮�� ��ȭ�� ������.
                    UIManager.Instance.SetInvenChange(Index, i, ItemDic[i]);
                }
            }
            else if (ItemDic[i].Count ==0) //�������
            {
                ItemDic[i].SetItem(_item);
                //#########�κ��丮�� ��ȭ�� ������.
                UIManager.Instance.SetInvenChange(Index, i, ItemDic[i]);
                return;
            }
        }

        if (_item.Count > 0) //�κ��� ���� �� ���� �������� ������ ����...
        {
            //#############�ٴڿ� ������...
        }        
    }

    //Ư�� ���Կ� ���ϱ�//Ư�����Կ� ���� ������ �ְԵǸ�, Maxcount ���� �������� ������ �޶�, ���� �������� �����޴� ��찡 ����..
    public Item AddSlot(int slotnum, Item _item)
    {                
        if (ItemDic[slotnum].Index == _item.Index) //�� ������ ���� �����۰� ���� �ε����� ����
        {
            int overnum = ItemDic[slotnum].AddCount(_item.Count);//�ش罽�Կ� �ְ�, ��ģ ������ �޾Ƴ�...
            _item.SetCount(overnum); //���࿡ ���������� _item�� �� ���ٸ�, overnum�� 0�����ϰ�..
            //#########�κ��丮�� ��ȭ�� ������.
            UIManager.Instance.SetInvenChange(Index, slotnum, ItemDic[slotnum]);
        }
        else
        {
            if (ItemDic[slotnum].Count <= 0) //���
            {
                ItemDic[slotnum].SetItem(_item);
                _item.Clear();
                //#########�κ��丮�� ��ȭ�� ������.
                UIManager.Instance.SetInvenChange(Index, slotnum, ItemDic[slotnum]);
            }
            else //�ƿ� �ٸ� ������ ������..
            {
                Item tempItem = new Item(ItemDic[slotnum]);
                ItemDic[slotnum].SetItem(_item);
                _item.SetItem(tempItem);
                //#########�κ��丮�� ��ȭ�� ������.
                UIManager.Instance.SetInvenChange(Index, slotnum, ItemDic[slotnum]);
            }
        }

        //return new Item( _item);        
        return _item;
    }

    //����
    //�׳� ����...(�׳� �ִ°� �ϴ� ����) ==> �Ŀ� ����.. ������ ���ϰ�
    
    /// <summary>
    /// �ش� index�� ��ǰ�� ��� �����ϴ��� �˻�...
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
    /// index�� �������� count������ŭ �������
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
                if (ItemDic[i].Index == index) //���� ã�� �׾�������
                {
                    count = ItemDic[i].SubCount(count); //���� ��ģ�Ϳ�����ó��. ���������� ���⸦ �ߴٸ� 0�� ��ȯ��.
                    //#########�κ��丮�� ��ȭ�� ������.
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

    //Ư�� ���Կ��� ����
    public Item SubSlot(int slotnum, int count=0 )
    {
        Item tempItem = new Item(ItemDic[slotnum]);
        if (count > 0) //�Ϻθ� ����
        {
            tempItem.SetCount(count);//������ �������� �䱸�� count�� ����.
            ItemDic[slotnum].SubCount(count);//����ĭ���� count��ŭ ����
            //#########�κ��丮�� ��ȭ�� ������.
            UIManager.Instance.SetInvenChange(Index, slotnum, ItemDic[slotnum]);
        }
        else //��ü ����
        {            
            ItemDic[slotnum].Clear();
            //#########�κ��丮�� ��ȭ�� ������.
            UIManager.Instance.SetInvenChange(Index, slotnum, ItemDic[slotnum]);
        }

        return tempItem;
    }

    //�׳� ������ �ش� ������ ������������ item���� �־����
    public void SetItemInfoForce(int slotnum, Item item)
    {
        ItemDic[slotnum].SetItem(item);
        //#########�κ��丮�� ��ȭ�� ������.
        UIManager.Instance.SetInvenChange(Index, slotnum, ItemDic[slotnum]);
    }
}
