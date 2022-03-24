using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerShot : MonoBehaviour
{
    public float speed = 0.01f;
    public string type = "r";
    private state attackState;

    private enum state
    {
      straight,
      turn,
      diagonal
    };

    // Start is called before the first frame update
    void Start()
    {
        attackState = state.straight;
    }

    void FixedUpdate()
    {
        if(attackState == state.straight)
        {
            transform.position += Vector3.up * speed;
        }
        else if(attackState == state.turn)
        {
            if (type == "r")
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                transform.position += Vector3.right * speed;
            }
            else if (type == "l")
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                transform.position += Vector3.left * speed;
            }
        }
        else if(attackState == state.diagonal)
        {
            if (type == "dr")
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, -45));
                transform.position += (transform.rotation * Vector3.up) * speed;
            }
            else if (type == "dl")
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 45));
                transform.position += (transform.rotation * Vector3.up) * speed;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BulletDestroy"))
        {
            Destroy(gameObject);
        }
        else if (other.CompareTag("Fairy"))
        {
            other.GetComponent<Fairy>().FairyDeath();
            if (type != "f") StartCoroutine(switchState(0.125f));
        }
    }

    IEnumerator switchState(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (type == "l" || type == "r") attackState = state.turn;
        else if (type == "dl" || type == "dr") attackState = state.diagonal;
    }
}
