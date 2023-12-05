using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rigid;
    public float speed = 10; //자기 이동속도
    public float aliveTime = 5; //총알 살아있는 시간
    public float damage = 0; //이 총알 주인의 데미지
    AllEnum.Type OwnerType = AllEnum.Type.End; //아군이 쏜 총알이 아군에 맞거나, 적군 총알이 적에게 맞았을때 피해주는 상황을 피하기 위함
    Coroutine cor = null; //아무것도 못맞췄을때 일정시간후에 ㅇ비활성화 하기 위함. 
    bool isCrash = false;
    public void Init(Transform parentTr, float damage, AllEnum.Type type)
    {
        gameObject.SetActive(true); //사실 밖에서 해주는게 더 나음.

        transform.parent = parentTr;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        transform.parent = null; //상관관계를 해제 시켜줘야...

        if (rigid == null)
        {
            rigid = GetComponent<Rigidbody>();
        }

        this.damage = damage;
        OwnerType = type;        

        isCrash = false;
        if (cor !=null)
        {
            StopCoroutine(cor);
        }
        cor = StartCoroutine(TimeOverDie());                
    }
 
    void FixedUpdate()
    {
        if (isCrash == false)
            rigid.velocity = transform.forward * speed;
    }

    IEnumerator TimeOverDie()
    {
        yield return new WaitForSeconds(aliveTime);

        if (isCrash ==false)
        {
            GameManager.Instance.ReturnBullet(this);
        }
        cor = null;
    }

    void OnTriggerEnter(Collider other)
    {
        isCrash = true;
        if (cor !=null)
        {
            StopCoroutine(cor);
            cor = null;
        }

        if (other.gameObject.CompareTag(OwnerType.ToString()) == false)
        {
            IHit hit = other.GetComponent<IHit>();
            if (hit !=null)
            {
                hit.Hit(damage);
            }
        }

        GameManager.Instance.ReturnBullet(this);
    }
}
