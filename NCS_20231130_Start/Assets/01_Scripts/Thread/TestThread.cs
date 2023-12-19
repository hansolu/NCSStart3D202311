using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class TestThread : MonoBehaviour
{        

    void Start()
    {
        DoTest();//배경 스레드를 하나 시작함.

        //Run(); //이건 메인스레드의 실행.. //전경스레드 //전경스레드가 계속 돌면 프로그램이 꺼지지않는다.
    }

    void DoTest() 
    {        
        //스레드를 만드는 법
        //1번 쓰레드클래스 생성자에 전달하는 형식        
        Thread t1 = new Thread(new ThreadStart(Run)); //이 델리게이트는 매개변수가 없는 애들만 넣을 수 있으므로, 
        t1.IsBackground = true; //배경 스레드로 설정함. 실행 종료에 영향을 미치지 않는다
        t1.Start(); //스레드 시작함        

        int randomsecond = Random.Range(10, 20) * 100;        
        //2번 이게 무난.....===================
        Thread t2 = new Thread(Run2);//컴파일러가 Run 메서드의 함수 프로토타입으로부터 ThreadStart 델리게이트 객체를 추론해서 생성...
        //t2.IsBackground = true; //배경 스레드로 설정 뺐음
        t2.Start(randomsecond); //스레드 시작함


        //이하 가능은 하지만 2번이 가장 무난함...
        
        //3번 익명메서드 이용
        Thread t3 = new Thread(delegate  ()
        {
            Run();
        }
            );
        t3.Start();

        //4번 
        Thread t4 = new Thread(() => Run()); //람다를 이용
        t4.Start();

        //5번 간략
        new Thread(() => Run()).Start(); //이러면 이제 얘는 아예 멈추거나 제어할 수 없음....


        //매개변수 여러개 넣기
        ThreadStruce thrStruct;
        thrStruct.str = "여러가지변수";
        thrStruct.ival = 10;
        thrStruct.fval = 1.234f;

        Thread t6 = new Thread(ThreadSample);                
        t6.Start(thrStruct); //스레드 시작함
    }

    void Run()
    {
        Debug.Log($"Thread ID : {Thread.CurrentThread.ManagedThreadId} 시작 ");
        Debug.Log($"Thread ID : {Thread.CurrentThread.ManagedThreadId} 가 기다려야 할 시간 == 2초");
        Thread.Sleep(2000);//단위가 밀리세컨임
        Debug.Log($"Thread ID : {Thread.CurrentThread.ManagedThreadId} 끝..");
    }
    void Run2(object val)
    {
        int randomsecond = (int)val;
        Debug.Log($"Thread ID : {Thread.CurrentThread.ManagedThreadId} 시작 ");        
        Debug.Log($"Thread ID : {Thread.CurrentThread.ManagedThreadId} 가 기다려야 할 시간 {randomsecond}");
        Thread.Sleep(randomsecond);//단위가 밀리세컨임
        Debug.Log($"Thread ID : {Thread.CurrentThread.ManagedThreadId} 끝..");
    }

    public struct ThreadStruce
    {
        public string str;
        public int ival;
        public float fval;
    }

    void ThreadSample(object val)
    {
        ThreadStruce thrstruct = (ThreadStruce)val;        
        Debug.Log("매개변수로 받은 str"+thrstruct.str);
        Debug.Log("매개변수로 받은 int" + thrstruct.ival);
        Debug.Log("매개변수로 받은 float" + thrstruct.fval);

    }
}
