using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Base : NormalMosterAI
{
    public Tower Ice;
    public Tower Fire;
    public Tower Water;
    public Tower Thunder;
    public float IceHp;
    public float FireHp;
    public float WaterHp;
    public float ThunderHp;
    public float IceHpR = 0;
    public float FireHpR = 0;
    public float WaterHpR = 0;
    public float ThunderHpR = 0;
    protected override void OnInit()
    {
        IceHp = 50;
        FireHp = 50;
        WaterHp = 50;
        ThunderHp = 50;
        Property.MoveSpeed = 0;
        GameManager.MainBase = this;
        fire = UIManager.GetImage("Fire");
        water = UIManager.GetImage("Water");
        thunder = UIManager.GetImage("Thunder");
        ice = UIManager.GetImage("Ice");
        f1 = UIManager.GetImage("f1");
        f2 = UIManager.GetImage("f2");
        f3 = UIManager.GetImage("f3");
        f4 = UIManager.GetImage("f4");
    }
    public override void UnderAttack(Damage damage, Role From)
    {
        damage.FinalDamage = 0 ;
        if (WindChimeEngnie.Lib.GetDistance(From.gameObject,gameObject) < 5)
        {
            if (From.TryGetComponent<NormalMosterAI>(out var ai))
            {
                if (ai.Contain == Element.Water)
                {
                    WaterHp -= 1;
                    var g = GameManager.Throw(GameManager.OtherEffect["Cube"]);
                    g.GetComponent<SpriteRenderer>().sprite = GameManager.HeadImage["Water"];
                }
                if (ai.Contain == Element.Fire)
                {
                    FireHp -= 1;

                    var g = GameManager.Throw(GameManager.OtherEffect["Cube"]);
                    g.GetComponent<SpriteRenderer>().sprite = GameManager.HeadImage["Fire"];
                }
                if (ai.Contain == Element.Thunder)
                {
                    ThunderHp -= 1;
                    var g = GameManager.Throw(GameManager.OtherEffect["Cube"]);
                    g.GetComponent<SpriteRenderer>().sprite = GameManager.HeadImage["Thunder"];

                }
                if (ai.Contain == Element.Ice)
                {
                    IceHp -= 1;
                    var g = GameManager.Throw(GameManager.OtherEffect["Cube"]);
                    g.GetComponent<SpriteRenderer>().sprite = GameManager.HeadImage["Ice"];

                }
            }
        }
    }
    Image fire;
    Image water;
    Image thunder;
    Image ice;
    Image f1;
    Image f2;
    Image f3;
    Image f4;
    public Vector3 vv2 = new Vector3(5.34f,0.15f);
    protected override void OnFixedUpdate()
    {
        RectTransform t = f1.transform as RectTransform;
        t.anchoredPosition = Salve(Fire.transform.position + vv2);
        t = f2.transform as RectTransform;
        t.anchoredPosition = Salve(Ice.transform.position + vv2);
        t = f3.transform as RectTransform;
        t.anchoredPosition = Salve(Thunder.transform.position + vv2);
        t = f4.transform as RectTransform;
        t.anchoredPosition = Salve(Water.transform.position + vv2);
        fire.fillAmount = FireHp / 100;
        water.fillAmount = WaterHp / 100;
        thunder.fillAmount = ThunderHp/ 100;
        ice.fillAmount = IceHp / 100;
        IceHp += IceHpR * Time.fixedDeltaTime;
        ThunderHp += ThunderHpR * Time.fixedDeltaTime;
        WaterHp += WaterHpR * Time.fixedDeltaTime;
        FireHp += FireHpR * Time.fixedDeltaTime;
        if (IceHp > 100) IceHp = 100;
        if (ThunderHp > 100) ThunderHp = 100;
        if (WaterHp > 100) WaterHp = 100;
        if (FireHp > 100) FireHp = 100;
        if (IceHp < 0) IceHp = 0;
        if (ThunderHp < 0) ThunderHp = 0;
        if (WaterHp < 0) WaterHp = 0;
        if (FireHp < 0) FireHp = 0;
    }
    protected override void OnUpdate()
    {
        Debug.Log("ggod");
        if (FireHp <= 0 || WaterHp <= 0 || ThunderHp <= 0 || IceHp <= 0 && !isd)
            StartCoroutine("dd");
    }
    bool isd = false;
    IEnumerator dd()
    {
        Debug.Log("d");
        isd = true;
        UIManager.instance.virtualCamera.Follow = this.transform;
        var k = AtkEffect.Create(GameManager.Effects["ThundExp"], transform.position,this,new Damage(0));
        k.transform.localScale = new Vector3(5, 5);
        yield return new WaitForSeconds(2);
        UIManager.instance.CloseWindow();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Dead");
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
