using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tumbling : Skill
{
    public int TumblingCount = 3;
    public int TumblingLevel = 3;
    private float TumblingSpeed = 0;
    private float TumblingDirection = 0;
    private PlayerController cont;
    public override void OnUpdate()
    {
        if (CoolTime <= 0)
        {
            TumblingCount++;
            CoolTime = 0;
        }
        if (TumblingCount >= TumblingLevel)
            TumblingCount = TumblingLevel;
    }

    protected override void OnFixedUsing()
    {
        var x = Mathf.Cos(Mathf.Deg2Rad * TumblingDirection)* TumblingSpeed;
        var y = Mathf.Sin(Mathf.Deg2Rad * TumblingDirection)* TumblingSpeed;
        role.Controller.Move(new Vector2(x * TumblingSpeed, y * TumblingSpeed));
        TumblingSpeed -= 4 * Time.fixedDeltaTime / ReleaseTime;
    }
    public override void Init()
    {
        AddAction(0, StartTumb);
        ReleaseTime = 0.3f;
        CoolDown = 1.5f;
        SkillState = "·­¹ö";
        Type = SkillType.Skill;
        cont = GetComponent<PlayerController>();
    }
    public override void Before()
    {
        TumblingCount--;
        TumblingSpeed = 5;
        TumblingDirection = WindChimeEngnie.Lib.GetAngle(cont.GetDirection);
    }
    public override bool CanUse()
    {
        if (TumblingCount > 0)
            return true;
        return base.CanUse();
    }
    public void StartTumb()
    {

    }
}
