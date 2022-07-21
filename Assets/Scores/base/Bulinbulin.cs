using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Bulinbulin : MonoBehaviour
{
    public Light2D spr;
    bool d;
    private void FixedUpdate()
    {
        if (d)
        {
            spr.intensity -= Time.deltaTime * 0.4f;
            if (spr.intensity <= -0.2f)
                d = !d;
        }
        else
        {
            spr.intensity+= Time.deltaTime * 0.4f;
            if (spr.intensity >= 0.2f)
                d = !d;
        }

    }
}
