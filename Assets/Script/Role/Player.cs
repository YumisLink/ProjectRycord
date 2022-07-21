using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Role
{
    public override void UnderAttack(Damage damage, Role From)
    {
        base.UnderAttack(damage, From);
        if (Invincible <= 0)
            Invincible = 0.75f;
    }
    Image img;
    protected override void OnInit()
    {
        img = UIManager.GetImage("MainHp");
        base.OnInit();
    }
    protected override void OnFixedUpdate()
    {
        img.fillAmount = hp / Property.Maxhp;        
        base.OnFixedUpdate();
    }
    public override void Die()
    {
        GameManager.MainBase.FireHp -= 10;
        GameManager.MainBase.IceHp -= 10;
        GameManager.MainBase.ThunderHp-= 10;
        GameManager.MainBase.WaterHp -= 10;
        hp = Property.Maxhp;
    }
}
