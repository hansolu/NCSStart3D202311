using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Pet��ũ��Ʈ ���� �δ°� �ƴϰ� PetStateMachine ��ũ��Ʈ �ϳ��� �ذẼ���
public class PetStateMachine : MonoBehaviour
{
    public Transform TargetTr; //������ transform
    NavMeshAgent agent;
    public NavMeshAgent Agent => agent;

    Dictionary<AllEnum.StateEnum, State_Pet> StateDic = new Dictionary<AllEnum.StateEnum, State_Pet>();
    AllEnum.StateEnum ExState = AllEnum.StateEnum.End; //�������� üũ����
    AllEnum.StateEnum NowState = AllEnum.StateEnum.Idle; //�������� üũ����
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StateDic.Add(AllEnum.StateEnum.Chase, new State_Pet_Chase(this));
        StateDic.Add(AllEnum.StateEnum.Idle, new State_Pet_Idle(this));

        SetState(AllEnum.StateEnum.Idle);
    }
    
    void Update()
    {
        //�¾�ڸ��� �ٷ� �����ʵ��� bool���� ������ �����ϴ°͵� ����..
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
