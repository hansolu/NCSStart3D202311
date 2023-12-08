using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowingEnemy : MonoBehaviour
{
    public Transform RayTr;
    public Transform playerTr;
    NavMeshAgent agent;
    Animator anim;
    Vector3 dir = Vector3.zero;

    [Tooltip("�þ߰�")]
    [Range(0,360)]
    [SerializeField]
    float ViewAngle = 0;
    
    [Tooltip("�þ߰Ÿ�")]
    [SerializeField]
    float ViewDistance = 0;

    [Tooltip("������ ������ ���̾� ����")]
    [SerializeField]
    LayerMask targetMask;

    [Tooltip("���ع� ���̾� ����")]
    [SerializeField]
    LayerMask obstacleMask;

    List<Collider> targetList = new List<Collider>();
    Vector3 targetDir = Vector3.zero;        
    float targetAngle=0;

    public bool DrawRay = false;
    bool IsMove = false; //���� �����̴� ������ üũ.

    Vector3 goalPos = Vector3.zero;
    
    Coroutine moveCor = null;

    int PatrolParentNum = 0; //���� ���� ����ϰ� �ִ� ���� �θ����� �ѹ�
    int PatrolNum = 0; //���� ���� ��ġ�� �ε���ȭ ��Ų��.
    float WaitTime = 0;

    //public void Init(int patrolParentNum, int patrolNum)
    //{ 
    //}
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        dir = playerTr.position - transform.position;

        //�� ȥ�� ����� ���ƴٴϴٰ�,
        //�̵�����...

        //���� �þ߿� ���� �ɸ��ٸ�, �̵��� ���߰� ������ ������ ������ �Ұ�..
        //=>

        #region �þ߰�
        if ( CheckSight())
        {
            if (moveCor !=null)
            { 
                StopCoroutine(moveCor);
                moveCor = null;
            }            

            if (IsArrive()==false)            
                SetMove();
        }
        else //�þ߰��� �ɸ��� ���� ���ٸ�
        {
            Patrol();
        }
        #endregion

        #region �ܼ� ����
        //���ݰŸ��ȿ� �÷��̾ ����
        //���� �÷��̾� ���̿� ���� ���θ��� ������ ������ üũ��, �ƹ��͵� ���ٸ� �÷��̾� �Ѿư���...
        //if (dir.sqrMagnitude <= 25) //���ݰŸ��� 5��� ġ��,  �ش� �Ÿ����� ����� ���ٸ�,
        //{
        //    RaycastHit hit;
        //    if (Physics.Raycast(RayTr.position, dir, out hit)) //���̸� ���. ���� RayTr�� ��ġ����, ����� ���� ��������
        //    {
        //        Debug.Log("��� " +hit.transform.name);
        //        if (hit.transform.CompareTag("Player"))
        //        {
        //            anim.SetFloat("Speed", agent.speed);
        //            anim.SetFloat("PosX", agent.velocity.x);
        //            anim.SetFloat("PosZ", agent.velocity.z);
        //            agent.isStopped = false;//���� ����
        //            agent.SetDestination(playerTr.position); //������ ����
        //        }
        //        //else
        //        //{
        //        //    //����� �����̿� ������ ����...
        //        //}
        //    }
        //}
        #endregion

        #region ����. �׳� �Ÿ��� �־����� �Ѿư��� �ڵ�.
        ////Vector3.Distance(playerTr.position, transform.position) > 5   ///  == ��Ȯ�� �Ÿ����� (�Լ�)
        ////(playerTr.position- transform.position ).Magnitude > 5 == ��Ȯ�� �Ÿ�����(���� ������Ƽ)
        //if (dir.sqrMagnitude > 25 ) //�Ÿ������� ����. ��Ʈ�Ⱦ����. ��Ʈ�� �Ⱦ��� ���������̱⋚����
        //    //���� ���ϴ� �Ÿ� 5 * 5 �ؼ� 25�ΰ�... 

        //    //��Ʈ�� ����ٴ°Ŵ� �������� �Ȱ��� ������������ ��ǻ�Ͱ� �����ϱ⶧���� ���귮�� ŭ...
        //{           
        //    anim.SetFloat("Speed", agent.speed);
        //    anim.SetFloat("PosX", agent.velocity.x);
        //    anim.SetFloat("PosZ", agent.velocity.z);
        //    agent.isStopped = false;//���� ����
        //    agent.SetDestination(playerTr.position); //������ ����
        //}
        //else //�÷��̾�� ������ �Ÿ��� 5���ϸ�.... �����
        //{
        //    anim.SetFloat("Speed", 0);            
        //    agent.isStopped = true;//����            
        //    agent.velocity = Vector3.zero;
        //}
        #endregion
    }
    #region ������
   
    Vector3 rightDir = Vector3.zero;
    Vector3 leftDir = Vector3.zero;
    void OnDrawGizmos()
    {
        if (DrawRay == false)
        {
            return;
        }

        Gizmos.DrawWireSphere(transform.position, ViewDistance);
        rightDir = DebugDir(transform.eulerAngles.y + ViewAngle * 0.5f); //transform.eulerAngles.y �� �ü�����
        leftDir = DebugDir(transform.eulerAngles.y - ViewAngle * 0.5f);
        Debug.DrawRay(transform.position, rightDir * ViewDistance, Color.blue);
        Debug.DrawRay(transform.position, leftDir * ViewDistance, Color.red);
    }
    Vector3 DebugDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }
    #endregion 

    public bool CheckSight()
    {
        targetList.Clear();
        Collider[] cols = Physics.OverlapSphere(transform.position, ViewDistance, targetMask);
        if (cols.Length > 0)
        {
            for (int i = 0; i < cols.Length; i++)
            {
                targetDir = (cols[i].transform.position - transform.position).normalized;
                targetAngle = Mathf.Acos(Vector3.Dot(transform.forward, targetDir)) * Mathf.Rad2Deg;
                if (targetAngle <= ViewAngle * 0.5f  //�� �þ߰� ���̰�,
                    && Physics.Raycast(transform.position, targetDir, ViewDistance, obstacleMask) == false)
                //���� ��ġ���� ����� ���� ���̸� ������, ��ֹ��� ��������������
                {
                    //���.
                    targetList.Add(cols[i]);
                }
            }        

            if (targetList.Count > 0) //�þ� �ȿ� ����� ����        
            {
                goalPos = targetList[0].transform.position; //��ǥ��ġ�� Ÿ��0��ģ���� �����...
                return true;
            }
            else //������          
                return false;            
        }
        else     
            return false;                   
    }

    bool IsArrive()
    {
        //��ǥ������ �ٴٶ����� üũ, �ٴٶ��ٸ� ���߱�.
        if ((transform.position - goalPos).sqrMagnitude <= 1) //a
        {            
            IsMove = false;
            anim.SetFloat("Speed", 0);
            agent.isStopped = true;//���� 
            agent.velocity = Vector3.zero;
            return true;
        }
        else
        {
            return false;
        }
    }

    void SetMove()
    {
        IsMove = true;
        //�þ� �ȿ� ����� ����.
        anim.SetFloat("Speed", agent.speed);
        anim.SetFloat("PosX", agent.velocity.x);
        anim.SetFloat("PosZ", agent.velocity.z);
        agent.isStopped = false;//���� ����        
        agent.SetDestination(goalPos); //������ ����
    }
    public void Patrol()
    {
        //�����̰� �ִ� ��
        if (IsMove)
        {
            IsArrive(); //�����ߴٸ� �˾Ƽ� ����...
        }
        else//�����̴� ���� �ƴ�
        {
            //���������� �ð��� ������ ���� ��ǥ�������� �̵������ϱ�            
            if (moveCor == null) //�ڷ�ƾ �ѹ�����...
                moveCor = StartCoroutine(SetNewGoal());
        }
    }

    IEnumerator SetNewGoal()
    {
        WaitTime = Random.Range(0.5f, 1f);
        Debug.Log("���ڸ� ���ð�"+ WaitTime);
        yield return new WaitForSeconds(WaitTime);
        goalPos = GameManager.Instance.GetNextPatrol(PatrolParentNum,ref PatrolNum, true);
        SetMove();                
        moveCor = null;        
    }
}


