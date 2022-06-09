using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MarisaManager : BossManager
{ 
    public void FixedUpdate()
    {
        if (transform.position.x < -1) dir = 'r';
        else if (transform.position.x > 1) dir = 'l';

        if (dir == 'l')
            transform.position += Vector3.left * 0.01f;
        else if (dir == 'r')
            transform.position += Vector3.right * 0.01f;
       
    }

 
    public void Start()
    {
        StartCoroutine(BlueAttack());
        StartCoroutine(MarisaAttack());
    }

    IEnumerator BlueAttack()
    {
        yield return new WaitForSeconds(2f);
        while (HP > 0)
        {
            MarisaBoss mb = currentAttack as MarisaBoss;
            mb.ShootBullet();
            attackingIdx++;
            attackingIdx %= 2;
            currentAttack = bossParts[attackingIdx];
            yield return new WaitForSeconds(0.75f);
        }
    }

    IEnumerator MarisaAttack()
    {
        yield return new WaitForSeconds(5f);
        while (HP > 0)
        {
            MarisaBoss mb = bossParts[bossParts.Length - 1] as MarisaBoss;
            mb.ShootBullet();
            yield return new WaitForSeconds(Random.Range(3, 5));
        }
    }
}

