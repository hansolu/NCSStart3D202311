using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerForInven : MonoBehaviour //플레이어. 기존에 플레이어 스크립트가 있어서 이렇게 지었음...
{
    public int InventoryIdx = 0; //나의 인벤토리 번호...
    //가방관리를 인벤매니저 에서 하기떄문에
    //내가방좀 접근좀하자 하려면 니가방이 뭔데 라고했을떄 내 가방의 고유번호를 들고있어야
    //내가방 InventoryIdx 이거 친구 뭘할게~ 

    //bool 로 내가 내 가방을 연상탠지 닫은상탠지 체크해서 직접 보내도 됨...

    void Start()
    {
        InventoryIdx = InvenManager.Instance.CreateInven(AllEnum.InventoryKind.Inventory_Player, 10);
    }
        
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            //###########UI매니저한테 가방 열어달라고 요청하기
            UIManager.Instance.ActiveUI(AllEnum.UIKind.Inventory, InventoryIdx);
        }

        if (Input.GetKeyDown(KeyCode.Space)) //줍기+ 치트키
        {
            InvenManager.Instance.AddItemSimple(InventoryIdx, ResourceManager.Instance.CreateDataItem());
        }
    }
}
