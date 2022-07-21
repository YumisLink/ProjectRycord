using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class 祝福川流 : Passive
{
    public PlayerShoot pls;
    protected override void GetPassive()
    {
        pls = GetComponent<PlayerShoot>();
        pls.bulletDatas[2].子弹伤害 /= 2;
    }
    public override void OnSucceedDamage(Damage damage, Role target)
    {
        if (damage.Data == "川怒")
        {
            Buff.GiveBuff(typeof(水流), role, target);
        }
    }
}