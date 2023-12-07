using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent] //컴포넌트 중복을 방지함. 
[RequireComponent(typeof(SpriteRenderer))] //반드시 이 스크립트가 붙은 객체에는 SpriteRenderer가 있어야 한다
//근데 해당 RequireComponent 에 들어갈 type 들은 반드시 Monobehavior에서 확장된 친구들만 가능.. 
public class LoadResoureces : MonoBehaviour
{    
    SpriteRenderer renderer;
    Sprite[] allsprites;
    int val = 0;

    //[HideInInspector] //퍼블릭이지만, 인스펙터 창에서 '숨김'
    [NonSerialized] //퍼블릭이지만, 인스펙터 창에 없는 것으로 처리함.
    public int IntValue = 0;
    
    public TempInfos infos;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        allsprites = Resources.LoadAll<Sprite>("Ingame");
        StartCoroutine(ChangeImgs());                
        
        Debug.Log("intvalue 의 값 ㅣ "+IntValue);
        Debug.Log("infos의 C의 내용 : "+ infos.c);
    }


    /// <summary>
    /// 스프라이트렌더러 안의 그림을 한장씩 0.5초마다 바꿔줌.
    /// </summary>    
    /// <returns></returns>
    IEnumerator ChangeImgs()
    {
        while (true)
        {
            renderer.sprite = allsprites[val];
            yield return new WaitForSeconds(0.5f);
            val++;
            if (val >= allsprites.Length)
            {
                val = 0;
            }
        }
    }
}

[Serializable]
public class TempInfos
{
    public int a = 0;
    public float b = 0;
    public string c = "";
}