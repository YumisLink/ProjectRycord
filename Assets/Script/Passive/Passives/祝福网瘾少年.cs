using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class 祝福网瘾少年 : Passive
{
    protected override void GetPassive()
    {
        var pls = role.GetComponent<PlayerShoot>();
        pls.bulletDatas[3].枪管数量 -=2;
        pls.bulletDatas[3].子弹伤害 += 5;
        pls.bulletDatas[3].攻击间隔 -= 0.05f;
    }
}