using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//############�����ư�.. �ϴ� XML�̵�, JSon�̵�,
//Serialize ����� ������, Public �����̾���ϰ�
//Monobehavior�� ��ӹ��� ���ƾ� �������� �������� ��Ȱ��...

//������ �������� , ���� ��ü ���� ����....

//������ ���� �� �ε带 ���� �������̺� ��ӹ��� �ʴ� �⺻ Ŭ����
public class SavedObjectInfoList
{
    public List<SavedObjectInfo> list = new List<SavedObjectInfo>();//
}

//������ ���� �⺻ ������ Ŭ����
public class SavedObjectInfo 
{
    public int ObjectType;
    public int HP_Min;
    public int HP_Max;
    public int Speed_Min;
    public int Speed_Max;
    public string Color;

    public SavedObjectInfo(int objtype, int hpmin, int hpmax, int speedmin, int speedmax, string col)
    {
        ObjectType = objtype;
        HP_Min = hpmin;
        HP_Max = hpmax;
        Speed_Min = speedmin;
        Speed_Max = speedmax;
        this.Color = col;
    }
    public void ShowInfo()
    {
        Debug.Log("objecttype : " +ObjectType);
        Debug.Log("HP_Min : " + HP_Min);
        Debug.Log("HP_Max : " + HP_Max);
        Debug.Log("Speed_Min : " + Speed_Min);
        Debug.Log("Speed_Max : " + Speed_Max);
        Debug.Log("Color : " + Color);
    }
}
