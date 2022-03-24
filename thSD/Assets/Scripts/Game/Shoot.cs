using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float speed = 1f;
    private ShotPool _poolManager;

    public void Awake()
    {
        _poolManager = GameObject.FindGameObjectWithTag("ShotPool").GetComponent<ShotPool>();
    }

    void FixedUpdate()
    {
        transform.position += Vector3.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BulletDestroy"))
        {
            _poolManager.returnShootToPool(gameObject);
        }
        else if (other.CompareTag("Fairy"))
        {
            other.GetComponent<Fairy>().reduceHP();
            _poolManager.returnShootToPool(gameObject);
        }
    }
}
