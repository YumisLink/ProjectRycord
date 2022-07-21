using System;
using UnityEngine;
[Serializable]
public class Property
{
    /// <summary>
    /// �������ֵ
    /// </summary>
    [InspectorShow("�������ֵ")]
    public float AtkSpeed = 1;
    public float AtkBonus = 1;

    public int Maxhp = 600;
    /// <summary>
    /// �����ָ��ٶ�
    /// </summary>
    [InspectorShow("�����ָ��ٶ�")]
    public float hps = 0;
    /// <summary>
    /// ��������
    /// </summary>
    [InspectorShow("��������")]
    public float sp = 0;
    /// <summary>
    /// ������Ȼ�ָ�
    /// </summary>
    [InspectorShow("������Ȼ�ָ�")]
    public float sps = 0;
    /// <summary>
    /// ��������
    /// </summary>
    [InspectorShow("������")]
    public float Atk = 0;
    /// <summary>
    /// ħ��������
    /// </summary>
    [InspectorShow("�������")]
    public float PhyDef = 0;
    /// <summary>
    /// ħ������
    /// </summary>
    [InspectorShow("ħ������")]
    public float MagDef = 0;
    /// <summary>
    /// ��������
    /// </summary>
    [InspectorShow("��������")]
    public float Crit = 1.5f;
    /// <summary>
    /// ��������
    /// </summary>
    public float Critical => critical * 0.01f;
    [SerializeField]
    [InspectorShow("��������")]
    public float critical = 1;
    /// <summary>
    /// ��ȴ����
    /// </summary>
    [InspectorShow("��ȴ����")]
    public float CDReduce = 1;
    /// <summary>
    /// �ƶ��ٶ�
    /// </summary>
    [InspectorShow("�ƶ��ٶ�")]
    public float MoveSpeed = 2;
    /// <summary>
    /// ӽ���ٶ�
    /// </summary>
    [InspectorShow("ӽ���ٶ�")]
    public float CastSpeed = 1;
    /// <summary>
    /// �������
    /// </summary>
    [InspectorShow("�������")]
    public float AtkInterval = 1;
    [InspectorShow("��������")]
    [SerializeField]
    private float range = 128;
    /// <summary>
    /// ��������
    /// </summary>
    public float Range => range * 0.012f;

}
