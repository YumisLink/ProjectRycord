using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class 遗物身经百战 : StackPassive
{
    public override void OnSucceedDamage(Damage damage, Role target)
    {
        if (target.hp < 0)
            role.Property.Maxhp += 1 * Cnt;
    }
}