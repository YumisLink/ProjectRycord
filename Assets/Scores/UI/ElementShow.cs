using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementShow : Effect
{
    SpriteRenderer spr;
    public float Mut = 1;
    protected override void OnStart()
    {
        spr = GetComponent<SpriteRenderer>();
    }
    bool d;
    public override void OnUpdate()
    {
        if (d)
        {
            var c = spr.color;
            c.a += Time.deltaTime * Mut;
            spr.color = c;
            if (c.a > 1)
                d = false;
        }
        else
        {
            var c = spr.color;
            c.a -= Time.deltaTime * Mut;
            spr.color = c;
            if (c.a  < 0)
                d = true;
        }
    }

}
