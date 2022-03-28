using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ReisenManager : MonoBehaviour
{
    public static bool isDead = false;
    int MaxHP = 100;
    public static int HP = 100;
    public Slider HPBar;

    private ReisenBoss currentAttack;
    public GameObject[] bossParts;
    private Color[] colors = new Color[5];
    public GameObject effect;

    public GameObject deathParticle;
    private GameObject _dpInstance;
    public AudioClip bossDeath;

    private int attackingIdx = 0;

    private GameManager _gm;

    public void ReduceHP()
    {
        for (int i = 0; i < bossParts.Length-1; i++)
        {
            bossParts[i].GetComponent<SpriteRenderer>().DOColor(Color.red, 0.125f);
        }
        bossParts[4].GetComponent<SpriteRenderer>().DOColor(Color.red, 0.125f).OnComplete(() => {
            for (int i = 0; i < bossParts.Length; i++)
                bossParts[i].GetComponent<SpriteRenderer>().DOColor(colors[i], 0.125f);
        });

        HP--;
        HPBar.value = (float) HP / 100;
        if (HP <= 0) BossDead();
    }

    public void BossDead()
    {
        currentAttack._as.PlayOneShot(bossDeath);
        foreach(GameObject b in bossParts)
        {
            b.GetComponent<SpriteRenderer>().DOColor(Color.clear, 0.25f);
            b.GetComponent<BoxCollider2D>().enabled = false;
            _dpInstance = Instantiate(deathParticle, b.transform.position, Quaternion.identity);
            Destroy(_dpInstance, _dpInstance.GetComponent<ParticleSystem>().main.duration);
            b.SetActive(false);
        }
        effect.SetActive(false);
        _gm.addScore(1500);
        isDead = true;
    }

    public void Start()
    {
        _gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        for(int i = 0; i < bossParts.Length; i++)
        {
            colors[i] = bossParts[i].GetComponent<SpriteRenderer>().color;
        }

        currentAttack = bossParts[0].GetComponent<ReisenBoss>();

        StartCoroutine(PurpleAttack());
        StartCoroutine(ReisenAttack());
        StartCoroutine(PinkAttack());
    }

    IEnumerator PurpleAttack()
    {
        yield return new WaitForSeconds(2f);
        while (HP > 0)
        {
            currentAttack.ShootBullet();
            attackingIdx++;
            attackingIdx %= 2;
            currentAttack = bossParts[attackingIdx].GetComponent<ReisenBoss>();
            yield return new WaitForSeconds(0.75f);
        }
    }

    IEnumerator PinkAttack()
    {
        yield return new WaitForSeconds(7f);
        while (HP > 0)
        {
            bossParts[2].GetComponent<ReisenBoss>().ShootBullet();
            yield return new WaitForSeconds(0.25f);
            bossParts[3].GetComponent<ReisenBoss>().ShootBullet();
            yield return new WaitForSeconds(10f);
        }
    }

    IEnumerator ReisenAttack()
    {
        yield return new WaitForSeconds(5f);
        while (HP > 0)
        {
            bossParts[4].GetComponent<ReisenBoss>().ShootBullet();
            yield return new WaitForSeconds(8f);
        }
    }
}
