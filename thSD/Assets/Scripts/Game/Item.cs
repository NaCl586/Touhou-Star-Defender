using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int type = 0;
    public float fallSpeed = 0.05f;

    void FixedUpdate()
    {
        transform.position = transform.position + Vector3.down * fallSpeed;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BulletDestroy"))
        {
            Destroy(gameObject);
        }
        else if (other.CompareTag("ItemCollector"))
        {
            other.GetComponent<ContentManager>().PickupItem(type);
            Destroy(gameObject);
        }
    }
}
