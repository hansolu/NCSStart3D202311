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

    public Transform handTr; //손에 총을 들려주기 위함
    public Transform holsterTr; //홀스터에 총을 넣어두기 위함
    public Transform PistolTr; //총 자체의 트랜스폼
    public Transform ShootPosTr;

    Vector3 jumpVec = Vector3.zero;
    Vector3 jumpVecOrg = new Vector3(0,10,0); //

    bool IsDraw = false; //총 장착 여부? 처음엔 맨손 걍 서있으니까 isDraw false상태고, 총을 한번 장착하면 true가 될것임..

    [SerializeField]
    SkinnedMeshRenderer renderer; //옷을 입힐 대상. 
    public Material[] bodies; //옷 시리즈
        
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //x = Input.GetAxis("Horizontal");//회전
        vec.x = Input.GetAxis("Horizontal");//회전으로 썼던것을 되돌림
        vec.z = Input.GetAxis("Vertical");

        //if (x ==0 && vec.z ==0) //가만히 있기 //회전용
        if (vec.x == 0 && vec.z == 0) //가만히 있기 
        {            
            speed = 0;
        }
        else
        {            
            if (Input.GetKey(KeyCode.LeftShift)) //시프트를 누르고 있으면 걷기 출력
            {
                //anim.SetFloat("Speed", 2.5f);                
                speed = walkSpeed;                
            }
            else //그게 아니면 뛰기 출력
            {
                //anim.SetFloat("Speed", 5);
                speed = runSpeed;                
            }
            //anim.SetFloat("PosX", /*vec.*/x);//회전용
            anim.SetFloat("PosX", vec.x);//복구
            anim.SetFloat("PosZ", vec.z);            
        }

        anim.SetFloat("Speed", speed);
        //vec = vec.normalized; //회전할때는 x값을 좌측이동으로 안썼어서..
        vec = vec.normalized; //복구
        transform.Translate(vec * Time.deltaTime * speed);
        //transform.Rotate(0,x,0);//회전

        if (Input.GetKeyDown(KeyCode.Space)) //스페이스를 딱 누르기 시작한 그때
        {
            anim.SetBool("IsJump",true);
            //rigid.AddForce(Vector3.up * 10 , ForceMode.Impulse); //힘을 한번 빡! 가함  
            rigid.useGravity = false; //중력사용을 안하겠음. 
            jumpVec = jumpVecOrg;//jumpVec == (0,10,0)
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            jumpVec.y = Mathf.Clamp(jumpVec.y - Time.deltaTime *10, 0, 10);
            rigid.velocity = jumpVec;
        }
        else if (Input.GetKeyUp(KeyCode.Space)) //스페이스에서 손을 뗐을때
        {            
            rigid.useGravity = true;
            anim.SetBool("IsJump", false);
        }


        //총을 차는거 / 총해제랑 동일하게 탭을 쓸것임..
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (IsDraw) //이미 총을 장착중이니 해제할것임
            {
                IsDraw = false;
                anim.SetTrigger("Holster");
            }
            else //총을 장착중이지 않아서 장착 진행할 것임.
            {
                IsDraw = true;
                anim.SetTrigger("Draw");
            }
        }

        //왼쪽 마우스 클릭하면 총을 발사할 것이고
        if (Input.GetMouseButtonDown(0)) 
        {
            anim.SetTrigger("Shoot");
            if (IsDraw) //총을 장착중이라면~~~~ 총쏘기 진행..
            {
                Shoot();
            }
        }

        //r키를 누르면 재장전 할것임
        if (Input.GetKeyDown(KeyCode.R))
        {
            anim.SetTrigger("Reload");
            if (IsDraw)
            {
                Debug.Log("재장전하기");
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
            //총을 손밑으로 달아줘야됨
            PistolTr.SetParent(handTr);            
        }
        else if(val == -1)
        {
            //총을 홀스터 밑으로 달아줘야함.
            PistolTr.SetParent(holsterTr);            
        }
        
        // 부모의 위치에 나를 맞춤
        PistolTr.localPosition = Vector3.zero;
        PistolTr.localRotation = Quaternion.identity;
    }

    public void Shoot()
    {        
        GameManager.Instance.GetBullet().Init(
            ShootPosTr, 10, AllEnum.Type.Player );
    }
}
