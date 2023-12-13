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

    public Transform ShootPosTr; //�ѱ���ġ

    public float AttackRange = 0; //�ٰŸ� ���ݹ���
    public float AttackFarRange = 0; //���Ÿ� ���ݹ���

    List<Collider> targetList = new List<Collider>();
    Vector3 targetDir = Vector3.zero;
    float targetAngle = 0;//�þ߰�

    ////����� ��ġ�� 
    ////��� �߰��ϰ� ������    
    [Header("TargetTr�� �ν����� â���� �ǵ��� ������")]
    [Tooltip("Ÿ�� �� �����ִ��� Ȯ�ο���")]
    public Transform TargetTr;

    public float FarAttackDelayTime = 2;//���Ÿ����� ��Ÿ��
    public float NearAttackDelayTime = 1f; //�������� ��Ÿ��
    Coroutine AttackCor = null;
    //�� ��ġ��, �ش����� �������ٸ� ���̻� ���Ѿư��� �ϰ�ʹٸ�
    //public Vector3 TargetPos = Vector3.zero;    

    //Enemy�� statemachine�� ��� �ְ� �ϴ� ���
    //public StateMachine statemachine { get; private set; }

    AllEnum.MyWeaponState weaponState = AllEnum.MyWeaponState.None;
        

    void Start()
    {        
        anim = GetComponent<AnimationComponent>();
        anim.SetInit();
        agent = GetComponent<NavMeshAgent>();

        //statemachine�� ������ ������ �ְ� �ϰ� �����ص��ǰ�..
        //�䋚 �ѹ��� �θ��� �ʹٸ� �̷��� �����ص��ɰŰ�....

        //�̰Ŵ� FSM��..
        //GetComponent<StateMachine>().SetInit();

        //�����̺��Ʈ�� ����.
        GetComponent<EnemyBehaviorTree>().SetInit();
    }

    public void Idle()
    {        
        anim.Idle();
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

    //�̵� ���ߴ� �Լ�
    public void Move(Vector3 vec, bool anim = false) 
    {
        if (anim)
        {
            SetMoveAnim();
        }
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

    public void SetWeaponOff()
    {
        Debug.Log(weaponState + " ��������");
        if (weaponState == AllEnum.MyWeaponState.Gun)
        {
            SetWeaponOn(true, false);
        }
        else if (weaponState == AllEnum.MyWeaponState.Sword)
        {
            SetWeaponOn(false, false);
        }
        weaponState = AllEnum.MyWeaponState.None;
    }
    public void SetWeaponOn(bool isFar, bool isOn)
    {        
        if (isFar)
        { 
            anim.Gun_Draw(isOn);
            if (isOn)
            {
                weaponState = AllEnum.MyWeaponState.Gun;
            }
            else
            {
                weaponState = AllEnum.MyWeaponState.None;
            }
        }
        else
        { 
            anim.Sword_Draw(isOn);
            if (isOn)
            {
                weaponState = AllEnum.MyWeaponState.Sword;
            }
            else
                weaponState = AllEnum.MyWeaponState.None;
        }
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

    public void AttackNear(bool isStart)
    {
        if (isStart)
        {            
            if (AttackCor == null)
            {
                if (weaponState != AllEnum.MyWeaponState.Sword)
                {
                    SetWeaponOff(); //Ȥ���� ���� ����ٸ� ���� �����ϰ�
                    SetWeaponOn(false, true);//���� ���                    
                }
                AttackCor = StartCoroutine(NearAttackDelay());
            }
        }
        else
        {
            if (AttackCor != null)
            {
                SetWeaponOff();
                StopCoroutine(AttackCor);
            }
            AttackCor = null;            
        }
    }
    IEnumerator NearAttackDelay()
    {
        //�ѽ�� ��� ���
        anim.Attack(false); //�ٰŸ� ���ݸ�� ���
        Debug.Log("�ٰŸ� ������");        
        Collider[] cols = Physics.OverlapSphere(transform.position,
           AttackRange, targetMask);
        if (cols.Length > 0)
        {
            targetList.Clear();
            for (int i = 0; i < cols.Length; i++)
            {
                targetDir = (cols[i].transform.position - transform.position).normalized;                
                if (Physics.Raycast(transform.position, targetDir, AttackRange, obstacleMask) == false)
                //���� ��ġ���� ����� ���� ���̸� ������, ��ֹ��� ��������������
                {
                    //���.
                    targetList.Add(cols[i]);
                    //return true; //���� ���� �Ѵ� ����� ������ �÷��̾� ���̶��
                }
            }

            if (targetList.Count > 0) //�þ� �ȿ� ����� ����        
            {
                for (int i = 0; i < targetList.Count; i++)
                {
                    //���� ���� �ɷ����� ���̾ �÷��̾��, ���� �Ѵ� �־����
                    IHit hit = targetList[i].GetComponent<IHit>();
                    //������ IHit�� �Ȱ��� �־�״ٸ�
                    if (hit!=null)
                    {
                        Debug.Log("��� ����");
                        hit.Hit(5); //�� ����� Player ��, Enemy�� �������� ���� �� ����                                    
                    }
                }                
            }            
        }

        yield return new WaitForSeconds(NearAttackDelayTime);
        AttackCor = null;
    }

    public void Shoot(bool isStart)
    {        
        if (isStart)
        {
            //#######�̰Ŵ� BT�� AttackFar�� ��������Ʈ�� ��Ű�� ����
            //���Ÿ� ������ �Ҷ��� ������ ���� �ϵ��� ������.
            Idle();
                        
            if (AttackCor == null)
            {
                //#######BT���� �������� �߰��� ������������ �����
                if (weaponState != AllEnum.MyWeaponState.Gun)
                {
                    SetWeaponOff(); //�ϴ� ���� ���� ����.               
                    SetWeaponOn(true, true);//�ѱ� ����                                    
                }

                AttackCor = StartCoroutine(ShootDelay());
            }                
        }
        else
        {                        
            if (AttackCor != null)
            {                
                StopCoroutine(AttackCor);
                //#######BT���� �������� �߰��� ������������ �����            
                SetWeaponOff();
            }                
            AttackCor = null;
        }
    }

    IEnumerator ShootDelay()
    {
        //���� ���������� ��⶧���� �ִϸ��̼��ʿ� Ÿ�ָ̹��缭 
        //�׳� �θ����� �ϴ°͵� ���
        
        Debug.Log("���");
        //�ѽ�� ��� ���
        anim.Attack(true);

        //������ �Ѿ��� �߻���.
        GameManager.Instance.GetBullet().Init(
           ShootPosTr/*�ѱ���ġ*/,
           5/*���ݷ�*/, AllEnum.Type.Enemy);

        yield return new WaitForSeconds(FarAttackDelayTime);
        AttackCor = null;
    }


    //�� �Լ��� �÷��̾�� ���ʹ̸� ���� fbx�� ����ϱ� ������
    //�� fbx�� �߰��� �ִϸ��̼� �̺�Ʈ ���� �θ������ 
    //SetDraw�� ��� ������ ��.
    //�ش� ������ ���ֱ� ���� �׳� ������.
    public void SetDraw(int val)
    { 
    }
}

