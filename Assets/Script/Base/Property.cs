using System;
using UnityEngine;
[Serializable]
public class Property
{
    /// <summary>
    /// 最大生命值
    /// </summary>
    [InspectorShow("最大生命值")]
    public float AtkSpeed = 1;
    public float AtkBonus = 1;

    public int Maxhp = 600;
    /// <summary>
    /// 生命恢复速度
    /// </summary>
    [InspectorShow("生命恢复速度")]
    public float hps = 0;
    /// <summary>
    /// 充能上限
    /// </summary>
    [InspectorShow("充能上限")]
    public float sp = 0;
    /// <summary>
    /// 充能自然恢复
    /// </summary>
    [InspectorShow("充能自然恢复")]
    public float sps = 0;
    /// <summary>
    /// 物理攻击力
    /// </summary>
    [InspectorShow("攻击力")]
    public float Atk = 0;
    /// <summary>
    /// 魔法攻击力
    /// </summary>
    [InspectorShow("物理防御")]
    public float PhyDef = 0;
    /// <summary>
    /// 魔法防御
    /// </summary>
    [InspectorShow("魔法防御")]
    public float MagDef = 0;
    /// <summary>
    /// 暴击倍率
    /// </summary>
    [InspectorShow("暴击倍率")]
    public float Crit = 1.5f;
    /// <summary>
    /// 暴击概率
    /// </summary>
    public float Critical => critical * 0.01f;
    [SerializeField]
    [InspectorShow("暴击概率")]
    public float critical = 1;
    /// <summary>
    /// 冷却缩减
    /// </summary>
    [InspectorShow("冷却缩减")]
    public float CDReduce = 1;
    /// <summary>
    /// 移动速度
    /// </summary>
    [InspectorShow("移动速度")]
    public float MoveSpeed = 2;
    /// <summary>
    /// 咏唱速度
    /// </summary>
    [InspectorShow("咏唱速度")]
    public float CastSpeed = 1;
    /// <summary>
    /// 攻击间隔
    /// </summary>
    [InspectorShow("攻击间隔")]
    public float AtkInterval = 1;
    [InspectorShow("攻击距离")]
    [SerializeField]
    private float range = 128;
    /// <summary>
    /// 攻击距离
    /// </summary>
    public float Range => range * 0.012f;

}
