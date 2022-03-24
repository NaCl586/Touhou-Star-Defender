using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPool : MonoBehaviour
{
    public GameObject character;
    public GameObject shoot;

    public Queue<GameObject> shootPool = new Queue<GameObject>();

    public GameObject GetNewBullet()
    {
        //kalau di pool ga ada apa2, maka instantiate baru
        if (shootPool.Count == 0) {
            GameObject newShoot = Instantiate(shoot);
            newShoot.SetActive(false);
            return newShoot;
        }
        //kalau ada simply dequeue yg di pool
        else
        {
            return shootPool.Dequeue();
        }
    }

    //instantiate diambil dari function getnewbullet, abis itu aktifin, then samain posisi awal spt posisi character
    public void InstantiateNewBullet()
    {
        GameObject bullet = GetNewBullet();
        bullet.SetActive(true);
        bullet.transform.position = character.transform.position; 
    }

    //dipanggil pas collide di collider offscreen, kalau misal offscreen, bullet yang kena disana bakal dinonaktifin, terus dienqueue
    public void returnShootToPool(GameObject bullet)
    {
        bullet.SetActive(false);
        shootPool.Enqueue(bullet);
    }
}
