using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDistance : Node
{            
    Enemy owner;
    float dist = 0;
    public CheckDistance(Enemy owner)
    {
        this.owner = owner;
        childrenNode.Add(new AttackNear(owner));
        childrenNode.Add(new AttackFar(owner.Shoot));//��������Ʈ ��� �ẻ��
        childrenNode.Add(new Chase(owner));        
    }

    //���¸� �����Ѵٸ� BT��� ���� �����...
    //���ǿ� ���� �ٷιٷ� �ٲ���ִ°� BT�� Ư¡...
    public override NodeState Evaluate()
    {
        if (owner.TargetTr == null)
        {            
            return NodeState.Failure;
        }
        //�÷��̾����� �Ĵٺ����� ����


        dist = Vector3.Distance(owner.TargetTr.position,
            owner.transform.position);

        if (dist <= owner.AttackRange)
        {
            //########����� ����
            owner.NowState = AllEnum.StateEnum.Attack_Near;
            childrenNode[0].Evaluate(); //�ٰŸ� ����
        }
        else if (dist <= owner.AttackFarRange)
        {
            //########����� ����
            owner.NowState = AllEnum.StateEnum.Attack_Far;
            childrenNode[1].Evaluate(); //���Ÿ� ����
        }
        else
        {
            //���� ������ ��� �ؿ� ������ �ʿ��ϱ⶧��.
            //BT�� ����Ÿ� FSM�� �޸� AttackCoroutine�� ���۰� ����
            //�ٸ��Լ��� ������ �Ұ�
            //if (AttackCor != null)            
            //StopCoroutine(AttackCor);
            //AttackCor = null;

            owner.SetWeaponOff();
            //########����� ����
            owner.NowState = AllEnum.StateEnum.Chase;
            childrenNode[2].Evaluate(); //�߰�
        }
        return NodeState.Success;
    }
}