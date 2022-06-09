using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Junko : JunkoBoss
{
    public override void ShootBullet()
    {
        if (JunkoManager.HP > 25) return;

        float addedAngle = 360 / (2 * shotCount);
        _as.PlayOneShot(shotSound);
        float angle = 360 / shotCount;
        for (int i = 0; i < shotCount; i++)
        {
            GameObject bullets = _bp.InstantiateNewBullet();
            bullets.GetComponent<SpriteRenderer>().sprite = bullet;
            bullets.transform.position = transform.position;
            bullets.SetActive(false);
            bullets.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
            bullets.GetComponent<Bullet>().homing = false;
            bullets.GetComponent<Bullet>().direction = (i * angle + addedAngle) % 360;
            bullets.SetActive(true);
        }
    }
}
