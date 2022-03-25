using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherFairyPool : MonoBehaviour
{
    public GameObject motherFairy;
    public Queue<GameObject> MFPool = new Queue<GameObject>();
    
    public GameObject GetNewMotherFairy(char dir)
    {
        GameObject motherFairyInstance;
        if (MFPool.Count == 0)
            motherFairyInstance = Instantiate(motherFairy);
        else
            motherFairyInstance = MFPool.Dequeue();

        motherFairyInstance.SetActive(false);
        motherFairyInstance.GetComponent<MotherFairy>().direction = dir;
        return motherFairyInstance;
    }

    public GameObject InstantiateNewMotherFairy(char dir)
    {
        GameObject motherFairyInstance = GetNewMotherFairy(dir);
        motherFairyInstance.SetActive(true);
        return motherFairyInstance;
    }

    //dipanggil pas collide di collider offscreen, kalau misal offscreen, bullet yang kena disana bakal dinonaktifin, terus dienqueue
    public void returnMotherFairyToPool(GameObject motherFairyInstance)
    {
        motherFairyInstance.SetActive(false);
        MFPool.Enqueue(motherFairyInstance);
    }
}
