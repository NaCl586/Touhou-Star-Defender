using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public bool isBonusRound = false;
    public bool isBossRound = false;
    public int bonusMotherFairy = 17;

    public Text gameOverText;
    public bool isWon;

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
    private MotherFairyPool _mfPool;

    public static bool isChangingDirection = false;
    public static float speedMultiplier = 1f;
    public static float shootFrequency = 0.25f;

    public void setLives(int live) {
        _lives = live;
        lives.text = _lives.ToString();
    }
    public int getLives(){return _lives; }

    public void setGameOverText()
    {
        lives.text = "0";
        gameOverText.text = "GAME OVER";
        gameOverText.DOColor(Color.clear, 3f).OnComplete(() => {
            gameOverText.DOColor(new Color(0, 0.830188f, 0.06344367f, 1), 0.25f);
        });
    }

    public void setWinText()
    {
        gameOverText.text = "MISSION COMPLETE";
        gameOverText.DOColor(Color.clear, 3f).OnComplete(() => {
            gameOverText.DOColor(new Color(0, 0.830188f, 0.06344367f, 1), 0.25f);
        });
    }

    public void setPowerupText(string text)
    {
        CancelInvoke("powerupTextFadeOut");
        powerupName.text = text;
        powerupName.color = new Color(0, 0.830188f, 0.06344367f, 1);
        Invoke("powerupTextFadeOut", 5f);
    }

    void powerupTextFadeOut() {
        powerupName.DOColor(Color.clear, 0.5f);
    }

    public void setPowerupIcon()
    {
        Color color = Color.clear;
        if (powerupType == 0) color = new Color(1, 0.3339622f, 0.3339622f, 1);
        else if (powerupType == 1) color = new Color(0, 0.7695842f, 1, 1);
        else if (powerupType == 2) color = Color.white;
        else if (powerupType == 3) color = Color.yellow;
        else if (powerupType == 4) color = Color.green;
        else if (powerupType == 5) color = Color.gray;

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
        _mfPool = GameObject.FindGameObjectWithTag("MotherFairyPool").GetComponent<MotherFairyPool>();

        score.text = _currentScore.ToString();
        levelName.text = SceneManager.GetActiveScene().name;
        lives.text = _lives.ToString();
        setPowerupIcon();

        loadingScreen.SetActive(false);

        if(!isBonusRound && !isBossRound)
        {
            foreach (GameObject f in GameObject.FindGameObjectsWithTag("Fairy"))
                fairies.Add(f.GetComponent<Fairy>());

            initFairyCount = fairies.Count;
            StartCoroutine(FairyShoot(2f));
            StartCoroutine(SpawnMotherFairy(Random.Range(5f, 15f)));
        }
        else if (isBonusRound)
        {
            StartCoroutine(SpawnMotherFairyBonus(2.5f));
        }
        else if (isBossRound)
        {
            powerupAmount = 0;
            Character.activePowerup = -1;
        }
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
            fairy.transform.DOMove(fairy.transform.position + Vector3.up * 0.25f, 1f);
    }

    private bool checkForBullet = false;
    private bool alreadyLoadNextLevel = false;
    private GameObject[] leftoverBullets;
    private GameObject[] leftoverItems;
    private GameObject[] leftoverMotherFairy;

    public Slider timeSlowSlider;
    public Slider doubleShotSlider;

    public static bool timeSlowisActive;
    public static float timeSlowStartTime;
    public static bool doubleShotisActive;
    public static float doubleShotStartTime;

    public void Update()
    {
        if (!isBossRound)
        {
            if (timeSlowisActive && (Time.time - timeSlowStartTime) >= 10)
            {
                timeSlowisActive = false;
                timeSlowSlider.gameObject.SetActive(false);
                speedMultiplier = 1f;
            }
            else if (timeSlowisActive)
            {
                timeSlowSlider.value = (Time.time - timeSlowStartTime) / 10;
                speedMultiplier = 0.25f;
            }
            else
            {
                timeSlowSlider.gameObject.SetActive(false);
            }

            if (doubleShotisActive && (Time.time - doubleShotStartTime) >= 10)
            {
                doubleShotisActive = false;
                doubleShotSlider.gameObject.SetActive(false);
                shootFrequency = 0.25f;
            }
            else if (doubleShotisActive)
            {
                doubleShotSlider.value = (Time.time - doubleShotStartTime) / 10;
                shootFrequency = 0.125f;
            }
            else
            {
                doubleShotSlider.gameObject.SetActive(false);
            }
        }
        else
        {
            speedMultiplier = 1f;
            shootFrequency = 0.25f;
        }
        
        if (!isBossRound && !isBonusRound)
        {
            if (fairies.Count <= 5)
                Fairy.moveSpeed = 0.0125f * 2;
            else if (fairies.Count <= 10)
                Fairy.moveSpeed = 0.0125f;
            else
                Fairy.moveSpeed = 0.0125f / 2;

            if (fairies.Count == 0)
            {
                checkBeforeNextRound();
            }
        }
        else if (isBonusRound)
        {
            if (bonusMotherFairy == 0) {
                checkBeforeNextRound();
            }
        }
        else if (isBossRound)
        {
            if (ReisenManager.isDead)
            {
                if (!isWon)
                {
                    isWon = true;
                    Invoke("setWinText", 3f);
                }
            }
        }
    }

    void checkBeforeNextRound()
    {
        if (!checkForBullet)
        {
            checkForBullet = true;
            leftoverBullets = GameObject.FindGameObjectsWithTag("Bullet");
            leftoverItems = GameObject.FindGameObjectsWithTag("Item");
            leftoverMotherFairy = GameObject.FindGameObjectsWithTag("MotherFairy");
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
            foreach (GameObject mf in leftoverMotherFairy)
            {
                if (mf.activeSelf == true)
                {
                    flag = false;
                    break;
                }
            }
            if (flag == true && GameObject.FindGameObjectWithTag("MFItem")) flag = false;

            if (flag == true && !alreadyLoadNextLevel)
            {
                alreadyLoadNextLevel = true;
                StartCoroutine(LoadAsycncronously(SceneManager.GetActiveScene().buildIndex + 1, 0.75f));
            }
        }
    }

    IEnumerator LoadAsycncronously(int sceneIndex, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 0;
        loadingScreen.SetActive(true);
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1;

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        
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

    IEnumerator SpawnMotherFairy(float delay)
    {
        yield return new WaitForSeconds(delay);
        while (fairies.Count > 0)
        {
            char dir = Random.Range(1, 100) % 2 == 0 ? 'l' : 'r';
            _mfPool.InstantiateNewMotherFairy(dir);
            yield return new WaitForSeconds(Random.Range(10f, 20f));
        }
    }

    IEnumerator SpawnMotherFairyBonus(float delay)
    {
        yield return new WaitForSeconds(delay);
        while (bonusMotherFairy > 0)
        {
            char dir = Random.Range(1, 100) % 2 == 0 ? 'l' : 'r';
            _mfPool.InstantiateNewMotherFairy(dir, Random.Range(-1.3f, 2.6f), Random.Range(0.0125f, 0.0375f));
            yield return new WaitForSeconds(Random.Range(0.5f, 5f));
            bonusMotherFairy--;
        }
    }

    public void addScore(int _score)
    {
        _currentScore += _score;
        score.text = _currentScore.ToString();
    }
}
