using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WriggleManager : BossManager
{
    public void Start()
    {
        StartCoroutine(YellowAttack());
        StartCoroutine(WriggleAttack());
        StartCoroutine(GreenAttack());
    }

    IEnumerator YellowAttack()
    {
        yield return new WaitForSeconds(2f);
        while (HP > 0)
        {
            WriggleBoss wb = currentAttack as WriggleBoss;
            wb.ShootBullet();
            attackingIdx++;
            attackingIdx %= 2;
            currentAttack = bossParts[attackingIdx];
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator GreenAttack()
    {
        yield return new WaitForSeconds(5f);
        while (HP > 0)
        {
            WriggleBoss wb = bossParts[2] as WriggleBoss;
            wb.ShootBullet();
            yield return new WaitForSeconds(0.25f);
            wb = bossParts[3] as WriggleBoss;
            wb.ShootBullet();
            yield return new WaitForSeconds(6f);
        }
    }

    IEnumerator WriggleAttack()
    {
        yield return new WaitForSeconds(3f);
        while (HP > 0)
        {
            WriggleBoss wb = bossParts[4] as WriggleBoss;
            wb.ShootBullet();
            yield return new WaitForSeconds(3f);
        }
    }
}
