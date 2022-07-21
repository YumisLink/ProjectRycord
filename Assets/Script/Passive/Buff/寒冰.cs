using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 寒冰 : Buff
{
    public int fl = 4;
    float del = 0;
    public Effect eff;
    protected override void OnStart()
    {
        base.OnStart();
        Type = BuffType.OnlyOne;
        eff = Effect.Create(GameManager.Effects["ElementShowIce"], transform.position, master: role, follow: true);
        eff.offset = new Vector3(0, -0.2f);
        del = role.Property.AtkInterval * 0.3f;
        role.Property.AtkInterval += del;
        if (TryGetComponent<雷电>(out var ld))
        {
            Buff.GiveBuff(typeof(超导), BuffCreater, role);
            DestroyBuff();
            ld.DestroyBuff();
            return;
        }
        if (TryGetComponent<火焰>(out var hy))
        {
            Buff.GiveBuff(typeof(碎裂), BuffCreater, role);
            DestroyBuff();
            hy.DestroyBuff();
            return;
        }
        if (TryGetComponent<水流>(out var sl))
        {
            Buff.GiveBuff(typeof(冻结), BuffCreater, role, Duration: 2);
            DestroyBuff();
            sl.DestroyBuff();
            return;
        }
    }
    protected override void LoseBuff()
    {
        base.LoseBuff();
        eff.Dead();
        role.Property.AtkInterval -= del;
    }
}