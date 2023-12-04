using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSample : MonoBehaviour
{
    float x, y = 0;
    Vector3 orgPos = Vector3.zero;
    Vector3 vec = Vector3.zero;
    Vector3 rotationVec = Vector3.zero;
    Vector3 currentVel = Vector3.zero;
    public Transform target; //플레이어
    Camera cam;
    void Start()
    {
        orgPos = transform.position; //시작전 나의 위치
        cam = this.GetComponent<Camera>();
    }

    void Update()
    {
        //줌인 줌아웃..
        Vector2 scroll = Input.mouseScrollDelta;               
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView - scroll.y , 15, 60); 

        //마우스 움직임을 받아 카메라 회전.
        y += Input.GetAxisRaw("Mouse X"); //마우스 좌우 ㅇ값으로 y축 회전을 하기 때문에 이렇게 하고
        x -= Input.GetAxisRaw("Mouse Y"); //마우스 위아래 값으로 x축 회전을 하는데, 하늘을 보려면 == 즉 y값이 양수이면, x는 -가 되어야 하늘을봄.

        vec.x = Mathf.Clamp(x, -20, 60); //
        vec.y = y;

        rotationVec = Vector3.SmoothDamp(rotationVec, vec, ref currentVel, Time.deltaTime); //부드러운 회전.
        transform.eulerAngles = rotationVec; //실제 내 회전에 대입        

        transform.position = target.position + orgPos; //타겟을 일정거리로 위치상 따라가게 함
    }
}
