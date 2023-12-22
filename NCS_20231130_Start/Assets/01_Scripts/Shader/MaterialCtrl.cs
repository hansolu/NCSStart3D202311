using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialCtrl : MonoBehaviour
{
    public Material Mat;
    public Light spotLight;
    
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        Mat.SetVector("_LightDirection", -spotLight.transform.forward);
        Mat.SetVector("_LightPosition", spotLight.transform.position);
        Mat.SetFloat("_LightAngle", spotLight.spotAngle);
        //}
    }
}
