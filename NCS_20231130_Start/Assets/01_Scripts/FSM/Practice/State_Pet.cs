using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State_Pet 
{
    //������ �� ���� ������ ������ �־�� ��.
    protected PetStateMachine owner; //�ൿ ����.        
    public State_Pet(PetStateMachine owner)
    {
        this.owner = owner;        
    }    

    public abstract void OnStateEnter(); //�� ���¿� ó�� �������� �����ؾ��ϴ°�
    public abstract void OnStateStay(); //�� ���¸� �����Ѵٸ� �ؾ��ϴ� ��
    public abstract void OnStateExit(); //�� ���¸� ������ �����ؾ��� ��.
}
