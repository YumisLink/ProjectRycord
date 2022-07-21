using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShopType
{
    YW,REC,Rdom,Hps
}
public class Shop : NPC
{
    public ShopType st;
    string getdaze;
    private void Start()
    {
        if (st == ShopType.YW)
        {
            getdaze = "遗物" + GameManager.ItemPool.遗物();
            sr.sprite = UIManager.Sprite[getdaze];
        }
        if (st == ShopType.Rdom)
            getdaze = "遗物" + GameManager.ItemPool.遗物();
    }
    public override void OnTouch(Role py)
    {
        if (st == ShopType.YW && GameManager.Money > 75)
        {
            py.gameObject.AddComponent(Type.GetType(getdaze));
            GameManager.Money -= 75;
            Destroy(gameObject);
        }
        if (st == ShopType.Rdom && GameManager.Money > 50)
        {
            py.gameObject.AddComponent(Type.GetType(getdaze));
            GameManager.Money -= 50;
            Destroy(gameObject);
        }
        if (st == ShopType.Hps && GameManager.Money > 20)
        {
            py.Heal(100);
            Destroy(gameObject);
            GameManager.Money -= 20;
        }
        if (st == ShopType.REC && GameManager.Money > 150)
        {
            GameManager.MainBase.FireHp += 20;
            GameManager.MainBase.ThunderHp += 20;
            GameManager.MainBase.WaterHp += 20;
            GameManager.MainBase.IceHp += 20;
            GameManager.Money -= 150;
            Destroy(gameObject);
        }
    }
}
