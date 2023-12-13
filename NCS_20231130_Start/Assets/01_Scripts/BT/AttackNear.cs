using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackNear : Node
{
    //�߰��� ���...
    // ���� enemy�� AttackNear�Լ���
    //��ü������ �ڷ�ƾ�� ������ �����Ƿ�
    //��� ���� �������� AttackNear�ҷ��� ū ������ ����..

    //�׷��� ��ü AttackNear�Լ��� �������� �θ��ٸ�,
    //float time�� �ʿ���� Enemy��ü�� ������ ���� �ʿ䵵����
    //=> ��������Ʈ�� �ش� AttackNear�Լ��� �޾Ƽ� ������ �־��...

    Enemy owner;
    float time = 0; //ī��Ʈ��    
    public AttackNear( Enemy owner/*��������Ʈ �Լ� �޴°͵� ������ ����.*/ )
    {
        this.owner = owner;
        time = owner.NearAttackDelayTime;
    }

    //�굵 CheckSightó�� 
    //�̾ȿ��ٰ� �ڵ带 ������ �ϴ���
    //(�� ����� ���ǿ� ���� �ʿ� ������ �޸��ָ� �������� �����ְ� �������
    //Ȥ�� �׳� Enemy���� �ڵ带 �״�� ������...
    public override NodeState Evaluate() //�̰� update���� �Ҹ��°Ͱ� ������
    {
        owner.Idle();
        //1����� enemy��ũ��Ʈ�� �پ��ִ� �Լ��� �̿���
        //(�Լ����ο� �ڷ�ƾ ����)
        owner.AttackNear(true);
        return NodeState.Success;

        //2����� - deltatime�� �̿���
        //���� �ѹ���������
        if (time >= owner.NearAttackDelayTime)
        {
            time = 0;
            owner.AttackNear(true); 

            return NodeState.Running;
        }

        time += Time.deltaTime;
        //�ٰŸ� ���� ����.
        return NodeState.Success;
    }
}