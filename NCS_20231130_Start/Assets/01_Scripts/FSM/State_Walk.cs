using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//무조건 움직이고 있는 상태.
public class State_Walk : State
{
    Vector3 goalPos = Vector3.zero;
    int pattrolNum = 0; //
    public State_Walk(Enemy enemy, SetStateDel StateDel) : base(enemy, StateDel)
    {
    }
    public override void OnStateEnter()
    {
        //목적지 세팅
        //목적지로 이동 //네비게이션을 이용한다면, 이때 한번만 세팅해도 충분함
        goalPos = GameManager.Instance.GetNextPatrol(enemy.PatterParentNum,
            ref pattrolNum, true);

        //실제로 움직여라 도 시키기
        enemy.Move(goalPos); //네비 세팅을 한것이고
    }

    public override void OnStateExit()
    {
        //뭔가 불안하다면, 애니메이션 걷던거 멈추는 작업들을 여기서 하면 될것.

    }

    public override void OnStateStay()
    {
        //만약 플레이어가 시야에 걸리면 
        //추격상태로 바꿈
        if (enemy.CheckSight(enemy.ViewDistance))
        {
            StateDel(AllEnum.StateEnum.Chase);
            return;
        }

        //1번 transform.translate같은것을 한다
        //Navi를 이용한다면, update때마다 계속 navigation을 부를 필요가 없음

        //도착했는지 여부.
        //도착했다면 멈춰서기 == idle 을 부름.

        if ( (enemy.transform.position - goalPos).sqrMagnitude < 1f)
        {
            StateDel(AllEnum.StateEnum.Idle);
            return;
        }

        //방향 애니메이션을 가졌기떄문에, 방향이 바뀔때마다 모션이 바뀌도록
        //stay에서 계속 콜링함.
        enemy.SetMoveAnim(); //실시간으로 애니메이션 콜링을 계속함.
    }
}
