using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public enum MissileType
{
    Follow, Throw, Missile
}
public class Missile : Effect
{
    /// <summary>
    /// 最终是否不会命中固定的目标
    /// </summary>
    public bool IsThrow = false;
    public MissileType type;
    public Role Target;
    public Vector2 direction;
    public Damage damage;
    public Type Element;
    /// <summary>
    /// 投掷物速度
    /// </summary>
    public float Speed = 9;
    protected float NowZ = 0;
    protected float Zdelat = 0;
    protected float AkTime = 0;
    protected float HeadUp = 0;
    protected float BaseAngle = 0;
    public GameObject effect;
    public int cross = 1;
    protected override void OnStart()
    {
        BaseAngle = transform.eulerAngles.z;
        if (IsThrow)
        {
            HeadUp = UnityEngine.Random.Range(8f, 13f);
            Zdelat = HeadUp;
            AkTime = WindChimeEngnie.Lib.GetDistance(gameObject, Target.gameObject) / Speed;
        }
    }
    
    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        if (cross <= 0)
        {
            Dead();
        }
        if (IsDEAD > 0)
            return;
        if (IsThrow)
        {
            NowZ += Zdelat;
            Zdelat -= 2 * AkTime * HeadUp * Time.deltaTime;
            if (NowZ < 0)
                IsThrow = false;
        }
        if (Target == null)
        {
            Dead();
            return;
        }
        if (WindChimeEngnie.Lib.GetDistance(gameObject, Target.gameObject) <= Time.fixedDeltaTime * Speed)
        {
            Damage.DealDamage(Master, Target, damage);
            Dead();
        }
        else
        {
            var angle = WindChimeEngnie.Lib.GetAngle(gameObject, Target.gameObject);
            var Position = transform.position;
            var addx = Speed * Mathf.Cos(Mathf.Deg2Rad * angle) * Time.fixedDeltaTime;
            var addy = Speed * Mathf.Sin(Mathf.Deg2Rad * angle) * Time.fixedDeltaTime + Zdelat * Time.fixedDeltaTime;
            Position.x += addx;
            Position.y += addy;
            transform.position = Position;
            var facet = transform.eulerAngles;
            facet.z = WindChimeEngnie.Lib.GetAngle(addx, addy) + BaseAngle;
            transform.eulerAngles = facet;
        }

    }

    public static Missile Create(Role who, Role target, Damage dam, GameObject obj)
    {
        var go = Instantiate(obj);
        foreach (var com in go.GetComponents<Component>())
        {
            if (com.GetType() != typeof(Transform) && com.GetType() != typeof(Animator) && com.GetType() != typeof(SpriteRenderer) && com.GetType() != typeof(Collider2D))
            {
                Destroy(com);
            }
        }
        var a = go.AddComponent<Missile>();
        a.Master = who;
        a.damage = dam;
        a.transform.position = who.transform.position + new Vector3(UnityEngine.Random.Range(-0.2f, 0.2f), UnityEngine.Random.Range(-0.2f, 0.2f));
        a.Target = target;
        return a;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsDEAD > 0)
            return;
        if (collision.gameObject.TryGetComponent<Role>(out var target))
        {
            if (Master.Camp.IsEnemy(target.Camp))
            {
                Damage.DealDamage(Master, target, damage);
                if (Element != null)
                    Buff.GiveBuff(Element, Master, target,Duration:6);
                cross--;
            }
        }
    }

}
