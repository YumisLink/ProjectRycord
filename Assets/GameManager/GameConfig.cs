using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class GameConfig 
{
    /// <summary>
    /// 物理伤害颜色
    /// </summary>
    [InspectorShow("物理伤害颜色")]
    public Color PhyDamageColor;
    /// <summary>
    /// 魔法伤害颜色
    /// </summary>
    [InspectorShow("魔法伤害颜色")]
    public Color MagDamageColor;
    /// <summary>
    /// 物理暴击伤害颜色
    /// </summary>
    [InspectorShow("物理暴击伤害颜色")]
    public Color PhyCritDamageColor;
    /// <summary>
    /// 魔法暴击伤害颜色
    /// </summary>
    [InspectorShow("魔法暴击伤害颜色")]
    public Color MagCritDamageColor;
    /// <summary>
    /// 真实伤害颜色
    /// </summary>
    [InspectorShow("真实伤害颜色")]
    public Color TrueDamageColor;
    /// <summary>
    /// 真实暴击伤害颜色
    /// </summary>
    [InspectorShow("真实暴击伤害颜色")]
    public Color TrueCritDamageColor;
    /// <summary>
    /// 伤害字体阴影距离
    /// </summary>
    [InspectorShow("伤害字体阴影距离")]
    public Vector2 FontShadowMoving;
    /// <summary>
    /// 角色控制方式
    /// </summary>
    [InspectorShow("角色控制方式")]
    public CharacterControllerMode CharacterControllerMode;
    /// <summary>
    /// 精英怪生成权值
    /// </summary>
    [InspectorShow("精英怪生成权值")]
    public int EliteSpanProbility = 5;
    /// <summary>
    /// 随机事件生成权值
    /// </summary>
    [InspectorShow("随机事件生成权值")]
    public int ShopSpanProbility = 15;
    /// <summary>
    /// 小怪生成权值
    /// </summary>
    [InspectorShow("小怪生成权值")]
    public int MonseterSpanProbility = 15;
}
public enum CharacterControllerMode
{
    LOL,GunAndDungen
}