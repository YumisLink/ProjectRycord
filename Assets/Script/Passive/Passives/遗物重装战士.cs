using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class 遗物重装战士 : StackPassive
{
    public override void BeforeDealDamage(Damage damage, Role target)
    {
        damage.FinalDamage += damage.damage * 0.01f * Cnt * role.Property.Maxhp / 5;
    }
}