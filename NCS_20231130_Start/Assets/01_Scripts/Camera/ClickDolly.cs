using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ClickDolly : MonoBehaviour
{
    CinemachineVirtualCamera vcam;
    CinemachineTrackedDolly dolly;

    public CinemachineDollyCart cart;
    bool isEnable = true;
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();        
        dolly = vcam.GetCinemachineComponent<CinemachineTrackedDolly>();          
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isEnable = !isEnable; //true였으면 false로 뒤집고, false였으면 true로 바꿈
            dolly.m_AutoDolly.m_Enabled = isEnable; //돌리트랙을 사용여부
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (cart!=null)
            {
                cart.m_Speed = Mathf.Clamp(cart.m_Speed+Time.deltaTime, 0, 10);
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (cart != null)
            {
                cart.m_Speed = Mathf.Clamp(cart.m_Speed - Time.deltaTime, 0, 10);
            }
        }
    }
}
