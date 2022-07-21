using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    /// <summary>
    /// 连射数量
    /// </summary>
    [InspectorShow("连射数")]
    public int TapsCount = 1;
    /// <summary>
    /// 散射角度
    /// </summary>
    [InspectorShow("散射度")]
    public float ScatterRate = 1;
    /// <summary>
    /// 子弹数量
    /// </summary>
    [InspectorShow("子弹数量")]
    public int ShootCnt = 1;
    /// <summary>
    /// 弹夹
    /// </summary>
    [InspectorShow("弹夹")]
    public int MaxClips = 600;
    /// <summary>
    /// 换弹时间
    /// </summary>
    [InspectorShow("换弹速度")]
    public float ReloadTime = 0.3f;
    public float LiveTime = 10;
    public BulletData BulletShoot;
    public List<BulletData> bulletDatas = new List<BulletData>();
    public bool Line = false;
    public List<LightingEffect> lets = new List<LightingEffect>();

    public GameObject Gun;
    public GameObject ShootAt;


    public Role role;
    public float reload = 0;
    private float Reloading = -10000;
    public float Interval = 0;
    public int Clip = 0;
    public Text clips;
    public int 下次换弹;
    public List<int> nx = new();
    public int now = 1;
    public int fs = 2;
    public int fsClip = 0;
    public void RecoverClip(int clip)
    {
        Clip += clip;
        if (Clip > BulletShoot.弹夹数量)
            Clip = BulletShoot.弹夹数量;
    }
    private void Start()
    {
        role = GetComponent<Role>();
        //BulletShoot = GameManager.Missile["SnowBall"];
        BulletShoot = bulletDatas[4]; 
        now = 4;
        fs = 1;
        fsClip = bulletDatas[fs].弹夹数量;
        Clip = 0;
        nx.Add(5);
        nx.Add(3);
        nx.Add(2);
        MainWeapon = UIManager.GetImage("MainWeapon");
        SwiWeapon = UIManager.GetImage("SwiWeapon");
        NextWeapon = UIManager.GetImage("NextWeapon");
        NextWeapon_1 = UIManager.GetImage("NextWeapon_1");
        NextWeapon_2 = UIManager.GetImage("NextWeapon_2");
        MainBullet = UIManager.GetImage("MainBullet");
        SwiBullet = UIManager.GetImage("SwiBullet");
        SwiBulletTXT = UIManager.GetText("SwiBullet");
        MainBulletTXT = UIManager.GetText("MainBullet");
        ResetImage();
    }
    Image MainWeapon;
    Image NextWeapon;
    Image SwiWeapon;
    Image NextWeapon_1;
    Image NextWeapon_2;
    Image SwiBullet;
    Image MainBullet;
    Text SwiBulletTXT;
    Text MainBulletTXT;
    public void SwitchWeapon()
    {
        var k = Clip;
        Clip = fsClip;
        fsClip = k;
        k = now;
        now = fs;
        fs = k;
        BulletShoot = bulletDatas[now];
        Interval = 0.2f;
        var spr = Gun.GetComponent<SpriteRenderer>();
        spr.sprite = BulletShoot.枪的贴图;
        ResetImage();
    }
    public bool canswtch = true;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
        if ((Input.GetKeyDown(KeyCode.Tab) || (Input.GetKeyDown(KeyCode.Q))) && canswtch)
        {
            SwitchWeapon();
        }
    }
    private void FixedUpdate()
    {
        MainBulletTXT.text = Convert.ToInt32(Math.Min(Clip, 9999)).ToString();
        SwiBulletTXT.text = Convert.ToInt32(Math.Min(fsClip, 9999)).ToString();
        var dire = WindChimeEngnie.Lib.GetAngle(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        Gun.transform.eulerAngles = new(0, 0, dire);
        if (dire > 90 || dire < -90)
        {
            Gun.transform.eulerAngles = new(0, 0, dire+180);
            role.SetFaceToLeft();
        }
        else
        {
            //Gun.transform.localScale = new Vector3(1, 1);
            role.SetFaceToRight();
        }
        if (Input.GetMouseButton(0))
        {
            Attack(WindChimeEngnie.Lib.GetAngle(Camera.main.ScreenToWorldPoint(Input.mousePosition) - ShootAt.transform.position));
        }
        Reloading -= Time.fixedDeltaTime;
        Interval -= Time.fixedDeltaTime * role.Property.AtkSpeed;
        if (Reloading <= 0 && Reloading > -10000)
        {
            Reloading = -10000;
            Clip = BulletShoot.弹夹数量;
        }
        if (Reloading > 0)
            clips.text = $"Reloading";
        else
            clips.text = "";
        RectTransform t = clips.transform as RectTransform;
        t.anchoredPosition = Salve(transform.position + new Vector3(0, 1));
    }
    void createLighting(GameObject from, Role to, int at)
    {
        if (lets.Count >= at)
        {
            if (lets[at - 1].IsDEAD > 0)
            {
                lets.Clear();
            }
            else if (lets[at - 1].Target == to.gameObject)
            {
                lets[at - 1].SetLiveTime(0.3f);
                if (from == ShootAt)
                {
                    var ar = lets[0].gameObject.GetComponent<AudioSource>();
                }
            }
            else
            {
                if (from == ShootAt)
                    Destroy(lets[0].GetComponent<AudioSource>());
                lets[at - 1].SetLiveTime(0);
                lets[at - 1] = LightingEffect.Create(role, to, GameManager.Effects["Thunder"],fm:from );
                if (from == ShootAt)
                {
                    var ar = lets[0].gameObject.AddComponent<AudioSource>();
                    ar.clip = BulletShoot.射击音效;
                    ar.loop = true;
                    ar.Play();
                }
            }
        }
        else
        {
            lets.Add(LightingEffect.Create(role, to, GameManager.Effects["Thunder"], fm: from));
            if (from == ShootAt)
            {
                var ar = lets[0].gameObject.AddComponent<AudioSource>();
                ar.clip = BulletShoot.射击音效;
                ar.loop = true;
                ar.Play();
            }
        }
    }
    void Attack(float angle)
    {
        if (Clip <= 0 && Reloading< -10000)
        {
            Reload();
            return;
        }
        if (Interval <= 0.03f && Clip > 0)
        {
            role.OnShoot();
            if (BulletShoot.是否激光武器)
            {
                var cnt = ShootCnt - 1;
                var list = role.SearchNearEnemy(LiveTime * 75f + 1000);
                float dis = 1000f;
                Role rls = null;
                for (int i = 0; i < list.Count; i++)
                {
                    if (Mathf.Abs(WindChimeEngnie.Lib.GetAngle(role.transform.position, list[i].transform.position) - angle) <= ScatterRate + 100)
                    {
                        float tdis = WindChimeEngnie.Lib.GetDistance(role.transform.position, list[i].transform.position);
                        if (tdis < dis)
                        {
                            dis = tdis;
                            rls = list[i];
                        }
                    }
                    else
                    {
                        list.RemoveAt(i);
                        i--;
                    }
                }
                if (rls != null)
                {
                    Debug.Log(rls);
                    list.Remove(rls);
                    createLighting(ShootAt, rls, 1);
                    Damage.DealDamage(role, rls, new Damage(BulletShoot.子弹伤害 * role.Property.AtkBonus));
                    if (UnityEngine.Random.Range(0f, 1f) < BulletShoot.元素异常概率)
                        Buff.GiveBuff(Type.GetType(BulletShoot.元素异常), role, rls, Duration: 6);
                    Clip -= 1;
                    List<Role> rol = new();
                    for (int i = 1; i < BulletShoot.枪管数量; i++)
                    {
                        var last = rls;
                        dis = 114514;
                        rls = null;
                        foreach (var r in list)
                        {
                            float tdis = WindChimeEngnie.Lib.GetDistance(last.transform.position, r.transform.position);
                            if (tdis < dis)
                            {
                                dis = tdis;
                                rls = r;
                            }
                        }
                        if (rls != null)
                        {
                            list.Remove(rls);
                            Damage.DealDamage(role, rls, new Damage(BulletShoot.子弹伤害 * role.Property.AtkBonus));
                            if (UnityEngine.Random.Range(0f, 1f) < BulletShoot.元素异常概率)
                                Buff.GiveBuff(Type.GetType(BulletShoot.元素异常),role,rls,Duration:6);
                            createLighting(last.gameObject, rls, i + 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                float Scatter = angle - ((BulletShoot.枪管数量 - 1) * BulletShoot.散射程度 * 0.5f);
                Clip -= 1;
                GameManager.PlayAudio(BulletShoot.射击音效);
                for (int i = 0; i < BulletShoot.枪管数量; i++)
                {
                    var mis = Bullet.Create(role, Scatter + UnityEngine.Random.Range(-BulletShoot.不精准程度, BulletShoot.不精准程度) +
                        BulletShoot.散射程度 * i, new Damage(BulletShoot.子弹伤害 * role.Property.AtkBonus) { Data=BulletShoot.名字 }, BulletShoot.预制体, speed: BulletShoot.子弹速度, liver: BulletShoot.飞行时间,
                        cross: BulletShoot.穿透);
                    if (BulletShoot.名字== "黑洞")
                    {
                        var t = mis.transform.localScale;
                        t.x += BulletShoot.元素异常概率;
                        t.y += BulletShoot.元素异常概率;
                        mis.transform.localScale = t;
                    }
                    if (UnityEngine.Random.Range(0f, 1f) < BulletShoot.元素异常概率)
                        if (BulletShoot.元素异常.Length > 1)
                            mis.Element = Type.GetType(BulletShoot.元素异常);
                    WindChimeEngnie.Lib.MoveTo(mis.gameObject, ShootAt.transform.position);
                    Scatter += ScatterRate;
                }
            }
            Interval = BulletShoot.攻击间隔;
        }
    }
    public void Reload()
    {
        var n = nx[0];
        BulletShoot = bulletDatas[nx[0]];
        nx.RemoveAt(0);
        nx.Add(now);
        now = n;
        var spr = Gun.GetComponent<SpriteRenderer>();
        spr.sprite = BulletShoot.枪的贴图;
        Clip = 0;
        role.SwitchWeapon(BulletShoot);
        if (Reloading <= -10000)
            Reloading = ReloadTime;
        ResetImage();
    }
    void ResetImage()
    {
        MainWeapon.sprite = BulletShoot.枪的贴图;
        SwiWeapon.sprite = bulletDatas[fs].枪的贴图;
        NextWeapon.sprite = bulletDatas[nx[0]].枪的贴图;
        NextWeapon_1.sprite = bulletDatas[nx[1]].枪的贴图;
        NextWeapon_2.sprite = bulletDatas[nx[2]].枪的贴图;
        MainBullet.color = BulletShoot.颜色;
        SwiBullet.color = bulletDatas[fs].颜色;
    }
    private Vector2 Salve(Vector2 v2d)
    {
        Vector2 ret;
        var sp = RectTransformUtility.WorldToScreenPoint(UIManager.Camera, v2d);
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(UIManager.Canvas.transform as RectTransform, sp, null, out ret))
            ret = new Vector2(-10000, -10000);
        return ret;
    }
}
[Serializable]
public class BulletData
{
    public string 名字 = "雪球";
    public float 攻击间隔 = 0.2f;
    public int 弹夹数量 = 25;
    public float 子弹速度 = 15;
    public float 子弹伤害 = 1;
    public int 枪管数量 = 1;
    public float 散射程度 = 0;
    public float 不精准程度 = 0;
    public int 穿透 = 1;
    public float 飞行时间 = 1.5f;
    public Sprite 枪的贴图;
    public GameObject 预制体;
    public bool 是否激光武器;
    public string 元素异常;
    public float 元素异常概率 = 0.75f;
    public Color 颜色;
    public AudioClip 射击音效;
}
