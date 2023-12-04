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
    //        Debug.Log("무브블랜드 해시 " + statecam.m_Instructions[0].m_FullHash);
    //        Debug.Log(Animator.StringToHash("Base Layer.MoveBlendTree"));

    //        //statecam.m_Instructions[0].m_FullHash = Animator.StringToHash("Base Layer.MoveBlendTree");
    //        //statecam.m_Instructions[0].m_VirtualCamera = vcam1;

    //        statecam.m_Instructions[0].m_FullHash = 1833178346; //애니메이션 STate의 고유 번호. 해시번호
    //        //statecam.m_Instructions[1].m_VirtualCamera = vcam2; //해당 스테이트에 연결할 카메라.
        //statecam.m_Instructions[0].m_MinDuration //Min이고 //다음으로 이동전에 이만큼 내 상태를 유지했다가 변경하겠음.
            //statecam.m_Instructions[0].m_ActivateAfter = 1;//Wait 와 같음. 1초후에 내 조건으로 변경하겠음.
    //    }
    //}
    }
