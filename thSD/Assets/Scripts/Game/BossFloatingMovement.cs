using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFloatingMovement : MonoBehaviour
{
    public float frequency;
    public float amplitude;

    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    void Start()
    {
        posOffset = transform.position;
    }

    public void FixedUpdate()
    {
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tempPos;
    }
}
