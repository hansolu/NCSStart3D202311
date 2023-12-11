using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//상태를 판별해서 다음상태로 바꿔줌.
public class StateMachine : MonoBehaviour //이거는 좀 Monobehavior를 상속받을 필요가 있음.
{
    [HideInInspector]
    public Enemy owner; //이 스테이트 머신의 소유주
    Dictionary<AllEnum.StateEnum, State> StateDic = new Dictionary<AllEnum.StateEnum, State>();
    AllEnum.StateEnum ExState; //이전상태 체크위함
    bool IsPlay = false; 
    #region 패턴예시    
    //패턴을 위한 상태Enum만 담은 리스트
    //List<AllEnum.StateEnum> patternList = new List<AllEnum.StateEnum>();
    //bool doPattern = false; //이거는 그냥 코루틴 제어용
    //int patternumNum = 0; //패턴리스트를 하나씩 돌기위한 변수
    #endregion

    //void Start()
    public void SetInit()
    {
        //내 행동 딕셔너리 세팅...
        owner = GetComponent<Enemy>();
        
        StateDic.Add(AllEnum.StateEnum.Patrol, new State_Patrol(owner, SetState));
        //StateDic.Add(AllEnum.StateEnum.Idle , new State_Idle(owner, SetState));
        //StateDic.Add(AllEnum.StateEnum.Walk, new State_Walk(owner, SetState));
        StateDic.Add(AllEnum.StateEnum.Chase, new State_Chase(owner, SetState));
        //공격

        ExState = AllEnum.StateEnum.End;
        SetState(AllEnum.StateEnum./*Idle*/Patrol);
        IsPlay = true;
    }
    void Update()
    {
        if (IsPlay == false) //1번 bool을줘서 End가 아닐때 실행하도록 한다
        {
            return;
        }

        if (ExState == owner.NowState 
            //&& owner.NowState != AllEnum.StateEnum.End 
            //2번, nowState가 End상태면 일을 안하도록 한다.
            )
        {
            StateDic[owner.NowState].OnStateStay();
        }
    }
    //여기에 접근하기 위해서
    //1번 애초에 StateMachine과 Enemy스크립트를 하나로 합친다.
    //Enemy가 자체적으로 StateMachine기능을 가지고 있도록 함
    //2번 StateMachine만 Enemy를 가지고 있는게 아니고, Enemy도 StateMachine을 가지고 있음
    //3번 State 의 생성자에 StateMachine 전달해준다.
    //==>statemachine을 통해서 Enemy에 접근하고, statemachine의 setstate를 자유롭게 부름
    //4번이 구현한, 생성자에 SetState를 delegate ==함수변수로 넘김
    public void SetState(AllEnum.StateEnum _enum)
    {        
        owner.NowState = _enum;
        //Debug.Log($"setstate이고 Exstate = {ExState} / nowState ={owner.NowState}");
        if (ExState != owner.NowState)
        {
            if (ExState!= AllEnum.StateEnum.End)            
                StateDic[ExState].OnStateExit();            

            StateDic[owner.NowState].OnStateEnter();
            ExState = owner.NowState;
        }
    }

    //아래 이런 패턴 진행은, 
    //전투를 예시로 들때, 전투 부모상태를 만들어서, 해당 부모의 내용에
    //이하 코루틴과 같은 내용들을 넣어두면, 전투 패턴 진행이 가능해짐...

    //onstatestay는 항상 update에서 시키고, 
    //상태의 변화는 패턴으로 주고싶을때, 
    //상태 변화 조건을 내부에서 주는게 아니고,
    //밖에 코루틴으로 강제로 일정시간, 또는 뭔가 원하는 조건으로 실행시킴.. 
    //IEnumerator SetPatternState()
    //{
    //    StateDic[patternList[patternumNum]].OnStateEnter(); //처음 패턴 시작
    //    while (doPattern)
    //    {
    //        yield return new WaitForSeconds(다음패턴시간);
    //        StateDic[patternList[patternumNum]].OnStateExit();
    //        patternumNum++;
    //        if (patternumNum >= patternList.Count)
    //        {
    //            patternumNum = 0;
    //        }
    //        StateDic[patternList[patternumNum]].OnStateEnter(); 
    //    }
    //}
}
