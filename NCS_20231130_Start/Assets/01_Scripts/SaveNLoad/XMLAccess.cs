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