using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ���ﲣ������ : StackPassive
{
    public override void BeforeDealDamage(Damage damage, Role target)
    {
        damage.FinalDamage *= 2;
    }
    public override void BeforeTakeDamage(Damage damage, Role target)
    {
        damage.FinalDamage *= 2;
    }
}