using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 聚怪effect : AtkEffect
{
    public GameObject atkeff;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsDEAD > 0) return;
        if(collision.gameObject.TryGetComponent<Role>(out var r))
        {
            if (r.Camp.IsEnemy(Master.Camp))
            {
                //Debug.Log(WindChimeEngnie.Lib.GetPosision(r.gameObject, gameObject).normalized * 100);
                r.HitBack(WindChimeEngnie.Lib.GetPosision(r.gameObject, gameObject).normalized * 500);
            }
        }
    }
    int cnt;
    public override void Dead()
    {
        base.Dead();
        var akf = AtkEffect.Create(atkeff, transform.position, Master) as AtkEffect;
        akf.transform.localScale = transform.localScale * 2;
        akf.damage = new Damage(80);  
    }
}
