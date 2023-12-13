using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviorTree : MonoBehaviour
{
    Node rootNode; //���� �ֻ��� �θ���. �� �����Ǻ����� ����
    Enemy owner;//�� Ʈ���� �θ� �Ǵ� ��ü
    //����
    //void Start()
    public void SetInit()
    {
        owner = GetComponent<Enemy>();

        //���Ϳ� �ش�Ǵ� Ʈ�� ����...
        rootNode = new SelectorNode
            (
            new List<Node>
            {
                new SequenceNode(//�þ� üũ.
                    new List<Node>
                    {
                        //�þ�üũ ���
                        new CheckSight(owner),

                    //new SequenceNode( //�Ÿ�üũ
                    //new List<Node>
                    //{                 
                        //�Ÿ�üũ ���
                        new CheckDistance(owner),

                        //new SelectorNode(
                        //    new List<Node>{                         
                        //new ��ų1��(),                        
                        //new ��ų2��(),
                        //new ��ų3��(),                                                
                        //}),
                //}
                //    ), //�Ÿ�üũ�� ��
                }
                    ),               //�þ�üũ�� ��
                 
                //���� ������ �ɾ���� �� ��Ÿ���ε� ����ϳ��� ���⿣ �� ���ѰŰ����ϱ�
                //�׳� �������� �⺻��� �ΰ���..
                         
                //���� ����Ʈ�� �ϰ�������� �տ��α�.                                

                //���� ����� �׾ȿ� �̵��� ���⸦ �ִ°� 
                new Patrol(owner)
                
                //���� �������� ���������� �����ð� ����...
                //�̵��ϱ� //�������� �����߰�, �����ð�+
                
                //����                
            }
            ) ;
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
