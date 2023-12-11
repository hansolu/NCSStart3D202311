using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//patrol�� SetStateDel ��������Ʈ��
//StateMachine�� SetState�̰�

//��Ʈ�� �ڽ����� ���Ե� Idle�� Walk Ŭ������
//SetStateDel�� �ҷ����� Patrol�� SetStateInPatrol�� �����ϰ� ��.
public class State_Patrol : State //Idle�� walk���·� �갡 �Ǻ��ؼ� �ٲ���..
{
    //�������� ���Ƽ� ������ �� �������� ��ųʸ� �����ŷ� �����ص��ǰ�
    //���� ��� �׳� ������ �ϳ��� �ص��ɰ�...
    Dictionary<AllEnum.StateEnum, State> StateDic = new Dictionary<AllEnum.StateEnum, State>();
    //State state_idle;
    //State state_walk;
    AllEnum.StateEnum ex_patrolState;//��������
    AllEnum.StateEnum patrolState;//�������
    public State_Patrol(Enemy enemy, SetStateDel StateDel) :base(enemy, StateDel)
    {        
        //��ư �����ư� �����Ҷ� ��׵��� ������ �־����...
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

        //Debug.Log($"setstate�̰� Exstate = {ex_patrolState} / nowState ={patrolState}");
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
