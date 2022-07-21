using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 超导 : Buff
{
    Effect eff1;
    protected override void OnStart()
    {
        eff1 = Effect.Create(GameManager.Effects["MagDown"], transform.position, master:role, follow: true);
        eff1.offset = new Vector3(0, 2);
        base.OnStart();
        role.Property.MagDef -= 10;
    }
    protected override void LoseBuff()
    {
        Destroy(eff1.gameObject);
        base.LoseBuff();
        role.Property.MagDef += 10;
    }
}
