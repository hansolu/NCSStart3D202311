using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class TestThread : MonoBehaviour
{        

    void Start()
    {
        DoTest();//��� �����带 �ϳ� ������.

        //Run(); //�̰� ���ν������� ����.. //���潺���� //���潺���尡 ��� ���� ���α׷��� �������ʴ´�.
    }

    void DoTest() 
    {        
        //�����带 ����� ��
        //1�� ������Ŭ���� �����ڿ� �����ϴ� ����        
        Thread t1 = new Thread(new ThreadStart(Run)); //�� ��������Ʈ�� �Ű������� ���� �ֵ鸸 ���� �� �����Ƿ�, 
        t1.IsBackground = true; //��� ������� ������. ���� ���ῡ ������ ��ġ�� �ʴ´�
        t1.Start(); //������ ������        

        int randomsecond = Random.Range(10, 20) * 100;        
        //2�� �̰� ����.....===================
        Thread t2 = new Thread(Run2);//�����Ϸ��� Run �޼����� �Լ� ������Ÿ�����κ��� ThreadStart ��������Ʈ ��ü�� �߷��ؼ� ����...
        //t2.IsBackground = true; //��� ������� ���� ����
        t2.Start(randomsecond); //������ ������


        //���� ������ ������ 2���� ���� ������...
        
        //3�� �͸�޼��� �̿�
        Thread t3 = new Thread(delegate  ()
        {
            Run();
        }
            );
        t3.Start();

        //4�� 
        Thread t4 = new Thread(() => Run()); //���ٸ� �̿�
        t4.Start();

        //5�� ����
        new Thread(() => Run()).Start(); //�̷��� ���� ��� �ƿ� ���߰ų� ������ �� ����....


        //�Ű����� ������ �ֱ�
        ThreadStruce thrStruct;
        thrStruct.str = "������������";
        thrStruct.ival = 10;
        thrStruct.fval = 1.234f;

        Thread t6 = new Thread(ThreadSample);                
        t6.Start(thrStruct); //������ ������
    }

    void Run()
    {
        Debug.Log($"Thread ID : {Thread.CurrentThread.ManagedThreadId} ���� ");
        Debug.Log($"Thread ID : {Thread.CurrentThread.ManagedThreadId} �� ��ٷ��� �� �ð� == 2��");
        Thread.Sleep(2000);//������ �и�������
        Debug.Log($"Thread ID : {Thread.CurrentThread.ManagedThreadId} ��..");
    }
    void Run2(object val)
    {
        int randomsecond = (int)val;
        Debug.Log($"Thread ID : {Thread.CurrentThread.ManagedThreadId} ���� ");        
        Debug.Log($"Thread ID : {Thread.CurrentThread.ManagedThreadId} �� ��ٷ��� �� �ð� {randomsecond}");
        Thread.Sleep(randomsecond);//������ �и�������
        Debug.Log($"Thread ID : {Thread.CurrentThread.ManagedThreadId} ��..");
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
        Debug.Log("�Ű������� ���� str"+thrstruct.str);
        Debug.Log("�Ű������� ���� int" + thrstruct.ival);
        Debug.Log("�Ű������� ���� float" + thrstruct.fval);

    }
}
