using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class 祝福坍塌 : Passive
{
    PlayerShoot pls;
    protected override void GetPassive()
    {
        pls = GetComponent<PlayerShoot>();
        pls.bulletDatas[4].元素异常概率 += 0.4f;
    }
}