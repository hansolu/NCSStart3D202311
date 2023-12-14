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
        //5�̻��̸� �Ѿư�����·� ��ȯ
        //==> ���� ��ġ, ������ ��ġ.
        if ((owner.TargetTr.position - owner.transform.position).sqrMagnitude >= 25)
        {
            owner.SetState(AllEnum.StateEnum.Chase);
            return;
        }        
    }
}
