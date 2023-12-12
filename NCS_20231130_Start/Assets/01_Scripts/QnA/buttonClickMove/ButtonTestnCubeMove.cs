using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Dir
{     
    Right,
    Left,

    End
}

public class ButtonTestnCubeMove : MonoBehaviour
{
    Vector3 vec = Vector3.zero;

    Dir myDir = Dir.End;

    void Update()
    {
        switch (myDir)
        {
            case Dir.Right:
                vec.x = 1;
                transform.Translate(vec * Time.deltaTime);
                break;
            case Dir.Left:
                vec.x = -1;
                transform.Translate(vec * Time.deltaTime);
                break;            
            default: //Dir.End
                break;
        }
    }
    public void SetDir(Dir dir)
    {
        Debug.Log("���� ���� ���°� " + dir + "���·� ���� �Ǿ���");
        myDir = dir ;
    }
}
