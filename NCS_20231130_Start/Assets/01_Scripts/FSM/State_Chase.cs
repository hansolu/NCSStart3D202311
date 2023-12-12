using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//무조건 플레이어를 쫓아서 갈것임.
public class State_Chase : State
{
    Vector3 TargetPos = Vector3.zero;
    float dist = 0;
    public State_Chase(Enemy enemy, SetStateDel StateDel) : base(enemy, StateDel)
    {
    }

    public override void OnStateEnter()
    {
        //내 목적지를 플레이어 위치로 설정하고
        //그곳을 향해 움직임
        //무조건 플레이어만 쫓게 하고싶다면
        //플레이어의 위치를 한번 받아와서
        //그걸 목적지로 세팅함
        TargetPos = enemy.TargetTr.position;
        enemy.Move(TargetPos); //네비 세팅
    }

    public override void OnStateExit()
    {
        TargetPos = Vector3.zero;
        //enemy.TargetTr = null;
        //추격하느라고 달리다가
        //Exit하면 일단 정지...할테니...
        enemy.Idle(); //정지하기~
    }

    public override void OnStateStay()
    {
        //플레이어와의 간격이 공격하기에 충분한 거리가 되었다면
        if (enemy.CheckSight(enemy.AttackRange))
        {
            StateDel(AllEnum.StateEnum.Attack);//공격상태로 변환
            return;
        }
        else
        {
            dist = (TargetPos - enemy.transform.position).sqrMagnitude;
            //목적지에 도착함. 도착하는때까지 공격거리안에 사람이 없었음...

            //그러면 일없는거니까, patrol상태로 돌아감...
            if (dist <= 1) //도착지점에 도착한 경우
            {
                StateDel(AllEnum.StateEnum.Patrol);//순찰상태로 변환
                return;
            }
            else if (dist > enemy.ViewDistance) //추격하기에도 너무 먼거리
            {
                StateDel(AllEnum.StateEnum.Patrol);//순찰상태로 변환
                return;
            }
        }        
    }
}
