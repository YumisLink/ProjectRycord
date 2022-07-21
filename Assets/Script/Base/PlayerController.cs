using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    Tumbling tumb;
    [SerializeField]
    private Vector2 direction = new Vector2();
    Text coin;
    public Vector2 GetDirection => direction;
    private void FixedUpdate()
    {
        coin.text = GameManager.Money.ToString();
    }
    void Start()
    {
        tumb = gameObject.AddComponent<Tumbling>();
        GameManager.CenterRole = GetComponent<Role>();
        coin = UIManager.GetText("Coin");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            var pls = GetComponent<PlayerShoot>();
            foreach(var a in pls.bulletDatas)
            {
                a.×Óµ¯ÉËº¦ += 100000;
            }
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            GameManager.MainBase.FireHp = 100;
            GameManager.MainBase.IceHp = 100;
            GameManager.MainBase.ThunderHp = 100;
            GameManager.MainBase.WaterHp = 100;
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            GameManager.MainBase.FireHp = 0;
            GameManager.MainBase.IceHp = 0;
            GameManager.MainBase.ThunderHp = 0;
            GameManager.MainBase.WaterHp = 0;
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            UIManager.GetBelssing();
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            GameManager.Money = 114514;
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            UIManager.instance.CloseWindow();
        }
        if (Input.GetKey(KeyCode.Space))
            tumb.WantSkill();
        if (characterController == null)
            characterController = GetComponent<CharacterController>();
        if (GameManager.Config.CharacterControllerMode == CharacterControllerMode.LOL)
        {
            if (Input.GetMouseButtonDown(1))
            {
                var target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Effect.Create(GameManager.OtherEffect["ClickEffect"], target);
                characterController.MoveToTarget(target);
            }
        }
        if (GameManager.Config.CharacterControllerMode == CharacterControllerMode.GunAndDungen)
        {
            direction = Vector2.zero;
            if (Input.GetKey(KeyCode.A))
                direction.x -= 1;
            if (Input.GetKey(KeyCode.W))
                direction.y += 1;
            if (Input.GetKey(KeyCode.D))
                direction.x += 1;
            if (Input.GetKey(KeyCode.S))
                direction.y -= 1;
            if (characterController)
            characterController.MoveDirection(direction);
        }
    }
    public void Select(int at)
    {
        at--;

    }
}
