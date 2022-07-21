using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool 
{
    public List<string> rPool = new List<string>();
    public List<string> ssrPool = new List<string>();
    public void Init()
    {
        rPool.Clear();
        ssrPool.Clear();
        for (int i = 1; i <= 3; i++)
        {
            rPool.Add("����ӵ�");
            rPool.Add("��������");
            rPool.Add("��������");
            rPool.Add("ɱ������");
            rPool.Add("��������");
            rPool.Add("��֮���");
            rPool.Add("��������");
            rPool.Add("�̱����");
            rPool.Add("����ս");
            rPool.Add("�������");
            rPool.Add("��װսʿ");
            rPool.Add("���𷴻�");
            rPool.Add("�ӵ�����");
            rPool.Add("��Եһǹ");
            rPool.Add("����Ѿ�");
            rPool.Add("��ȷ�Ƶ�");
            rPool.Add("����һ��");
            rPool.Add("�������");
            rPool.Add("������Ϣ");
            rPool.Add("Ԫ��Ԥ֪");
            rPool.Add("Ԫ�ش�ʦ");
            rPool.Add("ҽѧ�漣");
        }
        ssrPool.Add("�������");
        ssrPool.Add("�����ײ");
        ssrPool.Add("����");
        ssrPool.Add("��ϼ");
        ssrPool.Add("̮��");
        ssrPool.Add("����ڰ�");
        ssrPool.Add("����");
        ssrPool.Add("����");
        ssrPool.Add("����ѩ");
        ssrPool.Add("����");
        ssrPool.Add("��������");
        ssrPool.Add("������ʦ");
        ssrPool.Add("�������");
        ssrPool.Add("����֮��");
        ssrPool.Add("�޾�����");
    }
    public string ����()
    {
        var u = UnityEngine.Random.Range(0, rPool.Count);
        string ret = rPool[u];
        rPool.RemoveAt(u);
        return ret;
    }
    public string ף��()
    {
        var u = UnityEngine.Random.Range(0, ssrPool.Count);
        string ret = ssrPool[u];
        ssrPool.RemoveAt(u);
        return ret;
    }
}
