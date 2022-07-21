using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element
{
    None,Fire,Water,Thunder,Ice
}
public class NormalMosterAI : Role
{
    public static NormalMosterAI Create(GameObject go,Vector2 position)
    {
        var g = Instantiate(go);
        Effect.Create(GameManager.Effects["Spawn"], position);
        g.transform.position = position;
        var ret = g.GetComponent<NormalMosterAI>();
        return ret;
    }
    int cnt = 0;
    public Element Contain = Element.Fire;
    public override void PerTriggerUpdate()
    {
        if (Contain == Element.None)
            Contain = (Element)Random.Range(1, 5);
        cnt++;
        if (cnt < 1)
            return;
        cnt = 0;
        if (CanAction > 0)
             return;
        base.PerTriggerUpdate();
        var a = GetNearestEnemy();
        if (a != null && Target == null )
            Controller.MoveAttack(a.transform.position);
        if (Target != null)
            if (WindChimeEngnie.Lib.GetDistance(Target.gameObject, gameObject) > Property.Range)
            {   
                Controller.MoveAttack(Target.transform.position);
            }
    }
    public override void Die()
    {
        GameManager.Money += UnityEngine.Random.Range(0,4);
        var ef  = Effect.Create(GameManager.Effects["Evaporation"], transform.position);
        ef.GetComponent<SpriteRenderer>().color = Color.black;
        base.Die();
        if (Contain == Element.Fire)
            GameManager.MainBase.FireHp += 0.35f;
        if (Contain == Element.Water)
            GameManager.MainBase.WaterHp += 0.35f;
        if (Contain == Element.Ice)
            GameManager.MainBase.IceHp += 0.35f;
        if (Contain == Element.Thunder)
            GameManager.MainBase.ThunderHp += 0.35f;
    }
}
