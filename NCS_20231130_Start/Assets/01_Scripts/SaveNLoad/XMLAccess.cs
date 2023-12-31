using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization; //신식. 알아서 시리얼라이즈화 해줌
using System.Xml; //구식

public class XMLAccess : MonoBehaviour
{
    string filepath = "";
    void Start()
    {        
        filepath = Path.Combine(Application.dataPath, "xmltest.xml");

        //Save();
        Load();
    }

    void Load()
    {
        XmlSerializer xml = new XmlSerializer(typeof(XMLTestClass));
        FileStream filestream = new FileStream(filepath, FileMode.Open);

        XMLTestClass test = xml.Deserialize(filestream) as XMLTestClass;
        filestream.Close();

        test.ShowInfo();
    }

    void Save()
    {
        XMLTestClass test = new XMLTestClass();
        test.ShowInfo();
        XmlSerializer xml = new XmlSerializer(typeof(XMLTestClass));

        FileStream filestream = new FileStream(filepath, FileMode.Create);
        xml.Serialize(filestream, test);
        filestream.Close();

        Debug.Log("XML세이브 완료");
        #region Serializer 나오기전 형식
        ////위의 serializer가 없었다면 해야할 일들...
        //XmlDocument xmldoc = new XmlDocument();
        //XmlNode root = xmldoc.CreateNode(XmlNodeType.Element, "XMLTest", string.Empty);
        //xmldoc.AppendChild(root);
        //XmlNode child = xmldoc.CreateNode(XmlNodeType.Element, "XMLTestClassContent", 
        //    string.Empty);
        //root.AppendChild(child);

        ////해당 XMLTestClass 의 모든 요소를 이렇게 child친구의 child로 이름부여하고 내용 부여하고 붙여야한다...
        //XmlElement i = xmldoc.CreateElement("i");
        //i.InnerText = test.i.ToString();
        //child.AppendChild(i);
        
        //XmlElement f = xmldoc.CreateElement("f");
        //f.InnerText = test.f.ToString();
        //child.AppendChild(f);

        #endregion
    }
}

public class XMLTestClass
{
    public int i;
    public float f;
    public double d;
    public bool b;
    public string s;
    public int[] arr;
    public List<float> list = new List<float>();
    //딕셔너리가 안됨 

    public XMLTestClass() //데이터를 랜덤으로 임의로 채움
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
    }
}