using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class 祝福电磁碰撞 : Passive
{
    protected override void GetPassive()
    {
        role.GetComponent<PlayerShoot>().bulletDatas[3].枪管数量 += 3;       
    }
}