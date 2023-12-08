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

    [Tooltip("시야각")]
    [Range(0,360)]
    [SerializeField]
    float ViewAngle = 0;
    
    [Tooltip("시야거리")]
    [SerializeField]
    float ViewDistance = 0;

    [Tooltip("적으로 감지할 레이어 선택")]
    [SerializeField]
    LayerMask targetMask;

    [Tooltip("방해물 레이어 선택")]
    [SerializeField]
    LayerMask obstacleMask;

    List<Collider> targetList = new List<Collider>();
    Vector3 targetDir = Vector3.zero;
    float targetAngle=0;

    public bool DrawRay = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        dir = playerTr.position - transform.position;

        //나 혼자 맘대로 돌아다니다가,
        //이동진행...

        //만약 시야에 뭔가 걸린다면, 이동을 멈추고 상대방을 잡으러 가도록 할것..
        //=>

        #region 시야각
        if ( CheckSight())
        {
            //만약 이동중이었으면 잠깐 멈췄다가.
            

            //시야 안에 대상이 있음.
            anim.SetFloat("Speed", agent.speed);
            anim.SetFloat("PosX", agent.velocity.x);
            anim.SetFloat("PosZ", agent.velocity.z);
            agent.isStopped = false;//멈춤 해제
            agent.SetDestination(targetList[0].transform.position); //목적지 세팅
        }
        else //시야각에 걸리는 것이 없다면
        {
            //정찰... => 일정시간마다 다른 구역으로 이동...

            anim.SetFloat("Speed", 0);
            agent.isStopped = true;//멈춤 해제
            agent.velocity = Vector3.zero;
        }
        #endregion

        #region 단순 감지
        //공격거리안에 플레이어가 들어서면
        //나와 플레이어 사이에 벽이 가로막고 있지는 않은지 체크후, 아무것도 없다면 플레이어 쫓아가기...
        //if (dir.sqrMagnitude <= 25) //공격거리가 5라고 치고,  해당 거리내에 사람이 들어섰다면,
        //{
        //    RaycastHit hit;
        //    if (Physics.Raycast(RayTr.position, dir, out hit)) //레이를 쏜다. 나의 RayTr의 위치에서, 사람을 향한 방향으로
        //    {
        //        Debug.Log("대상 " +hit.transform.name);
        //        if (hit.transform.CompareTag("Player"))
        //        {
        //            anim.SetFloat("Speed", agent.speed);
        //            anim.SetFloat("PosX", agent.velocity.x);
        //            anim.SetFloat("PosZ", agent.velocity.z);
        //            agent.isStopped = false;//멈춤 해제
        //            agent.SetDestination(playerTr.position); //목적지 세팅
        //        }
        //        //else
        //        //{
        //        //    //사람과 나사이에 뭔가가 있음...
        //        //}
        //    }
        //}
        #endregion

        #region 기존. 그냥 거리가 멀어지면 쫓아가는 코드.
        ////Vector3.Distance(playerTr.position, transform.position) > 5   ///  == 정확한 거리차이 (함수)
        ////(playerTr.position- transform.position ).Magnitude > 5 == 정확한 거리차이(변수 프로퍼티)
        //if (dir.sqrMagnitude > 25 ) //거리차이의 제곱. 루트안씌운거. 루트를 안씌운 제곱상태이기떄문에
        //    //내가 원하는 거리 5 * 5 해서 25인것... 

        //    //루트를 씌운다는거는 같은수로 똑같이 나눠질때까지 컴퓨터가 연산하기때문에 연산량이 큼...
        //{           
        //    anim.SetFloat("Speed", agent.speed);
        //    anim.SetFloat("PosX", agent.velocity.x);
        //    anim.SetFloat("PosZ", agent.velocity.z);
        //    agent.isStopped = false;//멈춤 해제
        //    agent.SetDestination(playerTr.position); //목적지 세팅
        //}
        //else //플레이어와 나와의 거리가 5이하면.... 멈춰라
        //{
        //    anim.SetFloat("Speed", 0);            
        //    agent.isStopped = true;//멈춰            
        //    agent.velocity = Vector3.zero;
        //}
        #endregion
    }
    #region 디버깅용
   
    Vector3 rightDir = Vector3.zero;
    Vector3 leftDir = Vector3.zero;
    void OnDrawGizmos()
    {
        if (DrawRay == false)
        {
            return;
        }

        Gizmos.DrawWireSphere(transform.position, ViewDistance);
        rightDir = DebugDir(transform.eulerAngles.y + ViewAngle * 0.5f); //transform.eulerAngles.y 내 시선각도
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
                if (targetAngle <= ViewAngle * 0.5f  //내 시야각 안이고,
                    && Physics.Raycast(transform.position, targetDir, ViewDistance, obstacleMask) == false)
                //나의 위치에서 대상을 향해 레이를 쐈을때, 장애물이 막고잇지도않음
                {
                    //대상.
                    targetList.Add(cols[i]);

                }
            }        

            if (targetList.Count > 0) //시야 안에 대상이 있음        
                return true;          
            else //대상없음          
                return false;            
        }
        else     
            return false;                   
    }
}


