using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIsPlayerNear : Node
{
    Transform transform;
    Transform PlayerTr;
    float dist = 0;
    public CheckIsPlayerNear(Transform transform, Transform playerTr)
    {
        this.transform = transform;
        this.PlayerTr = playerTr;
    }
    public override NodeState Evaluate()
    {
        dist = Vector3.Distance(PlayerTr.position, transform.position);
        if (dist >= 5)
        {            
            return state= NodeState.Success;
        }
        else if (dist <= 2)
        {
            return state = NodeState.Failure;
        }
        else
        {
            if (state != NodeState.Failure)
            {
                return state = NodeState.Running;
            }
            else
                return NodeState.Failure;
        }        
    }
}
