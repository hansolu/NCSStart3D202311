using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public bool IsActive = false; //나라는 인벤토리가 지금 활성화 되어있는지 여부.
    public int InventoryIdx { get; private set; } = 0;//내가 지금 그리고 있는 현재의 인벤토리 번호
    List<SlotUI> SlotList = new List<SlotUI>();
    GridLayoutGroup grid = null;    
    
    public void SetInit(int inventoryIdx)
    {
        IsActive = true;
        this.InventoryIdx = inventoryIdx;

        if(grid == null)
            grid = transform.GetChild(0).GetComponent<GridLayoutGroup>();

        //후에 인벤토리 종류에 따라서 슬롯크기가 다르다던지, 슬롯그림이 달라야한다던지, 인벤토리 그림이 달라야한다던지 하는경우
        //여기서 추가로 뭔가 수정하면 될것....
        
        //grid.cellSize = Vector2(슬롯의 가로사이즈, 세로사이즈);를 부여할수도있음
        //grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount; //가로 최대개수 지정
        //grid.constraintCount = 5; //그걸 다섯개로 하겠음

        Inventory inven = InvenManager.Instance.GetInven(inventoryIdx);
        GameObject tmpobj;
        if (SlotList.Count >= inven.InvenCount) //기존에 슬롯리스트가 뭔가 남아있는데, 해당 개수가 invencount 이상임.
                                          //== 즉 새로운 슬롯을 만들 필요가 없음..
        {
            //invencount만큼 개수 세팅할거 하고, 넘는것에 대해서는 비활성화 하면됨
            for (int i = 0; i < SlotList.Count; i++)
            {
                if (i < inven.InvenCount) //내 인벤개수에 해당되는 슬롯만 켜둠
                {
                    SlotList[i].gameObject.SetActive(true);
                    SlotList[i].SetInit(inventoryIdx, i, inven.GetItemInfo(i));//슬롯 그림 세팅.
                }
                else //인벤개수를 벗어난 슬롯이므로, 내겐 쓸모없어서 꺼둠..
                {
                    SlotList[i].gameObject.SetActive(false);
                }
            }
        }
        else //기존에 미리 만들어둔 슬롯의 개수가, 현재 부르려고하는 인벤 칸수보다 적음. => 모자라는 만큼 새로 만들어서 추가해야함...
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
                SlotList[i].SetInit(inventoryIdx, i, inven.GetItemInfo(i));//슬롯 그림 세팅.
            }
        }
    }

    public void SetSlotInfo(int inventoryidx, int slotnum, Item item)
    {
        if (this.InventoryIdx != inventoryidx)
        {
            return;
        }
        SlotList[slotnum].SetSlotItem(item);//슬롯 그림 세팅.
    }
}
