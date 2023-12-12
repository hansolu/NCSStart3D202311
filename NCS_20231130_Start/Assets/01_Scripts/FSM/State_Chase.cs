using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������ �÷��̾ �ѾƼ� ������.
public class State_Chase : State
{
    Vector3 TargetPos = Vector3.zero;
    float dist = 0;
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
        //enemy.TargetTr = null;
        //�߰��ϴ���� �޸��ٰ�
        //Exit�ϸ� �ϴ� ����...���״�...
        enemy.Idle(); //�����ϱ�~
    }

    public override void OnStateStay()
    {
        //�÷��̾���� ������ �����ϱ⿡ ����� �Ÿ��� �Ǿ��ٸ�
        if (enemy.CheckSight(enemy.AttackRange))
        {
            StateDel(AllEnum.StateEnum.Attack);//���ݻ��·� ��ȯ
            return;
        }
        else
        {
            dist = (TargetPos - enemy.transform.position).sqrMagnitude;
            //�������� ������. �����ϴ¶����� ���ݰŸ��ȿ� ����� ������...

            //�׷��� �Ͼ��°Ŵϱ�, patrol���·� ���ư�...
            if (dist <= 1) //���������� ������ ���
            {
                StateDel(AllEnum.StateEnum.Patrol);//�������·� ��ȯ
                return;
            }
            else if (dist > enemy.ViewDistance) //�߰��ϱ⿡�� �ʹ� �հŸ�
            {
                StateDel(AllEnum.StateEnum.Patrol);//�������·� ��ȯ
                return;
            }
        }        
    }
}
