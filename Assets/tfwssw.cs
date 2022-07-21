using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tfwssw : MonoBehaviour
{
    public List<Image> imgs;
    int cnt = 0;
    public void AddMijuan(string name)
    {
        if (cnt < 14)
        {
            imgs[cnt].sprite = UIManager.Sprite[name];
            cnt++;
        }
    }
}
