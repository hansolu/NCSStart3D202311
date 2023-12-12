using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Attack_Far : State
{
    //float time = 0;
    public State_Attack_Far(Enemy enemy, SetStateDel StateDel) : base(enemy, StateDel)
    {
    }
    //���Ÿ� ������ �����ؾ��� ���°� �Ǿ���
    public override void OnStateEnter()
    {
        enemy.SetWeaponOn(true, true); //���Ÿ� ���� ����
    }
    
    //���Ÿ� ������ ���� ���� ���� ����.
    public override void OnStateExit()
    {
        enemy.Shoot(false);
        enemy.SetWeaponOn(true, false); //���Ÿ� ���� ��������        
    }

    //�̹� ������ �������� �����ְ�, ��ư �����غ� ��������
    public override void OnStateStay()
    {
        //�����ð�����
        //1�� deltatime �������ؼ� üũ
        //if (time> enemy.FarAttackDelayTime)
        //{
        //    time = 0;
        //    //�ѽ��
        //}
        //time += Time.deltaTime;

        //2�� coroutine / invoke�� üũ
        //=>2���� ��� �ش����� Monobehavior �ȿ� �ִµ�,
        //���� State Ŭ������ Monobehavior�� ��ӹް� ���� �ʱ⶧����
        //'�� �ȿ�����' ������ �� ����.

        enemy.Shoot(true);
    }
}
