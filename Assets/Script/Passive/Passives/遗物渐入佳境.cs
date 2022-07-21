using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class 遗物渐入佳境 : StackPassive
{
    int stack = 0;
    public override void BeforeDealDamage(Damage damage, Role target)
    {
        damage.FinalDamage += damage.damage * 0.1f * Cnt * stack;
        if (damage.IsCrit)
            stack++;
        else
            stack = 0;
    }
}