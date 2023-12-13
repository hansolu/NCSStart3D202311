using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Pet_Chase : State_Pet
{

    public State_Pet_Chase(PetStateMachine owner):base(owner)
    {
    }
    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateStay()
    {
        //조건체크...        
        //2이하가 되면 idle상태로 전환하기
        //return;
        //그게 아니면
        //플레이어위치로 계속 이동...
        //=> 최소한 플레이어 위치를 가지고있던지 혹은
        //PetStateMachine 여기 스크립트에 플레이어 위치로 이동하는 함수가 있던지...
    }
}
