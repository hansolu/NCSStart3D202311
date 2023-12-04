using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ClickMixing : MonoBehaviour
{
    CinemachineMixingCamera mcam;
    
    void Start()
    {
        mcam = GetComponent<CinemachineMixingCamera>();

    }
        
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            mcam.m_Weight0 = 1;
            mcam.m_Weight1 = 0.1f;
            mcam.m_Weight2 = 0.1f;            
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            mcam.m_Weight0 = 0.1f;
            mcam.m_Weight1 = 1f;
            mcam.m_Weight2 = 0.1f;
        }
    }
}
