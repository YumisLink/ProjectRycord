using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Role py;
    private Sprite shower;
    public SpriteRenderer sr;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !GameManager.IsStop)
            if (py!= null)
                OnTouch(py);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerShoot>(out var player))
        {
            py = player.GetComponent<Role>();
            if (sr != null)
            {
                var c = sr.color;
                c.a = 1;
                sr.color = c;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerShoot>(out var player))
        {
            py = null;
            if (sr != null)
            {
                var c = sr.color;
                c.a = 0;
                sr.color = c;
            }
        }
    }
    public virtual void OnTouch(Role py) { }
}
