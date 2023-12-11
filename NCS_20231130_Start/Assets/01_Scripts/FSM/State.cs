using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//State == ���µ��� �� �θ�(����)

//abstract�� ������ �ϴ���, interface�� ������ �ϴ���... ����������..
public abstract class State /*: MonoBehaviour*/
{
    //������ �� ���� ������ ������ �־�� ��.
    protected Enemy enemy; //�ൿ ����.
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

    public abstract void OnStateEnter(); //�� ���¿� ó�� �������� �����ؾ��ϴ°�
    public abstract void OnStateStay(); //�� ���¸� �����Ѵٸ� �ؾ��ϴ� ��
    public abstract void OnStateExit(); //�� ���¸� ������ �����ؾ��� ��.
}
