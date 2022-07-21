using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFrozeShoot : Skill
{
    protected override void OnStart()
    {
        base.OnStart();
        ReleaseTime = 1.4f;
        CoolDown = 0;
        SkillState = "Fz";
        Type = SkillType.Skill;
    }
    public override void Before()
    {
        base.Before();
        m_gun = Effect.Create(GameManager.OtherEffect["Shower"], transform.position).gameObject;
        m_gun.GetComponent<SpriteRenderer>().sprite = UIManager.Sprite["ฑ๙วน"];
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
        if (t >= 0.2)
        {
            var di = WindChimeEngnie.Lib.GetAngle(transform.position, GameManager.CenterRole.transform.position);
            WindChimeEngnie.Lib.SetRotate(m_gun, di);
            t -= 0.2f;
            Bullet.Create(role, di, new Damage(20), GameManager.Effects["IceShoot"], speed: 10);
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
