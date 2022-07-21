using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum TextType
{
    Damage, name
}

public class Fonts : MonoBehaviour
{
    [SerializeField]
    private float InitScale = 1;

    [SerializeField]
    private float SmallSpeed = 1;

    [SerializeField]
    private float MoveUpSpeed = 3;

    [SerializeField]
    private float disappearTime = 0.5f;

    [SerializeField]
    private float appendTime = 0.5f;

    [SerializeField]
    private float AllTime = 3f;

    [SerializeField]
    private float WaitTime = 0.5f;

    [SerializeField]
    private float CritInitScale = 1;

    [SerializeField]
    private float CritSmallSpeed = 1;

    [SerializeField]
    private float CritMoveUpSpeed = 3;

    [SerializeField]
    private float CritdisappearTime = 0.5f;

    [SerializeField]
    private float CritappendTime = 0.5f;

    [SerializeField]
    private float CritAllTime = 3f;

    [SerializeField]
    private float CritWaitTime = 0.5f;






    /// <summary>
    /// Canvas所在的Camera
    /// </summary>
    private Camera Camera;

    /// <summary>
    /// 跟随的目标
    /// </summary>
    public Transform Target;

    /// <summary>
    /// 世界空间中，UI位置的偏移量
    /// </summary>
    private Vector2 Offset;

    /// <summary>
    /// Canvas的Render Mode是<see cref="RenderMode.WorldSpace"/>该值可以为空
    /// </summary>
    private RectTransform Root;

    /// <summary>
    /// 字体的类型
    /// </summary>
    private TextType Type;

    public Vector2 v2d;

    private TextMeshProUGUI text;

    private float TimeCount;
    public bool IsCritcal = false;

    private void Start()
    {
        if (IsCritcal)
        {
            InitScale = CritInitScale;
            SmallSpeed = CritSmallSpeed;
            MoveUpSpeed = CritMoveUpSpeed;
            disappearTime = CritdisappearTime;
            appendTime = CritappendTime;
            AllTime = CritAllTime;
            WaitTime = CritWaitTime;
        }
        Camera = UIManager.Camera;
        Root = UIManager.Canvas.transform as RectTransform;
        text = GetComponent<TextMeshProUGUI>();
        TimeCount = 0;
        WindChimeEngnie.Lib.SetMultScale(gameObject, InitScale, InitScale);
        var color = text.color;
        color.a = 0;
        text.color = color;
    }
    private Vector2 Salve(Vector2 v2d)
    {
        Vector2 ret;
        var sp = RectTransformUtility.WorldToScreenPoint(Camera, v2d);
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(Root, sp, null, out ret))
            ret = new Vector2(-10000, -10000);
        return ret;
    }
    private void Update()
    {
        TimeCount += Time.deltaTime;
        if (Type == TextType.Damage)
        {
            if (TimeCount <= appendTime)
            {
                var color = text.color;
                color.a += Time.deltaTime / appendTime;
                if (color.a >= 1) color.a = 1;
                text.color = color;
                var scale = transform.localScale;
                scale.y -= SmallSpeed * Time.deltaTime;
                scale.x -= SmallSpeed * Time.deltaTime;
                transform.localScale = scale;
            }
            else if (TimeCount >= WaitTime)
            {
                Offset.y += MoveUpSpeed * Time.deltaTime;
            }
            if (TimeCount >= AllTime - disappearTime)
            {
                var color = text.color;
                color.a -= Time.deltaTime / disappearTime;
                text.color = color;
            }
            if (TimeCount >= AllTime)
            {
                Destroy(gameObject);
                Destroy(this);
            }
        }
    }
    private void LateUpdate()
    {
        RectTransform t = transform as RectTransform;
        if (Target)
            v2d = Target.position;
        t.anchoredPosition = Salve(v2d + Offset);
    }
}