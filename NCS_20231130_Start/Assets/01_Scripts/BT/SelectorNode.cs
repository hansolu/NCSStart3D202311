using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//OR �� �����ϴ�. �ϳ��� true�� �� true==> �ϳ��� �����̸� �� ����.�� �ش��.
public class SelectorNode : Node
{
    public SelectorNode() : base()
    {        
    }

    public SelectorNode(List<Node> children) :base(children)
    {        
    }

    public override NodeState Evaluate() //�� �ڽĵ��� ���ǵ��� �ϳ��� �����ϰų� �ϳ��� �̹� �������̾��ٸ� ������ ����...
    {
        foreach (Node node in childrenNode)
        {
            switch (node.Evaluate())
            {
                case NodeState.Running:
                    return NodeState.Running;
                    
                case NodeState.Success:
                    return NodeState.Success;

                case NodeState.Failure:
                    continue;                    
                default:
                    break;
            }
        }

        //���� ���� ���ð��� �����鼭 �װ� ������
        return state = NodeState.Failure;
    }
}
