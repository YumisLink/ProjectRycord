using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 碎裂 : Buff
{
    Effect eff1;
    protected override void OnStart()
    {
        eff1 = Effect.Create(GameManager.Effects["PhyDown"],transform.position, master: role, follow: true);
        eff1.offset = new Vector3(0, 2);
        base.OnStart();
        role.Property.PhyDef -= 10;
    }
    protected override void LoseBuff()
    {

        Destroy(eff1.gameObject);
        base.LoseBuff();
        role.Property.PhyDef  += 10;
    }
}
