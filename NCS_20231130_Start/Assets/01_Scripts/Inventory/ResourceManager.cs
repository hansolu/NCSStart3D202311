using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class ResourceManager : Singleton<ResourceManager>
{
    [Header("��� ������ ���")]
    public Sprite[] AllItemSprites; //��Ŵ� �����ϸ� ������ �׸������� ������ �������� ������Ұ�.

    //�Ŀ� ��ȹ�����͸� ������ ���� �ϰ�ʹٸ�... 
    //������ �Ǵ� �����͸� �̷��� �⺻ �迭���� ��Ƽ� ����ֱ�....

    //������ �⺻ ���� ������.
    public ItemInfo[] AllItemInfos; //������ �⺻�� �����͵��� ���⼭ ������. //���ӽ������� ��ȹ�����͸� �ҷ��ͼ� ������ �ص־��Ұ�...

    //public ItemObject[] 
    public GameObject ItemPrefab; //�ۿ� �����ϴ� ������ ������Ʈ.


    [SerializeField]
    int[] MaxCount;// = new int[5]{ 10,20,30,40,50 };

    //����Ʈ ������ ����
    //public GameObject[] effects;
    //����Ʈ ������Ʈ Ǯ...
    //public Dictionary< Enum ����ƮŸ��,List<GameObject>> EffectDic = new Dictionary..;
    //���� ��ó�� ������Ʈ Ǯ�������� ��������
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
        if (BulletEffectPrefab!=null)
        {
            bulletEffect = Instantiate(BulletEffectPrefab);
            bulletEffect.SetActive(false);
        }

        //AllItemInfos = new Item[xml.GetElementsByTagName("Item").count];
        LoadItemInfoFromXML();

        StartCoroutine(CreateItemCor()); //������Ʈ Ǯ�������� �ٲ���ϰ�, �ش� �ڷ�ƾ�� ������ ���߰ų� �׸��ΰų�, �ٸ������� �Ѿ����, ���ߵ��� ������ �� �ְ�
        //�ڷ�ƾ ������ �־ �����ϴ°��� ����.
    }

    void LoadItemInfoFromXML()
    {
        string filepath = System.IO.Path.Combine(Application.streamingAssetsPath, "ItemInfoDataTable.xml");

        XmlDocument xml = new XmlDocument();
        xml.Load(filepath);

        AllItemInfos = new ItemInfo[xml.GetElementsByTagName("Item").Count];
        
        int i = 0;
        foreach (XmlNode item in xml.GetElementsByTagName("Item"))
        {
            AllItemInfos[i] = new ItemInfo(
              item["Index"].InnerText,
              item["Type"].InnerText,
              item["Name"].InnerText,
              item["Value"].InnerText
              );
            i++;
        }
    }


    IEnumerator CreateItemCor()
    {
        while (true)
        {
            CreateFieldItem();
            yield return new WaitForSeconds(3f);

        }
    }
    //�ʵ忡 ������ ����
    //public void CreateFieldItem(Item item) //���߿� ItemPrefab�� �����ϸ鼭 Ȥ�� ������Ʈ Ǯ�� �����ִٸ�, �ش� ������Ʈ Ǯ�� ä����...
    //{ 
    //    //�ʵ忡 ������ ������ ������ �� �����ϰ� ���� ������ item���� ä��...
    //}
    public void CreateFieldItem(int index = -1) //�ʵ��. �ʵ忡 �����Ǵ� ������Ʈ���� ������.
    {
        //if (index >= 0) //�̷��� ���� ������
        //{

        //}
        //else //�̰Ŵ� ����������
        //{
        //}
        index = Random.Range(0, AllItemInfos.Length);
        ItemObject objsc = Instantiate(ItemPrefab, new Vector3(Random.Range(-10,10),0, Random.Range(-10, 10)), Quaternion.identity).GetComponent<ItemObject>();
        objsc.SetInfo(index);
    }

    public Item CreateDataItem_Cheat( int index = -1) //ġƮ
    {
        if (index >= 0) //�̷��� ���� ������
        {
            //return new Item(AllItemInfos[index]);
            return new Item(index, Random.Range(1,4), MaxCount[index] );
        }
        else //�̰Ŵ� ����������
        {
            //return new Item(AllItemInfos[Random.Range(0, AllItemInfos.Length)]);
            index = Random.Range(0, /*AllItemInfos*/AllItemSprites.Length);            
            return new Item(index, Random.Range(1, 4), MaxCount[index]);
        }
    }
    public Item CreateDataItem(int index) //�ش� �ε����� �ش�Ǵ� �������� �������.
    {
        Item createItem;

        switch (AllItemInfos[index].type)
        {
            case AllEnum.ItemType.Armor:
                createItem = new Item_Armor(index, 1, 10);
                break;
            case AllEnum.ItemType.Food:
            case AllEnum.ItemType.Potion:
                createItem = new Item_Potion(index, 1, 10);
                break;
            case AllEnum.ItemType.Sword:
            case AllEnum.ItemType.Weapon:
                createItem = new Item_Weapon(index, 1, 10);
                break;
            default:
                createItem = null;
                break;
        }

        return createItem;
    }

    //public Item CreateDataItem(Ÿ��) //���� ���� Ÿ�Ե� ������ �־��ٸ�.. �ش� Ÿ�Կ� ���ؼ�.. ���� �������� ������ֱ� ����..
    //{
    //    �ش� Ÿ���߿� �������� ������ ��ȯ...
    //}

    //�Ŀ� ����Ʈ ������Ʈ Ǯ�� ����� �����ϰԵȴٸ�
    //public void SetEffect(Enum����ƮŸ��, Vector3 position, Quaternion quat)
    //{    
    //    EffectDic[����ƮŸ��].transform.position = position;
    //    EffectDic[����ƮŸ��].transform.rotation = quat;
    //    EffectDic[����ƮŸ��].SetActive(true);
    //}

    public void SetEffect( Vector3 position, Quaternion quat)
    {
        bulletEffect.transform.position = position;
        bulletEffect.transform.rotation = quat;
        bulletEffect.SetActive(true);
    }
}
