using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class UseThread : MonoBehaviour
{
    public InputField field;

    //int findnum = 0;
    public void StartFind()
    {
        if (int.TryParse(field.text, out int findnum) == false)
            Debug.LogError("�߸��� �����Է�");
        else
        {
            //������� �Ҽ����� üũ �Լ� �θ��� ��.
            //CountPrimeNumbers(findnum);

            //�����尡 �Ű������� ���� �������� �Ѵٸ�...
            //�Ŀ� ���� �Ű������� ������ �ϰ� �ʹٸ�
            //�ش� �Ű��������� ����ü�� ������ ��
            Thread t = new Thread(CountPrimeNumbers);
            t.IsBackground = true;
            t.Start(findnum);
        }
    }

    void CountPrimeNumbers(object value) //���귮�� ��û��~~~~ 
    {
        int findnum = (int)value;
        int totalPrimes = 0;

        for (int i = 0; i < findnum; i++)
        {
            if (IsPrime(i)==true)
            {
                totalPrimes++;
            }
        }

        //return totalPrimes;
        Debug.Log(findnum + "������ �Ҽ����� = " + totalPrimes);
    }


    //�̰� �Ҽ����� �޼���.
    //�Ҽ� == ��� �ڿ����� ���������� ����������� ���� ���� �����ϰ� ����..
    //=> X�� �־�������, �ش� X�� 2���� X-1������ ��� ���� ����������� �ϳ��� ����������� ���� �ֵ���
    //�´� �Ҽ��� �ƴ�....
    bool IsPrime(int val)
    {
        if ((val&1) == 0)
        {
            return val == 2;
        }

        for (int i = 3; (i*i) <= val; i+=2)
        {
            if ((val % i) == 0)
            {
                return false;
            }
        }

        return val != 1;
    }
}
