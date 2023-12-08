using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowingEnemy : MonoBehaviour
{
    public Transform playerTr;
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    
    void Update()
    {
        //Vector3.Distance(playerTr.position, transform.position) > 5   ///  == ��Ȯ�� �Ÿ����� (�Լ�)
        //(playerTr.position- transform.position ).Magnitude > 5 == ��Ȯ�� �Ÿ�����(���� ������Ƽ)
        if ((playerTr.position- transform.position ).sqrMagnitude > 25 ) //�Ÿ������� ����. ��Ʈ�Ⱦ����. ��Ʈ�� �Ⱦ��� ���������̱⋚����
            //���� ���ϴ� �Ÿ� 5 * 5 �ؼ� 25�ΰ�... 

            //��Ʈ�� ����ٴ°Ŵ� �������� �Ȱ��� ������������ ��ǻ�Ͱ� �����ϱ⶧���� ���귮�� ŭ...
        {
            agent.isStopped = false;//���� ����
            agent.SetDestination(playerTr.position); //������ ����
        }
        else //�÷��̾�� ������ �Ÿ��� 5���ϸ�.... �����
        {
            agent.isStopped = true;//����            
        }
    }       
}


