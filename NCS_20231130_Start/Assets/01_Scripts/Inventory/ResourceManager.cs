using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    [Header("��� ������ ���")]
    public Sprite[] AllItemSprites; //��Ŵ� �����ϸ� ������ �׸������� ������ �������� ������Ұ�.

    //�Ŀ� ��ȹ�����͸� ������ ���� �ϰ�ʹٸ�... 
    //������ �Ǵ� �����͸� �̷��� �⺻ �迭���� ��Ƽ� ����ֱ�....
    //public Item[] AllItemInfos; //������ �⺻�� �����͵��� ���⼭ ������. //���ӽ������� ��ȹ�����͸� �ҷ��ͼ� ������ �ص־��Ұ�...

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
        bulletEffect = Instantiate(BulletEffectPrefab);
        bulletEffect.SetActive(false);
    }

    //�ʵ忡 ������ ����
    public void CreateFieldItem(Item item) //���߿� ItemPrefab�� �����ϸ鼭 Ȥ�� ������Ʈ Ǯ�� �����ִٸ�, �ش� ������Ʈ Ǯ�� ä����...
    { 
        //�ʵ忡 ������ ������ ������ �� �����ϰ� ���� ������ item���� ä��...
    }

    public Item CreateDataItem( int index = -1) 
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
