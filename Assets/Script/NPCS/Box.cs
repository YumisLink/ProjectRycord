using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : NPC
{
    public override void OnTouch(Role py)
    {
        WindChimeEngnie.Lib.MoveTo(Instantiate(GameManager.Gos[0]),transform.position);
        Destroy(gameObject);
    }
}
