using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WriggleYYGreen: WriggleBoss
{
    public bool clockwise = true;

    public override void ShootBullet()
    {
        int loops = 8;
        float bulletSpeed = 0.005f;
        int addedAngle = 0;
        int shots = 5;
        float delay = 0.3f / shots;
        StartCoroutine(greenFairyShots(loops, bulletSpeed, delay, addedAngle, shots));
    }

    IEnumerator greenFairyShots(int loops, float bulletSpeed, float delay, int addedAngle, int shots)
    {
        if (WriggleManager.HP <= 0) yield return null;

        int count = 10;
        float gap = 180 / count;
        int evenLoop = 0;
        for (int i = 0; i < loops; i++)
        {
            evenLoop = i % 2 == 0 ? 0 : 180;
            for (int j = 0; j < count; j++)
            {
                _as.PlayOneShot(shotSound);
                int offset = 360 / shots;
                for (int k = 0; k < shots; k++)
                {
                    if (WriggleManager.HP <= 0) break;

                    GameObject bullets = _bp.InstantiateNewBullet();
                    bullets.GetComponent<SpriteRenderer>().sprite = bullet;
                    bullets.transform.position = transform.position;
                    bullets.SetActive(false);
                    bullets.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
                    bullets.GetComponent<Bullet>().homing = false;
                    if(clockwise)
                        bullets.GetComponent<Bullet>().direction = (k * offset) + evenLoop + addedAngle + (j * gap);
                    else
                        bullets.GetComponent<Bullet>().direction = - (k * offset) - evenLoop + addedAngle - (j * gap);
                    bullets.SetActive(true);
                }
                if (WriggleManager.HP <= 0) break;
                yield return new WaitForSeconds(delay);
            }
            if (WriggleManager.HP <= 0) break;
        }
    }
}
