using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Text score;
    public Text levelName;
    public Text lives;
    public Text powerupName;
    public Image[] powerups = new Image[4];
    public Image currentPowerupImg;
    public Sprite[] powerupImages;
    public static int powerupType;
    public static int powerupAmount;

    public GameObject loadingScreen;

    private static int _currentScore = 0;
    private static int _lives = 5;
    [HideInInspector] public List<Fairy> fairies = new List<Fairy>();
    private int initFairyCount;
    private Character character;

    public static bool isChangingDirection = false;

    public void setLives(int live) { 
        _lives = live;
        lives.text = _lives.ToString();
    }
    public int getLives(){return _lives; }

    public void setPowerupText(string text)
    {
        powerupName.text = text;
        powerupName.color = new Color(0, 0.830188f, 0.06344367f, 1);
        powerupName.DOColor(new Color(0, 0.830188f, 0.06344367f, 1), 5f).OnComplete(() => {
            powerupName.DOColor(Color.clear, 0.5f);
        });
    }

    public void setPowerupIcon()
    {
        Color color = Color.clear;
        if (powerupType == 0) color = new Color(1, 0.3339622f, 0.3339622f, 1);
        else if (powerupType == 1) color = new Color(0, 0.7695842f, 1, 1);
        else if (powerupType == 2) color = Color.white;
        else if (powerupType == 3) color = Color.yellow;

        for (int i = 0; i < 4; i++)
        {
            if (i < powerupAmount)
                powerups[i].color = color;
            else
                powerups[i].color = Color.clear;
        }

        if (powerupAmount == 0)
        {
            currentPowerupImg.color = Color.clear;
        }
        else
        {
            currentPowerupImg.color = Color.white;
            currentPowerupImg.sprite = powerupImages[powerupType];
        }
    }

    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();

        score.text = _currentScore.ToString();
        levelName.text = SceneManager.GetActiveScene().name;
        lives.text = _lives.ToString();
        setPowerupIcon();

        loadingScreen.SetActive(false);

        foreach (GameObject f in GameObject.FindGameObjectsWithTag("Fairy"))
            fairies.Add(f.GetComponent<Fairy>());

        initFairyCount = fairies.Count;
        StartCoroutine(FairyShoot(2f));
    }

    public void moveDown()
    {
        if (Fairy.movingStates == 'd' || isChangingDirection) return;

        isChangingDirection = true;
        char prev = Fairy.movingStates;
        Fairy.movingStates = 'd';

        foreach (GameObject fairy in GameObject.FindGameObjectsWithTag("Fairy"))
        {
            if(fairies.Count <= initFairyCount - 1) fairy.transform.DOMove(fairy.transform.position + Vector3.down * 0.125f, 1f);
        }

        if (prev == 'r') Fairy.movingStates = 'l';
        else if (prev == 'l') Fairy.movingStates = 'r';

        Invoke("notChangingDirection", 1f);
    }

    public void notChangingDirection()
    {
        isChangingDirection = false;
    }

    public void bumpUp()
    {
        character.Death();
        foreach (GameObject fairy in GameObject.FindGameObjectsWithTag("Fairy"))
            fairy.transform.DOMove(fairy.transform.position + Vector3.up * 0.375f, 1f);
    }

    private bool checkForBullet = false;
    private GameObject[] leftoverBullets;
    private GameObject[] leftoverItems;

    public void Update()
    {
        if (fairies.Count <= 5)
            Fairy.moveSpeed = 0.0125f * 2;
        else if (fairies.Count <= 10)
            Fairy.moveSpeed = 0.0125f;
        else
            Fairy.moveSpeed = 0.0125f / 2;

        if(fairies.Count == 0)
        {
            if (!checkForBullet)
            {
                checkForBullet = true;
                leftoverBullets = GameObject.FindGameObjectsWithTag("Bullet");
                leftoverItems = GameObject.FindGameObjectsWithTag("Item");
            }
            else
            {
                bool flag = true;
                foreach (GameObject i in leftoverItems)
                {
                    if (i != null)
                    {
                        flag = false;
                        break;
                    }
                }
                foreach (GameObject b in leftoverBullets)
                {
                    if (b.activeSelf == true)
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag == true)
                {
                    StartCoroutine(LoadAsycncronously(SceneManager.GetActiveScene().buildIndex + 1));
                }
            }
        }
    }

    IEnumerator LoadAsycncronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            yield return null;
        }
    }

    IEnumerator FairyShoot(float delay)
    {
        yield return new WaitForSeconds(delay);
        while(fairies.Count > 0)
        {
            float upperBound = 5f;
            if (fairies.Count > 10) upperBound = 5f;
            else if (fairies.Count > 5) upperBound = 3f;
            else if (fairies.Count >= 1) upperBound = 1.5f;

            fairies[Random.Range(0, fairies.Count - 1)].Attack();
            yield return new WaitForSeconds(Random.Range(1f,upperBound));
        }
    }

    public void addScore(int _score)
    {
        _currentScore += _score;
        score.text = _currentScore.ToString();
    }
}
