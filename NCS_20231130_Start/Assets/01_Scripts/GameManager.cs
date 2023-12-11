using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject bulletPrefab; //원본프리팹

    public Queue<Bullet> bulletPool = new Queue<Bullet>();    
    
    Transform[] PatrolParentTr; //정찰패턴부모   
    Dictionary<int, Transform[]> AllPatrolTrDic =  new Dictionary<int, Transform[]>();
    //정찰패턴부모와 딕셔너리로 묶은 정찰패턴

    //임시변수
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
    /// 다음 이동할 위치 주는 함수
    /// </summary>
    /// <param name="parentNum">정찰 패턴 넘버</param>
    /// <param name="nowPatternNum">해당 위치를 인덱스화시킨 넘버</param>
    /// <param name="Next">바로 다음트랜스폼을 가질것인지, 혹은 랜덤으로 할것인지</param>
    /// <returns></returns>
    public Vector3 GetNextPatrol(int parentNum, ref int nowPatternNum, bool Next)
    {
        //부모번호에 대한 예외처리도 하면 더 좋을것..

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
            //같은 자리 또 되는게 싫다면 따로 처리 추가하기

            nowPatternNum = Random.Range(0, AllPatrolTrDic[parentNum].Length);
        }

        return AllPatrolTrDic[parentNum][nowPatternNum].position;
    }
}
