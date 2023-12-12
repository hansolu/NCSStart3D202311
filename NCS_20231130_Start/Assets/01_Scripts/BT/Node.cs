using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeState
{ 
    Running, //��������
    Success, //������
    Failure //������
}
//FSM���� state�� ���� ����. �� ���� ���Ѵٸ� interface�� �����ص��ɰ�.
public abstract class Node 
{
    protected NodeState state; //�� ����� ���� ����
    public Node ParentNode; //����ص� ��������.
                            //���� ��� �ڽĵ��� ��� ���°� �����߰ų�, �������� ���ư����Ҷ�
                            //�ٽ� �Ǻ��� �����ϰ� ������..
    protected List<Node> childrenNode = new List<Node>(); //���� �Ǻ��� �ڽĵ�.
    public Node()
    {
        ParentNode = null;
    }

    public Node(List<Node> children)
    {
        foreach (var item in children)
        {
            AttachChild(item);
        }
    }
    public void AttachChild(Node child) //���� �ڽ��� ����
    {
        childrenNode.Add(child);  
        child.ParentNode = this;//�ڽĵ��� �θ�� ���� ��
    }

    public abstract NodeState Evaluate(); //���� �Ǻ�
}
