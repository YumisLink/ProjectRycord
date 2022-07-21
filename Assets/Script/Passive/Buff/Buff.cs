using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BuffType
{
    none,OnlyOne
}

public class Buff : Passive
{
    /// <summary>
    /// Buff最大剩余时间
    /// </summary>
    public float Duration;
    /// <summary>
    /// Buff剩余时间
    /// </summary>
    public float RemainingTime;
    /// <summary>
    /// Buff的名字
    /// </summary>
    public string BuffName;
    /// <summary>
    /// Buff叠加的层数
    /// </summary>
    public int Stack;
    /// <summary>
    /// Buff是否可以叠加
    /// </summary>
    public bool IsStack;
    /// <summary>
    /// buff类型，用于叠甲buff
    /// </summary>
    public BuffType Type;
    /// <summary>
    /// 获得本buff的时候调用一次
    /// </summary>
    public virtual void GetBuff(){ }
    public Role BuffCreater;
    /// <summary>
    /// 失去本buff的时候调用一次
    /// </summary>
    protected virtual void LoseBuff() { }
    public virtual void Init() { }
    protected override void OnStart()
    {
        Init();
        role.BeforeGetBuff(this);
        role.OnConflictBuff(this);
        GetBuff();
        role.AfterGetBuff(this);
    }
    public override void OnConflictBuff(Buff buff)
    {
        if (Type == BuffType.OnlyOne)
        {
            if (buff.GetType() == this.GetType())
                DestroyBuff();
            return ;
        }
        //if (buff.Type == Type)
        //{
        //    if (IsStack)
        //        buff.Stack += Stack;
        //    DestroyBuff();
        //    return;
        //}
    }
    public void DestroyBuff()
    {
        LoseBuff();
        Destroy(this);
        //role.DeletePossiveQueue.Enqueue(this);
    }
    private static Buff GiveBuff(Type Type,Role to)
    {
        var buff = (Buff)to.gameObject.AddComponent(Type);
        return buff;
    }
    public static void GiveBuff(Type Type, Role from,Role to, int stack = 1, float Duration = 10)
    {
        Buff buff = GiveBuff(Type, to);
        if (buff)
        {
            buff.Stack = stack;
            buff.Duration = Duration;
            buff.RemainingTime = Duration;
            buff.BuffCreater = from;
            from.OnGiveBuff(buff);
        }
        else
            Debug.LogError("Buff为空");
    }
    protected virtual void OnUpdate() { }
    private void Update()
    {
        OnUpdate();
        RemainingTime -= Time.deltaTime;
        if (RemainingTime <= 0)
            DestroyBuff();
        if (Stack <= 0)
            DestroyBuff();
    }
}
