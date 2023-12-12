using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeState
{ 
    Running, //실행중임
    Success, //성공함
    Failure //실패함
}
//FSM에서 state와 같은 역할. 얘 또한 원한다면 interface로 구현해도될것.
public abstract class Node 
{
    protected NodeState state; //이 노드의 현재 상태
    public Node ParentNode; //기록해둘 이전상태.
                            //예를 들어 자식들의 모든 상태가 실패했거나, 이전으로 돌아가야할때
                            //다시 판별을 시작하고 싶을때..
    protected List<Node> childrenNode = new List<Node>(); //상태 판별할 자식들.
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
    public void AttachChild(Node child) //나의 자식을 더함
    {
        childrenNode.Add(child);  
        child.ParentNode = this;//자식들의 부모는 내가 됨
    }

    public abstract NodeState Evaluate(); //상태 판별
}
