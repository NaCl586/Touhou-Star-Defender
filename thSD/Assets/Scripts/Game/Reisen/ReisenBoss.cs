using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public abstract class ReisenBoss : MonoBehaviour
{
    protected BulletPool _bp;
    public AudioSource _as;
    public AudioClip shotSound;

    public Sprite bullet;
    public float bulletSpeed;
    public int shotCount;

    private void Start()
    {
        _bp = GameObject.FindGameObjectWithTag("BulletPool").GetComponent<BulletPool>();
        _as = this.gameObject.GetComponent<AudioSource>();
    }

    public abstract void ShootBullet();
}
