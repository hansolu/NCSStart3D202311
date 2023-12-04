using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ClickFreeLook : MonoBehaviour
{
    CinemachineFreeLook freecam;
    float scrollval = 0;

    void Start()
    {
        freecam = GetComponent<CinemachineFreeLook>();
        CinemachineCore.GetInputAxis = MyAxis;        
    }

    //void AA()
    //{
    //    //    freecam.Follow = 바꿀 대상의 Transform;
    //    //    freecam.LookAt = 
    //    //freecam.m_YAxis.
    //    //freecam.m_Lens.Dutch = 0;
    //}    

    public float MyAxis(string axisname)
    {
        //줌관리
        scrollval = Input.GetAxis("Mouse ScrollWheel"); //마우스 휠 값 가져오기. 이것도 가만히 있으면 0, 움직이면 -1~1 값을 줌.
        freecam.m_Lens.FieldOfView += scrollval * Time.deltaTime * 1000/*제어속도*/;

        //카메라의 회전 관리...
        if (Input.GetMouseButton(1)) //마우스의 오른쪽 클릭이 지속되는 이상 불림
        {
            return Input.GetAxis(axisname); //마우스 오른쪽 클릭을 할 때에만 실제로 내 마우스 현재 axis값을 보냄.
        }
        else
            return 0;         //마치 아무 인풋이 없는양 속이는것
    }
}
