using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//기본 아이템 스크립트 만들기.
//슬롯밑에 들어갈 아이템... 
//다양한 아이템들은 후에 이 친구를 상속받아서 구현하게 될 것임...
public class Item //: MonoBehaviour //해당 Item은 부모클래스 로써 만들어도되고, 혹은 구조체로 만들어도됨.
{
    //protected ItemInfo info;
    //Sprite sprite;//아이템이미지 //string....
    //아이템 개수
    //아이템의 고유번호 -이름이 됐건 숫자로 관리하건....
    public int Index { get; private set; } = -1; //인덱스를 0부터 시작할거면 -1로 지정해주는편이 좋고, 인덱스 1부터 시작이라면 0인게 무난.
    public int Count { get; private set; } = 0; //


    //이하 필수는 아니고 선택정보
    //뭔가 데이터 원본이 어딘가에있을때, 사실 인덱스가 정말로 고유번호라면 아래 정보는 굳이 들고있을 필요는 없음.
    //아이템 타입같은것도 있으면 좋겠죠
    //최대개수.. 
    public int MaxCount { get; private set; } = 0; //

    public int AbleCount => MaxCount - Count; //이 슬롯에 추가로 넣을 수 있는 개수. (최대개수-현재개수)


    public Item()
    {
        Clear();
    }
    
    public Item(int index, int count, int maxCount)
    {
        this.Index = index;
        this.Count = count;
        this.MaxCount = maxCount;
    }

    public Item(Item item) //편의위함
    {
        SetItem(item);
    }

    public void Clear() //아이템 정보 싹 비우기
    {
        Index = -1;
        Count = 0;
        MaxCount = 0;
    }

    public void SetItem(Item item) //강제로 아이템 정보 바꾸기
    {
        this.Index = item.Index;
        this.Count = item.Count;
        this.MaxCount = item.MaxCount;
    }
    public void SetCount(int count)//강제로 개수 세팅
    {
        this.Count = count;
    }
    public int AddCount(int count)
    {
        int returncount = 0;
        this.Count += count;
        if (this.Count > MaxCount)
        {
            returncount = MaxCount - this.Count;
            this.Count = MaxCount;            
        }

        return returncount;
    }
    public int SubCount(int count) //얘는 실행하기전에, 마이너스가 되지않도록 체크를 해야할것.
    {
        int returncount = 0;
        Count -= count;
        if (Count < 0)
        {
            returncount = -Count;
            Count = 0;
        }

        return returncount;
    }

    public virtual void Use(PlayerForInven player)
    { 

    }
}

