using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    /// <summary>
    /// ��������
    /// </summary>
    [InspectorShow("������")]
    public int TapsCount = 1;
    /// <summary>
    /// ɢ��Ƕ�
    /// </summary>
    [InspectorShow("ɢ���")]
    public float ScatterRate = 1;
    /// <summary>
    /// �ӵ�����
    /// </summary>
    [InspectorShow("�ӵ�����")]
    public int ShootCnt = 1;
    /// <summary>
    /// ����
    /// </summary>
    [InspectorShow("����")]
    public int MaxClips = 600;
    /// <summary>
    /// ����ʱ��
    /// </summary>
    [InspectorShow("�����ٶ�")]
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
    public int �´λ���;
    public List<int> nx = new();
    public int now = 1;
    public int fs = 2;
    public int fsClip = 0;
    public void RecoverClip(int clip)
    {
        Clip += clip;
        if (Clip > BulletShoot.��������)
            Clip = BulletShoot.��������;
    }
    private void Start()
    {
        role = GetComponent<Role>();
        //BulletShoot = GameManager.Missile["SnowBall"];
        BulletShoot = bulletDatas[4]; 
        now = 4;
        fs = 1;
        fsClip = bulletDatas[fs].��������;
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
        spr.sprite = BulletShoot.ǹ����ͼ;
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
            Clip = BulletShoot.��������;
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
                    ar.clip = BulletShoot.�����Ч;
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
                ar.clip = BulletShoot.�����Ч;
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
            if (BulletShoot.�Ƿ񼤹�����)
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
                    Damage.DealDamage(role, rls, new Damage(BulletShoot.�ӵ��˺� * role.Property.AtkBonus));
                    if (UnityEngine.Random.Range(0f, 1f) < BulletShoot.Ԫ���쳣����)
                        Buff.GiveBuff(Type.GetType(BulletShoot.Ԫ���쳣), role, rls, Duration: 6);
                    Clip -= 1;
                    List<Role> rol = new();
                    for (int i = 1; i < BulletShoot.ǹ������; i++)
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
                            Damage.DealDamage(role, rls, new Damage(BulletShoot.�ӵ��˺� * role.Property.AtkBonus));
                            if (UnityEngine.Random.Range(0f, 1f) < BulletShoot.Ԫ���쳣����)
                                Buff.GiveBuff(Type.GetType(BulletShoot.Ԫ���쳣),role,rls,Duration:6);
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
                float Scatter = angle - ((BulletShoot.ǹ������ - 1) * BulletShoot.ɢ��̶� * 0.5f);
                Clip -= 1;
                GameManager.PlayAudio(BulletShoot.�����Ч);
                for (int i = 0; i < BulletShoot.ǹ������; i++)
                {
                    var mis = Bullet.Create(role, Scatter + UnityEngine.Random.Range(-BulletShoot.����׼�̶�, BulletShoot.����׼�̶�) +
                        BulletShoot.ɢ��̶� * i, new Damage(BulletShoot.�ӵ��˺� * role.Property.AtkBonus) { Data=BulletShoot.���� }, BulletShoot.Ԥ����, speed: BulletShoot.�ӵ��ٶ�, liver: BulletShoot.����ʱ��,
                        cross: BulletShoot.��͸);
                    if (BulletShoot.����== "�ڶ�")
                    {
                        var t = mis.transform.localScale;
                        t.x += BulletShoot.Ԫ���쳣����;
                        t.y += BulletShoot.Ԫ���쳣����;
                        mis.transform.localScale = t;
                    }
                    if (UnityEngine.Random.Range(0f, 1f) < BulletShoot.Ԫ���쳣����)
                        if (BulletShoot.Ԫ���쳣.Length > 1)
                            mis.Element = Type.GetType(BulletShoot.Ԫ���쳣);
                    WindChimeEngnie.Lib.MoveTo(mis.gameObject, ShootAt.transform.position);
                    Scatter += ScatterRate;
                }
            }
            Interval = BulletShoot.�������;
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
        spr.sprite = BulletShoot.ǹ����ͼ;
        Clip = 0;
        role.SwitchWeapon(BulletShoot);
        if (Reloading <= -10000)
            Reloading = ReloadTime;
        ResetImage();
    }
    void ResetImage()
    {
        MainWeapon.sprite = BulletShoot.ǹ����ͼ;
        SwiWeapon.sprite = bulletDatas[fs].ǹ����ͼ;
        NextWeapon.sprite = bulletDatas[nx[0]].ǹ����ͼ;
        NextWeapon_1.sprite = bulletDatas[nx[1]].ǹ����ͼ;
        NextWeapon_2.sprite = bulletDatas[nx[2]].ǹ����ͼ;
        MainBullet.color = BulletShoot.��ɫ;
        SwiBullet.color = bulletDatas[fs].��ɫ;
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
    public string ���� = "ѩ��";
    public float ������� = 0.2f;
    public int �������� = 25;
    public float �ӵ��ٶ� = 15;
    public float �ӵ��˺� = 1;
    public int ǹ������ = 1;
    public float ɢ��̶� = 0;
    public float ����׼�̶� = 0;
    public int ��͸ = 1;
    public float ����ʱ�� = 1.5f;
    public Sprite ǹ����ͼ;
    public GameObject Ԥ����;
    public bool �Ƿ񼤹�����;
    public string Ԫ���쳣;
    public float Ԫ���쳣���� = 0.75f;
    public Color ��ɫ;
    public AudioClip �����Ч;
}
