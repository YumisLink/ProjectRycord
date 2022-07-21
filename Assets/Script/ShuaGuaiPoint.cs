using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuaGuaiPoint : MonoBehaviour
{
    public List<Vector2> point = new();
    void Start()
    {
        foreach (var a in GetComponentsInChildren<Transform>())
        {
            GameManager.MonsterSpawn.Add(a.position);
            if (a.name != name)
                Destroy(gameObject);
        }
    }

    void Update()
    {
        
    }
}
