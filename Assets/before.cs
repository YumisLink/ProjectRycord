using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class before : MonoBehaviour
{
    public static int kk = 154;
    public Text txt1;
    public Text txt2;
    void Start()
    {
        kk += 1;
        txt1.text = $"你是第{kk}个降临到伊甸的人";
    }
    int can = 0;
    private void Update()
    {
        if ((Input.anyKeyDown) && can == 2)
        {
            SceneManager.LoadScene("Main");
        }

        if ((Input.anyKeyDown) && can == 1)
        {
            txt2.text = "伊甸，创始之地。最初的时候，天上尚未降下雨水，地上却有雾气蒸腾，滋生植物，滋润大地，有电闪霄鸣，有星星之火。这一切的由来--伊甸之心，这个连接着初诞世界的核心，汇聚着最本质的原始元素冰、水、雷、火，鲁莽的生灵却要不顾一切的接近伊甸之心，妄图理解这个世界的本身。请你阻止他们，守住伊甸之心！先人的灵魂与元素之灵将与你同在，即使你倒下，也能够循序伊甸的法则，再一次见证最初的伊甸之心。"; ;
            can = 2;
        }
        if ((Input.anyKeyDown) && can == 0)
        {
            StartCoroutine("dd");
            can = 1;
        }
    }
    public int ss = 0;
    public string str;
    IEnumerator dd()
    {
        for (int ss = 0; ss < str.Length; ss++)
        {
            if (can == 2)
                break;
            yield return new WaitForSeconds(0.03f);
            if (can == 2)
                break;
            txt2.text += str[ss];
        }
        can = 2;
    }
}
