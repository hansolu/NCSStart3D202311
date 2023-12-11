using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationComponent : MonoBehaviour
{
    //��� ������ Animation���ø� �� ������ ����
    Animator anim;
    //enumŸ�԰����� �شٴ���
 
    //void Start()
    public void SetInit()
    {
        anim = GetComponent<Animator>();
    }

    public void Idle()
    {        
        //Idle�ִϸ��̼� ���
        anim.SetFloat("Speed", 0);
    }
    public void Walk(float x, float z)
    {        
        //�ȴ� �ִϸ��̼� ���
        anim.SetFloat("Speed", 2.5f);
        anim.SetFloat("PosX", x);
        anim.SetFloat("PosZ", z);
    }

    public void Run(float x, float z)
    {        
        //�ٴ� �ִϸ��̼� ���
        anim.SetFloat("Speed", 5f);
        anim.SetFloat("PosX", x);
        anim.SetFloat("PosZ", z);
    }

    public void Attack()
    { }
}
