using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shunyi : Skill
{
    protected override void OnStart()
    {
        base.OnStart();
        ReleaseTime = 1f;
        CoolDown = 0;
        SkillState = "zl";
        spr = GetComponent<SpriteRenderer>();
        Type = SkillType.Skill;
    }
    public override void Before()
    {
        base.Before();
    }
    public override void Init()
    {
        base.Init();
        AddAction(0.5f, Shunyi);
    }
    SpriteRenderer spr;
    public void Shunyi()
    {
        role.transform.position = GameManager.CenterRole.transform.position + WindChimeEngnie.Lib.Random(1).normalized * 1.2f;
    }
    GameObject m_gun;
    public override void After()
    {
        base.After();
        Destroy(m_gun);
    }
    public override void OnUsing()
    {
        if (UsingTime < 0.5f)
        {
            var c = spr.color;
            c.a -= Time.deltaTime * 2;
            spr.color = c;
        }
        else
        {
            var c = spr.color;
            c.a += Time.deltaTime * 2;
            spr.color = c;
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
