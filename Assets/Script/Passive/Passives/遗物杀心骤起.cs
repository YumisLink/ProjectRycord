using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 遗物杀心骤起 : StackPassive
{
    public override void OnSucceedDamage(Damage damage, Role target)
    {
        if (target.hp > 0 && (target.hp / target.Property.Maxhp) < 0.1f * Cnt + (Cnt >= 3 ? 0.05f : 0))
        {
            Damage.DealDamage(role, target, new Damage(target.Property.Maxhp, DamageType.True));
        }
    }
}
