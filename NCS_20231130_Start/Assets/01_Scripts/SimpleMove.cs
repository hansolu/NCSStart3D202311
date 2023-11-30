using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove : MonoBehaviour
{
    float x = 0;
    float z = 0;
    float angle = 0;
    Vector3 moveVec = Vector3.zero;
    Vector3 speedVec = Vector3.zero;
    Rigidbody rigid;
    bool isJump = false;

    public Transform Cube1;

    //예시. 색의 경우 new Color( 0,0,0,1)  ~new Color( 1,1,1,1)
    // color변수/255f  //0~255

    [Range(0,1)] //attribute인데 serializeField 
    //근데 이 Range는 인스펙터에만 해당되고, 내가 코드에서 강제로 2, 3이런걸 준다면 들어가짐..
    public float Timeval=0;


    //transform. ~~~하는 함수들은 transform의 정보 자체를 변환시키고
    //Quaternion이나 Vector 안에 들은 함수들은 매개변수로 주어진값을 가공 후에 그 값을 반환하여줌. (원본에 직접넣지않는한 원본과 관계없음)

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        x = Input.GetAxisRaw("Horizontal"); //A / D 혹은 왼쪽방향키, 오른쪽방향키 //왼쪽값이 들어오면 -1, 안누르면 0 , 오른쪽이면 1
        z = Input.GetAxisRaw("Vertical");
        
        if (isJump == false) //공중에서 방향 틀기를 금지하고 싶을 경우...
        {
            moveVec.x = x;
            moveVec.z = z;
            transform.Translate(moveVec.normalized * Time.deltaTime * 10);
        }
        else
        {
            if (transform.position.y <= 1)
            {
                isJump = false;
                moveVec.y = 0;
            }
        }        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isJump) //점프 한번밖에 못하도록...
            {
                return;
            }
            isJump = true;
            //y값으로 힘을 더하면....
            moveVec.x = x;
            moveVec.z = z;
            //moveVec.y = 1;
            
            rigid.AddForce(moveVec.normalized * 10+                
              Vector3.up * 10  
                ,ForceMode.Impulse);
        //    transform.rotation = Quaternion.LookRotation(Cube1.transform.position); //LookAt과 비슷           
        //    //transform.rotation = Quaternion.FromToRotation(transform.forward, Vector3.forward);     //잘안씀         
        }       
        
        ////transform.LookAt(Cube1); 
        ////transform.RotateAround(Cube1.position, Vector3.forward, 1); //공전의 표현..                 
        //Timeval += Time.deltaTime;
        
        ////가까운 각도로 회전하고, Timeval은 첫번째 인자에서 두번째 인자 로 회전을 할건데, 그 회전을 n등분했을때 timeval일때의 딱 그 값을 반환.
        //transform.rotation =  Quaternion.Slerp(Quaternion.Euler(0,0,0), Quaternion.Euler(0, 345, 0), Timeval );
    }
    //void FixedUpdate()
    //{
    //    #region 보편적 이동
    //    //moveVec.x = x;
    //    //moveVec.z = z;

    //    //transform.Translate(moveVec.normalized * Time.fixedDeltaTime * 10); //moveVec.normalized == moveVec을 크기 1짜리의 벡터로 만듦.
    //    ////transform.position += moveVec.normalized * Time.fixedDeltaTime * 10 ; //윗줄과 이 줄은 동일하다.
    //    ////아랫줄은 변수로 직접 제어하는거고, Translate는 함수로 조정하는 것일뿐.

    //    //velocity == 나의 속력. 속력을 = 집어넣어줌. 해당 속력으로 만듦.  / AddForce== 속력을 가함. 더해줌. 증가시킴
    //    //rigid.velocity = moveVec.normalized * 10; //나의 움직이는 속도를 이걸로 유지하겠다...
    //    //rigid.AddForce(); //rigid.velocity +=  //힘을 가하겠다.. //AddForce를 계속 부르면, 점점 더 빨라짐.. 

    //    #endregion
    //    #region 이동관련 함수 + 실시간 입력으로 하기 부적합한 애들.
    //    //목표위치를 주는 함수들은, 목적지에 도달하는것이 목적이므로, 인풋에 대한 이동으로는 적합치 않음.

    //    ////목적지가 명확할떄 적합한 함수.
    //    //transform.position = Vector3.MoveTowards(transform.position, new Vector3(5,0,5),
    //    //    Time.fixedDeltaTime * 10); 
    //    ////현재위치에서 목표위치까지 속력~으로 이동함.


    //    //transform.position = Vector3.SmoothDamp(현재위치, 목표위치, 참조속력, 이동소요시간);
    //    //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(10, 0, 10), ref speedVec, 0.1f);

    //    //transform.position = Vector3.Lerp(현재위치, 목표위치, 보간 간격);
    //    //transform.position = Vector3.Lerp(transform.position, new Vector3(10, 0, 10), 0.1f);

    //    //위의 친구들은 선형보간이고  === 등속운동

    //    //얘는 구면보간..  이동중에 속도의 변화가 있음..
    //    //transform.position = Vector3.Slerp(현재위치, 목표위치, 보간간격);
    //    //transform.position = Vector3.Slerp(transform.position, new Vector3(10, 0, 10), 0.05f);
    //    //Debug.Log(Vector3.Slerp(transform.position, new Vector3(10, 0, 10), 0.05f));
    //    #endregion

    //    #region 좌우는 회전하고 위아래는 앞뒤로 이동할 것임

    //    if (isJump == false)
    //    { 
    //        moveVec.z = z; //0,0,1 / 0,0,-1 / 0,0,0

    //    transform.Translate(moveVec * Time.fixedDeltaTime * 10); //로컬기준이구나 를 알 수 있음.

    //    if (x!=0)                    
    //        transform.Rotate(0,x,0); //rotation 에서 누적 더함.            
    //    }
    //    //angle += x;
    //    //transform.rotation *= Quaternion.Euler(0, angle, 0); //해당 벡터로 각도를 만듦.


    //    //만약 내가 10,10,10도로 회전하고 싶다면 z-y-x 축 차례로 곱해줘야 원하는 효과가 잘 나옴.
    //    //transform.rotation *= Quaternion.Euler(0, 0, 10); //해당 벡터로 각도를 만듦.
    //    //transform.rotation *= Quaternion.Euler(0, 10, 0); //해당 벡터로 각도를 만듦.
    //    //transform.rotation *= Quaternion.Euler(10, 0, 0); //해당 벡터로 각도를 만듦.
    //    #endregion
    //}
}
