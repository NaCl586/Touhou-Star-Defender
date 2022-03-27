using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float direction;
    public bool homing;
    public float bulletSpeed;

    private Vector3 dir;
    private GameObject character;
    private BulletPool _poolManager;

    // Start is called before the first frame update
    private void Awake()
    {
        character = GameObject.FindGameObjectWithTag("Player");
        _poolManager = GameObject.FindGameObjectWithTag("BulletPool").GetComponent<BulletPool>();
    }

    public void changeDirection(Vector3 dir)
    {
        this.dir = dir;
    }

    void OnEnable()
    {
        if (!homing)
        {
            dir = Quaternion.Euler(new Vector3(0, 0, direction)) * Vector3.down;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, direction));
        }
        else
        {
            if (character != null)
                dir = (character.transform.position - transform.position).normalized;
            else
                dir = Quaternion.Euler(new Vector3(0, 0, Random.Range(0f, 360f))) * Vector3.down;
            dir = Quaternion.Euler(new Vector3(0, 0, direction)) * dir;

            float rot_z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += dir * bulletSpeed * GameManager.speedMultiplier;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BulletDestroy"))
        {
            _poolManager.returnShootToPool(gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            if (other.transform.parent.gameObject.GetComponent<Character>().isInvincible) return;

            if (!Character.isShielded)
            {
                other.transform.parent.gameObject.GetComponent<Character>().Death();
                foreach (GameObject fairy in GameObject.FindGameObjectsWithTag("Fairy"))
                    fairy.transform.DOMove(fairy.transform.position + Vector3.up * 0.75f, 1f);
            }
            else
            {
                other.transform.parent.gameObject.GetComponent<Character>().deactivateShield();
            }
        }
    }
}
