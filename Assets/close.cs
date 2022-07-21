using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class close : MonoBehaviour
{
    public Image img;
    public bool st = false;
    void Start()
    {
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (st)
        {
            var c = img.color;
            c.a += Time.deltaTime * 0.5f;
            img.color = c;
        }
    }
}
