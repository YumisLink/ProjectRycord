using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 掉落的物品 : NPC
{
    public string PassiveName;
    public SpriteRenderer spr;
    private void Start()
    {
        PassiveName = GameManager.ItemPool.遗物();
        spr = GetComponent<SpriteRenderer>();
        sr.sprite = UIManager.Sprite["遗物" + PassiveName];
    }
    public override void OnTouch(Role py)
    {
        Debug.Log("Touch");
        base.OnTouch(py);
        py.gameObject.AddComponent(Type.GetType("遗物" + PassiveName));
        Destroy(gameObject);
    }
}
