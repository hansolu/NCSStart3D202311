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
        //�ҷ����°͵� �����ϰ�... filestream���� file.readalltext...�� ����� �ְ�, ���Ѵ�� ����...
        FileStream filestream = new FileStream(filepath, FileMode.Open);

        //���� streamReader �� binaryReader�� �Ⱦ��� �����, ���� �о���� ũ�⸦ ���� �����ؾ��ϱ⋚���� => ���赵�� �ִ�.        
        byte[] data = new byte[filestream.Length]; 
        filestream.Read(data, 0, data.Length);//�о���� �����Ϳ� ũ�⸦ ���� ������...
        filestream.Close(); //������ �������� close
        string jsondata = Encoding.UTF8.GetString(data); //�� �о���� jsondata�� json�����̱� ������ 
        //������� "i": 4 �̷����� ���¤�....

        JsonTestClass test = JsonConvert.DeserializeObject<JsonTestClass>(jsondata); //�� Ŭ������ ���缭 ��ȯ����...
        test.ShowInfo();
    }
    

    void Save()
    {
        JsonTestClass test = new JsonTestClass();
        test.ShowInfo();//������� ���̽� ���� Ȯ��
        string jsondata = JsonConvert.SerializeObject(test, Formatting.Indented);//�ι��� ���ڷ� Formatting ~ ������ ���ϸ� �������� ������ ����.....
                

        //���ϻ��� ���ϴ� ��������...

        //1 Filestream + streamwriter ���� binarywriter �ؼ� ���� //�׷��� �̶� binaryWriter�� �̿��ϸ� �ɱ��̳��� 1���� ������ �ɰ���
        FileStream filestream = new FileStream(filepath, FileMode.Create); //Append == ������ �����ؼ� ����, ������ �ִ°ſ��ٰ� �߰��ι���..                
        //Create == ������ �ִٸ� �����ϰ� ���ο� ������ ����...

        //StreamWriter  //����� �������ְ� ����

        //�̷��� ���� ����� �ִµ�, �̰ź��ٴ� streamWrite���� binarywriter���°� ���� ������
        byte[] data = Encoding.UTF8.GetBytes(jsondata); //
        filestream.Write(data, 0, data.Length);
        filestream.Close();

        //2 File.WriteAlltext �� ����
        //File.WriteAllText(filepath, jsondata );

        Debug.Log("���ϻ����Ϸ�");
    }
}

public class JsonTestClass_List //���� �߰��� JsonTestClass �����͸� �÷����鼭 ����ʹٸ�,
                                //�䷸�� �ѹ��� ���� ���ϴ� Ŭ������ List�� �ѹ��� ������ Ŭ������ �����
                                //�������� �������� ����� �� ����.
{
    public List<JsonTestClass> classlist = new List<JsonTestClass>();
}

//���� ������ ��������ν��� Ŭ����... ���� ������. ����Ƽ������ ���𰡶� ������� ���� ��ġ��... 
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
    public JsonTestClass() //�����͸� �������� ���Ƿ� ä��
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
        dic.Add("��ųʸ�", true);
        dic.Add("����", false);
    }

    public void ShowInfo()
    {
        Debug.Log("int i �� ���� : " + i);
        Debug.Log("float f �� ���� : " + f);
        Debug.Log("double d �� ���� : " + d);
        Debug.Log("bool b �� ���� : " + b);
        Debug.Log("string s �� ���� : " + s);
                
        for (int j = 0; j < i; j++)
        {
            Debug.Log("�迭 arr�� " + j + "���� ���� : " + arr[j]);
        }
        
        for (int j = 0; j < i; j++)
        {
            Debug.Log("����Ʈ list�� " + j + "���� ���� : " + list[j]);
        }

        foreach (var item in dic)
        {
            Debug.Log($"��ųʸ� dic[{item.Key}] �� ���� = {item.Value}");
        }
    }
}