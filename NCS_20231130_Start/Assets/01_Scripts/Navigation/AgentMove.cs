using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMove : MonoBehaviour
{
    public Camera cam;
    public Transform goal; //내가 클릭한 목적지를 확인하기 위한 sphere 연결용

    NavMeshAgent agent;

    int offMeshArea_Climb = 4; //오프메시의 구역번호. 
    int offMeshArea_Jump = 2; //오프메시 점프구간 구역 번호
    float climbSpeed = 0.5f;//오르내리는 이동속도
    float jumpSpeed = 15f; // 점프힘
    float gravity = -9.8f; //중력계수
    Coroutine MeshLinkCor = null;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //왼쪽클릭 눌렀을때
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition),out hit, Mathf.Infinity ))//카메라로부터 광선을쏨 (마우스 위치에서)
            {//해서 뭔가 걸리는게 있다면, 
                goal.position = hit.point; //goal의 위치를 해당 광선이 부딪힌 곳으로 설정함
                agent.SetDestination(goal.position); //에이전트 객체의 목표지점을 해당 지점으로 설정함...
            }
        }

        if (agent.isOnOffMeshLink) //내가 메시링크 위에 존재함
        {
            OffMeshLinkData linkdata = agent.currentOffMeshLinkData;
            if (linkdata.offMeshLink == null)
            {
                return;
            }

            if (linkdata.offMeshLink.area == offMeshArea_Climb) //해당 메시링크가 오르는 구역이면
            {
                if (MeshLinkCor == null)
                {
                    MeshLinkCor = StartCoroutine(ClimbCor(linkdata));
                }
            }
            else if (linkdata.offMeshLink.area == offMeshArea_Jump)
            {
                if (MeshLinkCor == null)
                {
                    MeshLinkCor = StartCoroutine(JumpCor(linkdata));
                }
            }
        }
    }

    IEnumerator ClimbCor(OffMeshLinkData linkdata)
    {
        //오르기 시작하는 애니메이션 시작.

        agent.isStopped = true; //잠시 내비로 이동하기를 중지함
        agent.updateRotation = false; //회전도 중지할것임

        Vector3 start = linkdata.startPos; //현재 내가 서있는 오프메시 링크의 시작지점..
        Vector3 end = linkdata.endPos; //현재 내가 서있는 오프메시 링크의 도착지점..
        Vector3 LookPos = end;

        float climbTime = Mathf.Abs(end.y - start.y) / climbSpeed; //오르내리는 시간 설정
        float currentTime = 0;
        float percent = 0;
        
        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / climbTime;//0부터 1 사이의 값이 됨..
            transform.position = Vector3.Lerp(start,end, percent);
            LookPos.y = transform.position.y;
            transform.LookAt(LookPos);
            yield return null;
        }
        agent.CompleteOffMeshLink();//오프메시끝지점에 도착했다고 알려줌 인데, 이걸 안해버리면 계속 오르락내리락 반복함.
        agent.isStopped = false;
        agent.updateRotation = true;

        //오르기 애니메이션을 중지시킴.

        MeshLinkCor = null;
    }

    IEnumerator JumpCor(OffMeshLinkData linkdata)
    {
        //오르기 시작하는 애니메이션 시작.

        agent.isStopped = true; //잠시 내비로 이동하기를 중지함
        agent.updateRotation = false; //회전도 중지할것임

        Vector3 start = linkdata.startPos; //현재 내가 서있는 오프메시 링크의 시작지점..
        Vector3 end = linkdata.endPos; //현재 내가 서있는 오프메시 링크의 도착지점..
        Vector3 LookPos = end;

        float jumpTime = Mathf.Max(0.3f, Vector3.Distance(start,end) / jumpSpeed);        
        float currentTime = 0;
        float percent = 0;
        float v0 = (end.y - start.y) - gravity; //y방향의 초기속도

        Vector3 tempPos = Vector3.zero;
        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / jumpTime;//0부터 1 사이의 값이 됨..
            tempPos = Vector3.Lerp(start,end, percent);
            tempPos.y = start.y + (v0 * percent) + (gravity * percent * percent); // 포물선공식...
            transform.position = tempPos;
            LookPos.y = transform.position.y;
            transform.LookAt(LookPos);
            yield return null;
        }
        agent.CompleteOffMeshLink();//오프메시끝지점에 도착했다고 알려줌 인데, 이걸 안해버리면 계속 오르락내리락 반복함.
        agent.isStopped = false;
        agent.updateRotation = true;

        //오르기 애니메이션을 중지시킴.

        MeshLinkCor = null;
    }
}
