using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderEnabler : MonoBehaviour
{
    void Update()
    {
        if (!GetComponent<BoxCollider2D>().enabled && !GameManager.isChangingDirection)
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
