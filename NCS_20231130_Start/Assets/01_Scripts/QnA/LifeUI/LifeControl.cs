using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeControl : MonoBehaviour
{
    public Transform LifeTr; //��� �׸� �� �θ�
    GameObject[] LifeObj;
    public Slider slider;
    public Text sliderValue;
    int LifeVal = 0;
    int exLifeVal = 0;
    int lifeMax = 0;

    bool IsChanging = false;

    void Start()
    {
        LifeObj = new GameObject[LifeTr.childCount];
        for (int i = 0; i < LifeObj.Length; i++)
        {            
            LifeObj[i] = LifeTr.GetChild(i).gameObject;
            //�� ��¥ �������� �Ѵٸ�

            //����׸��� Prefabȭ�ؼ�
            //Instantiate(����׸�, LifeTr);

        } //��� �׸��� ��������...


        lifeMax = LifeObj.Length; //�ش� �ִ� ��� ����
                
        slider.minValue = -lifeMax;
        slider.maxValue = lifeMax;
        slider.value = 0;
        LifeVal = lifeMax;
        exLifeVal = lifeMax;
        IsChanging = false;
    }

    public void SliderMove()
    {
        sliderValue.text = slider.value.ToString();
    }

    public void ButtonClick()
    {
        if (IsChanging)
        {
            return;
        }

        LifeVal += (int)slider.value;
        if (LifeVal > lifeMax)
        {
            LifeVal = lifeMax;
        }
        else if (LifeVal < 0)
        {
            LifeVal = 0;
        }

        LifeVal = (int)Mathf.Clamp(LifeVal + slider.value, 0, lifeMax);
        StartCoroutine(UIChangeWithTime());  
    }

    IEnumerator UIChangeWithTime()
    {
        IsChanging = true;

        if (exLifeVal < LifeVal) //����������� ���� ����� ���ڰ� ŭ. �� ����� �þ����
        {
            for (int i = exLifeVal; i < LifeVal; i++)
            {
                LifeObj[i].SetActive(true);
                yield return new WaitForSeconds(0.5f);
            }
        }
        else if (exLifeVal > LifeVal) //����� �پ�������.
        {
            for (int i = exLifeVal - 1; i >= LifeVal; i--)
            {
                LifeObj[i].SetActive(false);
                yield return new WaitForSeconds(0.5f);
            }
        }

        exLifeVal = LifeVal;
        IsChanging = false;
    }
}
