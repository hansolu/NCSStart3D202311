using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float x = 0;
    Vector3 vec = Vector3.zero;
    Animator anim;
    float speed = 0;
    [SerializeField]
    float walkSpeed = 2.5f;
    [SerializeField]
    float runSpeed = 5f;

    Rigidbody rigid;

    public Transform handTr; //�տ� ���� ����ֱ� ����
    public Transform holsterTr; //Ȧ���Ϳ� ���� �־�α� ����
    public Transform PistolTr; //�� ��ü�� Ʈ������
    public Transform ShootPosTr;

    Vector3 jumpVec = Vector3.zero;
    Vector3 jumpVecOrg = new Vector3(0,10,0); //

    bool IsDraw = false; //�� ���� ����? ó���� �Ǽ� �� �������ϱ� isDraw false���°�, ���� �ѹ� �����ϸ� true�� �ɰ���..

    [SerializeField]
    SkinnedMeshRenderer renderer; //���� ���� ���. 
    public Material[] bodies; //�� �ø���
        
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //x = Input.GetAxis("Horizontal");//ȸ��
        vec.x = Input.GetAxis("Horizontal");//ȸ������ ������� �ǵ���
        vec.z = Input.GetAxis("Vertical");

        //if (x ==0 && vec.z ==0) //������ �ֱ� //ȸ����
        if (vec.x == 0 && vec.z == 0) //������ �ֱ� 
        {            
            speed = 0;
        }
        else
        {            
            if (Input.GetKey(KeyCode.LeftShift)) //����Ʈ�� ������ ������ �ȱ� ���
            {
                //anim.SetFloat("Speed", 2.5f);                
                speed = walkSpeed;                
            }
            else //�װ� �ƴϸ� �ٱ� ���
            {
                //anim.SetFloat("Speed", 5);
                speed = runSpeed;                
            }
            //anim.SetFloat("PosX", /*vec.*/x);//ȸ����
            anim.SetFloat("PosX", vec.x);//����
            anim.SetFloat("PosZ", vec.z);            
        }

        anim.SetFloat("Speed", speed);
        //vec = vec.normalized; //ȸ���Ҷ��� x���� �����̵����� �Ƚ�..
        vec = vec.normalized; //����
        transform.Translate(vec * Time.deltaTime * speed);
        //transform.Rotate(0,x,0);//ȸ��

        if (Input.GetKeyDown(KeyCode.Space)) //�����̽��� �� ������ ������ �׶�
        {
            anim.SetBool("IsJump",true);
            //rigid.AddForce(Vector3.up * 10 , ForceMode.Impulse); //���� �ѹ� ��! ����  
            rigid.useGravity = false; //�߷»���� ���ϰ���. 
            jumpVec = jumpVecOrg;//jumpVec == (0,10,0)
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            jumpVec.y = Mathf.Clamp(jumpVec.y - Time.deltaTime *10, 0, 10);
            rigid.velocity = jumpVec;
        }
        else if (Input.GetKeyUp(KeyCode.Space)) //�����̽����� ���� ������
        {            
            rigid.useGravity = true;
            anim.SetBool("IsJump", false);
        }


        //���� ���°� / �������� �����ϰ� ���� ������..
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (IsDraw) //�̹� ���� �������̴� �����Ұ���
            {
                IsDraw = false;
                anim.SetTrigger("Holster");
            }
            else //���� ���������� �ʾƼ� ���� ������ ����.
            {
                IsDraw = true;
                anim.SetTrigger("Draw");
            }
        }

        //���� ���콺 Ŭ���ϸ� ���� �߻��� ���̰�
        if (Input.GetMouseButtonDown(0)) 
        {
            anim.SetTrigger("Shoot");
            if (IsDraw) //���� �������̶��~~~~ �ѽ�� ����..
            {
                Shoot();
            }
        }

        //rŰ�� ������ ������ �Ұ���
        if (Input.GetKeyDown(KeyCode.R))
        {
            anim.SetTrigger("Reload");
            if (IsDraw)
            {
                Debug.Log("�������ϱ�");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //renderer.materials[0] = bodies[Random.Range(0, bodies.Length)];                       
            renderer.material = bodies[Random.Range(0, bodies.Length)];
        }                
    }

    public void SetDraw(/*bool IsOn*/int val)     
    {
        if (val == 1)
        {
            //���� �չ����� �޾���ߵ�
            PistolTr.SetParent(handTr);            
        }
        else if(val == -1)
        {
            //���� Ȧ���� ������ �޾������.
            PistolTr.SetParent(holsterTr);            
        }
        
        // �θ��� ��ġ�� ���� ����
        PistolTr.localPosition = Vector3.zero;
        PistolTr.localRotation = Quaternion.identity;
    }

    public void Shoot()
    {        
        GameManager.Instance.GetBullet().Init(
            ShootPosTr, 10, AllEnum.Type.Player );
    }
}
