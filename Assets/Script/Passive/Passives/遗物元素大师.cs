using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class 遗物元素大师 : StackPassive
{
    public float t = -100000;
    public override void OnSucceedDamage(Damage damage, Role target)
    {
        if (target.TryGetComponent<Buff>(out _))
        {
            if (t <= 0)
                role.Property.AtkBonus += 0.25f * Cnt;
            t = 5;
        }
    }
    private void Update()
    {
        t -= Time.deltaTime;
        if (t <= 0 && t > -1000)
        {
            role.Property.AtkBonus -= 0.25f * Cnt;
            t = -10000;
        }
    }
    protected override void DiscardPassive()
    {
        if (t > 0)
            role.Property.AtkBonus -= 0.25f * Cnt;
    }
}