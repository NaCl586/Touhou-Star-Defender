using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ReisenYYPurple : ReisenBoss
{
    public override void ShootBullet()
    {
        _as.PlayOneShot(shotSound);
        float addedAngle = 0;
        if (Random.Range(0, 2) % 2 == 1) addedAngle += (Random.Range(-15f, 15f));
        if (ReisenManager.HP <= 0) return;
        for (int i = 0; i < shotCount; i++)
        {
            GameObject bullets = _bp.InstantiateNewBullet();
            bullets.GetComponent<SpriteRenderer>().sprite = bullet;
            bullets.transform.position = transform.position;
            bullets.SetActive(false);
            bullets.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
            bullets.GetComponent<Bullet>().homing = true;
            bullets.GetComponent<Bullet>().direction = ((i - shotCount / 2) * (float)(180 / shotCount) + addedAngle);
            bullets.SetActive(true);
        }
    }
}
