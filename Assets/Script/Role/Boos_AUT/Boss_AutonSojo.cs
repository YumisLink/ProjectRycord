using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_AutonSojo : Role
{
    Skill fGun;
    Skill bGun;
    Skill wGun;
    Skill tGun;
    Skill shuny;
    Skill hGun;
    protected override void OnInit()
    {
        base.OnInit();
        fGun = gameObject.AddComponent<Skill_RangeGun>();
        bGun = gameObject.AddComponent<SkillFrozeShoot>();
        tGun = gameObject.AddComponent<SkillThunderBoss>();
        wGun = gameObject.AddComponent<SkillWater>();
        hGun = gameObject.AddComponent<SkillHeav>();
        shuny = gameObject.AddComponent<shunyi>();
        img = UIManager.instance.GetBossHp();
    }
    Image img;
    float rest = 0;
    protected override void OnFixedUpdate()
    {
        img.fillAmount = hp / Property.Maxhp;
        rest -= Time.fixedDeltaTime;
        if (rest > 0)
            return;
        var k = UnityEngine.Random.Range(1, 6); 
        if( k == 1)
        {
            fGun.AddSkillQueue();
            shuny.AddSkillQueue();
            rest = 5;
        }
        if (k == 2)
        {
            wGun.AddSkillQueue();
            rest = 2;
        }
        if (k == 3)
        {
            shuny.AddSkillQueue();
            tGun.AddSkillQueue();
            rest = 5;
        }
        if (k == 4)
        {
            shuny.AddSkillQueue();
            bGun.AddSkillQueue();
            rest = 5;
        }
        if (k == 5)
        {
            shuny.AddSkillQueue();
            hGun.AddSkillQueue();
            rest = 5;
        }
    }
    public override void Die()
    {
        base.Die();
        GameManager.instance.GetComponent<WaveManager>().Success();
    }
}
