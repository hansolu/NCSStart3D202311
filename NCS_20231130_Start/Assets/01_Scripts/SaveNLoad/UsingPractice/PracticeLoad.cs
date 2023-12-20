using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class PracticeLoad : MonoBehaviour
{
    public GameObject[] AllObjectOrgPrefab; //객체 생성용 원본

    List<BaseObjectInfo> AllObjectList = new List<BaseObjectInfo>(); //생성된 객체 관리용




    Dictionary<AllEnum.ObjectType, SavedObjectInfo> AllBaseInfos
        = new Dictionary<AllEnum.ObjectType, SavedObjectInfo>();
    //저장했던 데이터 원본들...

    string filepath = "";
    void Start()
    {        
        filepath = Path.Combine(Application.dataPath, "practiceLoad.json");
        //SaveOrg();
        LoadOrg();
    }
    void SaveOrg()
    {
        SavedObjectInfoList savelist = new SavedObjectInfoList();
        savelist.list.Add(new SavedObjectInfo(0, 100,150, 2,4,"red"));
        savelist.list.Add(new SavedObjectInfo(1, 200, 250, 5, 7, "blue"));
        savelist.list.Add(new SavedObjectInfo(2, 100, 300, 1, 10, "green"));
                
        string jsondata = JsonConvert.SerializeObject(savelist, Formatting.Indented);

        File.WriteAllText(filepath, jsondata );

        Debug.Log("파일생성완료");
    }
    void LoadOrg()
    {
        string jsondata = File.ReadAllText(filepath);
        SavedObjectInfoList savelist = JsonConvert.DeserializeObject<SavedObjectInfoList>(jsondata); //내 클래스에 맞춰서 변환해줌...
        for (int i = 0; i < savelist.list.Count; i++)
        {
            AllBaseInfos.Add((AllEnum.ObjectType)savelist.list[i].ObjectType, savelist.list[i]);
        }                   
    }
        
    public void CreateType(int num)
    {
        GameObject tmp = Instantiate(AllObjectOrgPrefab[num], 
            Vector3.zero + Vector3.right * AllObjectList.Count, 
            Quaternion.identity); //프리팹 복사하여 생성

        BaseObjectInfo info = tmp.GetComponent<BaseObjectInfo>();
        //해당 프리팹에 붙어있는 스크립트에 접근.

        info.SetInfo(AllBaseInfos[(AllEnum.ObjectType)num]); //데이터설정 완료
        AllObjectList.Add(info); //내가 만든 객체들 관리용으로 리스트에 그냥 등록해둠...
    }
}
