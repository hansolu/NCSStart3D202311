using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviorTree : MonoBehaviour
{
    Node rootNode; //가장 최상위 부모노드. 내 상태판별기의 메인
    Enemy owner;//이 트리의 부모가 되는 본체
    //예시
    //void Start()
    public void SetInit()
    {
        owner = GetComponent<Enemy>();

        //내것에 해당되는 트리 설정...
        rootNode = new SelectorNode
            (
            new List<Node>
            {
                new SequenceNode(//시야 체크.
                    new List<Node>
                    {
                        //시야체크 노드
                        new CheckSight(owner),

                    //new SequenceNode( //거리체크
                    //new List<Node>
                    //{                 
                        //거리체크 노드
                        new CheckDistance(owner),

                        //new SelectorNode(
                        //    new List<Node>{                         
                        //new 스킬1번(),                        
                        //new 스킬2번(),
                        //new 스킬3번(),                                                
                        //}),
                //}
                //    ), //거리체크용 끝
                }
                    ),               //시야체크용 끝
                 
                //고작 어차피 걸어댕기는 용 쿨타임인데 노드하나로 빼기엔 좀 과한거같으니까
                //그냥 셀렉터의 기본노드 두개로..
                         
                //먼저 디폴트로 하고싶은것을 앞에두기.                                

                //순찰 만들고 그안에 이동과 쉬기를 넣는게 
                new Patrol(owner)
                
                //내가 도착지에 도착했으면 일정시간 쉬기...
                //이동하기 //도착지에 도착했고, 일정시간+
                
                //쉬기                
            }
            ) ;
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
