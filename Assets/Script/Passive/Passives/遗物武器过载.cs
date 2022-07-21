using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 遗物武器过载 : StackPassive
{
    public List<float> damageBound = new();
    public PlayerShoot ps;
    protected override void GetPassive()
    {
        var st = role.GetComponent<PlayerShoot>();
        for (int i = 1; i <= 5; i++)
        {
            damageBound.Add(st.bulletDatas[i].子弹伤害 *  Cnt * 0.45f);
            st.bulletDatas[i].子弹伤害 += damageBound[i - 1];
        }
        ps = role.GetComponent<PlayerShoot>();
    }
    protected override void DiscardPassive()
    {
        var st = role.GetComponent<PlayerShoot>();
        for (int i = 1; i <= 5; i++)
        {
            //子弹增加.Add(Convert.ToInt32(st.bulletDatas[i].弹夹数量 * (0.25f + 数量 * 0.25)));
            st.bulletDatas[i].子弹伤害 -= damageBound[i - 1];
        }
    }
    public override void OnShoot()
    {
        if (UnityEngine.Random.Range(0, 1f) < 0.05f)
            ps.Clip = 0;
    }
}
