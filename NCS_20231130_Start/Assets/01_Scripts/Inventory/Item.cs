using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�⺻ ������ ��ũ��Ʈ �����.
//���Թؿ� �� ������... 
//�پ��� �����۵��� �Ŀ� �� ģ���� ��ӹ޾Ƽ� �����ϰ� �� ����...
public class Item //: MonoBehaviour //�ش� Item�� �θ�Ŭ���� �ν� �����ǰ�, Ȥ�� ����ü�� ������.
{
    //protected ItemInfo info;
    //Sprite sprite;//�������̹��� //string....
    //������ ����
    //�������� ������ȣ -�̸��� �ư� ���ڷ� �����ϰ�....
    public int Index { get; private set; } = -1; //�ε����� 0���� �����ҰŸ� -1�� �������ִ����� ����, �ε��� 1���� �����̶�� 0�ΰ� ����.
    public int Count { get; private set; } = 0; //


    //���� �ʼ��� �ƴϰ� ��������
    //���� ������ ������ ��򰡿�������, ��� �ε����� ������ ������ȣ��� �Ʒ� ������ ���� ������� �ʿ�� ����.
    //������ Ÿ�԰����͵� ������ ������
    //�ִ밳��.. 
    public int MaxCount { get; private set; } = 0; //

    public int AbleCount => MaxCount - Count; //�� ���Կ� �߰��� ���� �� �ִ� ����. (�ִ밳��-���簳��)


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

    public Item(Item item) //��������
    {
        SetItem(item);
    }

    public void Clear() //������ ���� �� ����
    {
        Index = -1;
        Count = 0;
        MaxCount = 0;
    }

    public void SetItem(Item item) //������ ������ ���� �ٲٱ�
    {
        this.Index = item.Index;
        this.Count = item.Count;
        this.MaxCount = item.MaxCount;
    }
    public void SetCount(int count)//������ ���� ����
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
    public int SubCount(int count) //��� �����ϱ�����, ���̳ʽ��� �����ʵ��� üũ�� �ؾ��Ұ�.
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

