using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite; //SQLITE사용 위함
using System.IO; //파일 입출력 위함
using System.Data; //DB커넥션 위함

//DB는 너무 잦은 액세스와 변경은 안좋음...
//삭제도 뭔가 게임 시작할떄 쫙 하던지 게임 끄기전에 한번쫙 하던지..

//혹은 DB에다가 원본 데이터만 넣어두고, 그 데이터 활용해서 쓸거 다쓰고~
//저장하기 버튼 내지 여튼 한번씩만 저장 한번에 하는것이 훨씬 빠르고 훨씬 안정적임

//Open과 Close가 잦으면 안좋음== 그렇다고 오픈을 계속 해두는것도 안좋기 때문...
public class DBAccess : MonoBehaviour
{
    IDbConnection dbConnection = null;
    IDbCommand dbCommand = null;
    IDataReader reader = null;
    string filepath = "";

    void Start()
    {
        //Path.Combine() 이거랑 비슷한 걸로 
        filepath = Application.dataPath + Path.DirectorySeparatorChar/*경로 구분자*/ + "GameData.db";//"dbTest.db";

        if (File.Exists( filepath) == false)   
            File.Copy( Application.streamingAssetsPath + Path.DirectorySeparatorChar + "GameData.db"/*"dbTest.db"*/, filepath);

        //내가 새로 복사해서 만들었건, 기존에 있었건 간에 filepath에 이제 파일이 존재할 것임
        string file = "URI=file:" + Application.dataPath + Path.DirectorySeparatorChar + "GameData.db";//"dbTest.db";

        dbConnection = new SqliteConnection(file);
        dbConnection.Open(); //sqlite 파일과의 연결을 시작한다~~                

        //SeeData(dbConnection, dbCommand);
        //AddData();
        UpdateData();

        reader.Dispose(); //streamWriter 의 Flush와 동일...근데 얘는 필수..
        reader = null;//이거는 필수는 아닌데 해주면 깔끔

        dbCommand.Dispose(); //커맨드를 비워내기
        dbCommand = null; //이것도 필수는 아님. 해주면 깔끔...
        dbConnection.Close(); //제일 필수. open을 했으면 반드시 닫을것.....
                              //모든 stream~~~ 과 Connection~~~라인은 반드시 열었으면 닫기
        dbConnection = null; //요거는 권장
    }

    void AddData()
    {
        dbCommand = dbConnection.CreateCommand(); //연결을 시작한 파일에게 명령을 내릴 준비를 함
        dbCommand.CommandText = "Insert into ItemInfo (Name,Price,MaxCount,ItemType) values (\"활\", 1500, 1, 2 ) ";//CharacterInfo"; //CharacterInfo테이블 안에 있는 모든 정보를 가져와라.

        dbCommand.ExecuteReader(); //dbcommand로 실행하고, 실행내용을 IDataReader에 담아둠..

        SeeData(true);
    }

    void UpdateData()
    {
        dbCommand = dbConnection.CreateCommand(); //연결을 시작한 파일에게 명령을 내릴 준비를 함
        dbCommand.CommandText = "Update ItemInfo set Name = \"지팡이\" where Name = \"활\" and ItemType = 2 ";//CharacterInfo"; //CharacterInfo테이블 안에 있는 모든 정보를 가져와라.

        dbCommand.ExecuteReader(); //dbcommand로 실행하고, 실행내용을 IDataReader에 담아둠..

        SeeData(true);
    }

    void SeeData(bool all, AllEnum.ItemType type = AllEnum.ItemType.Food)
    {
        if (all)
        {
            Debug.Log("======================전체보기====================");
            dbCommand = dbConnection.CreateCommand(); //연결을 시작한 파일에게 명령을 내릴 준비를 함
            dbCommand.CommandText = "Select * from ItemInfo";//CharacterInfo"; //CharacterInfo테이블 안에 있는 모든 정보를 가져와라.

            reader = dbCommand.ExecuteReader(); //dbcommand로 실행하고, 실행내용을 IDataReader에 담아둠..
            while (reader.Read())
            {
                //Debug.Log는 내가 지금 유니티 에디터의 콘솔창에서 내용을 확인하기 위함.
                Debug.Log("Index : " + reader.GetInt32(0)); //해당 읽어온 한줄 중 0번째 요소를 정수로 치환
                Debug.Log("Name : " + reader.GetString(1)); //해당 읽어온 한줄 중 1번째 요소를 글자로 치환
                Debug.Log("가격 : " + reader./*GetFloat*/GetInt32(2)); //해당 읽어온 한줄 중 2번째 요소를 실수로 치환
                Debug.Log("최대개수 : " + reader.GetInt32(3));
                Debug.Log("아이템 타입 : " + (AllEnum.ItemType)reader.GetInt32(4));
            }

        }
        else
        {
            Debug.Log("====================일부검색====================");                        
            dbCommand = dbConnection.CreateCommand(); //명령어를 새로운것으로 설정할 것임.        
            dbCommand.CommandText = "Select * from ItemInfo where ItemType = " + (int)type;//CharacterInfo"; //CharacterInfo테이블 안에 있는 모든 정보를 가져와라.

            reader = dbCommand.ExecuteReader(); //dbcommand로 실행하고, 실행내용을 IDataReader에 담아둠..
            while (reader.Read())
            {
                //Debug.Log는 내가 지금 유니티 에디터의 콘솔창에서 내용을 확인하기 위함.
                Debug.Log("Index : " + reader.GetInt32(0)); //해당 읽어온 한줄 중 0번째 요소를 정수로 치환
                Debug.Log("Name : " + reader.GetString(1)); //해당 읽어온 한줄 중 1번째 요소를 글자로 치환
                Debug.Log("가격 : " + reader./*GetFloat*/GetInt32(2)); //해당 읽어온 한줄 중 2번째 요소를 실수로 치환
                Debug.Log("최대개수 : " + reader.GetInt32(3));
                Debug.Log("아이템 타입 : " + (AllEnum.ItemType)reader.GetInt32(4));
                Debug.Log("===============");
            }
        }                      
    }

}
