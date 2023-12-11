using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//State == 상태들을 찍어낼 부모(원본)

//abstract로 구현을 하던지, interface로 구현을 하던지... 편한쪽으로..
public abstract class State /*: MonoBehaviour*/
{
    //소유주 에 대한 정보도 가지고 있어야 함.
    protected Enemy enemy; //행동 주인.
    public delegate void SetStateDel(AllEnum.StateEnum _enum);
    protected SetStateDel StateDel;
    public State(Enemy enemy, SetStateDel StateDel)
    {
        this.enemy = enemy;
        this.StateDel = StateDel;
    }
    //public void SetOwner(Enemy enemy)
    //{
    //    this.enemy = enemy;
    //}

    public abstract void OnStateEnter(); //이 상태에 처음 들어왔을때 세팅해야하는것
    public abstract void OnStateStay(); //이 상태를 지속한다면 해야하는 일
    public abstract void OnStateExit(); //이 상태를 나갈때 정리해야할 것.
}
