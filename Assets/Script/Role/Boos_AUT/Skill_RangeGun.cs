using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_RangeGun : Skill
{
    protected override void OnStart()
    {
        base.OnStart();
        ReleaseTime = 2;
        CoolDown = 0;
        SkillState = "Fire";
        Type = SkillType.Skill;
    }
    GameObject m_gun1;
    GameObject m_gun2;
    public override void Before()
    {
        m_gun1 = Effect.Create(GameManager.OtherEffect["Shower"],transform.position).gameObject;
        m_gun1.GetComponent<SpriteRenderer>().sprite = UIManager.Sprite["ป๐วน"];
        m_gun2 = Effect.Create(GameManager.OtherEffect["Shower"], transform.position).gameObject;
        m_gun1.GetComponent<SpriteRenderer>().sprite = UIManager.Sprite["ป๐วน"];
        Speed = 0;
        dir1 = WindChimeEngnie.Lib.GetAngle(transform.position, GameManager.CenterRole.transform.position);
        dir2 = dir1;
        AddSpeed = 360;
    }
    float Speed = 0;
    float AddSpeed = 0;
    float dir1, dir2;
    float t = 0;
    
    protected override void OnFixedUsing()
    {
        Speed += AddSpeed * Time.fixedDeltaTime;
        if (Speed >= 360)
            AddSpeed = -360;
        dir1 += Speed * Time.fixedDeltaTime; 
        dir2 -= Speed * Time.fixedDeltaTime;
        WindChimeEngnie.Lib.SetRotate(m_gun1, dir1);
        WindChimeEngnie.Lib.SetRotate(m_gun2, dir2);
    }
    public override void After()
    {
        base.After();
        Destroy(m_gun1);
        Destroy(m_gun2);
    }
    public override void OnUsing()
    {
        t += Time.deltaTime;
        if (t >= 0.2)
        {
            t -= 0.2f;
            Bullet.Create(role, dir1, new Damage(20), GameManager.Effects["FireShoot"],speed:10);
            Bullet.Create(role, dir2, new Damage(20), GameManager.Effects["FireShoot"], speed: 10);
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
