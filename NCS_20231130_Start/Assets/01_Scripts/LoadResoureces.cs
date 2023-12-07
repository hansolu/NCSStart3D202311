using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent] //������Ʈ �ߺ��� ������. 
[RequireComponent(typeof(SpriteRenderer))] //�ݵ�� �� ��ũ��Ʈ�� ���� ��ü���� SpriteRenderer�� �־�� �Ѵ�
//�ٵ� �ش� RequireComponent �� �� type ���� �ݵ�� Monobehavior���� Ȯ��� ģ���鸸 ����.. 
public class LoadResoureces : MonoBehaviour
{    
    SpriteRenderer renderer;
    Sprite[] allsprites;
    int val = 0;

    //[HideInInspector] //�ۺ�������, �ν����� â���� '����'
    [NonSerialized] //�ۺ�������, �ν����� â�� ���� ������ ó����.
    public int IntValue = 0;
    
    public TempInfos infos;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        allsprites = Resources.LoadAll<Sprite>("Ingame");
        StartCoroutine(ChangeImgs());                
        
        Debug.Log("intvalue �� �� �� "+IntValue);
        Debug.Log("infos�� C�� ���� : "+ infos.c);
    }


    /// <summary>
    /// ��������Ʈ������ ���� �׸��� ���徿 0.5�ʸ��� �ٲ���.
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