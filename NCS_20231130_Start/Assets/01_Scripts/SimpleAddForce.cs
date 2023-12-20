using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAddForce : MonoBehaviour
{
    Rigidbody rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigid.AddForce(Vector3.forward * 10, ForceMode.Impulse);
        }
    }
}
