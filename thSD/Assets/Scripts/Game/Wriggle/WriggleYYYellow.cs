using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WriggleYYYellow : WriggleBoss
{
    //homing ke arah player, 3-12 shot

    public override void ShootBullet()
    {
        StartCoroutine(ShootDelay());
    }

    IEnumerator ShootDelay()
    {
        if (WriggleManager.HP <= 0) yield return null;
        _as.PlayOneShot(shotSound);

        int count = Random.Range(3, 8);
        for (int i = 0; i < count; i++)
        {
            if (WriggleManager.HP <= 0) break;

            GameObject bullets = _bp.InstantiateNewBullet();
            bullets.GetComponent<SpriteRenderer>().sprite = bullet;
            bullets.transform.position = transform.position;
            bullets.SetActive(false);
            bullets.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
            bullets.GetComponent<Bullet>().homing = true;
            bullets.GetComponent<Bullet>().direction = ((float)(i - count / 2) * (float)(90 / count));
            bullets.SetActive(true);
        }
        yield return null;
    }
}
