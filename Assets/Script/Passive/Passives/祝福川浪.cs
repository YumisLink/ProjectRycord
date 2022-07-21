using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class 祝福川浪 : Passive
{
    PlayerShoot pls;
    protected override void GetPassive()
    {
        pls = GetComponent<PlayerShoot>();
        pls.bulletDatas[2].枪管数量 += 2;
        pls.bulletDatas[2].散射程度 -= 1;
    }
}