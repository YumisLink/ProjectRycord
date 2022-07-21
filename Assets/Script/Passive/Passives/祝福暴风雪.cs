using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class 祝福暴风雪 : Passive
{
    protected override void GetPassive()
    {
        var r = role.GetComponent<PlayerShoot>();
        r.bulletDatas[1].攻击间隔 /= 2;
        r.bulletDatas[1].子弹速度 *= 1.3f;
        r.bulletDatas[1].弹夹数量 *= 2;
    }
}