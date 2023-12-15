using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//�κ��丮 �ȿ� ������, 
//������ UI�� ���ѰŰ�
//���� ��� �����͵��� ������...
public class SlotUI : MonoBehaviour, IPointerClickHandler,
    IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    List<RaycastResult> rayResults = new List<RaycastResult>();

    public int InventoryIdx { get; private set; } //�ش罽���� ��� �κ��丮�� ���� ������....
    public int Index { get; private set; } //�̽����� ������ȣ. ���� 0��° ��������, 1��°�����̹��� �����ε� �˱�����...
    Image img;
    Text txt;

    public bool isExist { get; private set; }
    public void SetInit(int inventoryIdx, int index, Item item) //������ SlotUi Ȱ��ȭ ��ų�� �ѹ� �θ��� �Ű�. (�ε����� �ο��ϱ�����)
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

    public void SetSlotItem(Item item) //��÷� �� ���Կ� ������ ������ �����ϰ� �ʹٸ� �׳� �θ�...
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
    public void OnBeginDrag(PointerEventData eventData) //�巡�׽���
    {
        //���� ���� �ƹ� �����̾��ٸ� �ǹ̾���..
        if (isExist == false)
        {
            return;
        }
        //�ϴ� �� ������ �����۱׸�������
        img.gameObject.SetActive(false);

        //�� ���콺�� ���� uimanager�� ������ �ִ� FollowDragItem�� ���콺�� ����ٴϰ� ����� ��.
        UIManager.Instance.SetFollowDragItem(true, img.sprite, txt.text);
    }

    public void OnDrag(PointerEventData eventData) //�巡����
    {
        if (isExist == false)
        {
            return;
        }
        //Debug.Log(eventData.position); //���� ���콺 Ŭ���� �������� ���콺�� ��ġ�� �޾ƿ�..
        UIManager.Instance.UpdateFollowDragItem(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData) //�巡�� ����
    {
        if (isExist==false)
        {
            return;
        }

        UIManager.Instance.SetFollowDragItem(false, null, "");

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

            //���� �� ��ũ��Ʈ�� �巡�׸� ������, ���� �����̰�, nextSlot�� �巡�װ� ������ ��� ��� ������.
            //���� �� ������ ������ �����ϸ� ��...

            InvenManager.Instance.SwapSlot(InventoryIdx, Index, nextSlot.InventoryIdx, nextSlot.Index);
            //�����ʹܿ� ������ �ٲ�޶�� ��û�� ���°�
            //�ش� �����ͳ������ �ٲ𶧸��� �ϴ��� UI�Ŵ������� ��Ź�ϴ� �ý���
            //�����Ͱ� �ٲ� ���� ���Գ��� �ٲ�׸���� Ui�Ŵ����� ���ؼ� �ٽ� �Ҹ��Ե�. 
            //����ȿ��� �ٽ� �׸��ٲٰ� �����ʿ䰡 ���ٴ� ���...
        }
        else //���� ������ �ƴ� ������� �����ߴٸ� ������ ������...
        {
            //�ʵ忡 ������ �ؾ��Ѵٸ�
            ResourceManager.Instance.CreateFieldItem( InvenManager.Instance.GetItemInfo(InventoryIdx, Index));

            //�� ���Կ� �ش�Ǵ� ģ���� �����޶�... ��û�ؾ��� 
            //�׷��� �ʿ��� ���� �κ��丮 ������ȣ + ���� �ѹ�. �ش� ���Գѹ��� ���� ������ ������, 

            //1�� ����UI�� �κ��丮 ��ȣ�� �ش�. �ؔf�� int �ϳ� �߰��Ǵ°Ŷ� �δ��� ��û ũ������
            //2�� ���� ������ ��ȣ������ ����, �ش� �κ��丮 �ѹ��� 
            InvenManager.Instance.SubItemSlot(InventoryIdx, Index);
            Debug.Log("����~");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isExist == false)
        {
            return;
        }
        //���콺�� �� �������� ���� ��� �Ҹ�
        //����â Ȱ��ȭ ��Ű�� �ش� ���������� �Ʒ��ٿ��� �ҷ���..        
        UIManager.Instance.SetExplainActive(true, transform.position);
        //InvenManager.Instance.GetItemInfo(InventoryIdx, Index)
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //���콺�� �� ���Թ��� ������ �Ҹ�.
        //����â ��Ȱ��ȭ....
        UIManager.Instance.SetExplainActive(false, transform.position);
    }

    public void OnPointerClick(PointerEventData eventData) //Ŭ��
    {
        Debug.Log("Ŭ��Ƚ�� : "+ eventData.clickCount); //����Ŭ���� �ϸ� Ŭ��Ƚ���� 1,2, �ϰ� �ι����...
        //����Ŭ�� ����� ���� �ǵ�
        //�׳� �ѹ� Ŭ�� ��ɵ� �ְ� �ʹٸ�
        //�ѹ�Ŭ�� ����� �����ð��Ŀ� �����ð����� �ι�° Ŭ���� ������ �ʴ´ٸ� �����ϵ��� �߰� ������ �ʿ���...

        Debug.Log("Ŭ���Ѱ� ����  : " + eventData.button);
    }
}
