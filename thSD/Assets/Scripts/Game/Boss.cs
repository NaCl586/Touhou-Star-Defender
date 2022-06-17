using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
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

        _as.volume = PlayerPrefs.GetFloat("soundVolume", 0.25f);
    }
}
