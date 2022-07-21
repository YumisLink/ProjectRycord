using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHeav : Skill
{
    protected override void OnStart()
    {
        base.OnStart();
        ReleaseTime = 2.5f;
        CoolDown = 0;
        SkillState = "zzl";
        Type = SkillType.Skill;
    }
    public override void Before()
    {
        m_gun = Effect.Create(GameManager.OtherEffect["Shower"], transform.position).gameObject;
        m_gun.GetComponent<SpriteRenderer>().sprite = UIManager.Sprite["ÖØÁ¦Ç¹"];
        base.Before();
        t = 0;
    }
    GameObject m_gun;
    public override void After()
    {
        base.After();
        Destroy(m_gun);
    }
    float t = 0;
    public override void OnUsing()
    {
        t += Time.deltaTime;
        if (t >= 0.5)
        {
            t = 0;
            WindChimeEngnie.Lib.SetRotate(m_gun, WindChimeEngnie.Lib.GetAngle(transform.position, GameManager.CenterRole.transform.position));
            var l = WindChimeEngnie.Lib.Random(3);
            var ef = Effect.Create(GameManager.OtherEffect["Spawner"], GameManager.CenterRole.transform.position + l+l.normalized*2) as AfterSpawEffect;
            ef.dam = new Damage(0);
            ef.Master = role;
            ef.spawn = GameManager.Effects["gt"];
        }
    }
    public override bool CanUse()
    {
        if (role.State == Role.NormalState)
            return true;
        else
            return false;
    }
}
