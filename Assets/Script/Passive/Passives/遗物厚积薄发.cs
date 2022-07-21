using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class 遗物厚积薄发 : StackPassive
{
    float t = 0;
    float cont = -10000;
    public override void OnSucceedDamage(Damage damage, Role target)
    {
        if (t < 0)
        {
            t = 8;
            cont = Cnt * 2;
        }
    }
    public override void BeforeDealDamage(Damage damage, Role target)
    {
        if (cont > 0)
            damage.FinalDamage += damage.damage * 0.5f;
    }
    private void Update()
    {
        t -= Time.deltaTime;
        cont -= Time.deltaTime;
    }
}