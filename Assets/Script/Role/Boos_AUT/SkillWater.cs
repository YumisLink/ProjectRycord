using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWater : Skill
{
    protected override void OnStart()
    {
        base.OnStart();
        ReleaseTime = 2.5f;
        CoolDown = 0;
        SkillState = "wat";
        Type = SkillType.Skill;
    }
    public override void Init()
    {
        base.Init();
        AddAction(0.5f, Shunyi);
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
    protected override void OnFixedUsing()
    {
        WindChimeEngnie.Lib.SetRotate(m_gun, WindChimeEngnie.Lib.GetAngle(transform.position, GameManager.CenterRole.transform.position));
        m_gun.transform.position = transform.position;
    }
    public void Shunyi()
    {
        role.transform.position = GameManager.CenterRole.transform.position + WindChimeEngnie.Lib.Random(1).normalized * 1.2f;
    }
    public override void OnUsing()
    {
        t += Time.deltaTime;

        if (t >= 1.5f)
        {
            var dir  = WindChimeEngnie.Lib.GetAngle(transform.position, GameManager.CenterRole.transform.position);
            for (int i = 0;i < 5; i++)
            {
                t = 0;
                Bullet.Create(role, dir + i * 10,new Damage(10), GameManager.Effects["WaterShoot"]);
            }
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
