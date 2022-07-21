using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
[Serializable]
public struct GameObjectMap
{
    public string Key;
    public GameObject Effect;
}
[Serializable]
public struct SpriteMap
{
    public string Key;
    public Sprite spr;
}
public class GameManager : MonoBehaviour {
    /// <summary>
    /// 游戏是否暂停
    /// </summary>
    public static bool IsStop;
    public static int Money;
    public static List<AudioClip> AC =>instance.ac;
    public List<AudioClip> ac;
    /// <summary>
    /// 主角
    /// </summary>
    public static Role CenterRole;
    public static Base MainBase;
    /// <summary>
    /// 所有干员的名字
    /// </summary>
    public static List<string> OperatorNames = new();
    /// <summary>
    /// 干员升级效果
    /// </summary>
    public static Dictionary<string, RankUpData> OperatorRankUp = new();
    /// <summary>
    /// 配置中心
    /// </summary>
    public static GameConfig Config => instance.config;
    /// <summary>
    /// 干员的基础数据
    /// </summary>
    public static Dictionary<string, ConfigOperator> OperatorData = new();
    public static Dictionary<string, SkillMap> SkillData = new();
    public static List<Role> AllRole => instance.allRole;
    [SerializeField]
    private List<Role> allRole = new();
    public static GameManager instance;
    /// <summary>
    /// 投掷物
    /// </summary>
    public static Dictionary<string, GameObject> Missile = new();
    /// <summary>
    /// 特效
    /// </summary>
    public static Dictionary<string, GameObject> Effects = new();
    /// <summary>
    /// 其他特效
    /// </summary>
    public static Dictionary<string, GameObject> OtherEffect = new();
    /// <summary>
    /// 在场的所有角色
    /// </summary>
    public static Dictionary<string,GameObject> Roles = new();
    /// <summary>
    /// 干员头像
    /// </summary>
    public static Dictionary <string, Sprite> HeadImage = new();
    [SerializeField]
    private List<SpriteMap> headImage = new();
    [SerializeField]
    private List<GameObjectMap> missile = new();
    [SerializeField]
    private List<GameObjectMap> effects = new();
    [SerializeField]
    private List<GameObjectMap> roles = new();
    [SerializeField]
    private List<GameObjectMap> otherEffect = new();
    [SerializeField]
    private GameConfig config = new();
    public GameData gameData = new();
    public static ItemPool ItemPool => instance.ip;
    public ItemPool ip = new();
    public List<GameObject> gos;
    public static List<GameObject> Gos => instance.gos;
    public static GameData GameData => instance.gameData;
    // Start is called before the first frame update
    void Awake()
    {
        ip.Init();
        if (instance == null)
        {
            Debug.Log("GameManager" + gameObject);
            instance = this;
            foreach (var map in effects)
                if (!Effects.ContainsKey(map.Key))
                    Effects.Add(map.Key, map.Effect);
            foreach (var map in otherEffect)
                if (!OtherEffect.ContainsKey(map.Key))
                    OtherEffect.Add(map.Key, map.Effect);
            foreach (var map in missile)
                if (!Missile.ContainsKey(map.Key))
                    Missile.Add(map.Key, map.Effect);
            foreach (var role in roles)
            {
                if (!Roles.ContainsKey(role.Key))
                    Roles.Add(role.Key, role.Effect);
            }
            foreach (var role in headImage)
            {
                if (!HeadImage.ContainsKey(role.Key))
                    HeadImage.Add(role.Key, role.spr);
            }
        }
    }
    public static List<Vector2> MonsterSpawn = new();
    float cntt = 0;
    // Update is called once per frame


    void Update()
    {
        cntt += Time.deltaTime;
        if (cntt > 0.5f)
        {
            cntt = 0;
            //NormalMosterAI.Create(GameManager.Roles["WaterSlime"],new Vector2(UnityEngine.Random.Range(-5,5), UnityEngine.Random.Range(-5, 5)));
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public static void AddEntity(Role Entity)
    {
        instance.allRole.Add(Entity);
    }
    public static void RemoveEntity(Role Entity)
    {
        instance.allRole.Remove(Entity);
    }

    [Serializable]
    public class ConfigOperator
    {
        public string Name;
        public string Cname;
        public float Attack;
        public float AtkInterval;
        public float Range;
        public float Multi;
        public string Missile;
        public string DamageType;
        public string OperatorClass;
        public string OperatorClassBranches;
        public int Random;
        public string AttackMode;
        public string AttackedEffect;
        public float ThrowSpeed;
        public List<string> Levels = new();
    }
    [Serializable]
    public class RankUpData
    {
        public string Name;
        public string CName;
        public float AttackAdd;
        public float AttackMut;
        public float IntervalReduce;
        public float RangeAdd;
        public float RangeMut;
        public float Multi;
        public float AttackSpeed;
        public float Random;
        public string Class;
        public string Detail;
        public string AttackType;
        public string Missile;
        public string AttackedEffect;
    }
    [Serializable]
    public class SkillMap
    {
        public string SkillName;
        public string SkillDetail;
        public float ReleaseTime;
        public float CoolDown;
        public string SkillType;
        public string SkillState;
        public string Chinese;
        public float[] Data;
    }
    public class OperatorsList
    {
        public List<ConfigOperator> Operators = new();
    }
    public class SkillList
    {
        public List<SkillMap> SkillMap = new();
    }
    public class RankUpDataList
    {
        public List<RankUpData> rankUpData = new();
    }


    public static GameObject Throw(GameObject go)
    {
        var g = Instantiate(go);
        var gg = g.AddComponent<ThrowObject>();
        gg.speed = WindChimeEngnie.Lib.FromAngleGetVector(UnityEngine.Random.Range(45, 135) * 10);
        return g;
    }
    public static void PlayAudio(AudioClip ac)
    {
        var g = Instantiate(GameManager.OtherEffect["Music"]);
        var k = g.GetComponent<AudioSource>();
        k.clip = ac;
        k.Play();
        g.GetComponent<Effect>().IsDEAD = k.clip.length + 10;
    }
}

