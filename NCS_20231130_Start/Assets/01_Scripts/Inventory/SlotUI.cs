using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//인벤토리 안에 있지만, 
//슬롯은 UI를 위한거고
//실제 모든 데이터들은 아이템...
public class SlotUI : MonoBehaviour, /*IPointerClickHandler,*/
    IBeginDragHandler, IDragHandler, IEndDragHandler
{

    List<RaycastResult> rayResults = new List<RaycastResult>();
    
    //eventData.position 
    public void OnBeginDrag(PointerEventData eventData) //드래그시작
    {        
    }

    public void OnDrag(PointerEventData eventData) //드래그중
    {
        Debug.Log(eventData.position); //내가 마우스 클릭을 뗄때까지 마우스의 위치를 받아옴..
    }

    public void OnEndDrag(PointerEventData eventData) //드래그 끝남
    {
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
        }
        else //뭔가 슬롯이 아닌 빈공간을 선택했다면 아이템 버리기...
        {

        }
    }

    //public void OnPointerClick(PointerEventData eventData) //클릭
    //{        
    //}
}
