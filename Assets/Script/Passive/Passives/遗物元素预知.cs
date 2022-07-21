using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class 遗物元素预知 : StackPassive
{
    public List<float> yc = new();
    public int 数量 = 1;
    public PlayerShoot ps;
    protected override void GetPassive()
    {
        var st = role.GetComponent<PlayerShoot>();
        for (int i = 1; i <= 5; i++)
        {
            yc.Add(st.bulletDatas[i].元素异常概率 * Cnt * 0.1f);
            st.bulletDatas[i].元素异常概率 += yc[i - 1];
        }
        ps = role.GetComponent<PlayerShoot>();
    }
    protected override void DiscardPassive()
    {
        var st = role.GetComponent<PlayerShoot>();
        for (int i = 1; i <= 5; i++)
        {
            //子弹增加.Add(Convert.ToInt32(st.bulletDatas[i].弹夹数量 * (0.25f + 数量 * 0.25)));
            st.bulletDatas[i].元素异常概率 -= yc[i - 1];
        }
    }
}