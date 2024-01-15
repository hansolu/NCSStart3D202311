using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerForInven : MonoBehaviour //플레이어. 기존에 플레이어 스크립트가 있어서 이렇게 지었음...
{
    Vector3 vec = Vector3.zero;
    float HP = 0; //나의 피
    float MP = 0;
    float SP = 0;

    float speed = 1;


    int itemLayer=0;
    public int InventoryIdx = 0; //나의 인벤토리 번호...
    //가방관리를 인벤매니저 에서 하기떄문에
    //내가방좀 접근좀하자 하려면 니가방이 뭔데 라고했을떄 내 가방의 고유번호를 들고있어야
    //내가방 InventoryIdx 이거 친구 뭘할게~ 

    //bool 로 내가 내 가방을 연상탠지 닫은상탠지 체크해서 직접 보내도 됨...

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
            //###########UI매니저한테 가방 열어달라고 요청하기
            UIManager.Instance.ActiveUI(AllEnum.UIKind.Inventory, InventoryIdx);
        }

        if (Input.GetKeyDown(KeyCode.Space)) //줍기+ 치트키
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
                Debug.Log(itemobj.Index+ " 아이템 주움");
                PickUp(itemobj.Index);

                //오브젝트 풀로 돌려보내야함. 근데 지금 오브젝트 풀이 없으니 Destroy시킴.
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
