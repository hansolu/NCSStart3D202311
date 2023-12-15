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
    List<AllEnum.UIKind> ActiveUIList = new List<AllEnum.UIKind>(); //==> Ȥ���� ���߿� �κ��丮�� �ΰ� ���������� ������� ����...
    
    public InventoryUI invenUI;    

    [SerializeField]
    FollowDragItem followItem; //���콺�� ����ٴ� ��ü
    [SerializeField]
    ShowItemInfo explain; //������ ����â.
    //����â. 

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
        if (ActiveUIList.Contains(kind)) //���������Ƿ� ��
        {
            invenUI.IsActive = false;            
            ActiveUIList.Remove(kind);
            UIObjects[kind].SetActive(false);
        }
        else //�����־����Ƿ� Ŵ
        {            
            ActiveUIList.Add(kind);
            UIObjects[kind].SetActive(true);
            invenUI.SetInit(inventoryIdx);
        }
    }

    //�� �κ��丮�� �����ִٸ�~ �� ���� ������ �ٽ� �׷��޶�
    public void SetInvenChange(int inventoryidx, int slotNum, Item item)
    {
        if (invenUI.IsActive == false) //�����ư� �κ��丮 ui�� �ƿ� ���������� �ݿ��� �ʿ䰡 ����
        {
            return;
        }
        else 
        {
            invenUI.SetSlotInfo(inventoryidx, slotNum, item);
        }
        //�� �κ��丮�� ���������� ���������� �갡 �˰�����...
    }

    public void SetFollowDragItem(bool isactive, Sprite spr, string count)
    {
        followItem.gameObject.SetActive(isactive);

        if (isactive)
        {
            followItem.transform.SetAsLastSibling();
            //���� ���� �θ��� ���̾��Ű���� ������ �ڽ����� �ڸ��� �ٲ���. 
            //���̾��Ű�� ��Ʈ���̱⶧����  �̰� ������Ʈ �������� �θ��� �� ��������           

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
