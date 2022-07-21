using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : NormalMosterAI
{
    protected override void OnInit()
    {
        Property.MoveSpeed = 0;
    }
}
