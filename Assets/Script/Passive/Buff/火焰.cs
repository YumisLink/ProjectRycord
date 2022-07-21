using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 火焰 : Buff
{
    public Effect eff;
    protected override void LoseBuff()
    {
        eff.Dead();
    }
    protected override void OnStart()
    {
        base.OnStart();
        Type = BuffType.OnlyOne;
        eff = Effect.Create(GameManager.Effects["ElementShowFire"], transform.position, master: role, follow: true);
        eff.offset = new Vector3(0, -0.2f);
        if (TryGetComponent<寒冰>(out var ld))
        {
            Buff.GiveBuff(typeof(超导), BuffCreater, role);
            DestroyBuff();
            ld.DestroyBuff();
            return;
        }
        if (TryGetComponent<水流>(out var ssf))
        {
            Effect.Create(GameManager.Effects["Evaporation"], transform.position);
            Damage.DealDamage(BuffCreater, role, new Damage(80, DamageType.Magic));
            DestroyBuff();
            ssf.DestroyBuff();
            return;
        }
        if (TryGetComponent<雷电>(out var hy))
        {
            var ate = AtkEffect.Create(GameManager.Effects["ThundExp"], role.transform.position, BuffCreater) as AtkEffect;
            ate.damage = new Damage(100, DamageType.Magic);
            DestroyBuff();
            hy.DestroyBuff();
            return;
        }
    }
    public override void OnSustainedTrigger()
    {
        Damage.DealDamage(BuffCreater, role, new Damage(5, DamageType.Magic));
    }
}
