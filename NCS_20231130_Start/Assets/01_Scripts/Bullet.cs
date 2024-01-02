using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rigid;
    public float speed = 10; //�ڱ� �̵��ӵ�
    public float aliveTime = 5; //�Ѿ� ����ִ� �ð�
    public float damage = 0; //�� �Ѿ� ������ ������
    AllEnum.Type OwnerType = AllEnum.Type.End; //�Ʊ��� �� �Ѿ��� �Ʊ��� �°ų�, ���� �Ѿ��� ������ �¾����� �����ִ� ��Ȳ�� ���ϱ� ����
    Coroutine cor = null; //�ƹ��͵� ���������� �����ð��Ŀ� ����Ȱ��ȭ �ϱ� ����. 
    bool isCrash = false;        

    public void Init(Transform parentTr, float damage, AllEnum.Type type)
    {
        gameObject.SetActive(true); //��� �ۿ��� ���ִ°� �� ����.
        
        //����ź���� Ư�� �տ� �پ ���� �̵��ϴٰ�, Ư�� Ÿ�ֿ̹� �տ��� �������������ϴ� ���
        //��ӽ��Ѽ� �����ϴ� ���
        //transform.parent = parentTr;
        //transform.localPosition = Vector3.zero;
        //transform.localRotation = Quaternion.identity;
        
        //transform.parent = null; //������踦 ���� �������... //�տ��� ������������ �ϴ� �ڵ�.

        //�Ѿ��� �̷��Ը� �ᵵ ����ϰ�
        //�׳� �� ���� �޾ƿͼ� �����ϴ� ���
        transform.position = parentTr.position;
        transform.rotation = parentTr.rotation;

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
            rigid.velocity =  transform.forward * speed; //���� ȸ�������� ȸ���� �� ��� �� ���ػ� ���� ����
        
        //Vector3.forward///������~ 0,0,1 ������ ��ǥ...
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

    //void OnCollisionEnter(Collision collision) //Collision �浹 ����~~~�� ����. �浹�������� ����. 
    //{            
    //    ResourceManager.Instance.SetEffect(
    //        collision.GetContact(0).point,  //�ε�������
    //        Quaternion.LookRotation( -collision.GetContact(0).normal));   //collision.GetContact(0).normal �Ѿ� �������� ����.
    //    //�Ѿ��� �������� �ݴ밡 �Ǿ�� �������� ����� ��������...
    //}

    void OnTriggerEnter(Collider other) //�浹ü �� ���� ����. ���� �浹�� ��� �浹ü... 
    {        
        //�����ư� ����Ʈ ���
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitinfo))
        {
            ResourceManager.Instance.SetEffect(hitinfo.point, Quaternion.LookRotation(-hitinfo.normal));
        }                

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
