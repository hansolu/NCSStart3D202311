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
        //Vector3.Distance(playerTr.position, transform.position) > 5   ///  == 정확한 거리차이 (함수)
        //(playerTr.position- transform.position ).Magnitude > 5 == 정확한 거리차이(변수 프로퍼티)
        if ((playerTr.position- transform.position ).sqrMagnitude > 25 ) //거리차이의 제곱. 루트안씌운거. 루트를 안씌운 제곱상태이기떄문에
            //내가 원하는 거리 5 * 5 해서 25인것... 

            //루트를 씌운다는거는 같은수로 똑같이 나눠질때까지 컴퓨터가 연산하기때문에 연산량이 큼...
        {
            agent.isStopped = false;//멈춤 해제
            agent.SetDestination(playerTr.position); //목적지 세팅
        }
        else //플레이어와 나와의 거리가 5이하면.... 멈춰라
        {
            agent.isStopped = true;//멈춰            
        }
    }       
}


