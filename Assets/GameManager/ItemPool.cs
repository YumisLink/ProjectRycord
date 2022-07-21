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
            rPool.Add("虚空子弹");
            rPool.Add("武器过载");
            rPool.Add("无情连击");
            rPool.Add("杀心骤起");
            rPool.Add("不进则退");
            rPool.Add("神之眷顾");
            rPool.Add("玻璃大炮");
            rPool.Add("短兵相接");
            rPool.Add("身经百战");
            rPool.Add("厚积薄发");
            rPool.Add("重装战士");
            rPool.Add("复仇反击");
            rPool.Add("子弹艺术");
            rPool.Add("随缘一枪");
            rPool.Add("渐入佳境");
            rPool.Add("精确制导");
            rPool.Add("致命一击");
            rPool.Add("疾速射击");
            rPool.Add("生生不息");
            rPool.Add("元素预知");
            rPool.Add("元素大师");
            rPool.Add("医学奇迹");
        }
        ssrPool.Add("网瘾少年");
        ssrPool.Add("电磁碰撞");
        ssrPool.Add("飞焰");
        ssrPool.Add("落霞");
        ssrPool.Add("坍塌");
        ssrPool.Add("陷入黑暗");
        ssrPool.Add("川浪");
        ssrPool.Add("川流");
        ssrPool.Add("暴风雪");
        ssrPool.Add("冻土");
        ssrPool.Add("武器粉碎");
        ssrPool.Add("武器大师");
        ssrPool.Add("虚空武器");
        ssrPool.Add("传家之宝");
        ssrPool.Add("无尽弹夹");
    }
    public string 遗物()
    {
        var u = UnityEngine.Random.Range(0, rPool.Count);
        string ret = rPool[u];
        rPool.RemoveAt(u);
        return ret;
    }
    public string 祝福()
    {
        var u = UnityEngine.Random.Range(0, ssrPool.Count);
        string ret = ssrPool[u];
        ssrPool.RemoveAt(u);
        return ret;
    }
}
