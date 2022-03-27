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

    public GameObject deathParticle;
    private GameObject _dpInstance;
    public AudioClip bossDeath;

    private int attackingIdx = 0;

    public void ReduceHP()
    {
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
        }
        isDead = true;
    }

    public void Start()
    {
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
