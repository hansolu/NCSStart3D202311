using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSight : Node
{
    Enemy owner;
    List<Collider> targetList = new List<Collider>();
    Vector3 targetDir = Vector3.zero;
    float targetAngle = 0;


    Vector3 LookingVec = Vector3.zero;

    public CheckSight(Enemy _owner) //�ؿ� �Լ��� 2��������� �ϰԵǸ�, Enemy�� �ƴϰ�, �������̽��� �� �ֻ��� �θ�� ������...
        //�������� CheckSight�� ��������...
    {
        owner = _owner;
    }
    public override NodeState Evaluate()
    {
        //float time = 0;
        //time += Time.deltaTime;//�̰Ŵ� ��������

        //1�� Enemy�ȿ� �ִ� �Լ��� �ٷ� �̿��Ͽ� ó���Ѵ�
        if (owner.CheckSight(owner.ViewDistance))
        {
            //Ÿ���� �ٶ󺸵���...
            if (owner.TargetTr != null)
            {
                LookingVec = owner.TargetTr.position;
                LookingVec.y = owner.transform.position.y;
                owner.transform.LookAt(LookingVec);
                return NodeState.Success;
            }            
        }

        Debug.Log("�þ� �ȿ� ������� ++���� ����");
        owner.SetWeaponOff();
        return NodeState.Failure;


        //2�� �� ��ũ��Ʈ �ȿ� �����Ѵ�.
        //�������

        //Enemy�鸸 CheckSight�� ���ٸ�
        //�׳� Enemy��ũ��Ʈ �ȿ� �־�ΰ� �װ� ���ؼ� ����Ǳ���
        //�ٵ� ���� ���߿� NPC / Pet / Enemy�� ��ӹ����ʴ� ��3�� ����~~~�� CheckSight�� ����ʹٸ�
        //�ش� CheckSight�� �ʿ�� �ϴ� ������ ��� ==> �������̽��� �ϴ���, �� �ֻ��� �θ� �δ���, �ؼ�

        targetList.Clear();
        Collider[] cols = Physics.OverlapSphere(owner.transform.position, owner.ViewDistance, owner.targetMask);
        if (cols.Length > 0)
        {
            for (int i = 0; i < cols.Length; i++)
            {
                targetDir = (cols[i].transform.position - owner.transform.position).normalized;
                targetAngle = Mathf.Acos(Vector3.Dot(owner.transform.forward, targetDir)) * Mathf.Rad2Deg;
                if (targetAngle <= owner.ViewAngle * 0.5f  //�� �þ߰� ���̰�,
                    && Physics.Raycast(owner.transform.position, targetDir, owner.ViewDistance, owner.obstacleMask) == false)
                //���� ��ġ���� ����� ���� ���̸� ������, ��ֹ��� ��������������
                {
                    //���.
                    targetList.Add(cols[i]);
                }
            }

            if (targetList.Count > 0) //�þ� �ȿ� ����� ����        
            {
                owner.TargetTr = targetList[0].transform; //Ÿ���� 0�� �� ���𰡷� �����                
                return NodeState.Success;
            }            
        }
        return NodeState.Failure; //�þ߾ȿ� ���� ����. ����.
    }
}
