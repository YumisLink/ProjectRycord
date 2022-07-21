using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ntxe : MonoBehaviour
{
    public List<Sprite> s = new List<Sprite>();
    int cnt = 0;
    public void next()
    {
        cnt++;
        if (cnt > 4)
        {
            SceneManager.LoadScene("MainMune");
            return;
        }
        gameObject.GetComponent<Image>().sprite = s[cnt];
    }
}
