public class AllEnum 
{
    public enum Type
    { 
        Player,
        Enemy,

        End
    }

    public enum StateEnum //Enum안에 내용을 뭘넣을지는 취향.
    { 
        Patrol, //부모랑
        Idle, //자식들 모두 한곳에 몰아넣었는데
        Walk, 
        
        Chase,

        Attack, //따로 부모라인 만 묶은 Enum과 자식Enum들로 나눠서 관리해도됨.취향
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
