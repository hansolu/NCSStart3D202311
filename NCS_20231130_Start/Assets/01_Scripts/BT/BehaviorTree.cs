using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//FSM���� StateMachine���� ����
public class BehaviorTree : MonoBehaviour
{
    Node rootNode; //���� �ֻ��� �θ���. �� �����Ǻ����� ����

    //����
    void Start()
    {
        //���Ϳ� �ش�Ǵ� Ʈ�� ����...
        rootNode = new SelectorNode
            (
            new List<Node> 
            { 
                //�켱������ �ϰ� ���� ���� �� ����
                //���� �־���.
                //new ����(),

                //�þ� üũ.
                new SequenceNode(
                    new List<Node>
                    { 
                //        new node�� ��ӹ��� �Ÿ�üũ�� ��ũ��Ʈ,
                //    new node�� ��ӹ��� chase��ũ��Ʈ,
                //new node�� ��ӹ��� attack��ũ��Ʈ
                }
                    ),               


                //���� �⺻�� �Ǵ°��� ���� �ڿ� ��.
                //new node�� ��ӹ��� Patrol��ũ��Ʈ,                
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
