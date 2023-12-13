using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : Node
{
    Transform transform;
    Transform PlayerTr;
    Vector3 vec = Vector3.zero;
    public FollowPlayer(Transform transform, Transform playerTr)
    {
        this.transform = transform;
        this.PlayerTr = playerTr;
    }
    public override NodeState Evaluate()
    {
        vec = PlayerTr.position - transform.position;
        transform.Translate(vec.normalized * Time.deltaTime * 5);
        return NodeState.Success;
    }
}
