using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class 遗物疾速射击 : StackPassive
{
    protected override void GetPassive()
    {
        role.Property.AtkSpeed += 0.1f;
    }
}