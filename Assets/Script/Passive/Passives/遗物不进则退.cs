using System.Collections;
using System.Collections.Generic;
using System;

public class 遗物不进则退 : StackPassive
{
    float time = 0;
    public PlayerShoot ps;
    protected override void OnStart()
    {
        base.OnStart();
        ps = GetComponent<PlayerShoot>();
    }
    public override void OnSucceedDamage(Damage damage, Role target)
    {
        time = 5;
    }
    public override void OnSustainedTrigger()
    {
        time -= 0.5f;
        if (time > 0)
            ps.RecoverClip(Convert.ToInt32(0.05f * Cnt * ps.BulletShoot.弹夹数量));
        else
            ps.RecoverClip(Convert.ToInt32(-0.05f * ps.BulletShoot.弹夹数量));
    }
}
