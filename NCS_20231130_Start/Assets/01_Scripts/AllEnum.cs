public class AllEnum 
{
    public enum Type
    { 
        Player,
        Enemy,

        End
    }

    public enum StateEnum //Enum�ȿ� ������ ���������� ����.
    { 
        Patrol, //�θ��
        Idle, //�ڽĵ� ��� �Ѱ��� ���Ƴ־��µ�
        Walk, 
        
        Chase,

        Attack, //���� �θ���� �� ���� Enum�� �ڽ�Enum��� ������ �����ص���.����
        Attack_Near,
        Attack_Far,
        
        End
    }
    //public enum MoveEnum
    //{
    //    Idle,
    //    Walk,
    //}
}
