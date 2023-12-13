using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDistance : Node
{            
    Enemy owner;
    float dist = 0;
    public CheckDistance(Enemy owner)
    {
        this.owner = owner;
        childrenNode.Add(new AttackNear(owner));
        childrenNode.Add(new AttackFar(owner.Shoot));//델리게이트 방법 써본것
        childrenNode.Add(new Chase(owner));        
    }

    //상태를 강제한다면 BT라고 보기 어려움...
    //조건에 따라 바로바로 바뀔수있는게 BT의 특징...
    public override NodeState Evaluate()
    {
        if (owner.TargetTr == null)
        {            
            return NodeState.Failure;
        }
        //플레이어쪽을 쳐다보도록 수정


        dist = Vector3.Distance(owner.TargetTr.position,
            owner.transform.position);

        if (dist <= owner.AttackRange)
        {
            //########디버깅 위함
            owner.NowState = AllEnum.StateEnum.Attack_Near;
            childrenNode[0].Evaluate(); //근거리 공격
        }
        else if (dist <= owner.AttackFarRange)
        {
            //########디버깅 위함
            owner.NowState = AllEnum.StateEnum.Attack_Far;
            childrenNode[1].Evaluate(); //원거리 공격
        }
        else
        {
            //위의 한줄은 사실 밑에 세줄이 필요하기때문.
            //BT로 만들거면 FSM과 달리 AttackCoroutine의 시작과 끝을
            //다른함수로 나눠야 할것
            //if (AttackCor != null)            
            //StopCoroutine(AttackCor);
            //AttackCor = null;

            owner.SetWeaponOff();
            //########디버깅 위함
            owner.NowState = AllEnum.StateEnum.Chase;
            childrenNode[2].Evaluate(); //추격
        }
        return NodeState.Success;
    }
}