using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerForInven : MonoBehaviour //�÷��̾�. ������ �÷��̾� ��ũ��Ʈ�� �־ �̷��� ������...
{
    public int InventoryIdx = 0; //���� �κ��丮 ��ȣ...
    //��������� �κ��Ŵ��� ���� �ϱ⋚����
    //�������� ���������� �Ϸ��� �ϰ����� ���� ��������� �� ������ ������ȣ�� ����־��
    //������ InventoryIdx �̰� ģ�� ���Ұ�~ 

    //bool �� ���� �� ������ �������� ���������� üũ�ؼ� ���� ������ ��...

    void Start()
    {
        InventoryIdx = InvenManager.Instance.CreateInven(AllEnum.InventoryKind.Inventory_Player, 10);
    }
        
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            //###########UI�Ŵ������� ���� ����޶�� ��û�ϱ�
            UIManager.Instance.ActiveUI(AllEnum.UIKind.Inventory, InventoryIdx);
        }

        if (Input.GetKeyDown(KeyCode.Space)) //�ݱ�+ ġƮŰ
        {
            InvenManager.Instance.AddItemSimple(InventoryIdx, ResourceManager.Instance.CreateDataItem());
        }
    }
}
