using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject bulletPrefab; //����������

    public Queue<Bullet> bulletPool = new Queue<Bullet>();    
    
    Transform[] PatrolParentTr; //�������Ϻθ�   
    Dictionary<int, Transform[]> AllPatrolTrDic =  new Dictionary<int, Transform[]>();
    //�������Ϻθ�� ��ųʸ��� ���� ��������

    //�ӽú���
    GameObject tmpobj = null;
    public Player player;

    void Start()
    {        
        for (int i = 0; i < 20; i++)
        {
            tmpobj = Instantiate(bulletPrefab, transform);
            bulletPool.Enqueue(tmpobj.GetComponent<Bullet>());
            tmpobj.SetActive(false);
        }

        PatrolParentTr = new Transform[transform.childCount];                
        for (int i = 0; i < PatrolParentTr.Length; i++)
        {
            PatrolParentTr[i] = transform.GetChild(i);
            AllPatrolTrDic.Add(i, new Transform[transform.GetChild(i).childCount]);
            for (int j = 0; j < transform.GetChild(i).childCount; j++)
            {
                AllPatrolTrDic[i][j] = PatrolParentTr[i].GetChild(j);
            }            
        }
    }

    public Bullet GetBullet()
    {        
        if (bulletPool.Count > 0 )
        {            
            return bulletPool.Dequeue();
        }
        else
        {
            return Instantiate(bulletPrefab, transform).GetComponent<Bullet>();
        }
    }

    public void ReturnBullet(Bullet bullet)
    {
        bulletPool.Enqueue(bullet);
        bullet.transform.SetParent(this.transform);
        bullet.gameObject.SetActive(false);
    }
    
    /// <summary>
    /// ���� �̵��� ��ġ �ִ� �Լ�
    /// </summary>
    /// <param name="parentNum">���� ���� �ѹ�</param>
    /// <param name="nowPatternNum">�ش� ��ġ�� �ε���ȭ��Ų �ѹ�</param>
    /// <param name="Next">�ٷ� ����Ʈ�������� ����������, Ȥ�� �������� �Ұ�����</param>
    /// <returns></returns>
    public Vector3 GetNextPatrol(int parentNum, ref int nowPatternNum, bool Next)
    {
        //�θ��ȣ�� ���� ����ó���� �ϸ� �� ������..

        if (Next)
        {
            nowPatternNum++;
            if (nowPatternNum>= AllPatrolTrDic[parentNum].Length)
            {
                nowPatternNum = 0;
            }
        }
        else
        {
            //���� �ڸ� �� �Ǵ°� �ȴٸ� ���� ó�� �߰��ϱ�

            nowPatternNum = Random.Range(0, AllPatrolTrDic[parentNum].Length);
        }

        return AllPatrolTrDic[parentNum][nowPatternNum].position;
    }
}
