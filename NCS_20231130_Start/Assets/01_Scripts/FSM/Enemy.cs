using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//적 객체. 애니메이션, 스킨, 스탯이나 공격력 등 ...
//rigid, collider... 
public class Enemy : MonoBehaviour
{        
    [Header("NowState는 인스펙터 창에서 건들지 마세요")]
    public AllEnum.StateEnum NowState =AllEnum.StateEnum.End;//현재상태
    AnimationComponent anim;
    NavMeshAgent agent;

    [Tooltip("게임매니저에 있는 정찰 패턴 부모 넘버 부여")]
    public int PatterParentNum = 0; //움직임 패턴 부모넘버.

    [Tooltip("시야각")]
    [Range(0, 360)]    
    public float ViewAngle = 0;

    [Tooltip("시야거리")]    
    public float ViewDistance = 0;

    [Tooltip("적으로 감지할 레이어 선택")]    
    public LayerMask targetMask;

    [Tooltip("방해물 레이어 선택")]    
    public LayerMask obstacleMask;

    public Transform ShootPosTr; //총구위치

    public float AttackRange = 0;

    List<Collider> targetList = new List<Collider>();
    Vector3 targetDir = Vector3.zero;
    float targetAngle = 0;//시야각

    ////대상의 위치도 
    ////계속 추격하고 싶으면    
    [Header("TargetTr은 인스펙터 창에서 건들지 마세요")]
    [Tooltip("타겟 잘 들어와있는지 확인용임")]
    public Transform TargetTr;

    public float FarAttackDelayTime = 1;//원거리공격 쿨타임
    public float NearAttackDelayTime = 0.8f; //근접공격 쿨타임
    Coroutine AttackCor = null;
    //그 위치로, 해당대상이 도망간다면 더이상 못쫓아가게 하고싶다면
    //public Vector3 TargetPos = Vector3.zero;    

    //Enemy도 statemachine을 들고 있게 하는 방법
    //public StateMachine statemachine { get; private set; }

    void Start()
    {        
        anim = GetComponent<AnimationComponent>();
        anim.SetInit();
        agent = GetComponent<NavMeshAgent>();

        //statemachine을 변수로 가지고 있게 하고 진행해도되고..
        //요떄 한번만 부르고 싶다면 이렇게 진행해도될거고....
        GetComponent<StateMachine>().SetInit();
    }

    public void Idle()
    {        
        anim.Idle();
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

    //이동 멈추는 함수
    public void Move(Vector3 vec)
    {
        agent.isStopped = false;
        agent.SetDestination(vec);
        //조건에 따라서
        //Walk의 애니메이션이 움직임이 좀 시원치 않을 수도 있음..
        //때문에 SetMoveAnim으로 따로 분리함.        
    }
    public void SetMoveAnim()
    {        
        anim.Walk(agent.velocity.x, agent.velocity.z);
    }

    public void SetWeaponOn(bool isFar, bool isOn)
    {
        if (isFar)
            anim.Gun_Draw(isOn);
        else
            anim.Sword_Draw(isOn);
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
                if (targetAngle <= ViewAngle * 0.5f  //내 시야각 안이고,
                    && Physics.Raycast(transform.position, targetDir, /*ViewDistance*/range, obstacleMask) == false)
                //나의 위치에서 대상을 향해 레이를 쐈을때, 장애물이 막고잇지도않음
                {
                    //대상.
                    targetList.Add(cols[i]);
                    //return true; //만약 내가 쫓는 대상이 무조건 플레이어 뿐이라면
                }
            }

            if (targetList.Count > 0) //시야 안에 대상이 있음        
            {
                TargetTr = targetList[0].transform;
                return true;
            }
            else //대상없음          
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
                AttackCor = StartCoroutine(NearAttackDelay());
        }
        else
        {
            if (AttackCor != null)
                StopCoroutine(AttackCor);
            AttackCor = null;
        }
    }
    IEnumerator NearAttackDelay()
    {
        //총쏘는 모션 출력
        anim.Attack(false); //근거리 공격모션 출력
        Debug.Log("근거리 공격함");        
        Collider[] cols = Physics.OverlapSphere(transform.position,
           AttackRange, targetMask);
        if (cols.Length > 0)
        {
            targetList.Clear();
            for (int i = 0; i < cols.Length; i++)
            {
                targetDir = (cols[i].transform.position - transform.position).normalized;                
                if (Physics.Raycast(transform.position, targetDir, AttackRange, obstacleMask) == false)
                //나의 위치에서 대상을 향해 레이를 쐈을때, 장애물이 막고잇지도않음
                {
                    //대상.
                    targetList.Add(cols[i]);
                    //return true; //만약 내가 쫓는 대상이 무조건 플레이어 뿐이라면
                }
            }

            if (targetList.Count > 0) //시야 안에 대상이 있음        
            {
                for (int i = 0; i < targetList.Count; i++)
                {
                    //만약 위에 걸러내는 레이어에 플레이어와, 적을 둘다 넣어놨고
                    IHit hit = targetList[i].GetComponent<IHit>();
                    //적에도 IHit를 똑같이 넣어뒀다면
                    if (hit!=null)
                    {
                        Debug.Log("사람 공격");
                        hit.Hit(5); //그 대상이 Player 건, Enemy건 데미지를 가할 수 있음                                    
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
            if (AttackCor == null)         
                AttackCor = StartCoroutine(ShootDelay());                    
        }
        else
        {
            if (AttackCor != null)
                StopCoroutine(AttackCor);
            AttackCor = null;
        }
    }

    IEnumerator ShootDelay()
    {
        //총쏘는 모션 출력
        anim.Attack(true);

        //실제로 총알을 발사함.
        GameManager.Instance.GetBullet().Init(
           ShootPosTr/*총구위치*/,
           5/*공격력*/, AllEnum.Type.Enemy);

        yield return new WaitForSeconds(FarAttackDelayTime);
        AttackCor = null;
    }


    //이 함수는 플레이어와 에너미를 같은 fbx를 사용하기 때문에
    //그 fbx에 추가된 애니메이션 이벤트 에서 부르기로한 
    //SetDraw가 없어서 오류가 남.
    //해당 오류를 없애기 위해 그냥 만들어둠.
    public void SetDraw(int val)
    { 
    }
}

