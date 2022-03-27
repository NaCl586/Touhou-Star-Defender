using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ReisenYYPink : ReisenBoss
{
    public bool clockwise = true;

    public override void ShootBullet()
    {
        StartCoroutine(ShootDelay(0.5f));
    }

    IEnumerator ShootDelay(float delay)
    {
        for (int i = 0; i < 10; i++)
        {
            if (ReisenManager.HP <= 0) yield break;
            _as.PlayOneShot(shotSound);
            GameObject bullets = _bp.InstantiateNewBullet();
            bullets.GetComponent<SpriteRenderer>().sprite = bullet;
            bullets.transform.position = transform.position;
            bullets.SetActive(false);
            bullets.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
            bullets.GetComponent<Bullet>().homing = false;
            if (clockwise) bullets.GetComponent<Bullet>().direction = (360 / shotCount) * i;
            else bullets.GetComponent<Bullet>().direction = (360 / shotCount) * (shotCount - i);
            bullets.GetComponent<Bullet>().direction += 180;
            bullets.SetActive(true);
            yield return new WaitForSeconds(delay);
        }
    }
}
