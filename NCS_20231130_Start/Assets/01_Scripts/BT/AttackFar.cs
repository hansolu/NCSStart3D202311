using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFar : Node
{
    //�Լ��� ���޹޾Ƽ� ������
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