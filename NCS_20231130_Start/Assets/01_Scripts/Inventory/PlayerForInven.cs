using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerForInven : MonoBehaviour //�÷��̾�. ������ �÷��̾� ��ũ��Ʈ�� �־ �̷��� ������...
{
    Vector3 vec = Vector3.zero;
    float HP = 0; //���� ��
    float MP = 0;
    float SP = 0;

    float speed = 1;


    int itemLayer=0;
    public int InventoryIdx = 0; //���� �κ��丮 ��ȣ...
    //��������� �κ��Ŵ��� ���� �ϱ⋚����
    //�������� ���������� �Ϸ��� �ϰ����� ���� ��������� �� ������ ������ȣ�� ����־��
    //������ InventoryIdx �̰� ģ�� ���Ұ�~ 

    //bool �� ���� �� ������ �������� ���������� üũ�ؼ� ���� ������ ��...

    void Start()
    {
        itemLayer = 1 << LayerMask.NameToLayer("Item");
        InventoryIdx = InvenManager.Instance.CreateInven(AllEnum.InventoryKind.Inventory_Player, 10);
    }
        
    void Update()
    {
        vec.x = Input.GetAxisRaw("Horizontal"); 
        vec.z = Input.GetAxisRaw("Vertical");

        transform.Translate(vec.normalized * Time.deltaTime * 10);

        if (Input.GetKeyDown(KeyCode.I))
        {
            //###########UI�Ŵ������� ���� ����޶�� ��û�ϱ�
            UIManager.Instance.ActiveUI(AllEnum.UIKind.Inventory, InventoryIdx);
        }

        if (Input.GetKeyDown(KeyCode.Space)) //�ݱ�+ ġƮŰ
        {
            PickUp();
            //InvenManager.Instance.AddItemSimple(InventoryIdx, ResourceManager.Instance.CreateDataItem());            
        }                
    }

    public void PickUp()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, 10f, itemLayer);

        for (int i = 0; i < cols.Length; i++)
        {
            ItemObject itemobj = cols[i].GetComponent<ItemObject>();
            if (itemobj !=null)
            {
                Debug.Log(itemobj.Index+ " ������ �ֿ�");
                PickUp(itemobj.Index);

                //������Ʈ Ǯ�� ������������. �ٵ� ���� ������Ʈ Ǯ�� ������ Destroy��Ŵ.
                Destroy(itemobj.gameObject);
            }
        }                
    }
    public void PickUp(int index)
    {
        InvenManager.Instance.AddItemSimple(InventoryIdx, ResourceManager.Instance.CreateDataItem(index));
    }


    public void SetPlayerStat(AllEnum.StatType stat, float val)
    {
        switch (stat)
        {
            case AllEnum.StatType.HP:
                HP += val;
                break;
            case AllEnum.StatType.MP:
                break;
            case AllEnum.StatType.SP:
                break;
            case AllEnum.StatType.Speed:
                speed += val;
                break;            
            default:
                break;
        }
    }
}
