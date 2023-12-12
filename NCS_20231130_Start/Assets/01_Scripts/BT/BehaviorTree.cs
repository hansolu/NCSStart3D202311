using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//FSM에서 StateMachine같은 역할
public class BehaviorTree : MonoBehaviour
{
    Node rootNode; //가장 최상위 부모노드. 내 상태판별기의 메인

    //예시
    void Start()
    {
        //내것에 해당되는 트리 설정...
        rootNode = new SelectorNode
            (
            new List<Node> 
            { 
                //우선적으로 하고 싶은 무언가 일 수록
                //위에 둬야함.
                //new 죽음(),

                //시야 체크.
                new SequenceNode(
                    new List<Node>
                    { 
                //        new node를 상속받은 거리체크용 스크립트,
                //    new node를 상속받은 chase스크립트,
                //new node를 상속받은 attack스크립트
                }
                    ),               


                //가장 기본이 되는것을 가장 뒤에 둠.
                //new node를 상속받은 Patrol스크립트,                
            }
            );
    }
    
    void Update()
    {
        if (rootNode == null)
        {
            return;
        }

        rootNode.Evaluate(); //매프레임 상태판별진행.
    }
}
