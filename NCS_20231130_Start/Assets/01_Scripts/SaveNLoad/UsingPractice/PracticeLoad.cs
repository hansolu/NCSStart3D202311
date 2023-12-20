using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class PracticeLoad : MonoBehaviour
{
    public GameObject[] AllObjectOrgPrefab; //��ü ������ ����

    List<BaseObjectInfo> AllObjectList = new List<BaseObjectInfo>(); //������ ��ü ������




    Dictionary<AllEnum.ObjectType, SavedObjectInfo> AllBaseInfos
        = new Dictionary<AllEnum.ObjectType, SavedObjectInfo>();
    //�����ߴ� ������ ������...

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

        Debug.Log("���ϻ����Ϸ�");
    }
    void LoadOrg()
    {
        string jsondata = File.ReadAllText(filepath);
        SavedObjectInfoList savelist = JsonConvert.DeserializeObject<SavedObjectInfoList>(jsondata); //�� Ŭ������ ���缭 ��ȯ����...
        for (int i = 0; i < savelist.list.Count; i++)
        {
            AllBaseInfos.Add((AllEnum.ObjectType)savelist.list[i].ObjectType, savelist.list[i]);
        }                   
    }
        
    public void CreateType(int num)
    {
        GameObject tmp = Instantiate(AllObjectOrgPrefab[num], 
            Vector3.zero + Vector3.right * AllObjectList.Count, 
            Quaternion.identity); //������ �����Ͽ� ����

        BaseObjectInfo info = tmp.GetComponent<BaseObjectInfo>();
        //�ش� �����տ� �پ��ִ� ��ũ��Ʈ�� ����.

        info.SetInfo(AllBaseInfos[(AllEnum.ObjectType)num]); //�����ͼ��� �Ϸ�
        AllObjectList.Add(info); //���� ���� ��ü�� ���������� ����Ʈ�� �׳� ����ص�...
    }
}
