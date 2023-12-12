using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//AND 조건. 하나라도 false면 다 false
public class SequenceNode : Node
{
    public SequenceNode() : base()
    {
    }

    public SequenceNode(List<Node> children) : base(children)
    {
    }

    public override NodeState Evaluate() //내 자식들의 조건들중 하나라도 부합하거나 하나라도 이미 진행중이었다면 무조건 실행...
    {
        bool isRunning = false;
        //자식들을 돌면서 조건체크함.
        //하나라도 불합격이면
        //더이상 다른자식들을 체크해보지않고 나감.
        foreach (Node node in childrenNode)
        {
            switch (node.Evaluate())
            {
                case NodeState.Running:
                    isRunning = true;
                    continue;

                case NodeState.Success:
                    continue;

                case NodeState.Failure:
                    return NodeState.Failure;
                default:
                    break;
            }
        }

        //나의 상태 세팅값을 넣으면서 그걸 리턴함
        return state = isRunning ? NodeState.Running : NodeState.Success;
    }
}
