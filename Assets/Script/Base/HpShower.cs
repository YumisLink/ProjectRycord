using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpShower : MonoBehaviour
{
    public Role role;
    public SpriteRenderer rend;
    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        GetComponent<SpriteRenderer>().color = role.Camp.GetColor();
    }
    void Update()
    {
        if (role == null)
            Destroy(gameObject);
        var a = rend.size;
        a.x = role.hp / role.Property.Maxhp * 2;
        rend.size = a;

        transform.position = role.transform.position + new Vector3(0,role.HeadHight);
    }
    public static void Create(Role go)
    {
        var hp = Instantiate(UIManager.UI["HpShow"]);
        hp.transform.parent = go.transform;
        
        var shower = hp.GetComponent<HpShower>();
        shower.role = go;
    }
}
