using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeControl : MonoBehaviour
{
    public Transform LifeTr; //목숨 그림 의 부모
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
            //더 진짜 동적으로 한다면

            //목숨그림을 Prefab화해서
            //Instantiate(목숨그림, LifeTr);

        } //목숨 그림의 동적세팅...


        lifeMax = LifeObj.Length; //해당 최대 목숨 개수
                
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

        if (exLifeVal < LifeVal) //예전목숨보다 현재 목숨이 숫자가 큼. 즉 목숨이 늘어났을떄
        {
            for (int i = exLifeVal; i < LifeVal; i++)
            {
                LifeObj[i].SetActive(true);
                yield return new WaitForSeconds(0.5f);
            }
        }
        else if (exLifeVal > LifeVal) //목숨이 줄어들었을때.
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
