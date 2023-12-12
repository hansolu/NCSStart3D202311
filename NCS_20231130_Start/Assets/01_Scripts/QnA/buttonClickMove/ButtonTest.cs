using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonTest : MonoBehaviour
{
    public ButtonTestnCubeMove Cube;
    Button button;
    public Dir Right = Dir.End;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(AA);           
    }

    void AA() //이거는 그냥 코드로 클릭이벤트 더하는 예시
    {
        Debug.Log("버튼눌림~~");
    }

    public void MoveX() //마우스로 버튼을 처음 꾹 누르면 한번불림
    {
        Debug.Log("클릭시작");
        Cube.SetDir(Right);
    }
    public void PointUp() //누르고있던 마우스에서 손을떼면 한번 불림
    {
        Debug.Log("클릭손뗌");
        Cube.SetDir(Dir.End);
    }
}
