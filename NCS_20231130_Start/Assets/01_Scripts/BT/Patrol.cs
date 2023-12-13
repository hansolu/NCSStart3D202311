using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : Node
{
    Enemy owner;
    //도착지점체크 필요 1
    //가만히 있는 시간 필요

    Vector3 goalPos = Vector3.zero;
    int pattrolNum = 0; //
    float currentTime = 0; //현재 카운팅하고있는 시간.
    float nextTime = 0; //다음으로 넘어가야하는 시간

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
        //#######디버깅위함          

        if (isMove) //움직이고 있는 상태
        {
            owner.NowState = AllEnum.StateEnum.Walk;            
            owner.Move(goalPos,true); 
            //내가 다른상태에 갔다가 돌아왔을때 내비에이전트가 멈춰있을경우가 있어서 세팅해줘야함            
            if ((owner.transform.position - goalPos).sqrMagnitude <= 1)//이동하려던 위치에 다 도착했음.
            {                
                isMove = false;
            }
        }
        else
        {
            owner.NowState = AllEnum.StateEnum.Idle;
            owner.Idle();
            if (currentTime >= nextTime) //가만히 있기 시간이 지났음
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
