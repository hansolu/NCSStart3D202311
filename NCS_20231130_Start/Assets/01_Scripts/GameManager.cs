using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject bulletPrefab; //����������

    public Queue<Bullet> bulletPool = new Queue<Bullet>();
    
    //�ӽú���
    GameObject tmpobj = null;    

    void Start()
    {        
        for (int i = 0; i < 20; i++)
        {
            tmpobj = Instantiate(bulletPrefab, transform);
            bulletPool.Enqueue(tmpobj.GetComponent<Bullet>());
            tmpobj.SetActive(false);
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
}
