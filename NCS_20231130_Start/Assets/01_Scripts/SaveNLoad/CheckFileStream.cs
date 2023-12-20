using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class CheckFileStream : MonoBehaviour
{
    void Start()
    {
        #region ����� ���� �� �ִ� �Ϲ� + �׳� ����
        //FileStream fs = new FileStream("testlog.log", FileMode.Create);
        //StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
        //sw.WriteLine(99);
        //sw.WriteLine(10.123);
        //sw.WriteLine("�ƹ�����~~~~");
        //double a = 1.23456789;
        //sw.WriteLine(a);

        //sw.Flush();
        //sw.Close();
        //fs.Close();
        #endregion

        #region ���̳ʸ� ����
        //FileStream fs = new FileStream("testlog.dat", FileMode.Create);
        //BinaryWriter sw = new BinaryWriter(fs);
        //sw.Write(99);
        //sw.Write("\n");
        //sw.Write(10.123);
        //sw.Write("\n");
        //sw.Write("�ƹ�����~~~~");
        //sw.Write("\n");
        //double a = 1.23456789;
        //sw.Write(a);       

        //sw.Flush();
        //sw.Close();
        //fs.Close();
        #endregion

        //StartCoroutine(CorTest());

        //����Ƽ�� ���
        //PathCheck();

        //PathFuncCheck();
        CheckFileName();
    }

    IEnumerator CorTest()
    {
        FileStream fs = new FileStream("testdat.txt", FileMode.Append /*,FileAccess.Write, FileShare.None*/);
        //�� �ɼ��� �����ɼ��� none�̾ ������ ������� �ʰ�, ���Ͽ� ������ ���� �뵵�θ� �����
        //�����ɼ��� read�� �ٲ㼭 �б�δ� ��������

        StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);

        int i = 0;
        while (i < 10)
        {
            Debug.Log(i + "�� ��");
            sw.WriteLine(i * 2);
            yield return new WaitForSeconds(0.2f);
            i++;
        }
        Debug.Log("while�� ������");
        sw.Flush(); //������ �������
        sw.Close(); //�ݱ�
        fs.Close(); //�ݱ�
    }

    void PathCheck()
    {
        FileStream fs = new FileStream("C:\\Users\\user\\Desktop\\a\\datapath.txt", FileMode.Append /*,FileAccess.Write, FileShare.None*/);
        //�� �ɼ��� �����ɼ��� none�̾ ������ ������� �ʰ�, ���Ͽ� ������ ���� �뵵�θ� �����
        //�����ɼ��� read�� �ٲ㼭 �б�δ� ��������

        StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);

        Debug.Log("" + Application.dataPath);
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            sw.WriteLine("������ �����Ϳ����� datapath :" + Application.dataPath);
            //D:/HANSOL/NCS_3D/NCSStart3D202311/NCS_20231130_Start/Assets �� ����Ƽ ������Ʈ�� �ִ� �� ���. �� Assets����.

            sw.WriteLine("������ �����Ϳ����� persistentpath :" + Application.persistentDataPath); //�����Ͱ� ����� ���� ��ġ.
            //�ش���ġ�� C:/Users/user/AppData/LocalLow/DefaultCompany(���� ������Ʈ�� ȸ���̸�)/NCS_20231130_Start(���� ������Ʈ �̸�)
            sw.WriteLine("������ �����Ϳ����� streamingAssets :" + Application.streamingAssetsPath);
            //D:/HANSOL/NCS_3D/NCSStart3D202311/NCS_20231130_Start/Assets/StreamingAssets
            //�� ������Ʈ Assets�ȿ� �ִ� �� streamingAssets����....
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            sw.WriteLine("������ �÷��̾���� datapath :" + Application.dataPath);
            //D:/HANSOL/NCS_3D/NCSStart3D202311/NCS_20231130_Start/Build/NCS_20231130_Start_Data �� ���� exe������ �ִ� �� ���.
            sw.WriteLine("������ �÷��̾���� persistentpath :" + Application.persistentDataPath);
            sw.WriteLine("������ �÷��̾���� streamingAssets :" + Application.streamingAssetsPath);
            //D:/HANSOL/NCS_3D/NCSStart3D202311/NCS_20231130_Start/Build/NCS_20231130_Start_Data/StreamingAssets
            //�ش� ���� ���� data����~ streamingAssets ���� �� ��ȯ => ������ġ�� c����̺�� �Ű����ٸ� /Build ���� ��ΰ� �� �ٲ��������..
        }

        //    Path.DirectorySeparatorChar ==�÷����� ������...

        sw.WriteLine("=========================");
        Debug.Log("�Է� ����");
        sw.Flush(); //������ �������
        sw.Close(); //�ݱ�
        fs.Close(); //�ݱ�
                    //        
    }

    void PathFuncCheck()
    {
        File.Copy("testdat.txt", "testdat.dat", true);//����. ������ ���ڷ� true�� ��⶧���� ����� �Ǿ���
        File.Move("testdat.txt", "testdat2.dat");//�̵�. ù��° ������ ������ �ι�° ���� �������� �����Ŵ.
        if (File.Exists("testdat.dat")) //�������� �����Ѵٸ�
        {
            Debug.Log("testdat.dat ������ ������");
            File.Delete("testdat.dat"); //�� ������ ���ֶ�
            Debug.Log("���� ������");
        }
        else
        {
            Debug.Log("testdat.dat ������ ����");
        }

        #region FileŬ�������� �Է��ϱ�
        ////�ݺ����̰ų� ������ ����� �ڲ� �ٴ´ٸ�. stringbuilder�� �����ϰ� �ű⿡�ٰ� append�ؼ� ���� �� ���̰� ����������
        ////string���� ��ȯ�ϴ°��� ������ ����...
        //StringBuilder strbuilder = new StringBuilder();
        //strbuilder.Append("����..���ϴ�...");
        //strbuilder.Append("�߰�1").Append("\n�߰�2\n�߰�3");

        //////string�⺻���ٰ� �߰�������, �ݺ�������, Ȥ�� �� ������ ���� ���� �ȴٸ�
        //////string �� �����ڸ� ���⺸�ٴ� STringbuilder�� ��� �ξ� ����ȭ�� ������ ��....
        ////string a = "����";
        ////a += "�����߰�\n";
        ////a += "\n �߰� ���" + "���� ���Ѵ�";

        ////for (int i = 0; i < 100; i++)
        ////{
        ////    //a += i.ToString();
        ////    strbuilder.Append(i);
        ////}

        //Debug.Log(Path.Combine(Application.dataPath, "FileWriteAllText.txt"));

        //WriteAllText == string�ϳ��� ���°Ű� string�迭�� �����Ͽ� �� �ְ������, WriteAllLines���...
        //File.WriteAllText( Path.Combine( Application.dataPath, "FileWriteAllText.txt"),  strbuilder.ToString());
        #endregion

        #region ����Ŭ�������� �����ϴ� �ҷ����� (�ǽ�������)
        ////File.ReadAllText //string��ȯ. �� ������ ���ٷ� �ҷ��� �� ����
        //string[] strarr= File.ReadAllLines(Path.Combine(Application.dataPath, "FileWriteAllText.txt")); //string[] �迭 ��ȯ. �� ���͸��� ���پ� �޾ƿͼ� �迭�� �� �޾Ƴ�������.
        ////�о�� ���� ����ϱ�...
        //for (int i = 0; i < strarr.Length; i++)
        //{
        //    Debug.Log(i+"��° �� ���� : " +strarr[i]);
        //}
        #endregion

        #region ���ϴ� ���ϵ� ��ϸ� �޾ƿ���(�ǽ�)
        //Ư�� ����� Ư�� ���ϵ鸸 �ҷ�����....
        //�� ������Ʈ�� Assets\01_Scripts ���� ������� �޾ƿ��� + ��ũ��Ʈ �鸸 �޾ƿ��� *.cs

        string path = Path.Combine(Application.dataPath, "01_Scripts");
        string[] alldirectories = Directory.GetDirectories(path);
        Debug.Log(path + "��� ���� ��� ���� ���");
        for (int i = 0; i < alldirectories.Length; i++)
        {
            //�׳� �޾ƿ��� �Ǹ� ��ü ��θ� ��� ��ȯ�ϱ� ������ �ʹ� ��
            //�����̸��� �߷�����
            Debug.Log(Path.GetFileName(alldirectories[i]));              
        }
        
        Debug.Log("\n�ش� ��ξ��� ��� ��ũ��Ʈ ���");
        string[] allscfiles = Directory.GetFiles(path, "*.cs"); //�ش� ��ο��ִ� ã�����ǰ� ���յǴ� ��� ���� ����� ��ȯ��...
        for (int i = 0; i < allscfiles.Length; i++)
        {
            //��� ��θ� �޾ƿ��⋚���� �ű⼭ �����̸��� ��������..
            Debug.Log(Path.GetFileName(allscfiles[i]));

            //���� Ȯ���ڸ� �Ⱥ��̰� �ϰ�ʹٸ�
            //Path.GetFileNameWithoutExtension(���) //�ش� ��ο��� �̸��� �����ϰ� ��ζ� Ȯ���ڸ� ��� �����ϰ� ��.
        }

        //���� ���׵��.. Path.GetFileName()�� �ϸ� �������    
        string fullpath= "���\\���\\���\\���\\���\\���\\�������̸� ";
        string[] aaa = fullpath.Split('\\');
        Debug.Log(aaa[aaa.Length - 1]); 
        #endregion
    }

    void CheckFileName()
    {        
        string filename = "abc!d@e#f$%^&*/dir";
        char[] aaa = Path.GetInvalidFileNameChars();
        Debug.Log("�ȵŸ��");
        for (int i = 0; i < aaa.Length; i++)
        {
            Debug.Log(aaa[i]);
        }

        string bbb = new string ("!@#$%^&*()"); 

        Debug.Log("==========");
        int num = filename.IndexOfAny(/*aaa*/bbb.ToCharArray());        
        if (num != -1)
        {
            Debug.Log(num+"�� ��ġ�� ��� �̸����� ����ġ ���� ���ڰ� ����");
        }        

        //�̰� Ȱ���ϸ�, �Ŀ� ĳ���� �̸����⿡�� �̻��� �۾� ���ϵ��� ���� �� ����.
        //string.Empty <color=#aa10db>

    }
}
