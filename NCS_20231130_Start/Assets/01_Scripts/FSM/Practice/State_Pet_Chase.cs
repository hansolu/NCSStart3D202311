using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Pet_Chase : State_Pet
{

    public State_Pet_Chase(PetStateMachine owner):base(owner)
    {
    }
    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateStay()
    {
        //����üũ...        
        //2���ϰ� �Ǹ� idle���·� ��ȯ�ϱ�
        //return;
        if ((owner.TargetTr.position - owner.transform.position).sqrMagnitude <= 4)
        {
            owner.SetState(AllEnum.StateEnum.Idle);
            return;
        }
        //�װ� �ƴϸ�
        //�÷��̾���ġ�� ��� �̵�...

        owner.SetDestination(owner.TargetTr.position);

        //=> �ּ��� �÷��̾� ��ġ�� �������ִ��� Ȥ��
        //PetStateMachine ���� ��ũ��Ʈ�� �÷��̾� ��ġ�� �̵��ϴ� �Լ��� �ִ���...
    }
}
