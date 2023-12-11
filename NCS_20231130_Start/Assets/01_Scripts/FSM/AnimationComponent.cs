using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationComponent : MonoBehaviour
{
    //얘는 정말로 Animation관련만 다 가지고 있음
    Animator anim;
    //enum타입같은걸 준다던지
 
    //void Start()
    public void SetInit()
    {
        anim = GetComponent<Animator>();
    }

    public void Idle()
    {        
        //Idle애니메이션 출력
        anim.SetFloat("Speed", 0);
    }
    public void Walk(float x, float z)
    {        
        //걷는 애니메이션 출력
        anim.SetFloat("Speed", 2.5f);
        anim.SetFloat("PosX", x);
        anim.SetFloat("PosZ", z);
    }

    public void Run(float x, float z)
    {        
        //뛰는 애니메이션 출력
        anim.SetFloat("Speed", 5f);
        anim.SetFloat("PosX", x);
        anim.SetFloat("PosZ", z);
    }

    public void Attack()
    { }
}
