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
        txt1.text = $"���ǵ�{kk}�����ٵ��������";
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
            txt2.text = "���飬��ʼ֮�ء������ʱ��������δ������ˮ������ȴ���������ڣ�����ֲ������أ��е���������������֮����һ�е�����--����֮�ģ���������ų�������ĺ��ģ��������ʵ�ԭʼԪ�ر���ˮ���ס���³ç������ȴҪ����һ�еĽӽ�����֮�ģ���ͼ����������ı���������ֹ���ǣ���ס����֮�ģ����˵������Ԫ��֮�齫����ͬ�ڣ���ʹ�㵹�£�Ҳ�ܹ�ѭ������ķ�����һ�μ�֤���������֮�ġ�"; ;
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
