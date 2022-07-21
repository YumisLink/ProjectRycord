using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circle_effect : AtkEffect
{
    float t = 0;
    public float mut = 1;
    SpriteRenderer sr;
    protected override void OnStart()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    public override void OnFixedUpdate()
    {
        if (IsDEAD > 0)
            return;
        t += Time.fixedDeltaTime;
        transform.localScale = new Vector3(t * mut, t * mut, 1);
        var c = sr.color;
        c.a -= Time.fixedDeltaTime;
        sr.color = c;
    }
}
