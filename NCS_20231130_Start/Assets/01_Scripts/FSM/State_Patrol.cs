using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//patrol의 SetStateDel 델리게이트는
//StateMachine의 SetState이고

//패트롤 자식으로 들어가게된 Idle과 Walk 클래스는
//SetStateDel을 불렀을때 Patrol의 SetStateInPatrol를 실행하게 됨.
public class State_Patrol : State //Idle과 walk상태로 얘가 판별해서 바꿔줌..
{
    //가짓수가 많아서 관리가 좀 귀찮으면 딕셔너리 같은거로 선언해도되고
    //수가 적어서 그냥 변수로 하나씩 해도될것...
    Dictionary<AllEnum.StateEnum, State> StateDic = new Dictionary<AllEnum.StateEnum, State>();
    //State state_idle;
    //State state_walk;
    AllEnum.StateEnum ex_patrolState;//이전상태
    AllEnum.StateEnum patrolState;//현재상태
    public State_Patrol(Enemy enemy, SetStateDel StateDel) :base(enemy, StateDel)
    {        
        //여튼 뭐가됐건 시작할때 얘네들의 정보가 있어야함...
        //state_idle = new State_Idle(enemy, StateDel);
        //state_walk = new State_Walk(enemy, StateDel);
        StateDic.Add(AllEnum.StateEnum.Idle, new State_Idle(enemy, SetStateInPatrol));
        StateDic.Add(AllEnum.StateEnum.Walk, new State_Walk(enemy, SetStateInPatrol));
    }

    public override void OnStateEnter()
    {
        ex_patrolState = AllEnum.StateEnum.End;
        patrolState = AllEnum.StateEnum.Idle;  
        SetStateInPatrol(AllEnum.StateEnum.Idle);
    }

    public override void OnStateExit()
    {
        StateDic[ex_patrolState].OnStateExit();
    }

    public override void OnStateStay()
    {
        if (ex_patrolState == patrolState)
            StateDic[patrolState].OnStateStay();
    }

    public void SetStateInPatrol(AllEnum.StateEnum _enum)
    {
        patrolState = _enum;

        //Debug.Log($"setstate이고 Exstate = {ex_patrolState} / nowState ={patrolState}");
        if (patrolState != AllEnum.StateEnum.Idle 
            && patrolState != AllEnum.StateEnum.Walk)
        {
            StateDel(_enum);
            return;
        }
                
        if (ex_patrolState != patrolState)
        {
            if (ex_patrolState != AllEnum.StateEnum.End)
                StateDic[ex_patrolState].OnStateExit();

            StateDic[patrolState].OnStateEnter();
            ex_patrolState = patrolState;
        }
    }
}
