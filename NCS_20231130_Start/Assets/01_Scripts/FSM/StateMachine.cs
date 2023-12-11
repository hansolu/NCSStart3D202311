using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���¸� �Ǻ��ؼ� �������·� �ٲ���.
public class StateMachine : MonoBehaviour //�̰Ŵ� �� Monobehavior�� ��ӹ��� �ʿ䰡 ����.
{
    [HideInInspector]
    public Enemy owner; //�� ������Ʈ �ӽ��� ������
    Dictionary<AllEnum.StateEnum, State> StateDic = new Dictionary<AllEnum.StateEnum, State>();
    AllEnum.StateEnum ExState; //�������� üũ����
    bool IsPlay = false; 
    #region ���Ͽ���    
    //������ ���� ����Enum�� ���� ����Ʈ
    //List<AllEnum.StateEnum> patternList = new List<AllEnum.StateEnum>();
    //bool doPattern = false; //�̰Ŵ� �׳� �ڷ�ƾ �����
    //int patternumNum = 0; //���ϸ���Ʈ�� �ϳ��� �������� ����
    #endregion

    //void Start()
    public void SetInit()
    {
        //�� �ൿ ��ųʸ� ����...
        owner = GetComponent<Enemy>();
        
        StateDic.Add(AllEnum.StateEnum.Patrol, new State_Patrol(owner, SetState));
        //StateDic.Add(AllEnum.StateEnum.Idle , new State_Idle(owner, SetState));
        //StateDic.Add(AllEnum.StateEnum.Walk, new State_Walk(owner, SetState));
        StateDic.Add(AllEnum.StateEnum.Chase, new State_Chase(owner, SetState));
        //����

        ExState = AllEnum.StateEnum.End;
        SetState(AllEnum.StateEnum./*Idle*/Patrol);
        IsPlay = true;
    }
    void Update()
    {
        if (IsPlay == false) //1�� bool���༭ End�� �ƴҶ� �����ϵ��� �Ѵ�
        {
            return;
        }

        if (ExState == owner.NowState 
            //&& owner.NowState != AllEnum.StateEnum.End 
            //2��, nowState�� End���¸� ���� ���ϵ��� �Ѵ�.
            )
        {
            StateDic[owner.NowState].OnStateStay();
        }
    }
    //���⿡ �����ϱ� ���ؼ�
    //1�� ���ʿ� StateMachine�� Enemy��ũ��Ʈ�� �ϳ��� ��ģ��.
    //Enemy�� ��ü������ StateMachine����� ������ �ֵ��� ��
    //2�� StateMachine�� Enemy�� ������ �ִ°� �ƴϰ�, Enemy�� StateMachine�� ������ ����
    //3�� State �� �����ڿ� StateMachine �������ش�.
    //==>statemachine�� ���ؼ� Enemy�� �����ϰ�, statemachine�� setstate�� �����Ӱ� �θ�
    //4���� ������, �����ڿ� SetState�� delegate ==�Լ������� �ѱ�
    public void SetState(AllEnum.StateEnum _enum)
    {        
        owner.NowState = _enum;
        //Debug.Log($"setstate�̰� Exstate = {ExState} / nowState ={owner.NowState}");
        if (ExState != owner.NowState)
        {
            if (ExState!= AllEnum.StateEnum.End)            
                StateDic[ExState].OnStateExit();            

            StateDic[owner.NowState].OnStateEnter();
            ExState = owner.NowState;
        }
    }

    //�Ʒ� �̷� ���� ������, 
    //������ ���÷� �鶧, ���� �θ���¸� ����, �ش� �θ��� ���뿡
    //���� �ڷ�ƾ�� ���� ������� �־�θ�, ���� ���� ������ ��������...

    //onstatestay�� �׻� update���� ��Ű��, 
    //������ ��ȭ�� �������� �ְ������, 
    //���� ��ȭ ������ ���ο��� �ִ°� �ƴϰ�,
    //�ۿ� �ڷ�ƾ���� ������ �����ð�, �Ǵ� ���� ���ϴ� �������� �����Ŵ.. 
    //IEnumerator SetPatternState()
    //{
    //    StateDic[patternList[patternumNum]].OnStateEnter(); //ó�� ���� ����
    //    while (doPattern)
    //    {
    //        yield return new WaitForSeconds(�������Ͻð�);
    //        StateDic[patternList[patternumNum]].OnStateExit();
    //        patternumNum++;
    //        if (patternumNum >= patternList.Count)
    //        {
    //            patternumNum = 0;
    //        }
    //        StateDic[patternList[patternumNum]].OnStateEnter(); 
    //    }
    //}
}
