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

    public enum MyWeaponState
    { 
        None,
        Sword,
        Gun
    }

    public enum InventoryKind
    { 
        Inventory_Player,
        //Store,
        //Storage,

        End
    }

    public enum UIKind //Ui�� ������ �� �� �ѱ�� ��...
    { 
        Inventory,

        End
    }

    public enum ItemType
    { 
        None,
        Food,
        Sword,
        Armor,

        End
    }

    public enum ObjectType
    { 
        Cube,
        Sphere,
        Capsule,

        End
    }
}
