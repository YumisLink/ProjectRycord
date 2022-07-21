using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class 遗物精确制导 : StackPassive
{
    protected override void GetPassive()
    {
        base.GetPassive();
        role.Property.critical += 10;
    }
}