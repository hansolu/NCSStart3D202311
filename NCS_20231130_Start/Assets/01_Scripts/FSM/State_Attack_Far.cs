using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Attack_Far : State
{
    //float time = 0;
    public State_Attack_Far(Enemy enemy, SetStateDel StateDel) : base(enemy, StateDel)
    {
    }
    //원거리 공격을 시작해야할 상태가 되었음
    public override void OnStateEnter()
    {
        enemy.SetWeaponOn(true, true); //원거리 무기 장착
    }
    
    //원거리 공격을 이제 하지 않을 것임.
    public override void OnStateExit()
    {
        enemy.Shoot(false);
        enemy.SetWeaponOn(true, false); //원거리 무기 장착해제        
    }

    //이미 공격을 진행했을 수도있고, 여튼 시작준비가 끝난후임
    public override void OnStateStay()
    {
        //일정시간마다
        //1번 deltatime 누적더해서 체크
        //if (time> enemy.FarAttackDelayTime)
        //{
        //    time = 0;
        //    //총쏘기
        //}
        //time += Time.deltaTime;

        //2번 coroutine / invoke로 체크
        //=>2번의 경우 해당기능이 Monobehavior 안에 있는데,
        //현재 State 클래스가 Monobehavior를 상속받고 있지 않기때문에
        //'이 안에서는' 구현할 수 없음.

        enemy.Shoot(true);
    }
}
