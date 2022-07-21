using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class 祝福虚空武器 : Passive
{
    PlayerShoot pls;
    protected override void GetPassive()
    {
        pls = GetComponent<PlayerShoot>();
    }
    public override void SwitchWeapon(BulletData btd)
    {
        var kkk = Random.Range(0, pls.nx.Count);
        var n = pls.nx[kkk];
        pls.BulletShoot = pls.bulletDatas[pls.nx[kkk]];
        pls.nx.RemoveAt(kkk);
        pls.nx.Add(pls.now);
        pls.now = n;
        var spr = pls.Gun.GetComponent<SpriteRenderer>();
        spr.sprite = pls.BulletShoot.枪的贴图;
        pls.Clip = 0;
    }
}