using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMove : MonoBehaviour
{
    public Camera cam;
    public Transform goal; //���� Ŭ���� �������� Ȯ���ϱ� ���� sphere �����

    NavMeshAgent agent;

    int offMeshArea_Climb = 4; //�����޽��� ������ȣ. 
    int offMeshArea_Jump = 2; //�����޽� �������� ���� ��ȣ
    float climbSpeed = 0.5f;//���������� �̵��ӵ�
    float jumpSpeed = 15f; // ������
    float gravity = -9.8f; //�߷°��
    Coroutine MeshLinkCor = null;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //����Ŭ�� ��������
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition),out hit, Mathf.Infinity ))//ī�޶�κ��� �������� (���콺 ��ġ����)
            {//�ؼ� ���� �ɸ��°� �ִٸ�, 
                goal.position = hit.point; //goal�� ��ġ�� �ش� ������ �ε��� ������ ������
                agent.SetDestination(goal.position); //������Ʈ ��ü�� ��ǥ������ �ش� �������� ������...
            }
        }

        if (agent.isOnOffMeshLink) //���� �޽ø�ũ ���� ������
        {
            OffMeshLinkData linkdata = agent.currentOffMeshLinkData;
            if (linkdata.offMeshLink == null)
            {
                return;
            }

            if (linkdata.offMeshLink.area == offMeshArea_Climb) //�ش� �޽ø�ũ�� ������ �����̸�
            {
                if (MeshLinkCor == null)
                {
                    MeshLinkCor = StartCoroutine(ClimbCor(linkdata));
                }
            }
            else if (linkdata.offMeshLink.area == offMeshArea_Jump)
            {
                if (MeshLinkCor == null)
                {
                    MeshLinkCor = StartCoroutine(JumpCor(linkdata));
                }
            }
        }
    }

    IEnumerator ClimbCor(OffMeshLinkData linkdata)
    {
        //������ �����ϴ� �ִϸ��̼� ����.

        agent.isStopped = true; //��� ����� �̵��ϱ⸦ ������
        agent.updateRotation = false; //ȸ���� �����Ұ���

        Vector3 start = linkdata.startPos; //���� ���� ���ִ� �����޽� ��ũ�� ��������..
        Vector3 end = linkdata.endPos; //���� ���� ���ִ� �����޽� ��ũ�� ��������..
        Vector3 LookPos = end;

        float climbTime = Mathf.Abs(end.y - start.y) / climbSpeed; //���������� �ð� ����
        float currentTime = 0;
        float percent = 0;
        
        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / climbTime;//0���� 1 ������ ���� ��..
            transform.position = Vector3.Lerp(start,end, percent);
            LookPos.y = transform.position.y;
            transform.LookAt(LookPos);
            yield return null;
        }
        agent.CompleteOffMeshLink();//�����޽ó������� �����ߴٰ� �˷��� �ε�, �̰� ���ع����� ��� ������������ �ݺ���.
        agent.isStopped = false;
        agent.updateRotation = true;

        //������ �ִϸ��̼��� ������Ŵ.

        MeshLinkCor = null;
    }

    IEnumerator JumpCor(OffMeshLinkData linkdata)
    {
        //������ �����ϴ� �ִϸ��̼� ����.

        agent.isStopped = true; //��� ����� �̵��ϱ⸦ ������
        agent.updateRotation = false; //ȸ���� �����Ұ���

        Vector3 start = linkdata.startPos; //���� ���� ���ִ� �����޽� ��ũ�� ��������..
        Vector3 end = linkdata.endPos; //���� ���� ���ִ� �����޽� ��ũ�� ��������..
        Vector3 LookPos = end;

        float jumpTime = Mathf.Max(0.3f, Vector3.Distance(start,end) / jumpSpeed);        
        float currentTime = 0;
        float percent = 0;
        float v0 = (end.y - start.y) - gravity; //y������ �ʱ�ӵ�

        Vector3 tempPos = Vector3.zero;
        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / jumpTime;//0���� 1 ������ ���� ��..
            tempPos = Vector3.Lerp(start,end, percent);
            tempPos.y = start.y + (v0 * percent) + (gravity * percent * percent); // ����������...
            transform.position = tempPos;
            LookPos.y = transform.position.y;
            transform.LookAt(LookPos);
            yield return null;
        }
        agent.CompleteOffMeshLink();//�����޽ó������� �����ߴٰ� �˷��� �ε�, �̰� ���ع����� ��� ������������ �ݺ���.
        agent.isStopped = false;
        agent.updateRotation = true;

        //������ �ִϸ��̼��� ������Ŵ.

        MeshLinkCor = null;
    }
}
