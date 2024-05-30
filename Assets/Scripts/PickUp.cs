using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickUp : MonoBehaviour
{
    public Proxy proxy;
    public bool destroyAfterCollected = true;
    private int id;

    public void Start()
    {
        var log = gameObject.GetComponent<Log>();
        var stick = gameObject.GetComponent<Stick>();
        var stone = gameObject.GetComponent<Stone>();
        if (log != null)
        {
            id = 1;
        }
        else if (stick != null)
        {
            id = 2;
        }
        else if (stone != null)
        {
            id = 3;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var Player = collision.GetComponent<PlayerController>();
        if (!Player) return;
        proxy = collision.gameObject.GetComponent<Proxy>();
        proxy.PickUpItem(id);

        if (!destroyAfterCollected) return;
        Destroy(gameObject);
    }


}
