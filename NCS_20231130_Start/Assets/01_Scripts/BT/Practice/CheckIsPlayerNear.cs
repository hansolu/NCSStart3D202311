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
        //3가지. 
        // (PlayerTr.position - transform.position).sqrmagnitude ==거리*거리
        //위의것은 빠르지만 부정확함...

        dist = Vector3.Distance(PlayerTr.position, transform.position);
        if (dist >= 5)
        {            
            return state= NodeState.Success;
        }
        else if (dist <= 2)
        {
            return state = NodeState.Failure;
        }
        else //거리가 2~5사이면....
        {
            if (state != NodeState.Failure)
            {
                return state = NodeState.Running;
            }
            else //기존 상태가 Failure 즉 2이하였던 상황이었고
                //뭔가 2보다 거리가 더 멀어졌지만
                //5이하일때...
                return NodeState.Failure;
        }        
    }
}
