using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//############뭐가됐건.. 일단 XML이든, JSon이든,
//Serialize 기능을 쓰려면, Public 선언이어야하고
//Monobehavior를 상속받지 말아야 데이터의 컨버팅이 원활함...

//데이터 원본따로 , 실제 객체 뭔가 따로....

//데이터 저장 및 로드를 위한 모노비헤이비어를 상속받지 않는 기본 클래스
public class SavedObjectInfoList
{
    public List<SavedObjectInfo> list = new List<SavedObjectInfo>();//
}

//정말로 가장 기본 데이터 클래스
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
