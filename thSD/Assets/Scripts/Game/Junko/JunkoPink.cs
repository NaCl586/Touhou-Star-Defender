using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkoPink : JunkoBoss
{
    private Vector3[] initPos = new Vector3[2];

    public override void ShootBullet()
    {
        if (JunkoManager.HP > 75) return;

        initPos[0] = new Vector2(Random.Range(-3f, -1f), Random.Range(-1f, 2f));
        initPos[1] = new Vector2(Random.Range(1f, 3f), Random.Range(-1f, 2f));
        _as.PlayOneShot(shotSound);

        for(int j = 0; j < 2; j++)
        {
            float angle = 360 / shotCount;
            float addedAngle = Random.Range(0f, 90f);
            for (int i = 0; i < shotCount; i++)
            {
                GameObject bullets = _bp.InstantiateNewBullet();
                bullets.GetComponent<SpriteRenderer>().sprite = bullet;
                bullets.transform.position = initPos[j];
                bullets.SetActive(false);
                bullets.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
                bullets.GetComponent<Bullet>().homing = false;
                bullets.GetComponent<Bullet>().direction = (i * angle + addedAngle) % 360;
                bullets.SetActive(true);
            }
        }
    }
}
