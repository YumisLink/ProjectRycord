using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 水流 : Buff
{
    public int fl = 4;
    public Effect eff;
    protected override void OnStart()
    {
        base.OnStart();
        Type = BuffType.OnlyOne;
    }
    protected override void LoseBuff()
    {
        eff.Dead();
    }
    public override void OnSustainedTrigger()
    {
        if (role.TryGetComponent<雷电>(out _))
        {
            fl--;
            var ate =  AtkEffect.Create(GameManager.Effects["Thunders"], role.transform.position, BuffCreater,new Damage(10,DamageType.Magic)) as AtkEffect;

            if (fl <= 0)
                DestroyBuff();
        }   
    }
    public override void GetBuff()
    {
        eff = Effect.Create(GameManager.Effects["ElementShowWater"], transform.position, master: role, follow: true);
        eff.offset = new Vector3(0, -0.2f);
        if (role.TryGetComponent<寒冰>(out var sl))
        {
            Buff.GiveBuff(typeof(冻结), BuffCreater, role,Duration:2);
            DestroyBuff();
            sl.DestroyBuff();
            return;
        }
        if (role.TryGetComponent<火焰>(out var hy))
        {
            Damage.DealDamage(BuffCreater, role, new Damage(80, DamageType.Magic));
            Effect.Create(GameManager.Effects["Evaporation"], transform.position);
            hy.DestroyBuff();
            DestroyBuff();
            return;
        }
    }
}
