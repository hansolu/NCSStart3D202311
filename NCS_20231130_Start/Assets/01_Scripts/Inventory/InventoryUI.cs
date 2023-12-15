using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public bool IsActive = false; //����� �κ��丮�� ���� Ȱ��ȭ �Ǿ��ִ��� ����.
    public int InventoryIdx { get; private set; } = 0;//���� ���� �׸��� �ִ� ������ �κ��丮 ��ȣ
    List<SlotUI> SlotList = new List<SlotUI>();
    GridLayoutGroup grid = null;    
    
    public void SetInit(int inventoryIdx)
    {
        IsActive = true;
        this.InventoryIdx = inventoryIdx;

        if(grid == null)
            grid = transform.GetChild(0).GetComponent<GridLayoutGroup>();

        //�Ŀ� �κ��丮 ������ ���� ����ũ�Ⱑ �ٸ��ٴ���, ���Ա׸��� �޶���Ѵٴ���, �κ��丮 �׸��� �޶���Ѵٴ��� �ϴ°��
        //���⼭ �߰��� ���� �����ϸ� �ɰ�....
        
        //grid.cellSize = Vector2(������ ���λ�����, ���λ�����);�� �ο��Ҽ�������
        //grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount; //���� �ִ밳�� ����
        //grid.constraintCount = 5; //�װ� �ټ����� �ϰ���

        Inventory inven = InvenManager.Instance.GetInven(inventoryIdx);
        GameObject tmpobj;
        if (SlotList.Count >= inven.InvenCount) //������ ���Ը���Ʈ�� ���� �����ִµ�, �ش� ������ invencount �̻���.
                                          //== �� ���ο� ������ ���� �ʿ䰡 ����..
        {
            //invencount��ŭ ���� �����Ұ� �ϰ�, �Ѵ°Ϳ� ���ؼ��� ��Ȱ��ȭ �ϸ��
            for (int i = 0; i < SlotList.Count; i++)
            {
                if (i < inven.InvenCount) //�� �κ������� �ش�Ǵ� ���Ը� �ѵ�
                {
                    SlotList[i].gameObject.SetActive(true);
                    SlotList[i].SetInit(inventoryIdx, i, inven.GetItemInfo(i));//���� �׸� ����.
                }
                else //�κ������� ��� �����̹Ƿ�, ���� ������ ����..
                {
                    SlotList[i].gameObject.SetActive(false);
                }
            }
        }
        else //������ �̸� ������ ������ ������, ���� �θ������ϴ� �κ� ĭ������ ����. => ���ڶ�� ��ŭ ���� ���� �߰��ؾ���...
        {
            int createNum = inven.InvenCount - SlotList.Count;            
            for (int i = 0; i < createNum; i++)
            {
                tmpobj = Instantiate(UIManager.Instance.SlotPrefab, grid.transform);
                SlotList.Add(tmpobj.GetComponent<SlotUI>());
            }

            for (int i = 0; i < SlotList.Count; i++)
            {
                SlotList[i].gameObject.SetActive(true);
                SlotList[i].SetInit(inventoryIdx, i, inven.GetItemInfo(i));//���� �׸� ����.
            }
        }
    }

    public void SetSlotInfo(int inventoryidx, int slotnum, Item item)
    {
        if (this.InventoryIdx != inventoryidx)
        {
            return;
        }
        SlotList[slotnum].SetSlotItem(item);//���� �׸� ����.
    }
}
