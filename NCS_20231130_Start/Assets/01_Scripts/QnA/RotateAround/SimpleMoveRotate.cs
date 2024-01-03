using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMoveRotate : MonoBehaviour
{
    float speed = 10;
    Vector3 moveVec = Vector3.zero;

    void Update()
    {
        moveVec.x = Input.GetAxisRaw("Horizontal"); //A / D Ȥ�� ���ʹ���Ű, �����ʹ���Ű //���ʰ��� ������ -1, �ȴ����� 0 , �������̸� 1
        moveVec.z = Input.GetAxisRaw("Vertical");
                    
        transform.Translate(moveVec.normalized * Time.deltaTime * speed);
        
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, -1, 0);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0, 1, 0);
        }
    }
}
