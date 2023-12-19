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
            Debug.LogError("잘못된 숫자입력");
        else
        {
            //스레드로 소수개수 체크 함수 부르면 됨.
            //CountPrimeNumbers(findnum);

            //스레드가 매개변수를 갖는 형식으로 한다면...
            //후에 많은 매개변수를 가지게 하고 싶다면
            //해당 매개변수들을 구조체에 담으면 됨
            Thread t = new Thread(CountPrimeNumbers);
            t.IsBackground = true;
            t.Start(findnum);
        }
    }

    void CountPrimeNumbers(object value) //연산량이 엄청남~~~~ 
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
        Debug.Log(findnum + "까지의 소수개수 = " + totalPrimes);
    }


    //이건 소수판정 메서드.
    //소수 == 모든 자연수로 나누었을때 나누어떨어지는 수가 본인 제외하고 없음..
    //=> X가 주어졌을때, 해당 X를 2부터 X-1까지의 모든 수로 나누어봤을때 하나라도 나누어떨어지는 수가 있따면
    //걔는 소수가 아님....
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
