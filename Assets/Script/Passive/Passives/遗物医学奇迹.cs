using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class 遗物医学奇迹 : StackPassive
{
    public float cooltime = 0;
    public override void BeforeTakeDamage(Damage damage, Role target)
    {
        if (damage.FinalDamage > role.hp && cooltime < 0)
        {
            damage.FinalDamage = 0;
            role.hp = role.Property.Maxhp;
            cooltime = Mathf.Max(120, 240 - 60 * Cnt);
        }
    }
    public override void OnSustainedTrigger()
    {
        cooltime -= 0.5f;
    }
}