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

    void AA() //�̰Ŵ� �׳� �ڵ�� Ŭ���̺�Ʈ ���ϴ� ����
    {
        Debug.Log("��ư����~~");
    }

    public void MoveX() //���콺�� ��ư�� ó�� �� ������ �ѹ��Ҹ�
    {
        Debug.Log("Ŭ������");
        Cube.SetDir(Right);
    }
    public void PointUp() //�������ִ� ���콺���� �������� �ѹ� �Ҹ�
    {
        Debug.Log("Ŭ���ն�");
        Cube.SetDir(Dir.End);
    }
}
