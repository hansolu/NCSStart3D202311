using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFar : Node
{
    //함수만 전달받아서 실행함
    public delegate void AttackFunc(bool isstart);
    AttackFunc attack;
    public AttackFar(AttackFunc attack)
    {
        this.attack = attack;
    }
    public override NodeState Evaluate()
    {        
        attack(true);//owner.Shoot(true);
        return NodeState.Success;
    }
}