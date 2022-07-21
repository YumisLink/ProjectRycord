using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 冻结 : Buff
{
    Effect eff;
    protected override void OnStart()
    {
        base.OnStart();
        GameManager.PlayAudio(GameManager.AC[3]);
        Type = BuffType.OnlyOne;
        role.CanAction++;
        eff = Effect.Create(GameManager.Effects["Freeze"], transform.position);
        WindChimeEngnie.Lib.Move(eff.gameObject, new Vector2(0, 0.5f));
        WindChimeEngnie.Lib.SetMultScale(eff.gameObject, role.CharacterFigure, role.CharacterFigure);
    }
    protected override void LoseBuff()
    {
        base.LoseBuff();
        role.CanAction --;
        eff.Dead();
    }
}
