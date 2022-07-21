using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class 祝福冻土 : Passive
{
    PlayerShoot pls;
    protected override void GetPassive()
    {
        pls = GetComponent<PlayerShoot>();
        pls.bulletDatas[1].子弹伤害 *= 2;
        pls.bulletDatas[1].元素异常概率 *= 2;
    }
    public override void OnShoot()
    {
        if (pls.BulletShoot.名字 == "凛冬")
        {
            pls.Clip--;
        }
    }
}