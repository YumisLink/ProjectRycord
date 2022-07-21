using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class 遗物虚空子弹 : StackPassive
{
    public List<int> 子弹增加 = new();
    public int 数量 = 1;
    public PlayerShoot ps;
    protected override void GetPassive()
    {
        var st = role.GetComponent<PlayerShoot>();
        for (int i = 1; i <= 5; i++)
        {
            子弹增加.Add(Convert.ToInt32(st.bulletDatas[i].弹夹数量 * (0.25f + Cnt * 0.25f)));
            st.bulletDatas[i].弹夹数量 += 子弹增加[i-1];
        }
        ps = role.GetComponent<PlayerShoot>();
    }
    protected override void DiscardPassive()
    {
        var st = role.GetComponent<PlayerShoot>();
        for (int i = 1; i <= 5; i++)
        {
            //子弹增加.Add(Convert.ToInt32(st.bulletDatas[i].弹夹数量 * (0.25f + 数量 * 0.25)));
            st.bulletDatas[i].弹夹数量 -= 子弹增加[i - 1];
        }
    }
    public override void OnSucceedDamage(Damage damage, Role target)
    {
        if (UnityEngine.Random.Range(0,1f) < 0.025f)
        ps.Clip += Convert.ToInt32(ps.BulletShoot.弹夹数量 * 0.1f);
        if (ps.Clip > ps.BulletShoot.弹夹数量)
            ps.Clip = ps.BulletShoot.弹夹数量;
    }
}
