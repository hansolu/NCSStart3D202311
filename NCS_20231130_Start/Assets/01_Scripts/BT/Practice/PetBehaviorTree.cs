using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetBehaviorTree : MonoBehaviour
{
    public Transform PlayerTr; //�÷��̾� ��ġ.
    Node rootNode = null;   
    void Start()
    {

        rootNode = new SelectorNode( 
            new List<Node>()
            {
                new SequenceNode(
                    new List<Node>
                    {
                        new CheckIsPlayerNear(transform, PlayerTr),
                        new FollowPlayer(transform, PlayerTr)
                    }
                    ),
                new Idle(/*transform*/)
            }
            );
    }
    
    void Update()
    {
        if (rootNode == null)
        {
            return;
        }

        rootNode.Evaluate(); //�������� �����Ǻ�����.
    }
}
