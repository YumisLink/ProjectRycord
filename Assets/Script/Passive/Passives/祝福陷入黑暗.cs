using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class 祝福陷入黑暗 : Passive
{
    PlayerShoot pls;
    protected override void GetPassive()
    {
        pls = role.GetComponent<PlayerShoot>(); 
    }
    public override void OnShoot()
    {
        if (pls.BulletShoot.名字 == "黑洞")
        {
            var r = Random.Range(0, GameManager.AllRole.Count);
            Effect.Create(GameManager.Effects["gt"], GameManager.AllRole[r].transform.position);
        }
    }
}