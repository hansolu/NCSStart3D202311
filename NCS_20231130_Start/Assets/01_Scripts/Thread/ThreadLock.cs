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
        Debug.Log("Run�������� ��ŸƮ �Լ�");
    }

    void Run()
    {
        for (int i = 0; i < 10; i++)
        {
            new Thread(SafeCalc).Start();
        //    new Thread(UnsafeCalc).Start(); //10���� ���潺���尡 ���ư�... 10���� ��� ����Ǿ�� ���α׷��� ���� ��������...
        }
        
        Debug.Log("Run �Լ� ���� 10���� ������ ���� ��");
    }

    void SafeCalc()
    {
        //lockŰ���带 �ɰ� �����带 �����ϸ�, �ش� �����尡 lock�ȿ� ������ ������� �� ���������� �ٸ� �����尡 �Ժη� ���� �� ���� ��.
        lock (lockobject)
        {
            count++;
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    Debug.Log("������ ���̵� : " + Thread.CurrentThread.ManagedThreadId + "�� count �� : " + count);
                }
            }
        }
    }

    void UnsafeCalc()
    {
        count++;

        //������ ���� ���� �Ѵٰ� �����Ѵ�
        for (int i = 0; i < count; i++)
        {
            for (int j = 0; j < count; j++)
            {
                Debug.Log("������ ���̵� : "+Thread.CurrentThread.ManagedThreadId+"�� count �� : " + count);
            }
        }                
    }
}
