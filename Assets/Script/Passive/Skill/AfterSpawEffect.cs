using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterSpawEffect : Effect
{
    public GameObject spawn;
    public Damage dam;
    public override void Dead()
    {
        var atrk = AtkEffect.Create(spawn,transform.position,Master,dam);
        base.Dead();
    }
}
