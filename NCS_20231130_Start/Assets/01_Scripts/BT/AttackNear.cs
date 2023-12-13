using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackNear : Node
{
    //추가된 방법...
    // 기존 enemy에 AttackNear함수가
    //자체적으로 코루틴을 가지고 있으므로
    //사실 나는 매프레임 AttackNear불러도 큰 위험이 없음..

    //그래서 자체 AttackNear함수를 매프레임 부른다면,
    //float time도 필요없고 Enemy전체를 가지고 있을 필요도없고
    //=> 델리게이트로 해당 AttackNear함수만 받아서 가지고 있어도됨...

    Enemy owner;
    float time = 0; //카운트용    
    public AttackNear( Enemy owner/*델리게이트 함수 받는것도 가능한 상태.*/ )
    {
        this.owner = owner;
        time = owner.NearAttackDelayTime;
    }

    //얘도 CheckSight처럼 
    //이안에다가 코드를 구현을 하던지
    //(즉 대상의 조건에 따라 필요 정보만 달리주면 공용으로 쓸수있게 만들던지
    //혹은 그냥 Enemy안의 코드를 그대로 쓰던지...
    public override NodeState Evaluate() //이게 update에서 불리는것과 같은데
    {
        owner.Idle();
        //1번방법 enemy스크립트에 붙어있던 함수를 이용함
        //(함수내부에 코루틴 존재)
        owner.AttackNear(true);
        return NodeState.Success;

        //2번방법 - deltatime을 이용함
        //공격 한번실행위함
        if (time >= owner.NearAttackDelayTime)
        {
            time = 0;
            owner.AttackNear(true); 

            return NodeState.Running;
        }

        time += Time.deltaTime;
        //근거리 공격 실행.
        return NodeState.Success;
    }
}