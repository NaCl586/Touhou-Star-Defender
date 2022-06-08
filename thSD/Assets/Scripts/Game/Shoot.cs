using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float speed = 1f;
    private ShotPool _poolManager;
    private Vector3 dir;

    public void Awake()
    {
        _poolManager = GameObject.FindGameObjectWithTag("ShotPool").GetComponent<ShotPool>();
        dir = Vector3.up;
    }

    public void changeDirection(int direction)
    {
        dir = Quaternion.Euler(new Vector3(0, 0, direction)) * Vector3.up;
        float rot_z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
    }

    void FixedUpdate()
    {
        transform.position += dir * speed;
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
        else if (other.CompareTag("MotherFairy"))
        {
            other.GetComponent<MotherFairy>().reduceHP();
            _poolManager.returnShootToPool(gameObject);
        }
        else if (other.CompareTag("Boss"))
        {
            if(other.name == "Reisen")
                other.transform.parent.GetComponent<ReisenManager>().ReduceHP();
            else if(other.name == "Marisa")
                other.transform.parent.GetComponent<MarisaManager>().ReduceHP();
            else if (other.name == "Wriggle")
                other.transform.parent.GetComponent<WriggleManager>().ReduceHP();

            _poolManager.returnShootToPool(gameObject);
        }
    }
}
