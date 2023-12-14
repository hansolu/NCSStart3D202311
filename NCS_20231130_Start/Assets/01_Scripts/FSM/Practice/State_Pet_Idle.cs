using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Pet_Idle : State_Pet
{    
    public State_Pet_Idle(PetStateMachine owner) : base(owner)
    {        
    }
    public override void OnStateEnter()
    {
        owner.Stop();
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateStay()
    {
        //5이상이면 쫓아가기상태로 전환
        //==> 나의 위치, 상대방의 위치.
        if ((owner.TargetTr.position - owner.transform.position).sqrMagnitude >= 25)
        {
            owner.SetState(AllEnum.StateEnum.Chase);
            return;
        }        
    }
}
