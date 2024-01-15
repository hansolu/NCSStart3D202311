using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization; //�Ž�. �˾Ƽ� �ø��������ȭ ����
using System.Xml; //����

public class XMLAccess : MonoBehaviour
{
    string filepath = "";
    void Start()
    {
        //filepath = Path.Combine(Application.dataPath, "xmltest.xml");        
        filepath = Path.Combine(Application.dataPath, "TestItemXML.xml");
        //Save();
        Load();

        //LoadItemArrayTest();
    }
    void LoadItemArrayTest()
    {        
        XmlDocument xml = new XmlDocument();
        xml.Load(filepath);

        List<XMLTestItemClass> list = new List<XMLTestItemClass>();        
        foreach (XmlNode item in xml.GetElementsByTagName("Item"))
        {
            list.Add(new XMLTestItemClass(item["Index"].InnerText,
                item["Name"].InnerText,
                item["Type"].InnerText,
                item["HP"].InnerText,
                item["Att"].InnerText
                ));
        }
        
        for (int i = 0; i < list.Count; i++)
        {
            list[i].ShowInfo();
        }
    }

    void Load()
    {
        XmlSerializer xml = new XmlSerializer(typeof(XMLTestClass));
        //XmlSerializer xml = new XmlSerializer(typeof(XMLTestItemListClass));
        FileStream filestream = new FileStream(filepath, FileMode.Open);

        XMLTestClass test = xml.Deserialize(filestream) as XMLTestClass;
        //XMLTestItemListClass test = xml.Deserialize(filestream) as XMLTestItemListClass; 
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

        Debug.Log("XML���̺� �Ϸ�");
        #region Serializer �������� ����
        ////���� serializer�� �����ٸ� �ؾ��� �ϵ�...
        //XmlDocument xmldoc = new XmlDocument();
        //XmlNode root = xmldoc.CreateNode(XmlNodeType.Element, "XMLTest", string.Empty);
        //xmldoc.AppendChild(root);
        //XmlNode child = xmldoc.CreateNode(XmlNodeType.Element, "XMLTestClassContent", 
        //    string.Empty);
        //root.AppendChild(child);

        ////�ش� XMLTestClass �� ��� ��Ҹ� �̷��� childģ���� child�� �̸��ο��ϰ� ���� �ο��ϰ� �ٿ����Ѵ�...
        //XmlElement i = xmldoc.CreateElement("i");
        //i.InnerText = test.i.ToString();
        //child.AppendChild(i);
        
        //XmlElement f = xmldoc.CreateElement("f");
        //f.InnerText = test.f.ToString();
        //child.AppendChild(f);

        #endregion
    }
}

//json�� ������ xml�� �ȵ�
//public class XMLTestItemListClass
//{
//    public List<XMLTestItemClass> list = new List<XMLTestItemClass>();

//    public void ShowInfo()
//    {
//        for (int i = 0; i < list.Count; i++)
//        {
//            list[i].ShowInfo();
//        }
//    }
//}


public class XMLTestItemClass
{
    public int Index;
    public string Name;
    public string Type;
    public float HP;
    public float Att;

    public XMLTestItemClass(string index, string name, string type, string hp, string att)
    {
        Index = int.Parse( index);
        this.Name = name;
        this.Type = type;
        this.HP = float.Parse(hp);
        this.Att = float.Parse(att);
    }

    public void ShowInfo()
    {
        Debug.Log($"Index : {Index}\nName : {Name}\nType : {Type}\nHP : {HP}\nATT : {Att}\n");
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
    //��ųʸ��� �ȵ� 

    public XMLTestClass() //�����͸� �������� ���Ƿ� ä��
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
    }
}