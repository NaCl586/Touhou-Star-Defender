using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YinYangRotatingMovement : MonoBehaviour
{
    public float speed;
    public float speedY = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, Time.fixedDeltaTime * speedY, Time.fixedDeltaTime * speed));
    }
}
