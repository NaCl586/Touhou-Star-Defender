using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptA : MonoBehaviour
{
    public static int StaticInt = 0;
    public int intBiasa = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            intBiasa += 1;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            StaticInt += 1;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(StaticInt);
        }
    }
}
