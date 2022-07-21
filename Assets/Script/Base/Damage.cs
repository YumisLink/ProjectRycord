using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum DamageType
{
    Magic,
    Physical,
    True
}
[Serializable]
public class Damage
{
    public float damage;
    public float FinalDamage;
    public DamageType Type;
    public List<Type> TakeBuff = new(); 
    public string Data;
    public float HitPower = 0;
    /// <summary>
    /// ÊÇ·ñ±©»÷
    /// </summary>
    public bool IsCrit = false;
    public Damage(float damage, DamageType type)
    {
        this.damage = damage;
        Type = type;
        FinalDamage = damage;
    }
    public void SetHitPower(float power)
    {
        HitPower = power;
    }
    public Damage(float damage)
    {
        this.damage = damage;
        FinalDamage = damage;
    }
    public static void DealDamage(Role From,Role Target,Damage damage)
    {
        if (Target == null)
            return;
        if (From == null) 
            return ;
        float mut = 1.6f;
        Color c = Color.white;
        if (damage.Type == DamageType.Magic)
            c = GameManager.Config.MagDamageColor;
        if (damage.Type == DamageType.Physical)
            c = GameManager.Config.PhyDamageColor;
        if (damage.Type == DamageType.True)
            c = GameManager.Config.TrueDamageColor;
        if (UnityEngine.Random.Range(0f, 1f) <= From.Property.Critical)
        {
            damage.FinalDamage *= From.Property.Crit;
            mut = 2;
            damage.IsCrit = true;
            if (damage.Type == DamageType.Magic)
                c = GameManager.Config.MagCritDamageColor;
            if (damage.Type == DamageType.Physical)
                c = GameManager.Config.PhyCritDamageColor;
            if (damage.Type == DamageType.True)
                c = GameManager.Config.TrueCritDamageColor;
        }
        From.BeforeDealDamage(damage, Target);
        Target.HitBack(WindChimeEngnie.Lib.GetPosision(From.gameObject, Target.gameObject).normalized * damage.HitPower);
        Target.UnderAttack(damage,From);
        From.OnSucceedDamage(damage, Target);
        foreach (var b in damage.TakeBuff)
        {
            Buff.GiveBuff(b, From, Target);
        }
        UIManager.CreateDamageShow(damage, Target.transform.position, mut,c);
    }
}
