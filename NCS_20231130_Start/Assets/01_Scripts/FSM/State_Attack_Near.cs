using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Attack_Near : State
{
    public State_Attack_Near(Enemy enemy, SetStateDel StateDel) : base(enemy, StateDel)
    {
    }
    public override void OnStateEnter()
    {
        enemy.SetWeaponOn(false, true);
    }

    public override void OnStateExit()
    {
        enemy.AttackNear(false);
        enemy.SetWeaponOn(false, false);
    }

    public override void OnStateStay()
    {        
        enemy.AttackNear(true);
    }
}
