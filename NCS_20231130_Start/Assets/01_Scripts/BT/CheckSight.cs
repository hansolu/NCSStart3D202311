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

    public CheckSight(Enemy _owner) //밑에 함수를 2번방법으로 하게되면, Enemy가 아니고, 인터페이스든 더 최상위 부모든 넣으면...
        //누구든지 CheckSight를 쓸수있음...
    {
        owner = _owner;
    }
    public override NodeState Evaluate()
    {
        //float time = 0;
        //time += Time.deltaTime;//이거는 문제있음

        //1번 Enemy안에 있는 함수를 바로 이용하여 처리한다
        if (owner.CheckSight(owner.ViewDistance))
        {
            //타겟을 바라보도록...
            if (owner.TargetTr != null)
            {
                LookingVec = owner.TargetTr.position;
                LookingVec.y = owner.transform.position.y;
                owner.transform.LookAt(LookingVec);
                return NodeState.Success;
            }            
        }

        Debug.Log("시야 안에 사람없음 ++전투 안함");
        owner.SetWeaponOff();
        return NodeState.Failure;


        //2번 이 스크립트 안에 구현한다.
        //예를들어

        //Enemy들만 CheckSight를 쓴다면
        //그냥 Enemy스크립트 안에 넣어두고 그걸 콜해서 쓰면되긴함
        //근데 만약 나중에 NPC / Pet / Enemy를 상속받지않는 제3의 무언가~~~도 CheckSight를 쓰고싶다면
        //해당 CheckSight가 필요로 하는 정보를 묶어서 ==> 인터페이스로 하던지, 더 최상위 부모를 두던지, 해서

        targetList.Clear();
        Collider[] cols = Physics.OverlapSphere(owner.transform.position, owner.ViewDistance, owner.targetMask);
        if (cols.Length > 0)
        {
            for (int i = 0; i < cols.Length; i++)
            {
                targetDir = (cols[i].transform.position - owner.transform.position).normalized;
                targetAngle = Mathf.Acos(Vector3.Dot(owner.transform.forward, targetDir)) * Mathf.Rad2Deg;
                if (targetAngle <= owner.ViewAngle * 0.5f  //내 시야각 안이고,
                    && Physics.Raycast(owner.transform.position, targetDir, owner.ViewDistance, owner.obstacleMask) == false)
                //나의 위치에서 대상을 향해 레이를 쐈을때, 장애물이 막고잇지도않음
                {
                    //대상.
                    targetList.Add(cols[i]);
                }
            }

            if (targetList.Count > 0) //시야 안에 대상이 있음        
            {
                owner.TargetTr = targetList[0].transform; //타겟을 0번 인 무언가로 잡아줌                
                return NodeState.Success;
            }            
        }
        return NodeState.Failure; //시야안에 뭔가 없음. 실패.
    }
}
