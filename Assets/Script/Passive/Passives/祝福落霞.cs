using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class 祝福落霞 : Passive
{
    protected override void GetPassive()
    {
        var ps = role.GetComponent<PlayerShoot>();
        ps.bulletDatas[5].子弹伤害 = 60;
        ps.bulletDatas[5].穿透 = 1;
    }
    public override void BeforeDealDamage(Damage damage, Role target)
    {
        if (damage.Data == "余辉")
        {
            var k = Effect.Create(GameManager.Effects["FireBoom"],target.transform.position,master:role) as AtkEffect;
            k.damage = new Damage(60, DamageType.Magic);

        }
    }
}