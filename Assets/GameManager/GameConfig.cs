using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class GameConfig 
{
    /// <summary>
    /// �����˺���ɫ
    /// </summary>
    [InspectorShow("�����˺���ɫ")]
    public Color PhyDamageColor;
    /// <summary>
    /// ħ���˺���ɫ
    /// </summary>
    [InspectorShow("ħ���˺���ɫ")]
    public Color MagDamageColor;
    /// <summary>
    /// �������˺���ɫ
    /// </summary>
    [InspectorShow("�������˺���ɫ")]
    public Color PhyCritDamageColor;
    /// <summary>
    /// ħ�������˺���ɫ
    /// </summary>
    [InspectorShow("ħ�������˺���ɫ")]
    public Color MagCritDamageColor;
    /// <summary>
    /// ��ʵ�˺���ɫ
    /// </summary>
    [InspectorShow("��ʵ�˺���ɫ")]
    public Color TrueDamageColor;
    /// <summary>
    /// ��ʵ�����˺���ɫ
    /// </summary>
    [InspectorShow("��ʵ�����˺���ɫ")]
    public Color TrueCritDamageColor;
    /// <summary>
    /// �˺�������Ӱ����
    /// </summary>
    [InspectorShow("�˺�������Ӱ����")]
    public Vector2 FontShadowMoving;
    /// <summary>
    /// ��ɫ���Ʒ�ʽ
    /// </summary>
    [InspectorShow("��ɫ���Ʒ�ʽ")]
    public CharacterControllerMode CharacterControllerMode;
    /// <summary>
    /// ��Ӣ������Ȩֵ
    /// </summary>
    [InspectorShow("��Ӣ������Ȩֵ")]
    public int EliteSpanProbility = 5;
    /// <summary>
    /// ����¼�����Ȩֵ
    /// </summary>
    [InspectorShow("����¼�����Ȩֵ")]
    public int ShopSpanProbility = 15;
    /// <summary>
    /// С������Ȩֵ
    /// </summary>
    [InspectorShow("С������Ȩֵ")]
    public int MonseterSpanProbility = 15;
}
public enum CharacterControllerMode
{
    LOL,GunAndDungen
}