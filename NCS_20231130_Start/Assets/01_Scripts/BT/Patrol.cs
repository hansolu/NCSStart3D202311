using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : Node
{
    Enemy owner;
    //��������üũ �ʿ� 1
    //������ �ִ� �ð� �ʿ�

    Vector3 goalPos = Vector3.zero;
    int pattrolNum = 0; //
    float currentTime = 0; //���� ī�����ϰ��ִ� �ð�.
    float nextTime = 0; //�������� �Ѿ���ϴ� �ð�

    bool isMove = false;

    public Patrol(Enemy owner)
    {
        this.owner = owner;
        SetNextTime();        
    }
    void SetNextTime()
    {
        nextTime = Random.Range(1f,2f);
    }
    public override NodeState Evaluate()
    {
        //#######���������          

        if (isMove) //�����̰� �ִ� ����
        {
            owner.NowState = AllEnum.StateEnum.Walk;            
            owner.Move(goalPos,true); 
            //���� �ٸ����¿� ���ٰ� ���ƿ����� ��������Ʈ�� ����������찡 �־ �����������            
            if ((owner.transform.position - goalPos).sqrMagnitude <= 1)//�̵��Ϸ��� ��ġ�� �� ��������.
            {                
                isMove = false;
            }
        }
        else
        {
            owner.NowState = AllEnum.StateEnum.Idle;
            owner.Idle();
            if (currentTime >= nextTime) //������ �ֱ� �ð��� ������
            {
                currentTime = 0;
                SetNextTime();
                goalPos = GameManager.Instance.GetNextPatrol
                    (owner.PatterParentNum, ref pattrolNum, true);                
                isMove = true;
            }
            else
            {
                currentTime += Time.deltaTime;
            }
        }
        return NodeState.Success;
    }
    
}
