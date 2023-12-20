using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class CheckFileStream : MonoBehaviour
{
    void Start()
    {
        #region 사람이 읽을 수 있는 일반 + 그냥 생성
        //FileStream fs = new FileStream("testlog.log", FileMode.Create);
        //StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
        //sw.WriteLine(99);
        //sw.WriteLine(10.123);
        //sw.WriteLine("아무내용~~~~");
        //double a = 1.23456789;
        //sw.WriteLine(a);

        //sw.Flush();
        //sw.Close();
        //fs.Close();
        #endregion

        #region 바이너리 쓰기
        //FileStream fs = new FileStream("testlog.dat", FileMode.Create);
        //BinaryWriter sw = new BinaryWriter(fs);
        //sw.Write(99);
        //sw.Write("\n");
        //sw.Write(10.123);
        //sw.Write("\n");
        //sw.Write("아무내용~~~~");
        //sw.Write("\n");
        //double a = 1.23456789;
        //sw.Write(a);       

        //sw.Flush();
        //sw.Close();
        //fs.Close();
        #endregion

        //StartCoroutine(CorTest());

        //유니티의 경로
        //PathCheck();

        //PathFuncCheck();
        CheckFileName();
    }

    IEnumerator CorTest()
    {
        FileStream fs = new FileStream("testdat.txt", FileMode.Append /*,FileAccess.Write, FileShare.None*/);
        //이 옵션은 공유옵션이 none이어서 공유는 허용하지 않고, 파일에 접근은 쓰는 용도로만 허락함
        //공유옵션을 read로 바꿔서 읽기로는 열수있음

        StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);

        int i = 0;
        while (i < 10)
        {
            Debug.Log(i + "의 값");
            sw.WriteLine(i * 2);
            yield return new WaitForSeconds(0.2f);
            i++;
        }
        Debug.Log("while문 끝났음");
        sw.Flush(); //내용을 비워내고
        sw.Close(); //닫기
        fs.Close(); //닫기
    }

    void PathCheck()
    {
        FileStream fs = new FileStream("C:\\Users\\user\\Desktop\\a\\datapath.txt", FileMode.Append /*,FileAccess.Write, FileShare.None*/);
        //이 옵션은 공유옵션이 none이어서 공유는 허용하지 않고, 파일에 접근은 쓰는 용도로만 허락함
        //공유옵션을 read로 바꿔서 읽기로는 열수있음

        StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);

        Debug.Log("" + Application.dataPath);
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            sw.WriteLine("윈도우 에디터에서의 datapath :" + Application.dataPath);
            //D:/HANSOL/NCS_3D/NCSStart3D202311/NCS_20231130_Start/Assets 내 유니티 프로젝트가 있는 그 경로. 의 Assets까지.

            sw.WriteLine("윈도우 에디터에서의 persistentpath :" + Application.persistentDataPath); //에디터건 빌드건 같은 위치.
            //해당위치는 C:/Users/user/AppData/LocalLow/DefaultCompany(각자 프로젝트의 회사이름)/NCS_20231130_Start(각자 프로젝트 이름)
            sw.WriteLine("윈도우 에디터에서의 streamingAssets :" + Application.streamingAssetsPath);
            //D:/HANSOL/NCS_3D/NCSStart3D202311/NCS_20231130_Start/Assets/StreamingAssets
            //내 프로젝트 Assets안에 있는 그 streamingAssets폴더....
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            sw.WriteLine("윈도우 플레이어에서의 datapath :" + Application.dataPath);
            //D:/HANSOL/NCS_3D/NCSStart3D202311/NCS_20231130_Start/Build/NCS_20231130_Start_Data 내 빌드 exe파일이 있는 그 경로.
            sw.WriteLine("윈도우 플레이어에서의 persistentpath :" + Application.persistentDataPath);
            sw.WriteLine("윈도우 플레이어에서의 streamingAssets :" + Application.streamingAssetsPath);
            //D:/HANSOL/NCS_3D/NCSStart3D202311/NCS_20231130_Start/Build/NCS_20231130_Start_Data/StreamingAssets
            //해당 빌드 안의 data안의~ streamingAssets 폴더 를 반환 => 빌드위치가 c드라이브로 옮겨진다면 /Build 앞의 경로가 다 바뀌어있을것..
        }

        //    Path.DirectorySeparatorChar ==플랫폼별 구분자...

        sw.WriteLine("=========================");
        Debug.Log("입력 종료");
        sw.Flush(); //내용을 비워내고
        sw.Close(); //닫기
        fs.Close(); //닫기
                    //        
    }

    void PathFuncCheck()
    {
        File.Copy("testdat.txt", "testdat.dat", true);//복사. 세번쨰 인자로 true를 줬기때문에 덮어쓰기 되었음
        File.Move("testdat.txt", "testdat2.dat");//이동. 첫번째 인자의 파일을 두번째 인자 사항으로 변경시킴.
        if (File.Exists("testdat.dat")) //이파일이 존재한다면
        {
            Debug.Log("testdat.dat 파일이 존재함");
            File.Delete("testdat.dat"); //그 파일을 없애라
            Debug.Log("파일 삭제함");
        }
        else
        {
            Debug.Log("testdat.dat 파일이 없음");
        }

        #region File클래스에서 입력하기
        ////반복적이거나 내용이 긴것이 자꾸 붙는다면. stringbuilder로 선언하고 거기에다가 append해서 내용 다 붙이고 마지막으로
        ////string으로 변환하는것이 가볍고 빠름...
        //StringBuilder strbuilder = new StringBuilder();
        //strbuilder.Append("내용..씁니다...");
        //strbuilder.Append("추가1").Append("\n추가2\n추가3");

        //////string기본에다가 추가적으로, 반복적으로, 혹은 긴 내용을 많이 쓰게 된다면
        //////string 에 연산자를 쓰기보다는 STringbuilder를 써야 훨씬 최적화에 도움이 됨....
        ////string a = "내용";
        ////a += "내용추가\n";
        ////a += "\n 추가 계속" + "뭔가 더한다";

        ////for (int i = 0; i < 100; i++)
        ////{
        ////    //a += i.ToString();
        ////    strbuilder.Append(i);
        ////}

        //Debug.Log(Path.Combine(Application.dataPath, "FileWriteAllText.txt"));

        //WriteAllText == string하나로 쓰는거고 string배열을 한파일에 다 넣고싶으면, WriteAllLines사용...
        //File.WriteAllText( Path.Combine( Application.dataPath, "FileWriteAllText.txt"),  strbuilder.ToString());
        #endregion

        #region 파일클래스에서 제공하는 불러오기 (실습진행함)
        ////File.ReadAllText //string반환. 그 내용을 한줄로 불러올 수 있음
        //string[] strarr= File.ReadAllLines(Path.Combine(Application.dataPath, "FileWriteAllText.txt")); //string[] 배열 반환. 각 엔터마다 한줄씩 받아와서 배열로 다 받아낼수있음.
        ////읽어온 내용 출력하기...
        //for (int i = 0; i < strarr.Length; i++)
        //{
        //    Debug.Log(i+"번째 줄 내용 : " +strarr[i]);
        //}
        #endregion

        #region 원하는 파일들 목록만 받아오기(실습)
        //특정 경로의 특정 파일들만 불러오기....
        //내 프로젝트의 Assets\01_Scripts 안의 폴더목록 받아오기 + 스크립트 들만 받아오기 *.cs

        string path = Path.Combine(Application.dataPath, "01_Scripts");
        string[] alldirectories = Directory.GetDirectories(path);
        Debug.Log(path + "경로 안의 모든 폴더 목록");
        for (int i = 0; i < alldirectories.Length; i++)
        {
            //그냥 받아오게 되면 전체 경로를 모두 반환하기 때문에 너무 길어서
            //폴더이름만 추려내기
            Debug.Log(Path.GetFileName(alldirectories[i]));              
        }
        
        Debug.Log("\n해당 경로안의 모든 스크립트 목록");
        string[] allscfiles = Directory.GetFiles(path, "*.cs"); //해당 경로에있는 찾는조건게 부합되는 모든 파일 목록을 반환함...
        for (int i = 0; i < allscfiles.Length; i++)
        {
            //모든 경로를 받아오기떄문에 거기서 파일이름만 가져오기..
            Debug.Log(Path.GetFileName(allscfiles[i]));

            //만약 확장자를 안보이게 하고싶다면
            //Path.GetFileNameWithoutExtension(경로) //해당 경로에서 이름만 제외하고 경로랑 확장자를 모두 제외하고 줌.
        }

        //위의 사항들로.. Path.GetFileName()을 하면 결과물이    
        string fullpath= "경로\\경로\\경로\\경로\\경로\\경로\\마지막이름 ";
        string[] aaa = fullpath.Split('\\');
        Debug.Log(aaa[aaa.Length - 1]); 
        #endregion
    }

    void CheckFileName()
    {        
        string filename = "abc!d@e#f$%^&*/dir";
        char[] aaa = Path.GetInvalidFileNameChars();
        Debug.Log("안돼목록");
        for (int i = 0; i < aaa.Length; i++)
        {
            Debug.Log(aaa[i]);
        }

        string bbb = new string ("!@#$%^&*()"); 

        Debug.Log("==========");
        int num = filename.IndexOfAny(/*aaa*/bbb.ToCharArray());        
        if (num != -1)
        {
            Debug.Log(num+"의 위치에 경로 이름으로 적합치 않은 문자가 있음");
        }        

        //이걸 활용하면, 후에 캐릭터 이름짓기에서 이상한 작업 못하도록 막을 수 있음.
        //string.Empty <color=#aa10db>

    }
}
