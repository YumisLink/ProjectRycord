using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 遗物神之眷顾 : StackPassive
{
    public override void BeforeDealDamage(Damage damage, Role target)
    {
        var ans = UnityEngine.Random.Range(0, 1f);
        if (ans < 0.01f)
        {
            damage.FinalDamage *= 2;
        }
        if (ans < 0.05f)
        {
            damage.FinalDamage *= (1 + 1 * Cnt);
        }
    }
}
