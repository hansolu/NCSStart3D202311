using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : Node
{
    Enemy owner;
    public Chase(Enemy owner)
    {
        this.owner = owner;
    }

    public override NodeState Evaluate()
    {
        owner.Move(owner.TargetTr.position, true);
        return NodeState.Success;
    }
}
