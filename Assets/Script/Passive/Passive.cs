using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Role))]
public class Passive : MonoBehaviour
{
    public Role role;
    private void Start()
    {
        role = GetComponent<Role>();
        role.OnConflictPassive(this);
        OnStart();
        GetPassive();
        role.passives.Add(this);
        if (role == GameManager.CenterRole)
        {
            Debug.Log(this.GetType().Name);
            var obj = UIManager.instance.ClickC;
            if (this.GetType().Name.Contains("祝福"))
                obj.GetComponent<tfwssw>().AddMijuan(this.GetType().Name);
        }
    }
    protected void Discard()
    {
        DiscardPassive();
        Destroy(this);
    }
    private void OnDestroy()
    {
        if (role != null)
            role.passives.Remove(this);
    }
    protected virtual void OnStart(){}
    public virtual void OnConflictPassive(Passive passive) { }
    public virtual void BeforeTakeDamage(Damage damage,Role target){ }
    public virtual void AfterTakeDamage(Damage damage, Role target) { }
    public virtual void OnSucceedDamage(Damage damage, Role target) { }
    public virtual void BeforeDealDamage(Damage damage, Role target) { }
    public virtual void BeforeFinalAttack(Damage damage, Role target) { }
    public virtual void OnConflictBuff(Buff buff) { }
    public virtual void BeforeGetBuff(Buff buff) { }
    public virtual void AfterGetBuff(Buff buff) { }
    public virtual void OnGiveBuff(Buff buff) { }
    public virtual void BeforeUseSkill(Skill skill) { }
    public virtual void AfterUseSkill(Skill skill) { }
    public virtual void OnSustainedTrigger() { }
    public virtual float BeforeHealing(float heal) { return 0; }
    public virtual void SwitchWeapon(BulletData btd) { }
    public virtual void OnShoot() { }
    protected virtual void GetPassive() { }
    protected virtual void DiscardPassive() { }
}
