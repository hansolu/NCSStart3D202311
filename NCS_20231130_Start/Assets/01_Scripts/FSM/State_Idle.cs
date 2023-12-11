using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Idle : State
{    
    float currentTime = 0; //���� ī�����ϰ��ִ� �ð�.
    float nextTime = 0; //�������� �Ѿ���ϴ� �ð�
    
    public State_Idle(Enemy enemy, SetStateDel StateDel) :base(enemy, StateDel)
    {         
    }
    public override void OnStateEnter()
    {        
        currentTime = 0;
        nextTime = Random.Range(1f, 2f);
        //�� ���´� ����, �������� �ʰ� ������ �ִ°�
        //��, �����ð��� ������ �����̴� ���·� ���Ұ�..
        //enemy.������ �ִ� �ִϸ��̼� ���(); //�Լ� �θ��� ��
        //+ ������ ���߱�.=> enemy�� �̵����ߴ� �Լ� ��        
        enemy.Idle(); //�ִϸ��̼� ��°� ������ navi���ߴ� ��ɱ��� ����.
        //1-1�� ����̶�� �̶� invoke�� �ڷ�ƾ�̵� �θ��� �ɰ�.
        //�ٵ� �츮�� ���� ��Ȳ�� monobehavior�� ��ӹް� ���� �ʱ⶧����
    }

    public override void OnStateExit()
    {
        //�׷� �̻��¸� ������, ���� �����ϴ� �������, Time�����̹Ƿ�
        //�ð� �ڷ�ƾ�̾��ٸ�, Ȥ�ø𸣴� �ڷ�ƾ ������ �����ϰ�, �ڷ�ƾ null�� �������ֱ�
        //�Ǵ� update���� �ð��� �������̾��ٸ�, time =0; �ʱ�ȭ ���ֱ�...
        currentTime = 0;
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
            

        if (currentTime >= nextTime) //�������� �Ѿ �ð��� �Ǿ��ٸ�
        {            
            StateDel(AllEnum.StateEnum.Walk);

            //���ʹ� ��ũ��Ʈ ���� statemachine�� �����ؼ�
            //setstate�� �θ�.. ���� ���� ����.
            //Enemy.StateMachine.SetState() //�� �θ�
            return;
        }

        currentTime += Time.deltaTime;
        //1 �ð�üũ
        //1-1=> Enter���� Invoke�Ǵ� �ڷ�ƾ���� �����ð��Ŀ� ������ Walk���·� �Ѿ���� ��
        //1-2=> �� Stay�� Update���� �Ҹ��� ���̹Ƿ�, Time.deltaTime�������� ���ؼ�
        // �����ð��� �����ٸ�, �������·� �ٲ�� ��� �� �� ������.
    }        
   

}
