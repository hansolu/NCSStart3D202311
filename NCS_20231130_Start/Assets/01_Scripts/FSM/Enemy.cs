using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//�� ��ü. �ִϸ��̼�, ��Ų, �����̳� ���ݷ� �� ...
//rigid, collider... 
public class Enemy : MonoBehaviour
{        
    [Header("NowState�� �ν����� â���� �ǵ��� ������")]
    public AllEnum.StateEnum NowState =AllEnum.StateEnum.End;//�������
    AnimationComponent anim;
    NavMeshAgent agent;

    [Tooltip("���ӸŴ����� �ִ� ���� ���� �θ� �ѹ� �ο�")]
    public int PatterParentNum = 0; //������ ���� �θ�ѹ�.

    [Tooltip("�þ߰�")]
    [Range(0, 360)]    
    public float ViewAngle = 0;

    [Tooltip("�þ߰Ÿ�")]    
    public float ViewDistance = 0;

    [Tooltip("������ ������ ���̾� ����")]    
    public LayerMask targetMask;

    [Tooltip("���ع� ���̾� ����")]    
    public LayerMask obstacleMask;

    public float AttackRange = 0;

    List<Collider> targetList = new List<Collider>();
    Vector3 targetDir = Vector3.zero;
    float targetAngle = 0;//�þ߰�

    ////����� ��ġ�� 
    ////��� �߰��ϰ� ������
    public Transform TargetTr; 

    //�� ��ġ��, �ش����� �������ٸ� ���̻� ���Ѿư��� �ϰ�ʹٸ�
    //public Vector3 TargetPos = Vector3.zero;    

    //Enemy�� statemachine�� ��� �ְ� �ϴ� ���
    //public StateMachine statemachine { get; private set; }

    void Start()
    {        
        anim = GetComponent<AnimationComponent>();
        anim.SetInit();
        agent = GetComponent<NavMeshAgent>();

        //statemachine�� ������ ������ �ְ� �ϰ� �����ص��ǰ�..
        //�䋚 �ѹ��� �θ��� �ʹٸ� �̷��� �����ص��ɰŰ�....
        GetComponent<StateMachine>().SetInit();
    }

    public void Idle()
    {        
        anim.Idle();
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

    //�̵� ���ߴ� �Լ�
    public void Move(Vector3 vec)
    {
        agent.isStopped = false;
        agent.SetDestination(vec);
        //���ǿ� ����
        //Walk�� �ִϸ��̼��� �������� �� �ÿ�ġ ���� ���� ����..
        //������ SetMoveAnim���� ���� �и���.        
    }
    public void SetMoveAnim()
    {        
        anim.Walk(agent.velocity.x, agent.velocity.z);
    }

    public bool CheckSight( float range )
    {
        targetList.Clear();
        Collider[] cols = Physics.OverlapSphere(transform.position,
            /*ViewDistance*/range, targetMask);
        if (cols.Length > 0)
        {
            for (int i = 0; i < cols.Length; i++)
            {
                targetDir = (cols[i].transform.position - transform.position).normalized;
                targetAngle = Mathf.Acos(Vector3.Dot(transform.forward, targetDir)) * Mathf.Rad2Deg;
                if (targetAngle <= ViewAngle * 0.5f  //�� �þ߰� ���̰�,
                    && Physics.Raycast(transform.position, targetDir, /*ViewDistance*/range, obstacleMask) == false)
                //���� ��ġ���� ����� ���� ���̸� ������, ��ֹ��� ��������������
                {
                    //���.
                    targetList.Add(cols[i]);
                    //return true; //���� ���� �Ѵ� ����� ������ �÷��̾� ���̶��
                }
            }

            if (targetList.Count > 0) //�þ� �ȿ� ����� ����        
            {
                TargetTr = targetList[0].transform;
                return true;
            }
            else //������          
                return false;
        }
        else
            return false;
    }
}
