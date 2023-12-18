using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite; //SQLITE��� ����
using System.IO; //���� ����� ����
using System.Data; //DBĿ�ؼ� ����

//DB�� �ʹ� ���� �׼����� ������ ������...
//������ ���� ���� �����ҋ� �� �ϴ��� ���� �������� �ѹ��� �ϴ���..

//Ȥ�� DB���ٰ� ���� �����͸� �־�ΰ�, �� ������ Ȱ���ؼ� ���� �پ���~
//�����ϱ� ��ư ���� ��ư �ѹ����� ���� �ѹ��� �ϴ°��� �ξ� ������ �ξ� ��������

//Open�� Close�� ������ ������== �׷��ٰ� ������ ��� �صδ°͵� ������ ����...
public class DBAccess : MonoBehaviour
{
    IDbConnection dbConnection = null;
    IDbCommand dbCommand = null;
    IDataReader reader = null;
    string filepath = "";

    void Start()
    {
        //Path.Combine() �̰Ŷ� ����� �ɷ� 
        filepath = Application.dataPath + Path.DirectorySeparatorChar/*��� ������*/ + "GameData.db";//"dbTest.db";

        if (File.Exists( filepath) == false)   
            File.Copy( Application.streamingAssetsPath + Path.DirectorySeparatorChar + "GameData.db"/*"dbTest.db"*/, filepath);

        //���� ���� �����ؼ� �������, ������ �־��� ���� filepath�� ���� ������ ������ ����
        string file = "URI=file:" + Application.dataPath + Path.DirectorySeparatorChar + "GameData.db";//"dbTest.db";

        dbConnection = new SqliteConnection(file);
        dbConnection.Open(); //sqlite ���ϰ��� ������ �����Ѵ�~~                

        //SeeData(dbConnection, dbCommand);
        //AddData();
        UpdateData();

        reader.Dispose(); //streamWriter �� Flush�� ����...�ٵ� ��� �ʼ�..
        reader = null;//�̰Ŵ� �ʼ��� �ƴѵ� ���ָ� ���

        dbCommand.Dispose(); //Ŀ�ǵ带 �������
        dbCommand = null; //�̰͵� �ʼ��� �ƴ�. ���ָ� ���...
        dbConnection.Close(); //���� �ʼ�. open�� ������ �ݵ�� ������.....
                              //��� stream~~~ �� Connection~~~������ �ݵ�� �������� �ݱ�
        dbConnection = null; //��Ŵ� ����
    }

    void AddData()
    {
        dbCommand = dbConnection.CreateCommand(); //������ ������ ���Ͽ��� ����� ���� �غ� ��
        dbCommand.CommandText = "Insert into ItemInfo (Name,Price,MaxCount,ItemType) values (\"Ȱ\", 1500, 1, 2 ) ";//CharacterInfo"; //CharacterInfo���̺� �ȿ� �ִ� ��� ������ �����Ͷ�.

        dbCommand.ExecuteReader(); //dbcommand�� �����ϰ�, ���೻���� IDataReader�� ��Ƶ�..

        SeeData(true);
    }

    void UpdateData()
    {
        dbCommand = dbConnection.CreateCommand(); //������ ������ ���Ͽ��� ����� ���� �غ� ��
        dbCommand.CommandText = "Update ItemInfo set Name = \"������\" where Name = \"Ȱ\" and ItemType = 2 ";//CharacterInfo"; //CharacterInfo���̺� �ȿ� �ִ� ��� ������ �����Ͷ�.

        dbCommand.ExecuteReader(); //dbcommand�� �����ϰ�, ���೻���� IDataReader�� ��Ƶ�..

        SeeData(true);
    }

    void SeeData(bool all, AllEnum.ItemType type = AllEnum.ItemType.Food)
    {
        if (all)
        {
            Debug.Log("======================��ü����====================");
            dbCommand = dbConnection.CreateCommand(); //������ ������ ���Ͽ��� ����� ���� �غ� ��
            dbCommand.CommandText = "Select * from ItemInfo";//CharacterInfo"; //CharacterInfo���̺� �ȿ� �ִ� ��� ������ �����Ͷ�.

            reader = dbCommand.ExecuteReader(); //dbcommand�� �����ϰ�, ���೻���� IDataReader�� ��Ƶ�..
            while (reader.Read())
            {
                //Debug.Log�� ���� ���� ����Ƽ �������� �ܼ�â���� ������ Ȯ���ϱ� ����.
                Debug.Log("Index : " + reader.GetInt32(0)); //�ش� �о�� ���� �� 0��° ��Ҹ� ������ ġȯ
                Debug.Log("Name : " + reader.GetString(1)); //�ش� �о�� ���� �� 1��° ��Ҹ� ���ڷ� ġȯ
                Debug.Log("���� : " + reader./*GetFloat*/GetInt32(2)); //�ش� �о�� ���� �� 2��° ��Ҹ� �Ǽ��� ġȯ
                Debug.Log("�ִ밳�� : " + reader.GetInt32(3));
                Debug.Log("������ Ÿ�� : " + (AllEnum.ItemType)reader.GetInt32(4));
            }

        }
        else
        {
            Debug.Log("====================�Ϻΰ˻�====================");                        
            dbCommand = dbConnection.CreateCommand(); //��ɾ ���ο������ ������ ����.        
            dbCommand.CommandText = "Select * from ItemInfo where ItemType = " + (int)type;//CharacterInfo"; //CharacterInfo���̺� �ȿ� �ִ� ��� ������ �����Ͷ�.

            reader = dbCommand.ExecuteReader(); //dbcommand�� �����ϰ�, ���೻���� IDataReader�� ��Ƶ�..
            while (reader.Read())
            {
                //Debug.Log�� ���� ���� ����Ƽ �������� �ܼ�â���� ������ Ȯ���ϱ� ����.
                Debug.Log("Index : " + reader.GetInt32(0)); //�ش� �о�� ���� �� 0��° ��Ҹ� ������ ġȯ
                Debug.Log("Name : " + reader.GetString(1)); //�ش� �о�� ���� �� 1��° ��Ҹ� ���ڷ� ġȯ
                Debug.Log("���� : " + reader./*GetFloat*/GetInt32(2)); //�ش� �о�� ���� �� 2��° ��Ҹ� �Ǽ��� ġȯ
                Debug.Log("�ִ밳�� : " + reader.GetInt32(3));
                Debug.Log("������ Ÿ�� : " + (AllEnum.ItemType)reader.GetInt32(4));
                Debug.Log("===============");
            }
        }                      
    }

}
