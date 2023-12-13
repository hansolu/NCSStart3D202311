using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : Node
{    
    public Idle()
    {
        //this.transform = transform;
    }
    public override NodeState Evaluate()
    {        
        return NodeState.Success;
    }
}