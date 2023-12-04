using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ClickControlPriority : MonoBehaviour
{
    //모두 같은 우선순위일때
    //일시적으로 실제 우선순위를 변경하지는 않고, 그 순간만 우선순위를 높이는
    //같은 우선순위들 중에 우선으로 치게 만드는 코드
    //MoveToTopOfPrioritySubqueue()    

    public CinemachineVirtualCamera vcam;
    void Start()
    {
        vcam.MoveToTopOfPrioritySubqueue(); //내가 원하는 vcam 그 카메라가
        //같은 우선순위들 중에서 가장 우선권을 가짐.

        ICinemachineCamera currentcam =
        CinemachineCore.Instance.GetActiveBrain(0).ActiveVirtualCamera;

        //currentcam.Name //현재 활성화된 카메라의 이름 가져오기. 게임오브젝트의 이름이 출력됨
        //currentcam.VirtualCameraGameObject == 일반적인 그 대상의 gameobject와 같다
    }

}
