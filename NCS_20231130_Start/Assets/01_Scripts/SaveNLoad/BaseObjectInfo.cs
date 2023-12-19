using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//���� ����Ƽ�� ���̾��Ű�� ������ �������� ��ü�� ���� ��ũ��Ʈ
public class BaseObjectInfo : MonoBehaviour
{    
    public AllEnum.ObjectType Type { get; private set; }
    
    public int HP { get; private set; }

    [Tooltip("�� Ȯ�ο���")]
    [SerializeField]
    int speed;
    public int Speed => speed;
    

    Vector3 vec = Vector3.zero;

    public void SetInfo(SavedObjectInfo info)
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
