using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class 祝福武器粉碎 : Passive
{
    PlayerShoot pls;
    protected override void GetPassive()
    {
        pls = GetComponent<PlayerShoot>();
    }
    float cooldown = 10;
    private void Update()
    {
        cooldown -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.T)  && cooldown < 0)
        {
            cooldown = 10;
            pls.Clip = 0;
            pls.Reload();
            var ef = Effect.Create(GameManager.Effects["chongjibo"],transform.position) as AtkEffect;
            ef.damage = new Damage(0) { HitPower = 100 };
        }
    }
}