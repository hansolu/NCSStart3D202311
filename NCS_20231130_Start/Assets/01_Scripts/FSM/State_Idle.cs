using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Idle : State
{    
    float currentTime = 0; //현재 카운팅하고있는 시간.
    float nextTime = 0; //다음으로 넘어가야하는 시간
    
    public State_Idle(Enemy enemy, SetStateDel StateDel) :base(enemy, StateDel)
    {         
    }
    public override void OnStateEnter()
    {        
        currentTime = 0;
        nextTime = Random.Range(1f, 2f);
        //내 상태는 이제, 움직이지 않고 가만히 있는것
        //단, 일정시간이 지나면 움직이는 상태로 변할것..
        //enemy.가만히 있는 애니메이션 출력(); //함수 부르면 됨
        //+ 움직임 멈추기.=> enemy의 이동멈추는 함수 콜        
        enemy.Idle(); //애니메이션 출력과 실제로 navi멈추는 기능까지 포함.
        //1-1의 방법이라면 이때 invoke든 코루틴이든 부르면 될것.
        //근데 우리의 지금 상황은 monobehavior를 상속받고 있지 않기때문에
    }

    public override void OnStateExit()
    {
        //그럼 이상태를 나갈떄, 내가 수정하던 내용들은, Time관련이므로
        //시간 코루틴이었다면, 혹시모르니 코루틴 완전히 정지하고, 코루틴 null로 정리해주기
        //또는 update에서 시간을 정리중이었다면, time =0; 초기화 해주기...
        currentTime = 0;
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
            

        if (currentTime >= nextTime) //다음으로 넘어갈 시간이 되었다면
        {            
            StateDel(AllEnum.StateEnum.Walk);

            //에너미 스크립트 안의 statemachine에 접근해서
            //setstate를 부름.. 으로 변경 가능.
            //Enemy.StateMachine.SetState() //를 부름
            return;
        }

        currentTime += Time.deltaTime;
        //1 시간체크
        //1-1=> Enter에서 Invoke또는 코루틴으로 일정시간후에 무조건 Walk상태로 넘어가도록 함
        //1-2=> 이 Stay가 Update에서 불리는 것이므로, Time.deltaTime같은것을 더해서
        // 일정시간이 지났다면, 다음상태로 바꿔라 라고 할 수 있을것.
    }        
   

}
