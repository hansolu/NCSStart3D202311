using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    [Header("모든 아이템 목록")]
    public Sprite[] AllItemSprites; //요거는 웬만하면 아이템 그림개수와 아이템 종류수를 맞춰야할것.

    //후에 기획데이터를 가지고 뭔가 하고싶다면... 
    //원형이 되는 데이터를 이렇게 기본 배열에다 담아서 들고있기....
    //public Item[] AllItemInfos; //아이템 기본형 데이터들을 여기서 관리함. //게임시작전에 기획데이터를 불러와서 세팅을 해둬야할것...

    [SerializeField]
    int[] MaxCount;// = new int[5]{ 10,20,30,40,50 };

    //이펙트 프리팹 원본
    //public GameObject[] effects;
    //이펙트 오브젝트 풀...
    //public Dictionary< Enum 이펙트타입,List<GameObject>> EffectDic = new Dictionary..;
    //보통 위처럼 오브젝트 풀형식으로 만들어야함
    public GameObject BulletEffectPrefab;
    GameObject bulletEffect;


    protected override void Awake()
    {
        base.Awake();

        AllItemSprites = Resources.LoadAll<Sprite>("Ingame");
        MaxCount = new int[AllItemSprites.Length];
        for (int i = 0; i < AllItemSprites.Length; i++)
        {
            MaxCount[i] = (i + 1) * 5;
        }
        bulletEffect = Instantiate(BulletEffectPrefab);
        bulletEffect.SetActive(false);
    }

    //필드에 아이템 생성
    public void CreateFieldItem(Item item) //나중에 ItemPrefab을 생성하면서 혹은 오브젝트 풀이 남아있다면, 해당 오브젝트 풀을 채워서...
    { 
        //필드에 존재할 아이템 프리팹 뭐 생성하고 걔의 내용을 item으로 채움...
    }

    public Item CreateDataItem( int index = -1) 
    {
        if (index >= 0) //이러면 지정 아이템
        {
            //return new Item(AllItemInfos[index]);
            return new Item(index, Random.Range(1,4), MaxCount[index] );
        }
        else //이거는 랜덤아이템
        {
            //return new Item(AllItemInfos[Random.Range(0, AllItemInfos.Length)]);
            index = Random.Range(0, /*AllItemInfos*/AllItemSprites.Length);            
            return new Item(index, Random.Range(1, 4), MaxCount[index]);
        }
    }
    //public Item CreateDataItem(타입) //만약 내가 타입도 가지고 있었다면.. 해당 타입에 대해서.. 랜덤 아이템을 만들어주기 가능..
    //{
    //    해당 타입중에 랜덤으로 아이템 반환...
    //}

    //후에 이펙트 오브젝트 풀을 만들고 관리하게된다면
    //public void SetEffect(Enum이펙트타입, Vector3 position, Quaternion quat)
    //{    
    //    EffectDic[이펙트타입].transform.position = position;
    //    EffectDic[이펙트타입].transform.rotation = quat;
    //    EffectDic[이펙트타입].SetActive(true);
    //}

    public void SetEffect( Vector3 position, Quaternion quat)
    {
        bulletEffect.transform.position = position;
        bulletEffect.transform.rotation = quat;
        bulletEffect.SetActive(true);
    }
}
