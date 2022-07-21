using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class 祝福传家之宝 : Passive
{
    protected override void GetPassive()
    {
        base.GetPassive();
        for(int i = 1; i <= 3; i++)
        {
            role.gameObject.AddComponent(Type.GetType("遗物"+GameManager.ItemPool.遗物()));
        }
    }
}