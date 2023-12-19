using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class ThreadLock : MonoBehaviour
{
    int count = 10;
    object lockobject = new object();
    void Start()
    {
        Run();
        Debug.Log("Run실행후의 스타트 함수");
    }

    void Run()
    {
        for (int i = 0; i < 10; i++)
        {
            new Thread(SafeCalc).Start();
        //    new Thread(UnsafeCalc).Start(); //10개의 전경스레드가 돌아감... 10개가 모두 종료되어야 프로그램이 종료 가능해짐...
        }
        
        Debug.Log("Run 함수 안의 10개의 스레드 실행 후");
    }

    void SafeCalc()
    {
        //lock키워드를 걸고 스레드를 진행하면, 해당 스레드가 lock안에 구현된 내용들이 다 끝날때까지 다른 스레드가 함부로 일할 수 없게 함.
        lock (lockobject)
        {
            count++;
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    Debug.Log("스레드 아이디 : " + Thread.CurrentThread.ManagedThreadId + "의 count 값 : " + count);
                }
            }
        }
    }

    void UnsafeCalc()
    {
        count++;

        //복잡한 무언가 일을 한다고 가정한다
        for (int i = 0; i < count; i++)
        {
            for (int j = 0; j < count; j++)
            {
                Debug.Log("스레드 아이디 : "+Thread.CurrentThread.ManagedThreadId+"의 count 값 : " + count);
            }
        }                
    }
}
