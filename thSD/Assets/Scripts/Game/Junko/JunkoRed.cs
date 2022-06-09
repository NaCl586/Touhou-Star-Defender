using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkoRed : JunkoBoss
{
    private Vector3 initPos;

    public override void ShootBullet()
    {
        float addedAngle = Random.Range(0f, 90f);
        initPos = new Vector2(Random.Range(-1f, 1f), Random.Range(-0.5f, 1.5f));
        _as.PlayOneShot(shotSound);
        float angle = 360 / shotCount;
        for (int i = 0; i < shotCount; i++)
        {
            GameObject bullets = _bp.InstantiateNewBullet();
            bullets.GetComponent<SpriteRenderer>().sprite = bullet;
            bullets.transform.position = initPos;
            bullets.SetActive(false);
            bullets.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
            bullets.GetComponent<Bullet>().homing = false;
            bullets.GetComponent<Bullet>().direction = (i * angle + addedAngle) % 360;
            bullets.SetActive(true);
        }
    }
}
