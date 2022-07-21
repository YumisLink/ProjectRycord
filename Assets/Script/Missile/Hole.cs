using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : Bullet
{
    public GameObject eff1;
    public float AddValue;
    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        var tr = transform.localScale;
        tr.x += Time.fixedDeltaTime * AddValue ;
        tr.y += Time.fixedDeltaTime * AddValue;
        transform.localScale = tr;

    }
    public override void Dead()
    {
        if (IsDEAD > 0) return;
        Debug.Log(123);
        base.Dead();
        var ef = Effect.Create(eff1, transform.position,Master);
        ef.transform.localScale = transform.localScale;
    }
}
