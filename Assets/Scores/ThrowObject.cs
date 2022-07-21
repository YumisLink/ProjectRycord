using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    public Vector2 speed = Vector2.zero;
    private void FixedUpdate()
    {
        speed += Time.fixedDeltaTime * new Vector2(0, -4.9f);
        WindChimeEngnie.Lib.Move(gameObject, speed * Time.fixedDeltaTime);
        if (transform.position.y < -20)
            Destroy(gameObject);
    }
}
