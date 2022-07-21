using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class 祝福无尽弹夹 : Passive
{
    public PlayerShoot pls;
    protected override void GetPassive()
    {
        pls = GetComponent<PlayerShoot>();
        int e = UnityEngine.Random.Range(1, 6);
        if (pls.fs == e)
            pls.SwitchWeapon();
        pls.bulletDatas[e].弹夹数量 = 11451419;
        pls.canswtch = false;
    }
}