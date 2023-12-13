using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Pet��ũ��Ʈ ���� �δ°� �ƴϰ� PetStateMachine ��ũ��Ʈ �ϳ��� �ذẼ���
public class PetStateMachine : MonoBehaviour
{
    Dictionary<AllEnum.StateEnum, State_Pet> StateDic = new Dictionary<AllEnum.StateEnum, State_Pet>();
    AllEnum.StateEnum ExState; //�������� üũ����
    AllEnum.StateEnum NowState; //�������� üũ����
    void Start()
    {
        StateDic.Add(AllEnum.StateEnum.Chase, new State_Pet_Chase(this));
        StateDic.Add(AllEnum.StateEnum.Idle, new State_Pet_Idle(this));
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
}
