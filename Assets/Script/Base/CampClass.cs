using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CampType{
    [EnumNameAttribute("友军")]
    Friend,
    [EnumNameAttribute("敌军")]
    Enemy
}
[Serializable]
public class CampClass
{
    [InspectorShow("所属阵营")]
    public CampType Belong;
    public bool IsEnemy(CampClass T)
    {
        if (T.Belong != Belong)
            return true;
        else return false;
    }
    public CampClass(CampType belong)
    {
        Belong = belong;
    }
    public CampClass()
    {
        Belong = CampType.Friend;
    }
    public Color GetColor()
    {
        if (Belong == CampType.Enemy)
            return Color.red;
        if (Belong == CampType.Friend)
            return Color.green;
        else return Color.white;
    }

}
