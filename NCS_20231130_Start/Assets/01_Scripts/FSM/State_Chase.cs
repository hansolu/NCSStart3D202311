using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������ �÷��̾ �ѾƼ� ������.
public class State_Chase : State
{
    Vector3 TargetPos = Vector3.zero;
    public State_Chase(Enemy enemy, SetStateDel StateDel) : base(enemy, StateDel)
    {
    }

    public override void OnStateEnter()
    {
        //�� �������� �÷��̾� ��ġ�� �����ϰ�
        //�װ��� ���� ������
        //������ �÷��̾ �Ѱ� �ϰ�ʹٸ�
        //�÷��̾��� ��ġ�� �ѹ� �޾ƿͼ�
        //�װ� �������� ������
        TargetPos = enemy.TargetTr.position;
        enemy.Move(TargetPos); //�׺� ����
    }

    public override void OnStateExit()
    {
        TargetPos = Vector3.zero;
        enemy.TargetTr = null;
    }

    public override void OnStateStay()
    {
        //�÷��̾���� ������ �����ϱ⿡ ����� �Ÿ��� �Ǿ��ٸ�
        if (enemy.CheckSight(enemy.AttackRange))
        {
            StateDel(AllEnum.StateEnum.Attack);
            return;
        }
        else
        {
            //�������� ������. �����ϴ¶����� ���ݰŸ��ȿ� ����� ������...

            //�׷��� �Ͼ��°Ŵϱ�, patrol���·� ���ư�...
            if ((TargetPos - enemy.transform.position).sqrMagnitude <=1 )
            {
                StateDel(AllEnum.StateEnum.Patrol);
                return;
            }
        }        
    }
}
