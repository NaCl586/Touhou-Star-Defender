using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wriggle : WriggleBoss
{
    public override void ShootBullet()
    {
        StartCoroutine(ShootDelay(Random.Range(0.125f, 0.5f)));
    }

    IEnumerator ShootDelay(float delay)
    {
        int shots = Random.Range(3, 10);
        shotCount = Random.Range(5, 16);
        _as.PlayOneShot(shotSound);
        float bulletSpeed = Random.Range(0.005f, 0.01f);
        float offset = (360 / shotCount) / 2;
        for(int shot = 0; shot < shots; shot++)
        {
            for (int i = 0; i < shotCount; i++)
            {
                if (WriggleManager.HP <= 0) break;

                GameObject bullets = _bp.InstantiateNewBullet();
                bullets.GetComponent<SpriteRenderer>().sprite = bullet;
                bullets.transform.position = transform.position;
                bullets.SetActive(false);
                bullets.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
                bullets.GetComponent<Bullet>().homing = false;
                bullets.GetComponent<Bullet>().direction = (i * (float)(360 / shotCount)) + offset;
                bullets.SetActive(true);
            }
            if (WriggleManager.HP <= 0) break;
            yield return new WaitForSeconds(delay);
        } 
    }
}
