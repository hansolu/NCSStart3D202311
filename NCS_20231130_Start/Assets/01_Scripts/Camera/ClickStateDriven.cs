using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ClickStateDriven : MonoBehaviour
{
    CinemachineStateDrivenCamera statecam;
    CinemachineVirtualCameraBase vcam1;
    CinemachineVirtualCameraBase vcam2;    
    
    void Start()
    {        
        statecam = GetComponent<CinemachineStateDrivenCamera>();
        vcam1 = transform.GetChild(0).GetComponent<CinemachineVirtualCameraBase>();
        vcam2 = transform.GetChild(1).GetComponent<CinemachineVirtualCameraBase>();
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        Debug.Log("������� �ؽ� " + statecam.m_Instructions[0].m_FullHash);
    //        Debug.Log(Animator.StringToHash("Base Layer.MoveBlendTree"));

    //        //statecam.m_Instructions[0].m_FullHash = Animator.StringToHash("Base Layer.MoveBlendTree");
    //        //statecam.m_Instructions[0].m_VirtualCamera = vcam1;

    //        statecam.m_Instructions[0].m_FullHash = 1833178346; //�ִϸ��̼� STate�� ���� ��ȣ. �ؽù�ȣ
    //        //statecam.m_Instructions[1].m_VirtualCamera = vcam2; //�ش� ������Ʈ�� ������ ī�޶�.
        //statecam.m_Instructions[0].m_MinDuration //Min�̰� //�������� �̵����� �̸�ŭ �� ���¸� �����ߴٰ� �����ϰ���.
            //statecam.m_Instructions[0].m_ActivateAfter = 1;//Wait �� ����. 1���Ŀ� �� �������� �����ϰ���.
    //    }
    //}
    }
