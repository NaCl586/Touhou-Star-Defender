using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ReisenManager : BossManager
{
    public void Start()
    {
        StartCoroutine(PurpleAttack());
        StartCoroutine(ReisenAttack());
        StartCoroutine(PinkAttack());
    }

    IEnumerator PurpleAttack()
    {
        yield return new WaitForSeconds(2f);
        while (HP > 0)
        {
            ReisenBoss rb = currentAttack as ReisenBoss;
            rb.ShootBullet();
            attackingIdx++;
            attackingIdx %= 2;
            currentAttack = bossParts[attackingIdx];
            yield return new WaitForSeconds(0.75f);
        }
    }

    IEnumerator PinkAttack()
    {
        yield return new WaitForSeconds(7f);
        while (HP > 0)
        {
            ReisenBoss rb = bossParts[2] as ReisenBoss;
            rb.ShootBullet();
            yield return new WaitForSeconds(0.25f);
            rb = bossParts[3] as ReisenBoss;
            rb.ShootBullet();
            yield return new WaitForSeconds(10f);
        }
    }

    IEnumerator ReisenAttack()
    {
        yield return new WaitForSeconds(5f);
        while (HP > 0)
        {
            ReisenBoss rb = bossParts[4] as ReisenBoss;
            rb.ShootBullet();
            yield return new WaitForSeconds(8f);
        }
    }
}
