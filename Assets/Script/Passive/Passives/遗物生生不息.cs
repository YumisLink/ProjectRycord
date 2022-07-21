using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class 遗物生生不息 : StackPassive
{
    protected override void GetPassive()
    {
        role.Property.hps += 0.3f;
    }
}