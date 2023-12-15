using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    GraphicRaycaster graphicRay;
    public GraphicRaycaster GraphicRay=> graphicRay;

    Dictionary<AllEnum.UIKind, GameObject> UIObjects = new Dictionary<AllEnum.UIKind, GameObject>();
    //Dictionary<AllEnum.UIKind, bool> ActiveUiDic = new Dictionary<AllEnum.UIKind, bool>();
    List<AllEnum.UIKind> ActiveUIList = new List<AllEnum.UIKind>(); //==> 혹여나 나중에 인벤토리가 두개 켜져있으면 어떤앤지가 문제...
    
    public InventoryUI invenUI;    

    [SerializeField]
    FollowDragItem followItem; //마우스를 따라다닐 객체
    [SerializeField]
    ShowItemInfo explain; //아이템 설명창.
    //설명창. 

    public GameObject SlotPrefab;
        
    void Start()
    {
        graphicRay = GetComponent<GraphicRaycaster>();
        UIObjects.Add(AllEnum.UIKind.Inventory, invenUI.gameObject);
        followItem.gameObject.SetActive(false);
        explain.gameObject.SetActive(false);
        //invenUI = transform.GetChild(0).GetComponent<InventoryUI>();
    }

    public void ActiveUI(AllEnum.UIKind kind, int inventoryIdx)
    {
        if (ActiveUIList.Contains(kind)) //켜져있으므로 끔
        {
            invenUI.IsActive = false;            
            ActiveUIList.Remove(kind);
            UIObjects[kind].SetActive(false);
        }
        else //꺼져있었으므로 킴
        {            
            ActiveUIList.Add(kind);
            UIObjects[kind].SetActive(true);
            invenUI.SetInit(inventoryIdx);
        }
    }

    //내 인벤토리가 켜져있다면~ 그 슬롯 정보를 다시 그려달라
    public void SetInvenChange(int inventoryidx, int slotNum, Item item)
    {
        if (invenUI.IsActive == false) //뭐가됐건 인벤토리 ui가 아예 꺼져있으면 반영할 필요가 없음
        {
            return;
        }
        else 
        {
            invenUI.SetSlotInfo(inventoryidx, slotNum, item);
        }
        //내 인벤토리가 켜졌ㅇ는지 꺼졌는지는 얘가 알고있음...
    }

    public void SetFollowDragItem(bool isactive, Sprite spr, string count)
    {
        followItem.gameObject.SetActive(isactive);

        if (isactive)
        {
            followItem.transform.SetAsLastSibling();
            //내가 속한 부모의 하이어라키상의 마지막 자식으로 자리를 바꿔줌. 
            //하이어라키의 컨트롤이기때문에  이걸 업데이트 같은데서 부르면 좀 좋지않음           

            followItem.SetSprite(spr);
            followItem.SetText(count);
        }
    }

    public void UpdateFollowDragItem(Vector2 pos)
    {        
        followItem.transform.position = pos;
    }

    public void SetExplainActive(bool isactive, Vector2 vec)
    {
        explain.gameObject.SetActive(isactive);
        explain.transform.position = vec;
    }    
}
