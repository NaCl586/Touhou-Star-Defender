using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BossManager : MonoBehaviour
{
    public static bool isDead = false;
    protected int MaxHP = 100;
    public static int HP = 100;

    protected Color[] colors = new Color[5];
    public GameObject effect;
    public GameObject glow;

    public GameObject deathParticle;
    protected GameObject _dpInstance;
    public AudioClip bossDeath;

    protected int attackingIdx = 0;

    protected GameManager _gm;
    protected char dir = 'l';

    public Slider HPBar;
    protected Boss currentAttack;
    public Boss[] bossParts;

    public void Awake()
    {
        dir = Random.Range(0, 100) % 2 == 0 ? 'l' : 'r';
        _gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        for (int i = 0; i < bossParts.Length; i++)
        {
            colors[i] = bossParts[i].gameObject.GetComponent<SpriteRenderer>().color;
        }

        currentAttack = bossParts[0].GetComponent<Boss>();
    }

    public void ReduceHP()
    {
        for (int i = 0; i < bossParts.Length - 1; i++)
        {
            bossParts[i].gameObject.GetComponent<SpriteRenderer>().DOColor(Color.red, 0.125f);
        }
        bossParts[bossParts.Length - 1].gameObject.GetComponent<SpriteRenderer>().DOColor(Color.red, 0.125f).OnComplete(() => {
            if (HP > 0)
            {
                for (int i = 0; i < bossParts.Length; i++)
                    bossParts[i].gameObject.GetComponent<SpriteRenderer>().DOColor(colors[i], 0.125f);
            }
        });

        HP--;
        HPBar.value = (float)HP / 100;
        if (HP <= 0) BossDead();
    }

    public void BossDead()
    {
        currentAttack._as.PlayOneShot(bossDeath);
        foreach (Boss b in bossParts)
        {
            b.gameObject.GetComponent<SpriteRenderer>().DOColor(Color.clear, 0.25f);
            b.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            _dpInstance = Instantiate(deathParticle, b.gameObject.transform.position, Quaternion.identity);
            Destroy(_dpInstance, _dpInstance.GetComponent<ParticleSystem>().main.duration);
        }
        effect.SetActive(false);
        glow.SetActive(false);
        _gm.addScore(1500);
        isDead = true;
    }
}
