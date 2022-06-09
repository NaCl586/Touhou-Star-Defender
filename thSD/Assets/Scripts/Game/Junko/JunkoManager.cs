using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkoManager : BossManager
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RedCircle());
        StartCoroutine(PinkCircle());
        StartCoroutine(BlueCircle());
        StartCoroutine(JunkoAttack());
    }

    IEnumerator RedCircle()
    {
        yield return new WaitForSeconds(3f);
        while (JunkoManager.HP > 0)
        {
            JunkoBoss jb = bossParts[0] as JunkoBoss;
            jb.ShootBullet();
            yield return new WaitForSeconds(0.25f);
        }
    }

    IEnumerator PinkCircle()
    {
        yield return new WaitForSeconds(3f);
        while (JunkoManager.HP > 0)
        {
            JunkoBoss jb = bossParts[1] as JunkoBoss;
            jb.ShootBullet();
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator BlueCircle()
    {
        yield return new WaitForSeconds(3f);
        while (JunkoManager.HP > 0)
        {
            JunkoBoss jb = bossParts[2] as JunkoBoss;
            jb.ShootBullet();
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator JunkoAttack()
    {
        yield return new WaitForSeconds(3f);
        while (JunkoManager.HP > 0)
        {
            JunkoBoss jb = bossParts[3] as JunkoBoss;
            jb.ShootBullet();
            yield return new WaitForSeconds(0.2f);
        }
    }
}
