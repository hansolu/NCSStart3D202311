using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Text;

public class JsonAccess : MonoBehaviour
{
    string filepath ="";
    void Start()
    {
        filepath = Path.Combine(Application.dataPath, "jsontest.json");
        //Save();
        Load();
    }
    void Load()
    {
        //불러오는것도 동일하게... filestream사용과 file.readalltext...요 방법이 있고, 편한대로 쓰기...
        FileStream filestream = new FileStream(filepath, FileMode.Open);

        //이하 streamReader 나 binaryReader를 안쓰는 방식은, 내가 읽어들일 크기를 직접 지정해야하기떄문에 => 위험도가 있다.        
        byte[] data = new byte[filestream.Length]; 
        filestream.Read(data, 0, data.Length);//읽어들일 데이터와 크기를 직접 지정함...
        filestream.Close(); //할일이 끝났으니 close
        string jsondata = Encoding.UTF8.GetString(data); //이 읽어들인 jsondata가 json형식이기 떄문에 
        //예를들어 "i": 4 이런식의 상태ㅣ....

        JsonTestClass test = JsonConvert.DeserializeObject<JsonTestClass>(jsondata); //내 클래스에 맞춰서 변환해줌...
        test.ShowInfo();
    }
    

    void Save()
    {
        JsonTestClass test = new JsonTestClass();
        test.ShowInfo();//만들어진 제이슨 내용 확인
        string jsondata = JsonConvert.SerializeObject(test, Formatting.Indented);//두번쨰 인자로 Formatting ~ 지정을 안하면 보기좋게 나오지 않음.....
                

        //파일생성 원하는 형식으로...

        //1 Filestream + streamwriter 내지 binarywriter 해서 쓰기 //그래서 이때 binaryWriter를 이용하면 쪼금이나마 1차적 보안이 될것임
        FileStream filestream = new FileStream(filepath, FileMode.Create); //Append == 없으면 생성해서 열고, 있으면 있는거에다가 추가로뭘함..                
        //Create == 기존게 있다면 삭제하고 새로운 것으로 덮음...

        //StreamWriter  //사람이 읽을수있게 진행

        //이렇게 쓰는 방법도 있는데, 이거보다는 streamWrite내지 binarywriter쓰는게 좀더 안전함
        byte[] data = Encoding.UTF8.GetBytes(jsondata); //
        filestream.Write(data, 0, data.Length);
        filestream.Close();

        //2 File.WriteAlltext 로 쓰기
        //File.WriteAllText(filepath, jsondata );

        Debug.Log("파일생성완료");
    }
}

public class JsonTestClass_List //만약 추가로 JsonTestClass 데이터를 늘려가면서 쓰고싶다면,
                                //요렇게 한번더 내가 원하는 클래스를 List로 한번더 랩핑한 클래스를 만들면
                                //여러개를 무리없이 등록할 수 있음.
{
    public List<JsonTestClass> classlist = new List<JsonTestClass>();
}

//정말 데이터 저장용으로써의 클래스... 순수 데이터. 유니티에서의 무언가랑 상관없는 정말 수치들... 
public class JsonTestClass
{
    public int i;
    public float f;
    public double d;
    public bool b;
    public string s;
    public int[] arr;
    public List<float> list = new List<float>();
    public Dictionary<string, bool> dic = new Dictionary<string, bool>();
    public JsonTestClass() //데이터를 랜덤으로 임의로 채움
    {
        i = Random.Range(1, 10);
        f = Random.Range(10f, 15f);
        d = Random.Range(15f, 20f) * 0.11;
        b = i % 2 == 0 ? true : false;
        s = ((char)Random.Range((int)'a', (int)'z')).ToString();

        arr = new int[i];
        for (int j = 0; j < i; j++)
        {
            arr[j] = j;
            list.Add(j * 0.1f);
        }
        dic.Add("딕셔너리", true);
        dic.Add("더함", false);
    }

    public void ShowInfo()
    {
        Debug.Log("int i 의 내용 : " + i);
        Debug.Log("float f 의 내용 : " + f);
        Debug.Log("double d 의 내용 : " + d);
        Debug.Log("bool b 의 내용 : " + b);
        Debug.Log("string s 의 내용 : " + s);
                
        for (int j = 0; j < i; j++)
        {
            Debug.Log("배열 arr의 " + j + "번쨰 내용 : " + arr[j]);
        }
        
        for (int j = 0; j < i; j++)
        {
            Debug.Log("리스트 list의 " + j + "번쨰 내용 : " + list[j]);
        }

        foreach (var item in dic)
        {
            Debug.Log($"딕셔너리 dic[{item.Key}] 의 내용 = {item.Value}");
        }
    }
}