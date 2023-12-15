using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����Ŵ���
public class InvenManager : Singleton<InvenManager>
{
    public static int LastInventoryIndex = 0; //�갡 �κ��丮 ������ȣ�� �ο��ϱ� ����
    Dictionary<int, Inventory> AllInventoryDic = new Dictionary<int, Inventory>(); //��� �κ��丮�� ��ųʸ��� ������ ����

    /// <summary>
    /// ȣ��� �κ��丮 ������, �ش� �κ��丮�� ������ȣ�� ��ȯ����...
    /// </summary>
    /// <param name="kind"></param>
    /// <param name="invencount"></param>
    /// <returns></returns>
    public int CreateInven(AllEnum.InventoryKind kind, int invencount)
    {
        LastInventoryIndex++; //�κ��丮 ������ȣ
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
            throw new System.Exception($"{invenidx}�� ���� �κ��丮 ��ȣ");
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
    /// ���԰��� �ٲٱ� ������ ������ �͵�...
    /// </summary>
    public void SwapSlot(int Start_InventoryIdx, int Start_Slotnum, int End_Inventoryidx, int End_slotnum )
    {
        if (Start_InventoryIdx == End_Inventoryidx && Start_Slotnum == End_slotnum)
        {
            Debug.Log("���� ��ġ");
            return;
        }
        Item startitem = AllInventoryDic[Start_InventoryIdx].SubSlot(Start_Slotnum); //���۽����� �������� ����
        Item resultitem = AllInventoryDic[End_Inventoryidx].AddSlot(End_slotnum, startitem); //���۽����� �������� �� ���Կ��ٰ� �ֱ�                             
        AllInventoryDic[Start_InventoryIdx].AddSlot(Start_Slotnum, resultitem); //�����Կ��� ���� �λ깰�� ���۽��Կ� �ֱ�.

        //Item tempitem = new Item(GetItemInfo(Start_InventoryIdx, Start_Slotnum)); //�������� ������
        //AllInventoryDic[Start_InventoryIdx].SetItemInfoForce(Start_Slotnum, GetItemInfo(End_Inventoryidx, End_slotnum));
        ////���۽��Կ� �ش�Ǵ� ������ ������, �� ���Կ� �ش�Ǵ� �����͸� �������. (��ü�ع���)
        //AllInventoryDic[End_Inventoryidx].SetItemInfoForce(End_slotnum, tempitem);
    }
}
