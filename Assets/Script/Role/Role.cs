using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role : MonoBehaviour 
{
    public Skill nowSkill = null;
    public string State = NormalState;
    public int CanAction = 0;
    public bool CanNotSelect = false;
    public bool CanChangeHead = true;
    public float Invincible = 0;
    public void SetAttackTarget(Role Target)
    {
        if (CanAction > 0)
            return;
        if (CanAttack)
        {
            this.Target = Target;
            if (Target == null)
                return;
            if (Target.transform.position.x > transform.position.x)
                SetFaceToRight();
            else
                SetFaceToLeft();
        }
    }
    public void Attack()
    {
        if (CanAction > 0)
            return;
        if (AttackCoolTime <= 0 && Controller.IsMoving == false )
        {
            //Animator.Play("Attack");
            if (MissileObj == null)
                MissileObj = GameManager.Effects["Null"];
            var dmg = Property.Atk;
            Damage dm = new(dmg, DamageType);
            dm.SetHitPower(HitBackPower);
            if (Property.Range < 2)
            {
                var mis = Missile.Create(this, Target, dm, MissileObj);
                mis.transform.position = transform.position + new Vector3(0, ChestPosition.y);
                mis.IsThrow = IsT;
                mis.Speed = MissileSpeed * 0.01f;
            }
            else
            {
                var mis = Bullet.Create(this,WindChimeEngnie.Lib.GetAngle(gameObject,Target.gameObject),dm,MissileObj,5,10,1);
                mis.transform.position = transform.position + new Vector3(0, ChestPosition.y);
            }
            AttackCoolTime = Property.AtkInterval;
        }
    }
    void Start()
    {
        GameManager.AddEntity(this);
        spr = GetComponent<SpriteRenderer>();
        if (TryGetComponent<Animator>(out _))
            Animator = GetComponent<Animator>();
        Animator = GetComponent<Animator>();
        Controller = gameObject.AddComponent<CharacterController>();
        hp = Property.Maxhp;
        OnInit();
    }
    public virtual void Die()
    {
        GameManager.RemoveEntity(this);
        Destroy(gameObject);
    }
    public virtual void PerTriggerUpdate() 
    {
        OnSustainedTrigger();
    }
    protected virtual void OnFixedUpdate() { }
    private void FixedUpdate()
    {
        RedTime -= Time.fixedDeltaTime;
        if (spr != null)
            spr.sortingOrder = -(int)(transform.position.y * 10);
        else
            spr = GetComponent<SpriteRenderer>();
        OnFixedUpdate();
    }
    void Update()
    {
        Invincible -= Time.deltaTime;
        AttackCoolTime -= Time.deltaTime;
        Heal(Property.hps * Time.deltaTime);
        TimeCount += Time.deltaTime;
        if (TimeCount > 0.5f)
        {
            TimeCount -= 0.5f;
            PerTriggerUpdate();
        }
        if (Target != null)
        {
            var dis = WindChimeEngnie.Lib.GetDistance(Target.gameObject, gameObject);
            if (dis <= Property.Range)
            {
                if (Controller.IsMoving)
                {
                    Controller.IsMoving  = false;
                    if (Animator)
                    Animator.SetBool("Move",false);
                }
                Attack();
            }
            else
            {
                Controller.NearGo(Target.transform.position);
                Controller.IsMoving = true;
                if (Animator)
                    Animator.SetBool("Move", true);
            }
        }
        if (hp <= 0)
            Die();
        OnUpdate();
        if (SkillQueue.Count > 0)
            if (SkillQueue.Peek().CanUse())
            {
                var skill = SkillQueue.Dequeue();
                skill.WantSkill();
            }
        if (SkillQueue.Count > 0)
            if (SkillQueue.Peek().InputTime < Time.time)
                SkillQueue.Dequeue();
    }
    public void SetFaceToLeft()
    {
        if (!CanChangeHead) return;
        face = -1;
        if (transform.localScale.x > 0) 
            WindChimeEngnie.Lib.SetFlipX(gameObject);
    }
    public void SetFaceToRight()
    {
        if (!CanChangeHead) return;
        face = 1;
        if (transform.localScale.x < 0)
            WindChimeEngnie.Lib.SetFlipX(gameObject);
    }
    public List<Role> SearchNearEnemy(float Range)
    {
        List<Role> result = new List<Role>();
        foreach(var a in GameManager.AllRole)
            if (!a.CanNotSelect)
                if (WindChimeEngnie.Lib.GetDistance(gameObject,a.gameObject) <= Range * 0.01f)
                    if (Camp.IsEnemy(a.Camp))
                        result.Add(a);
        return result;
    }
    public Role GetNearestEnemy()
    {
        float saver = 114514;
        Role result = null;
        foreach (var a in GameManager.AllRole)
        {
            if (a.CanNotSelect)
                continue;
            var dis = WindChimeEngnie.Lib.GetDistance(gameObject, a.gameObject);
            if (dis <= saver && Camp.IsEnemy(a.Camp))
            {
                result = a;
                saver = dis;
            }
        }
        return result;
    }
    protected virtual void OnInit() { }
    protected virtual void OnUpdate() { }


    public virtual void UnderAttack(Damage damage,Role From)
    {
        if (Invincible > 0)
        {
            damage.FinalDamage = 0;
        }
        BeforeTakeDamage(damage, From);
        float reduce = 0;
        if (damage.Type == DamageType.Physical)
            reduce = Property.PhyDef * 0.01f / (1 + Property.PhyDef * 0.01f);
        if (damage.Type == DamageType.Magic)
            reduce = Property.MagDef * 0.01f / (1 + Property.MagDef * 0.01f);
        damage.FinalDamage -= reduce * damage.FinalDamage;
        From.BeforeFinalAttack(damage, this);
        hp -= damage.FinalDamage;
        if (damage.FinalDamage > 5f && damage.Data != "��˹��")
        {
            Effect.Create(GameManager.OtherEffect["HitSuccess"], transform.position + new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(0.2f, 0.8f)));
            GameManager.PlayAudio(GameManager.AC[4]);
        }
            //RedTime = 0.15f;
        AfterTakeDamage(damage, From);
    }
    public void Heal(float healing)
    {
        healing = BeforeHealing(healing);
        hp += healing;
        if (hp > Property.Maxhp)
            hp = Property.Maxhp;
    }
    public void HitBack(Vector2 v2)
    {
        Controller.hitbackPower += v2;
    }






    public void BeforeTakeDamage(Damage damage, Role target) { foreach (var a in passives) a.BeforeTakeDamage(damage, target); }
    public void AfterTakeDamage(Damage damage, Role target) { foreach (var a in passives) a.AfterTakeDamage(damage, target); }
    public void OnSucceedDamage(Damage damage, Role target) { foreach (var a in passives) a.OnSucceedDamage(damage, target); }
    public void BeforeDealDamage(Damage damage, Role target) { foreach (var a in passives) a.BeforeDealDamage(damage, target); }
    public void BeforeFinalAttack(Damage damage, Role target) { foreach (var a in passives) a.BeforeFinalAttack(damage, target); }
    public void OnConflictBuff(Buff buff) { foreach (var a in passives) a.OnConflictBuff(buff); }
    public void OnConflictPassive(Passive buff) { foreach (var a in passives) a.OnConflictPassive(buff); }
    public void OnShoot() { foreach (var a in passives) a.OnShoot(); }
    public void BeforeGetBuff(Buff buff) { foreach (var a in passives) a.BeforeGetBuff(buff); }
    public void AfterGetBuff(Buff buff) { foreach (var a in passives) a.AfterGetBuff(buff); }
    public void OnGiveBuff(Buff buff) { foreach (var a in passives) a.OnGiveBuff(buff); }
    public void BeforeUseSkill(Skill skill) { foreach (var a in passives) a.BeforeUseSkill(skill); }
    public void AfterUseSkill(Skill skill) { foreach (var a in passives) a.AfterUseSkill(skill); }
    public void OnSustainedTrigger() { foreach (var a in passives) a.OnSustainedTrigger(); }
    public float BeforeHealing(float heal) {  foreach (var a in passives) heal += a.BeforeHealing(heal); return heal; }
    public void SwitchWeapon(BulletData btd) { foreach (var a in passives) a.SwitchWeapon(btd); }









    public Queue<Skill> SkillQueue = new();
    [HideInInspector]
    public CharacterController Controller;
    [SerializeField]
    protected Role Target;
    [SerializeField]
    private float AttackCoolTime = 0;
    private SpriteRenderer spr;
    private float RedTime = 0;
    public List<Passive> passives = new();

    public int GetFaceTo => face;
    public Animator Animator;

    [InspectorShow("��ǰ����ֵ")]
    public float hp;
    [InspectorShow("��ǰ����")]
    public float sp;
    [InspectorShow("�˺�����")]
    public DamageType DamageType;
    private int face = 1;
    [InspectorShow("������������")]
    public float HitBackPower = 1;
    [InspectorShow("���䷶Χ")]
    [SerializeField]
    private float alertRange = 900;
    /// <summary>
    /// ���䷶Χ
    /// </summary>
    public float AlertRange => alertRange * 0.01f;
    [InspectorShow("Ͷ��������")]
    public GameObject MissileObj;
    private float TimeCount = 0;
    [InspectorShow("�Ƿ��ܹ���")]
    public bool CanAttack = true;
    public Property Property = new();
    public CampClass Camp = new();
    [InspectorShow("��ɫͷ���߶�")]
    public float HeadHight = 1;
    [InspectorShow("���䵯ҩ�����λ��")]
    public Vector2 ShotPlace = new(0, 1);
    [InspectorShow("��ɫ����")]
    public float CharacterFigure = 0.5f;
    [InspectorShow("����Ч���ǲ���Ͷ����")]
    public bool IsT = false;
    [InspectorShow("Ͷ�����ٶ�")]
    public float MissileSpeed = 900;
    [InspectorShow("��ɫ�ŵ�λ��")]
    public Vector2 FootPosition = new(0,0);
    [InspectorShow("��ɫ�ؿڵ�λ��")]
    public Vector2 ChestPosition = new(0f, 0.5f);








    public static readonly string NormalState = "None";
}
