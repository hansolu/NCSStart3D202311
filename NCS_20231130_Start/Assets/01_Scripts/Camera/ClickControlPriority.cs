using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ClickControlPriority : MonoBehaviour
{
    //��� ���� �켱�����϶�
    //�Ͻ������� ���� �켱������ ���������� �ʰ�, �� ������ �켱������ ���̴�
    //���� �켱������ �߿� �켱���� ġ�� ����� �ڵ�
    //MoveToTopOfPrioritySubqueue()    

    public CinemachineVirtualCamera vcam;
    void Start()
    {
        vcam.MoveToTopOfPrioritySubqueue(); //���� ���ϴ� vcam �� ī�޶�
        //���� �켱������ �߿��� ���� �켱���� ����.

        ICinemachineCamera currentcam =
        CinemachineCore.Instance.GetActiveBrain(0).ActiveVirtualCamera;

        //currentcam.Name //���� Ȱ��ȭ�� ī�޶��� �̸� ��������. ���ӿ�����Ʈ�� �̸��� ��µ�
        //currentcam.VirtualCameraGameObject == �Ϲ����� �� ����� gameobject�� ����
    }

}
