using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ClickFreeLook : MonoBehaviour
{
    CinemachineFreeLook freecam;
    float scrollval = 0;

    void Start()
    {
        freecam = GetComponent<CinemachineFreeLook>();
        CinemachineCore.GetInputAxis = MyAxis;        
    }

    //void AA()
    //{
    //    //    freecam.Follow = �ٲ� ����� Transform;
    //    //    freecam.LookAt = 
    //    //freecam.m_YAxis.
    //    //freecam.m_Lens.Dutch = 0;
    //}    

    public float MyAxis(string axisname)
    {
        //�ܰ���
        scrollval = Input.GetAxis("Mouse ScrollWheel"); //���콺 �� �� ��������. �̰͵� ������ ������ 0, �����̸� -1~1 ���� ��.
        freecam.m_Lens.FieldOfView += scrollval * Time.deltaTime * 1000/*����ӵ�*/;

        //ī�޶��� ȸ�� ����...
        if (Input.GetMouseButton(1)) //���콺�� ������ Ŭ���� ���ӵǴ� �̻� �Ҹ�
        {
            return Input.GetAxis(axisname); //���콺 ������ Ŭ���� �� ������ ������ �� ���콺 ���� axis���� ����.
        }
        else
            return 0;         //��ġ �ƹ� ��ǲ�� ���¾� ���̴°�
    }
}
