using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSimpleRotate : MonoBehaviour
{    
    public Transform target;        
    float targetRotAngle =0;
    
    
    void Start()
    {        
        targetRotAngle = target.rotation.eulerAngles.y;
    }
    void LateUpdate()
    {
        if (target.rotation.eulerAngles.y - targetRotAngle != 0)
        {
            transform.RotateAround(target.position, Vector3.up, (target.rotation.eulerAngles.y- targetRotAngle) * Time.deltaTime);            
            targetRotAngle = target.rotation.eulerAngles.y;
        }
        
    }
}
