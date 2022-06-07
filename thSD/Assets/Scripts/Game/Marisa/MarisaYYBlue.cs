using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MarisaYYBlue : MarisaBoss
{
    public int direction = 1;
    private float addedAngle = 0;

    public override void ShootBullet()
    {
        _as.PlayOneShot(shotSound);
        
        if (addedAngle < -45 && direction == -1)
            direction = 1;
        else if (addedAngle > 45 && direction == 1)
            direction = -1;

        if (direction == -1) addedAngle -= Random.Range(5,15);
        else if (direction == 1) addedAngle += Random.Range(5, 15);

        if (MarisaManager.HP <= 0) return;
        for (int i = 0; i < shotCount; i++)
        {
            GameObject bullets = _bp.InstantiateNewBullet();
            bullets.GetComponent<SpriteRenderer>().sprite = bullet;
            bullets.transform.position = transform.position;
            bullets.SetActive(false);
            bullets.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
            bullets.GetComponent<Bullet>().homing = false;
            bullets.GetComponent<Bullet>().direction = ((i - shotCount / 2) * (float)(90 / shotCount) + addedAngle);
            bullets.SetActive(true);
        }
    }
}
