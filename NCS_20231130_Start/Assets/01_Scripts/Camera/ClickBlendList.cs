using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ClickBlendList : MonoBehaviour
{
    CinemachineBlendListCamera blendcam;

    CinemachineVirtualCameraBase vcam1;
    CinemachineVirtualCameraBase vcam2;

    public Transform PlayerHeadTr;

    bool IsChange = false;
    void Start()
    {
        blendcam = GetComponent<CinemachineBlendListCamera>();
        blendcam.m_Loop = false; //코드에서 원할 때 제어할 수 있게 반복하지 않도록
        vcam1 = transform.GetChild(0).GetComponent<CinemachineVirtualCameraBase>();
        vcam2 = transform.GetChild(1).GetComponent<CinemachineVirtualCameraBase>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (IsChange)
            {
                IsChange = false;

                blendcam.m_Instructions[0].m_VirtualCamera = vcam1;
                blendcam.m_Instructions[1].m_VirtualCamera = vcam2;

                //0번 카메라에서 1번카메라로 이동할때, 이동할 속도 변화의 그래프를 easeinout을 쓰겠다.
                //그리고 해당 이동에 걸리는 시간은 2초를 줌.
                blendcam.m_Instructions[1].m_Blend.m_Style = CinemachineBlendDefinition.Style.EaseInOut;
                blendcam.m_Instructions[1].m_Blend.m_Time = 2f;

                //이동전에 0번에서 머무를 시간은 2초이다
                blendcam.m_Instructions[0].m_Hold = 2f;                
            }
            else
            {
                IsChange = true;

                blendcam.m_Instructions[0].m_VirtualCamera = vcam2;
                blendcam.m_Instructions[1].m_VirtualCamera = vcam1;

                //vcam2.LookAt = PlayerHeadTr; //코드상에서 제어가능
                //vcam2.LookAt = null;

                //0번 카메라에서 1번카메라로 이동할때, 이동할 속도 변화의 그래프를 Linear을 쓰겠다.
                //그리고 해당 이동에 걸리는 시간은 3초를 줌.
                blendcam.m_Instructions[1].m_Blend.m_Style = CinemachineBlendDefinition.Style.Linear;
                blendcam.m_Instructions[1].m_Blend.m_Time = 3f;

                //이동전에 0번에서 머무를 시간은 1초이다
                blendcam.m_Instructions[0].m_Hold = 1f;
            }          
        }        
    }
}
