using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class 祝福火焰之环 : Passive
{
    public float T = 0;
    public bool IsTower = false;
    protected override void GetPassive()
    {
        if (TryGetComponent<Tower>(out _))
            IsTower = true;
    }
    private void FixedUpdate()
    {
        if (IsTower)
            T -= Time.fixedDeltaTime * GameManager.MainBase.FireHp / 100;
        else
            T -= Time.fixedDeltaTime;
        if (T < 0)
        {
            T = 4;
            var ef = AtkEffect.Create(GameManager.Effects["RingFire"],transform.position,role, new Damage(20));
            ef.damage.TakeBuff.Add(typeof(火焰));
        }
    }
}