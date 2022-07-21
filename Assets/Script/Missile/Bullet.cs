using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Missile
{
    public float Ang = 0;
    protected override void OnStart()
    {
        base.OnStart();
        var Position = transform.position;
        var addx = Speed * Mathf.Cos(Mathf.Deg2Rad * Ang) * Time.fixedDeltaTime;
        var addy = Speed * Mathf.Sin(Mathf.Deg2Rad * Ang) * Time.fixedDeltaTime;
        Position.x += addx;
        Position.y += addy;
        transform.position = Position;
        var facet = transform.eulerAngles;
        facet.z = WindChimeEngnie.Lib.GetAngle(addx, addy) + BaseAngle;
        transform.eulerAngles = facet;
    }
    public override void OnFixedUpdate()
    {
        if (cross <= 0)
        {
            Dead();
        }
        var Position = transform.position;
        var addx = Speed * Mathf.Cos(Mathf.Deg2Rad * Ang) * Time.fixedDeltaTime;
        var addy = Speed * Mathf.Sin(Mathf.Deg2Rad * Ang) * Time.fixedDeltaTime;
        Position.x += addx;
        Position.y += addy;
        transform.position = Position;
        var facet = transform.eulerAngles;
        facet.z = WindChimeEngnie.Lib.GetAngle(addx, addy) + BaseAngle;
        transform.eulerAngles = facet;
    }
    public static Bullet Create(Role who, float direction, Damage dam, GameObject obj, float speed = 30,float liver = 10,int cross = 1)
    {
        var go = Instantiate(obj);
        Bullet a;
        if (go.TryGetComponent<Bullet>(out var aj))
        {
            a = aj;
        }
        else
        {
            a = go.AddComponent<Bullet>();
        }
        a.Master = who;
        a.damage = dam;
        a.Speed = speed;
        a.LiveTime = liver;
        a.cross = cross;
        a.transform.position = who.transform.position+ new Vector3(0, 0.3f);
        a.Ang = direction;


        return a;

    }
}
