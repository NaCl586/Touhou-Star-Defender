using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kuru : KuruBoss
{
    public override void ShootBullet()
    {
        StartCoroutine(ShootDelay(0.5f));
    }

    IEnumerator ShootDelay(float delay)
    {
        float angle = 15;
        int startingShot = 3;
        for (int j = 0; j < 10; j++)
        {
            if (KuruManager.HP <= 0) break;
            _as.PlayOneShot(shotSound);
            for (int i = 0; i < startingShot; i++)
            {
                GameObject bullets = _bp.InstantiateNewBullet();
                bullets.GetComponent<SpriteRenderer>().sprite = bullet;
                bullets.transform.position = transform.position;
                bullets.SetActive(false);
                bullets.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
                bullets.GetComponent<Bullet>().homing = false;
                bullets.GetComponent<Bullet>().direction = ((i - startingShot / 2) * angle);
                if (startingShot % 2 == 0) bullets.GetComponent<Bullet>().direction += (angle / 2);
                bullets.SetActive(true);
            }
            yield return new WaitForSeconds(delay);
            startingShot++;
        }
    }
}
