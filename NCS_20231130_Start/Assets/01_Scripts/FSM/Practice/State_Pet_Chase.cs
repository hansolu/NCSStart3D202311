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
        //�װ� �ƴϸ�
        //�÷��̾���ġ�� ��� �̵�...
        //=> �ּ��� �÷��̾� ��ġ�� �������ִ��� Ȥ��
        //PetStateMachine ���� ��ũ��Ʈ�� �÷��̾� ��ġ�� �̵��ϴ� �Լ��� �ִ���...
    }
}
