using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class 遗物短兵相接 : StackPassive
{
    public override void BeforeDealDamage(Damage damage, Role target)
    {
        var dis = WindChimeEngnie.Lib.GetDistance(target.gameObject, gameObject);
        if (dis < 5)
            damage.FinalDamage += damage.damage * (0.25f + 0.25f * Cnt);
        else
            damage.FinalDamage *= 0.5f;
    }
}