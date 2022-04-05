using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static float lastCollectedTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Time.time - lastCollectedTime < 2f) return;
            lastCollectedTime = Time.time;
            CollectItem(collision);
        }
    }

    private void CollectItem(Collider2D collision)
    {
        GameManager.instance.AddCoin();
        Destroy(gameObject);
    }
}
