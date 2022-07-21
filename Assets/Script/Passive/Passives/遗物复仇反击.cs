using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class 遗物复仇反击 : StackPassive
{
    float t = 0;
    public override void AfterTakeDamage(Damage damage, Role target)
    {
        t = 5;
    }
    public override void BeforeDealDamage(Damage damage, Role target)
    {
        if (t > 0)
            damage.FinalDamage += damage.damage * 0.1f * Cnt;
    }
    public override void OnSustainedTrigger()
    {
        t -= 0.5f;
    }
}