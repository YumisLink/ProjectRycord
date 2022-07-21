using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingEffect : Effect
{
    public Vector2 TargetPlace;
    public SpriteRenderer spr;
    public float Offset;
    public float dis;
    public PlayerShoot ps;
    public void SetLiveTime (float time)
    {
        LiveTime = time;
    }
    protected override void OnStart()
    {
        if (From.TryGetComponent<Role>(out _))
        {
            iss = false;
        }
        spr = GetComponent<SpriteRenderer>();
        Offset = -WindChimeEngnie.Lib.GetAngle(TargetPlace);
        dis = WindChimeEngnie.Lib.GetDistance(TargetPlace);
        if (iss)
            transform.position = From.transform.position;
        else
            transform.position = From.transform.position + new Vector3(0,0.5f);
        ps = Master.GetComponent<PlayerShoot>();
        var scale = transform.localScale;
        var eul = transform.localEulerAngles;
        Vector2 targetPosition = Target.transform.position + new Vector3(0, 0.5f);
        var dist = WindChimeEngnie.Lib.GetDistance(gameObject.transform.position, targetPosition);
        var dire = WindChimeEngnie.Lib.GetAngle(gameObject.transform.position, targetPosition);
        scale.x = dist / dis;
        eul.z = dire + Offset;
        transform.localEulerAngles = eul;
        transform.localScale = scale;
    }
    public GameObject From;
    public bool iss = true;
    public GameObject Target;
    private void FixedUpdate()
    {
        if (From == null || Target == null)
            return;
        if (iss)
            transform.position = From.transform.position;
        else
            transform.position = From.transform.position + new Vector3(0, 0.5f);
        var scale = transform.localScale;
        var eul = transform.localEulerAngles;
        Vector2 targetPosition = Target.transform.position + new Vector3(0, 0.5f);
        var dist = WindChimeEngnie.Lib.GetDistance(gameObject.transform.position, targetPosition);
        var dire = WindChimeEngnie.Lib.GetAngle(gameObject.transform.position, targetPosition);
        scale.x =  dist / dis;
        eul.z = dire + Offset;
        transform.localEulerAngles = eul;
        transform.localScale = scale;
    }
    public static LightingEffect Create(Role from, Role to,GameObject go,float time = 0.3f, GameObject fm = null)
    {
        go = Instantiate(go);
        LightingEffect le = go.GetComponent<LightingEffect>();  
        le.Master = from;
        if (fm == null)
            le.From = le.Master.gameObject;
        else
            le.From = fm;
        le.Target = to.gameObject;
        le.LiveTime = time;
        return le;
    }


}
