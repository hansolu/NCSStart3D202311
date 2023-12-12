using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Attack : State
{
    AllEnum.StateEnum ex_State;//��������
    AllEnum.StateEnum State;//�������
    Dictionary<AllEnum.StateEnum, State> StateDic = new Dictionary<AllEnum.StateEnum, State>();
    Vector3 vec = Vector3.zero;


    //###### ���⼭ ��Ÿ�� üũ�ϴ� �����
    //�ڷ�ƾ�� �̿��ؼ� �� ��ų���� ��Ÿ���� ������'
    //�������¸� �� �Ұ����� �ϵ����ϴ� ���....

    //���� attack���� ��Ÿ�ӵ����鼭, �������µ� �����ϰ�
    //�ش� �� ��ũ��Ʈ(state_attack_near / far) ������
    //�ڷ�ƾ���� ��Ÿ���� �����ϰ��־
    //�ణ�� �����̰� ���� �� ����.

    float time = 0; //��Ÿ�� ī����
    float coolTime = 0; //��Ÿ�ӿ�.

    //float[] delaytime; //�Ŀ� ��ų���� ������Ÿ���� ���⼭ ����ִ���
    ////Ȥ�� �̰͵� �� Enemy��ũ��Ʈ�� ����ִ���.
        
    public State_Attack(Enemy enemy, SetStateDel StateDel) : base(enemy, StateDel)
    {
        StateDic.Add(AllEnum.StateEnum.Attack_Near, new State_Attack_Near(enemy, SetStateInAttack));
        StateDic.Add(AllEnum.StateEnum.Attack_Far, new State_Attack_Far(enemy, SetStateInAttack));
    }
    public override void OnStateEnter()
    {
        //�Ÿ� �Ǻ��ؼ�, ������ ����� ����, �ָ�, �� ����
        ex_State = AllEnum.StateEnum.End;

        //�Ÿ��Ǻ��Ͽ� ���������Ұ����� ���Ÿ� �����Ұ����� ���Ұ���
        if (/*enemy.TargetTr == null*/
            enemy.CheckSight(enemy.ViewDistance)==false) //���ݻ��� �������� �þ�üũ �ѹ� �� �ϱ�
        {
            StateDel(AllEnum.StateEnum.Patrol);
        }
        else
        {
            SetNextState(); //�����Ǻ�

            time = coolTime; //�������ڸ��� ������ �ѹ� �ϱ� ����.
        }
    }

    public override void OnStateExit()
    {
        StateDic[ex_State].OnStateExit(); //�������� ����������        
    }

    public override void OnStateStay()
    {
        if (enemy.TargetTr !=null)
        {
            vec = enemy.TargetTr.position;
            vec.y = enemy.transform.position.y;
        }
        else //����̾���
        {
            //����̾����ϱ� ������ ���ư�
            StateDel(AllEnum.StateEnum.Patrol);
            return;
        }

        enemy.transform.LookAt(vec);//Ÿ�� ��ġ ������ �ٶ󺸱�~ �ؼ�
        //�˾Ƽ� ȸ���ؼ� �Ĵٺ��� ���°� ��.


        if (time >= coolTime)
        {                        
            //�������¿� ���ݻ��°� ������� ����
            if (ex_State == State)
                StateDic[State].OnStateStay();

            time = 0;
            SetNextState(); //���� ���� �Ǻ�
        }

        time += Time.deltaTime;
    }


    public void SetStateInAttack(AllEnum.StateEnum _enum)
    {
        State = _enum;
        Debug.Log($"setstate�̰� Exstate = {ex_State} / nowState ={State}");
        if ( State != AllEnum.StateEnum.Attack_Near
            &&  State != AllEnum.StateEnum.Attack_Far)
        {
            StateDel(_enum);
            return;
        }

        if (ex_State !=  State)
        {
            if (ex_State != AllEnum.StateEnum.End)
                StateDic[ex_State].OnStateExit();

            StateDic[State].OnStateEnter();
            ex_State =  State;
        }
    }

    void SetNextState()
    {                
        //���� '��'(enemy)�� �Ÿ�����
        float dist = Vector3.Distance(enemy.TargetTr.position,
            enemy.transform.position);
        Debug.Log("Ÿ�ٰ��� �Ÿ��� : " + dist);
        //���� attackRange���̶��, ��������
        if (dist <= enemy.AttackRange)
        {            
            coolTime = enemy.NearAttackDelayTime;
            State = AllEnum.StateEnum.Attack_Near;
            SetStateInAttack(AllEnum.StateEnum.Attack_Near);
        }
        else if (dist <= enemy.ViewDistance)
        {            
            coolTime = enemy.FarAttackDelayTime;
            State = AllEnum.StateEnum.Attack_Far;
            SetStateInAttack(AllEnum.StateEnum.Attack_Far);
        }
        else
        {
            //���� ���� ���� �Ѿư����ִ� �������� ���� �����Ƿ�, 
            //�ϻ���·� ���ư�.
            StateDel(AllEnum.StateEnum.Chase);
        }
    }
}
