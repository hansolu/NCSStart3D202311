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
        blendcam.m_Loop = false; //�ڵ忡�� ���� �� ������ �� �ְ� �ݺ����� �ʵ���
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

                //0�� ī�޶󿡼� 1��ī�޶�� �̵��Ҷ�, �̵��� �ӵ� ��ȭ�� �׷����� easeinout�� ���ڴ�.
                //�׸��� �ش� �̵��� �ɸ��� �ð��� 2�ʸ� ��.
                blendcam.m_Instructions[1].m_Blend.m_Style = CinemachineBlendDefinition.Style.EaseInOut;
                blendcam.m_Instructions[1].m_Blend.m_Time = 2f;

                //�̵����� 0������ �ӹ��� �ð��� 2���̴�
                blendcam.m_Instructions[0].m_Hold = 2f;                
            }
            else
            {
                IsChange = true;

                blendcam.m_Instructions[0].m_VirtualCamera = vcam2;
                blendcam.m_Instructions[1].m_VirtualCamera = vcam1;

                //vcam2.LookAt = PlayerHeadTr; //�ڵ�󿡼� �����
                //vcam2.LookAt = null;

                //0�� ī�޶󿡼� 1��ī�޶�� �̵��Ҷ�, �̵��� �ӵ� ��ȭ�� �׷����� Linear�� ���ڴ�.
                //�׸��� �ش� �̵��� �ɸ��� �ð��� 3�ʸ� ��.
                blendcam.m_Instructions[1].m_Blend.m_Style = CinemachineBlendDefinition.Style.Linear;
                blendcam.m_Instructions[1].m_Blend.m_Time = 3f;

                //�̵����� 0������ �ӹ��� �ð��� 1���̴�
                blendcam.m_Instructions[0].m_Hold = 1f;
            }          
        }        
    }
}
