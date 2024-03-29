﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MotherFairy : MonoBehaviour
{
    public char direction = 'r';
    public float moveSpeed = 0.025f;
    private MotherFairyPool _pool;
    private int HP = 3;

    private GameManager _gm;

    private AudioSource _as;
    public AudioClip _deathSound;

    public GameObject _deathParticle;
    private GameObject _dpInstance;

    public GameObject[] items;

    private void Start()
    {
        _pool = GameObject.FindGameObjectWithTag("MotherFairyPool").GetComponent<MotherFairyPool>();
        _as = this.GetComponent<AudioSource>();
        _as.volume = PlayerPrefs.GetFloat("soundVolume", 0.25f);
        _gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        HP = 3;
        if (direction == 'l')
        {
            transform.position = new Vector3(5, 2.59f, 0f);
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (direction == 'r')
        {
            transform.position = new Vector3(-5, 2.59f, 0f);
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (direction == 'r')
            transform.position += Vector3.right * moveSpeed * GameManager.speedMultiplier;
        else if (direction == 'l')
            transform.position += Vector3.left * moveSpeed * GameManager.speedMultiplier;

        if(Mathf.Abs(transform.position.x) >= 6)
            _pool.returnMotherFairyToPool(this.gameObject);

        if (_dpInstance) _dpInstance.transform.position = transform.position;
    }

    public void reduceHP()
    {
        HP--;
        this.gameObject.GetComponent<SpriteRenderer>().DOColor(Color.red, 0.125f).OnComplete(() => {
            this.gameObject.GetComponent<SpriteRenderer>().DOColor(Color.white, 0.125f);
        });
        if (HP == 0)
        {
            MFDeath();
        }
    }

    public void MFDeath()
    {
        _as.PlayOneShot(_deathSound);

        _dpInstance = Instantiate(_deathParticle, transform.position, Quaternion.identity);
        _dpInstance.transform.localScale *= 1.75f;
        Destroy(_dpInstance, _dpInstance.GetComponent<ParticleSystem>().main.duration);

        //probability:
        //extend: 15%, bonus pts: 25%, double shot: 20%, time slow: 15%, shield: 25%)
        int index, rng = Random.Range(1, 101);
        if (rng >= 1 && rng <= 15) index = 0;
        else if (rng >= 16 && rng <= 50) index = 1;
        else if (rng >= 41 && rng <= 60) index = 2;
        else if (rng >= 61 && rng <= 75) index = 3;
        else index = 4;

        Instantiate(items[index], transform.position, Quaternion.identity);

        this.gameObject.GetComponent<SpriteRenderer>().DOColor(Color.clear, 0.25f).OnComplete(() => {
            _pool.returnMotherFairyToPool(this.gameObject);
            _gm.addScore(100);
        }); 
    }
}
