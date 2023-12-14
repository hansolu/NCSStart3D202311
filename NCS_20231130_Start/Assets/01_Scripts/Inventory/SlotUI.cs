using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//�κ��丮 �ȿ� ������, 
//������ UI�� ���ѰŰ�
//���� ��� �����͵��� ������...
public class SlotUI : MonoBehaviour, /*IPointerClickHandler,*/
    IBeginDragHandler, IDragHandler, IEndDragHandler
{

    List<RaycastResult> rayResults = new List<RaycastResult>();
    
    //eventData.position 
    public void OnBeginDrag(PointerEventData eventData) //�巡�׽���
    {        
    }

    public void OnDrag(PointerEventData eventData) //�巡����
    {
        Debug.Log(eventData.position); //���� ���콺 Ŭ���� �������� ���콺�� ��ġ�� �޾ƿ�..
    }

    public void OnEndDrag(PointerEventData eventData) //�巡�� ����
    {
        rayResults.Clear();
        //���� �巡�׸� ������ �� ���� ��ũ��Ʈ ���� EndDrag�̱⋚����
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

        if (nextSlot !=null) //���� �巡�� ���� Ÿ ������ Ŭ������...
        {
            //������ �ٲ㳢��..
            //������ �ٲ㳢�°� �Ȱ��� SlotUI
            //���۽��԰� ������ ������ ���� �� �����ֱ⋚���� ����
        }
        else //���� ������ �ƴ� ������� �����ߴٸ� ������ ������...
        {

        }
    }

    //public void OnPointerClick(PointerEventData eventData) //Ŭ��
    //{        
    //}
}
