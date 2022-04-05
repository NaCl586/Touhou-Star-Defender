using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CharaController : MonoBehaviour
{
    public static CharaController instance;
    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != null && instance != this)
        {
            Debug.Log("Instance Destroyed");
            Destroy(gameObject);
        }
    }


    public float moveSpeed = 0.05f;
    public bool canMove = true;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    // Update is called once per frame
    void Update()
    {
        if (!canMove) return;

        if (Input.GetKey(KeyCode.UpArrow))
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.DownArrow))
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftArrow))
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.RightArrow))
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
