using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skyBook : NPC
{
    public override void OnTouch(Role py)
    {
        UIManager.GetBelssing();
        Destroy(gameObject);
    }
}
