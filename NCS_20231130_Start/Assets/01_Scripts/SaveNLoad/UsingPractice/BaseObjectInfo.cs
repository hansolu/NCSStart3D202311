using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//실제 유니티의 하이어라키상에 존재할 몸을가진 객체에 붙을 스크립트
public class BaseObjectInfo : MonoBehaviour
{    
    public AllEnum.ObjectType Type { get; private set; }
    
    public int HP { get; private set; }

    [Tooltip("값 확인용임")]
    [SerializeField]
    int speed;
    public int Speed => speed;
    

    Vector3 vec = Vector3.zero;

    public void SetInfo(SavedObjectInfo info) //savedObjectInfo ==
                                              //제이슨파일에서 읽어온 데이터의 원본...원본데이터...
                                              //후에 기획자가 데이터파일을 준다면
                                              //해당 데이터파일을 읽어서 내가 쓸수있게
                                              //클래스상태로 가공해둔 그 데이터.
    {
        this.Type = (AllEnum.ObjectType)info.ObjectType;
        this.HP = Random.Range( info.HP_Min, info.HP_Max);
        this.speed = Random.Range(info.Speed_Min, info.Speed_Max);
        
        switch (info.Color)
        {
            case "red":
                this.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                break;
            case "blue":
                this.gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
                break;
            case "green":
                this.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
                break;
            default:
                break;
        }
    }
    public void SetInfo(AllEnum.ObjectType type, int hp, int speed, string color)
    {
        this.Type = type;
        this.HP = hp;
        this.speed = speed;
        switch (color)
        {
            case "red":
                this.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                break;
            case "blue":
                this.gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
                break;
            case "green":
                this.gameObject.GetComponent<MeshRenderer>().material.color = Color.green                    ;
                break;
            default:
                break;
        }
    }
    //protected virtual void Start()
    //{        
    //}
        
    protected virtual void Update()
    {
        vec.x = Input.GetAxisRaw("Horizontal");
        vec.z = Input.GetAxisRaw("Vertical");
        transform.Translate(vec.normalized * Speed * Time.deltaTime);
    }
}
