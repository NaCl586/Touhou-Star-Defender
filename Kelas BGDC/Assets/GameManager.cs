using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
    }

    public Text collectedCoinText;
    public static int collectedCoin = 0;

    // Start is called before the first frame update
    void Start()
    {
        ReferenceText();
    }

    public void ReferenceText()
    {
        collectedCoinText = GameObject.Find("collectedCoinText").GetComponent<Text>();
    }

    public void Update()
    {
        if (collectedCoinText == null)
        {
            ReferenceText();
            collectedCoinText.text = "Collected Coins: " + collectedCoin;
        }
            
    }

    public void AddCoin()
    {
        collectedCoin++;
        collectedCoinText.text = "Collected Coins: " + collectedCoin;
    }
}
