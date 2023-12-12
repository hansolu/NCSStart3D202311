using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//AND ����. �ϳ��� false�� �� false
public class SequenceNode : Node
{
    public SequenceNode() : base()
    {
    }

    public SequenceNode(List<Node> children) : base(children)
    {
    }

    public override NodeState Evaluate() //�� �ڽĵ��� ���ǵ��� �ϳ��� �����ϰų� �ϳ��� �̹� �������̾��ٸ� ������ ����...
    {
        bool isRunning = false;
        //�ڽĵ��� ���鼭 ����üũ��.
        //�ϳ��� ���հ��̸�
        //���̻� �ٸ��ڽĵ��� üũ�غ����ʰ� ����.
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

        //���� ���� ���ð��� �����鼭 �װ� ������
        return state = isRunning ? NodeState.Running : NodeState.Success;
    }
}
