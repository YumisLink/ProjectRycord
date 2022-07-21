using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkEffect : Effect
{
    public Damage damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsDEAD > 0)
            return;
        if (collision.gameObject.TryGetComponent<Role>(out var r))
        {
            if (Master != null)
            {
                if (r.Camp.IsEnemy(Master.Camp))
                {
                    Damage.DealDamage(Master, r, damage);
                }
            }
            else
            {
                Damage.DealDamage(GameManager.CenterRole, r, damage);
            }

        }
    }
    public static AtkEffect Create(GameObject go, Vector2 at, Role master,Damage dam, bool follow = false, float time = 10)
    {
        var a = Instantiate(go);
        WindChimeEngnie.Lib.MoveTo(a, at);
        var eff = a.GetComponent<AtkEffect>();
        eff.damage = dam;
        eff.Master = master;
        eff.follow = follow;
        eff.dies = follow;
        return eff;
    }

}
