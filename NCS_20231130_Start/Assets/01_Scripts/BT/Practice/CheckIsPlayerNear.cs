using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIsPlayerNear : Node
{
    Transform transform;
    Transform PlayerTr;
    float dist = 0;
    public CheckIsPlayerNear(Transform transform, Transform playerTr)
    {
        this.transform = transform;
        this.PlayerTr = playerTr;
    }
    public override NodeState Evaluate()
    {
        //3����. 
        // (PlayerTr.position - transform.position).sqrmagnitude ==�Ÿ�*�Ÿ�
        //���ǰ��� �������� ����Ȯ��...

        dist = Vector3.Distance(PlayerTr.position, transform.position);
        if (dist >= 5)
        {            
            return state= NodeState.Success;
        }
        else if (dist <= 2)
        {
            return state = NodeState.Failure;
        }
        else //�Ÿ��� 2~5���̸�....
        {
            if (state != NodeState.Failure)
            {
                return state = NodeState.Running;
            }
            else //���� ���°� Failure �� 2���Ͽ��� ��Ȳ�̾���
                //���� 2���� �Ÿ��� �� �־�������
                //5�����϶�...
                return NodeState.Failure;
        }        
    }
}
