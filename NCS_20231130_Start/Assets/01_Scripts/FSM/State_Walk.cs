using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������ �����̰� �ִ� ����.
public class State_Walk : State
{
    Vector3 goalPos = Vector3.zero;
    int pattrolNum = 0; //
    public State_Walk(Enemy enemy, SetStateDel StateDel) : base(enemy, StateDel)
    {
    }
    public override void OnStateEnter()
    {
        //������ ����
        //�������� �̵� //�׺���̼��� �̿��Ѵٸ�, �̶� �ѹ��� �����ص� �����
        goalPos = GameManager.Instance.GetNextPatrol(enemy.PatterParentNum,
            ref pattrolNum, true);

        //������ �������� �� ��Ű��
        enemy.Move(goalPos); //�׺� ������ �Ѱ��̰�
    }

    public override void OnStateExit()
    {
        //���� �Ҿ��ϴٸ�, �ִϸ��̼� �ȴ��� ���ߴ� �۾����� ���⼭ �ϸ� �ɰ�.

    }

    public override void OnStateStay()
    {
        //���� �÷��̾ �þ߿� �ɸ��� 
        //�߰ݻ��·� �ٲ�
        if (enemy.CheckSight(enemy.ViewDistance))
        {
            StateDel(AllEnum.StateEnum.Chase);
            return;
        }

        //1�� transform.translate�������� �Ѵ�
        //Navi�� �̿��Ѵٸ�, update������ ��� navigation�� �θ� �ʿ䰡 ����

        //�����ߴ��� ����.
        //�����ߴٸ� ���缭�� == idle �� �θ�.

        if ( (enemy.transform.position - goalPos).sqrMagnitude < 1f)
        {
            StateDel(AllEnum.StateEnum.Idle);
            return;
        }

        //���� �ִϸ��̼��� �����⋚����, ������ �ٲ𶧸��� ����� �ٲ��
        //stay���� ��� �ݸ���.
        enemy.SetMoveAnim(); //�ǽð����� �ִϸ��̼� �ݸ��� �����.
    }
}
