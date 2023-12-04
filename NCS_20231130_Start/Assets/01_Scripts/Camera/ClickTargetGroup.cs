using UnityEngine;
using Cinemachine;

public class ClickTargetGroup : MonoBehaviour
{
    CinemachineTargetGroup targetGroup;    
    public Transform player;
    public Transform[] tr;

    void Start()
    {
        this.targetGroup = GetComponent<CinemachineTargetGroup>();
        targetGroup.m_Targets = new CinemachineTargetGroup.Target[3];

        CinemachineTargetGroup.Target target = new CinemachineTargetGroup.Target();
        target.target = tr[0];
        target.weight = 1;
        target.radius = 1;
        targetGroup.m_Targets[0] = target;
        CinemachineTargetGroup.Target target2 = new CinemachineTargetGroup.Target();
        target2.target = tr[1];
        target2.weight = 2;
        target2.radius = 2;
        targetGroup.m_Targets[1] = target2;
        CinemachineTargetGroup.Target target3 = new CinemachineTargetGroup.Target();
        target3.target = player;
        target3.weight = 3;
        target3.radius = 3;
        targetGroup.m_Targets[2] = target3;
        targetGroup.DoUpdate();

        targetGroup.RemoveMember(tr[1]);
        targetGroup.DoUpdate();
    }
}