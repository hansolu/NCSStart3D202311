using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//인벤토리 안에 있지만, 
//슬롯은 UI를 위한거고
//실제 모든 데이터들은 아이템...
public class SlotUI : MonoBehaviour, IPointerClickHandler,
    IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    List<RaycastResult> rayResults = new List<RaycastResult>();

    public int InventoryIdx { get; private set; } //해당슬롯이 어느 인벤토리에 속한 것인지....
    public int Index { get; private set; } //이슬롯의 고유번호. 내가 0번째 슬롯인지, 1번째슬롯이뭔지 스스로도 알기위함...
    Image img;
    Text txt;

    public bool isExist { get; private set; }
    public void SetInit(int inventoryIdx, int index, Item item) //정말로 SlotUi 활성화 시킬때 한번 부르는 거고. (인덱스도 부여하기위함)
    {
        this.InventoryIdx = inventoryIdx;
        Index = index;

        if (img == null || txt == null)
        {
            img = transform.GetChild(0).GetComponent<Image>();
            txt = img.transform.GetChild(0).GetComponent<Text>();
        }

        SetSlotItem(item);      
    }

    public void SetSlotItem(Item item) //상시로 이 슬롯에 아이템 정보를 세팅하고 싶다면 그냥 부름...
    {
        if (item.Count == 0)
        {
            isExist = false;
            img.gameObject.SetActive(false);
        }
        else
        {
            isExist = true;
            img.gameObject.SetActive(true);
            img.sprite = ResourceManager.Instance.AllItemSprites[item.Index];
            txt.text = item.Count.ToString();
        }
    }

    //eventData.position 
    public void OnBeginDrag(PointerEventData eventData) //드래그시작
    {
        //만약 내가 아무 내용이없다면 의미없음..
        if (isExist == false)
        {
            return;
        }
        //일단 이 슬롯의 아이템그림을끄고
        img.gameObject.SetActive(false);

        //내 마우스를 따라서 uimanager가 가지고 있는 FollowDragItem을 마우스를 따라다니게 만들면 됨.
        UIManager.Instance.SetFollowDragItem(true, img.sprite, txt.text);
    }

    public void OnDrag(PointerEventData eventData) //드래그중
    {
        if (isExist == false)
        {
            return;
        }
        //Debug.Log(eventData.position); //내가 마우스 클릭을 뗄때까지 마우스의 위치를 받아옴..
        UIManager.Instance.UpdateFollowDragItem(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData) //드래그 끝남
    {
        if (isExist==false)
        {
            return;
        }

        UIManager.Instance.SetFollowDragItem(false, null, "");

        rayResults.Clear();
        //내가 드래그를 시작한 그 슬롯 스크립트 안의 EndDrag이기떄문에
        UIManager.Instance.GraphicRay.Raycast(eventData, rayResults);
        SlotUI nextSlot = null;
        for (int i = 0; i < rayResults.Count; i++)
        {
           nextSlot = rayResults[i].gameObject.GetComponent<SlotUI>();
            if (nextSlot !=null)
            {
                break;
            }
        }

        if (nextSlot !=null) //나의 드래그 끝이 타 슬롯을 클릭했음...
        {
            //아이템 바꿔끼기..
            //아이템 바꿔끼는건 똑같이 SlotUI
            //시작슬롯과 끝슬롯 정보를 현재 다 갖고있기떄문에 가능

            //지금 이 스크립트는 드래그를 시작한, 시작 슬롯이고, nextSlot이 드래그가 도착한 상대 대상 슬롯임.
            //뭔가 두 슬롯의 내용을 스왑하면 됨...

            InvenManager.Instance.SwapSlot(InventoryIdx, Index, nextSlot.InventoryIdx, nextSlot.Index);
            //데이터단에 데이터 바꿔달라고 요청을 보냈고
            //해당 데이터내용들이 바뀔때마다 일단은 UI매니저에게 부탁하는 시스템
            //데이터가 바뀜에 따라 슬롯내용 바꿔그리기는 Ui매니저를 통해서 다시 불리게됨. 
            //여기안에서 다시 그림바꾸고 뭘할필요가 없다는 얘기...
        }
        else //뭔가 슬롯이 아닌 빈공간을 선택했다면 아이템 버리기...
        {
            //필드에 생성을 해야한다면
            ResourceManager.Instance.CreateFieldItem( InvenManager.Instance.GetItemInfo(InventoryIdx, Index));

            //이 슬롯에 해당되는 친구를 버려달라... 요청해야함 
            //그러면 필요한 것이 인벤토리 고유번호 + 슬롯 넘버. 해당 슬롯넘버는 내가 가지고 있지만, 

            //1번 슬롯UI에 인벤토리 번호도 준다. 해봣자 int 하나 추가되는거라서 부담이 엄청 크진않음
            //2번 좀더 데이터 보호수준을 위해, 해당 인벤토리 넘버는 
            InvenManager.Instance.SubItemSlot(InventoryIdx, Index);
            Debug.Log("버림~");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isExist == false)
        {
            return;
        }
        //마우스가 이 슬롯위에 오면 요게 불림
        //정보창 활성화 시키고 해당 정보내용을 아래줄에서 불러옴..        
        UIManager.Instance.SetExplainActive(true, transform.position);
        //InvenManager.Instance.GetItemInfo(InventoryIdx, Index)
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //마우스가 이 슬롯밖을 나가면 불림.
        //정보창 비활성화....
        UIManager.Instance.SetExplainActive(false, transform.position);
    }

    public void OnPointerClick(PointerEventData eventData) //클릭
    {
        Debug.Log("클릭횟수 : "+ eventData.clickCount); //더블클릭을 하면 클릭횟수가 1,2, 하고 두번뜬다...
        //더블클릭 기능을 넣을 건데
        //그냥 한번 클릭 기능도 넣고 싶다면
        //한번클릭 기능을 일정시간후에 일정시간동안 두번째 클릭이 들어오지 않는다면 실행하도록 추가 조건이 필요함...

        Debug.Log("클릭한게 뭔지  : " + eventData.button);
    }
}
