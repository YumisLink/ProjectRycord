using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static Dictionary<string, Sprite> Sprite = new Dictionary<string, Sprite>();
    public List<Sprite> spriteMaps = new List<Sprite>();
    public static UIManager instance;
    public Cinemachine.CinemachineVirtualCamera virtualCamera;
    public static Camera Camera => instance.cameras;
    public static Canvas Canvas => instance.canvas;
    public Camera cameras;
    public Canvas canvas;
    public static Dictionary<string, GameObject> UI = new();
    public List<GameObjectMap> ui = new();
    public GameObject ClickB;
    public GameObject ClickC;
    private void Update()
    {
        if (Input.GetKey(KeyCode.B))
            ClickB.SetActive(true);
        else
            ClickB.SetActive(false);
        if (Input.GetKey(KeyCode.C))
            ClickC.SetActive(true);
        else
            ClickC.SetActive(false);
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            foreach (GameObjectMap map in ui)
                if (!UI.ContainsKey(map.Key))
                    UI.Add(map.Key, map.Effect);
            foreach (var sp in spriteMaps)
                if (!Sprite.ContainsKey(sp.name))
                {
                    Sprite.Add(sp.name, sp);
                }
        }
        foreach (var a in GetComponentsInChildren<Image>())
        {
            imgs.Add(a);
        }
        foreach (var a in GetComponentsInChildren<Text>())
        {
            txt.Add(a);
        }
        CloseBelssing();
    }
    public static List<Image> UIImages => instance.imgs;
    public List<Image> imgs;
    public static List<Text> UIText => instance.txt;
    public List<Text> txt;
    public static Image GetImage(string name)
    {
        foreach (var a in UIImages)
        {
            if (a.name == name)
                return a;
        }
        return null;
    }
    public static Text GetText(string name)
    {
        foreach (var a in UIText)
        {
            if (a.name == name)
                return a;
        }
        return null;
    }

    [InspectorShow("岆瘁珆尨夼漲")]
    public bool IsDamageShow = false;
    public static void CreateDamageShow(Damage dam, Vector2 position, float mut,Color color)
    {
        if (!instance.IsDamageShow)
            return;
        if (dam.FinalDamage < 0.5f)
            return;
        var go2 = Instantiate(UI["DamageText"], UIManager.Canvas.transform);
        var go = Instantiate(UI["DamageText"], UIManager.Canvas.transform);
        var text = go.GetComponent<TextMeshProUGUI>();
        text.text = Convert.ToInt32(dam.FinalDamage).ToString();
        text.color = color;
        Fonts font = go.GetComponent<Fonts>();
        if (mut >= 2)
        {
            font.IsCritcal = true;
            text.text += "!";
        }
        font.v2d = position + new Vector2(UnityEngine.Random.Range(-0.2f, 0.2f), 1.5f + UnityEngine.Random.Range(-0.2f, 0.2f));

        text = go2.GetComponent<TextMeshProUGUI>();
        text.text = Convert.ToInt32(dam.FinalDamage).ToString();
        text.color = Color.black;
        Fonts font2 = go2.GetComponent<Fonts>();
        if (mut >= 2)
        {
            font2.IsCritcal = true;
            text.text += "!";
        }
        font2.v2d = font.v2d + GameManager.Config.FontShadowMoving;
    }
    public static bool openBelssing = false;

    public static void GetBelssing()
    {
        if (openBelssing)
            return;
        instance.BlessingShower.SetActive(true);
        openBelssing = true;
        instance.str1 = GameManager.ItemPool.蛅腦();
        instance.str2 = GameManager.ItemPool.蛅腦();
        instance.str3 = GameManager.ItemPool.蛅腦();
        instance.seleces[0].sprite = UIManager.Sprite["蛅腦" + instance.str1];
        instance.seleces[1].sprite = UIManager.Sprite["蛅腦" + instance.str2];
        instance.seleces[2].sprite = UIManager.Sprite["蛅腦" + instance.str3];
    }
    public static void CloseBelssing()
    {
        openBelssing = false;
        instance.BlessingShower.SetActive(false);
    }
    public GameObject BlessingShower;
    string str1;
    string str2;
    string str3;
    public List<Image> seleces = new();
    public void select1()
    {
        GameManager.CenterRole.gameObject.AddComponent(Type.GetType("蛅腦" + str1));
        CloseBelssing();
        GameManager.ItemPool.ssrPool.Add(str2);
        GameManager.ItemPool.ssrPool.Add(str3);
    }
    public void select2()
    {
        GameManager.CenterRole.gameObject.AddComponent(Type.GetType("蛅腦" + str2));
        CloseBelssing();
        GameManager.ItemPool.ssrPool.Add(str1);
        GameManager.ItemPool.ssrPool.Add(str3);
    }
    public void select3()
    {
        GameManager.CenterRole.gameObject.AddComponent(Type.GetType("蛅腦" + str3));
        CloseBelssing();
        GameManager.ItemPool.ssrPool.Add(str2);
        GameManager.ItemPool.ssrPool.Add(str1);
    }
    public void CloseWindow()
    {
        GetImage("Window").gameObject.GetComponent<close>().st = true ;
    }
    public Image GetBossHp()
    {
        BossHp.gameObject.SetActive(true);
        return BossH2p;
    }
    public Image BossHp;
    public Image BossH2p;
}
