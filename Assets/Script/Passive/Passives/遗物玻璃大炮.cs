using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class “≈ŒÔ≤£¡ß¥Û≈⁄ : StackPassive
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