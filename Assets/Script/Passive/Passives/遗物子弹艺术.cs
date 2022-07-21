using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class 遗物子弹艺术 : StackPassive
{
    protected override void GetPassive()
    {
        role.Property.Crit -= (0.7f - 0.2f * Cnt);
        role.Property.critical += 50;
    }
    protected override void DiscardPassive()
    {
        role.Property.Crit += (0.7f - 0.2f * Cnt);
        role.Property.critical -= 50;
    }

}