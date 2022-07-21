using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Effect : MonoBehaviour
{
    [SerializeField]
    protected float LiveTime = 999;
    public Role Master;
    public float IsDEAD = 0;
    public bool follow;
    public bool dies = false;
    public Vector3 offset = new();
    protected virtual void OnStart() { }
    void Start()
    {
        OnStart();
    }
    public virtual void Dead()
    {
        if (TryGetComponent<Light2D>(out var l2d))
        {
            l2d.pointLightOuterRadius = 0;
        }
        var sp = GetComponent<SpriteRenderer>();
        if (sp != null)
            sp.color = Color.clear;
        if (TryGetComponent<AudioSource>(out var aud))
        {
            aud.Stop();
        }
        IsDEAD = 11;
        foreach (var a in gameObject.GetComponentsInChildren<ParticleSystem>())
            a.Stop();
        foreach (var a in gameObject.GetComponentsInChildren<SpriteRenderer>())
            a.color = new Color(0,0,0,0);
    }
    public virtual void OnUpdate() { }
    public virtual void OnFixedUpdate() { }
    private void FixedUpdate()
    {
        OnFixedUpdate();
        if (IsDEAD > 0)
            return;

        if (follow)
        {
            if (Master == null)
            {
                Dead();
                return ;
            }
            transform.position = Master.transform.position + offset;
        }
    }
    void Update()
    {
        if (Master == null && dies)
            Dead();
        LiveTime -= Time.deltaTime;
        if (LiveTime < 0 && IsDEAD <= 0)
            Dead();
        if (IsDEAD >= 10)
        {
            IsDEAD -= Time.deltaTime;
            if (IsDEAD <= 10)
                Destroy(gameObject);
            return;
        }
        OnUpdate();
    }
    public static Effect Create(GameObject go,Vector2 at,Role master = null,bool follow = false,float time= 10)
    {
        var a = Instantiate(go);
        WindChimeEngnie.Lib.MoveTo(a, at);
        var eff =  a.GetComponent<Effect>();
        eff.Master = master;
        eff.follow = follow;
        eff.dies = follow;
        return eff;
    }
}
