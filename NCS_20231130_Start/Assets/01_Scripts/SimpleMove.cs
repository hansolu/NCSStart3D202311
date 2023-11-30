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

    //����. ���� ��� new Color( 0,0,0,1)  ~new Color( 1,1,1,1)
    // color����/255f  //0~255

    [Range(0,1)] //attribute�ε� serializeField 
    //�ٵ� �� Range�� �ν����Ϳ��� �ش�ǰ�, ���� �ڵ忡�� ������ 2, 3�̷��� �شٸ� ����..
    public float Timeval=0;


    //transform. ~~~�ϴ� �Լ����� transform�� ���� ��ü�� ��ȯ��Ű��
    //Quaternion�̳� Vector �ȿ� ���� �Լ����� �Ű������� �־������� ���� �Ŀ� �� ���� ��ȯ�Ͽ���. (������ ���������ʴ��� ������ �������)

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        x = Input.GetAxisRaw("Horizontal"); //A / D Ȥ�� ���ʹ���Ű, �����ʹ���Ű //���ʰ��� ������ -1, �ȴ����� 0 , �������̸� 1
        z = Input.GetAxisRaw("Vertical");
        
        if (isJump == false) //���߿��� ���� Ʋ�⸦ �����ϰ� ���� ���...
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
            if (isJump) //���� �ѹ��ۿ� ���ϵ���...
            {
                return;
            }
            isJump = true;
            //y������ ���� ���ϸ�....
            moveVec.x = x;
            moveVec.z = z;
            //moveVec.y = 1;
            
            rigid.AddForce(moveVec.normalized * 10+                
              Vector3.up * 10  
                ,ForceMode.Impulse);
        //    transform.rotation = Quaternion.LookRotation(Cube1.transform.position); //LookAt�� ���           
        //    //transform.rotation = Quaternion.FromToRotation(transform.forward, Vector3.forward);     //�߾Ⱦ�         
        }       
        
        ////transform.LookAt(Cube1); 
        ////transform.RotateAround(Cube1.position, Vector3.forward, 1); //������ ǥ��..                 
        //Timeval += Time.deltaTime;
        
        ////����� ������ ȸ���ϰ�, Timeval�� ù��° ���ڿ��� �ι�° ���� �� ȸ���� �Ұǵ�, �� ȸ���� n��������� timeval�϶��� �� �� ���� ��ȯ.
        //transform.rotation =  Quaternion.Slerp(Quaternion.Euler(0,0,0), Quaternion.Euler(0, 345, 0), Timeval );
    }
    //void FixedUpdate()
    //{
    //    #region ������ �̵�
    //    //moveVec.x = x;
    //    //moveVec.z = z;

    //    //transform.Translate(moveVec.normalized * Time.fixedDeltaTime * 10); //moveVec.normalized == moveVec�� ũ�� 1¥���� ���ͷ� ����.
    //    ////transform.position += moveVec.normalized * Time.fixedDeltaTime * 10 ; //���ٰ� �� ���� �����ϴ�.
    //    ////�Ʒ����� ������ ���� �����ϴ°Ű�, Translate�� �Լ��� �����ϴ� ���ϻ�.

    //    //velocity == ���� �ӷ�. �ӷ��� = ����־���. �ش� �ӷ����� ����.  / AddForce== �ӷ��� ����. ������. ������Ŵ
    //    //rigid.velocity = moveVec.normalized * 10; //���� �����̴� �ӵ��� �̰ɷ� �����ϰڴ�...
    //    //rigid.AddForce(); //rigid.velocity +=  //���� ���ϰڴ�.. //AddForce�� ��� �θ���, ���� �� ������.. 

    //    #endregion
    //    #region �̵����� �Լ� + �ǽð� �Է����� �ϱ� �������� �ֵ�.
    //    //��ǥ��ġ�� �ִ� �Լ�����, �������� �����ϴ°��� �����̹Ƿ�, ��ǲ�� ���� �̵����δ� ����ġ ����.

    //    ////�������� ��Ȯ�ҋ� ������ �Լ�.
    //    //transform.position = Vector3.MoveTowards(transform.position, new Vector3(5,0,5),
    //    //    Time.fixedDeltaTime * 10); 
    //    ////������ġ���� ��ǥ��ġ���� �ӷ�~���� �̵���.


    //    //transform.position = Vector3.SmoothDamp(������ġ, ��ǥ��ġ, �����ӷ�, �̵��ҿ�ð�);
    //    //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(10, 0, 10), ref speedVec, 0.1f);

    //    //transform.position = Vector3.Lerp(������ġ, ��ǥ��ġ, ���� ����);
    //    //transform.position = Vector3.Lerp(transform.position, new Vector3(10, 0, 10), 0.1f);

    //    //���� ģ������ ���������̰�  === ��ӿ

    //    //��� ���麸��..  �̵��߿� �ӵ��� ��ȭ�� ����..
    //    //transform.position = Vector3.Slerp(������ġ, ��ǥ��ġ, ��������);
    //    //transform.position = Vector3.Slerp(transform.position, new Vector3(10, 0, 10), 0.05f);
    //    //Debug.Log(Vector3.Slerp(transform.position, new Vector3(10, 0, 10), 0.05f));
    //    #endregion

    //    #region �¿�� ȸ���ϰ� ���Ʒ��� �յڷ� �̵��� ����

    //    if (isJump == false)
    //    { 
    //        moveVec.z = z; //0,0,1 / 0,0,-1 / 0,0,0

    //    transform.Translate(moveVec * Time.fixedDeltaTime * 10); //���ñ����̱��� �� �� �� ����.

    //    if (x!=0)                    
    //        transform.Rotate(0,x,0); //rotation ���� ���� ����.            
    //    }
    //    //angle += x;
    //    //transform.rotation *= Quaternion.Euler(0, angle, 0); //�ش� ���ͷ� ������ ����.


    //    //���� ���� 10,10,10���� ȸ���ϰ� �ʹٸ� z-y-x �� ���ʷ� ������� ���ϴ� ȿ���� �� ����.
    //    //transform.rotation *= Quaternion.Euler(0, 0, 10); //�ش� ���ͷ� ������ ����.
    //    //transform.rotation *= Quaternion.Euler(0, 10, 0); //�ش� ���ͷ� ������ ����.
    //    //transform.rotation *= Quaternion.Euler(10, 0, 0); //�ش� ���ͷ� ������ ����.
    //    #endregion
    //}
}
