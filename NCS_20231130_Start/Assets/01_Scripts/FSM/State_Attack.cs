using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Attack : State
{
    AllEnum.StateEnum ex_State;//이전상태
    AllEnum.StateEnum State;//현재상태
    Dictionary<AllEnum.StateEnum, State> StateDic = new Dictionary<AllEnum.StateEnum, State>();
    Vector3 vec = Vector3.zero;


    //###### 여기서 쿨타임 체크하는 방법과
    //코루틴을 이용해서 각 스킬에서 쿨타임이 끝나면'
    //다음상태를 뭘 할것인지 하도록하는 방법....

    //현재 attack에서 쿨타임돌리면서, 다음상태도 관리하고
    //해당 각 스크립트(state_attack_near / far) 에서도
    //코루틴으로 쿨타임을 관리하고있어서
    //약간의 딜레이가 있을 수 있음.

    float time = 0; //쿨타임 카운터
    float coolTime = 0; //쿨타임용.

    //float[] delaytime; //후에 스킬별로 딜레이타임을 여기서 들고있던지
    ////혹은 이것도 다 Enemy스크립트에 들려주던지.
        
    public State_Attack(Enemy enemy, SetStateDel StateDel) : base(enemy, StateDel)
    {
        StateDic.Add(AllEnum.StateEnum.Attack_Near, new State_Attack_Near(enemy, SetStateInAttack));
        StateDic.Add(AllEnum.StateEnum.Attack_Far, new State_Attack_Far(enemy, SetStateInAttack));
    }
    public override void OnStateEnter()
    {
        //거리 판별해서, 가까우면 가까운 공격, 멀면, 먼 공격
        ex_State = AllEnum.StateEnum.End;

        //거리판별하여 근접공격할것인지 원거리 공격할것인지 정할것임
        if (/*enemy.TargetTr == null*/
            enemy.CheckSight(enemy.ViewDistance)==false) //공격상태 시작전에 시야체크 한번 더 하기
        {
            StateDel(AllEnum.StateEnum.Patrol);
        }
        else
        {
            SetNextState(); //상태판별

            time = coolTime; //시작하자마자 공격을 한번 하기 위함.
        }
    }

    public override void OnStateExit()
    {
        StateDic[ex_State].OnStateExit(); //이전상태 마무리짓기        
    }

    public override void OnStateStay()
    {
        if (enemy.TargetTr !=null)
        {
            vec = enemy.TargetTr.position;
            vec.y = enemy.transform.position.y;
        }
        else //대상이없음
        {
            //대상이없으니까 순찰로 돌아감
            StateDel(AllEnum.StateEnum.Patrol);
            return;
        }

        enemy.transform.LookAt(vec);//타겟 위치 쪽으로 바라보기~ 해서
        //알아서 회전해서 쳐다보는 상태가 됨.


        if (time >= coolTime)
        {                        
            //이전상태와 지금상태가 같을경우 실행
            if (ex_State == State)
                StateDic[State].OnStateStay();

            time = 0;
            SetNextState(); //다음 공격 판별
        }

        time += Time.deltaTime;
    }


    public void SetStateInAttack(AllEnum.StateEnum _enum)
    {
        State = _enum;
        Debug.Log($"setstate이고 Exstate = {ex_State} / nowState ={State}");
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
        //대상과 '나'(enemy)의 거리차이
        float dist = Vector3.Distance(enemy.TargetTr.position,
            enemy.transform.position);
        Debug.Log("타겟과의 거리차 : " + dist);
        //나의 attackRange안이라면, 근접공격
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
            //내가 공격 내지 쫓아갈수있는 범위내에 적이 없으므로, 
            //일상상태로 돌아감.
            StateDel(AllEnum.StateEnum.Chase);
        }
    }
}
