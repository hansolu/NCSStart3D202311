using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Pet스크립트 따로 두는게 아니고 PetStateMachine 스크립트 하나로 해결볼경우
public class PetStateMachine : MonoBehaviour
{
    public Transform TargetTr; //상대방의 transform
    NavMeshAgent agent;
    public NavMeshAgent Agent => agent;

    Dictionary<AllEnum.StateEnum, State_Pet> StateDic = new Dictionary<AllEnum.StateEnum, State_Pet>();
    AllEnum.StateEnum ExState = AllEnum.StateEnum.End; //이전상태 체크위함
    AllEnum.StateEnum NowState = AllEnum.StateEnum.Idle; //이전상태 체크위함
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StateDic.Add(AllEnum.StateEnum.Chase, new State_Pet_Chase(this));
        StateDic.Add(AllEnum.StateEnum.Idle, new State_Pet_Idle(this));

        SetState(AllEnum.StateEnum.Idle);
    }
    
    void Update()
    {
        //태어나자마자 바로 하지않도록 bool같은 변수로 제어하는것도 좋음..
        if (ExState == NowState)
        {
            StateDic[NowState].OnStateStay();
        }
    }

    public void SetState(AllEnum.StateEnum _enum)
    {
        NowState = _enum;        
        if (ExState != NowState)
        {
            if (ExState != AllEnum.StateEnum.End)
                StateDic[ExState].OnStateExit();

            StateDic[NowState].OnStateEnter();
            ExState = NowState;
        }
    }

    public void SetDestination(Vector3 vec)
    {
        agent.isStopped = false;
        agent.SetDestination(vec);
    }

    public void Stop()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }
}
