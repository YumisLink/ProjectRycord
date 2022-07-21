using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class 遗物随缘一枪 : StackPassive
{
    protected override void GetPassive()
    {
        role.Property.critical -= 10;
    }
    protected override void DiscardPassive()
    {
        role.Property.critical += 10;
    }
    public override void BeforeDealDamage(Damage damage, Role target)
    {
        if (!damage.IsCrit)
        {
            damage.FinalDamage += damage.damage * (0.2f + 0.2f * Cnt);
        }
    }
}