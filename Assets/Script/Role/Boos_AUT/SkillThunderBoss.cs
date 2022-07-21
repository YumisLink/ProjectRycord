using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillThunderBoss : Skill
{
    protected override void OnStart()
    {
        base.OnStart();
        ReleaseTime = 1.4f;
        CoolDown = 0;
        SkillState = "tz";
        Type = SkillType.Skill;
    }
    public override void Before()
    {
        m_gun = Effect.Create(GameManager.OtherEffect["Shower"], transform.position).gameObject;
        m_gun.GetComponent<SpriteRenderer>().sprite = UIManager.Sprite["WaterGun"];
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
            var ef = Effect.Create(GameManager.OtherEffect["Spawner"],GameManager.CenterRole.transform.position + WindChimeEngnie.Lib.Random(1)) as AfterSpawEffect;
            ef.dam = new Damage(35);
            ef.Master = role;
            ef.spawn = GameManager.Effects["FireBoom"];
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
