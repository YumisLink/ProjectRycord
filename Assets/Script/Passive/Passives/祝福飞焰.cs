using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class 祝福飞焰 : Passive
{
    PlayerShoot pls;
    protected override void GetPassive()
    {
        pls = role.GetComponent<PlayerShoot>();
        pls.bulletDatas[5].穿透 = 114514;
        pls.bulletDatas[5].攻击间隔 += 0.1f;

    }
    public override void BeforeDealDamage(Damage damage, Role target)
    {
        if (pls.BulletShoot.名字 == "余辉")
        {
            if (!damage.IsCrit)
            {
                damage.IsCrit = true;
                damage.FinalDamage += role.Property.Critical;
            }
        }
    }
}