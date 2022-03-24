using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;
using DG.Tweening;

public class Fairy : MonoBehaviour
{
    private GameManager _gm;
    private int HP;
    public static float moveSpeed = 0.00625f;

    public FairyData fairyData;
    private int shotType = 0;
    private int shotCount = 20;

    private AudioSource _as;
    private GameObject _dpInstance;
    private BulletPool _poolManager;

    public static char movingStates;

    void Start()
    {
        _gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _poolManager = GameObject.FindGameObjectWithTag("BulletPool").GetComponent<BulletPool>();
        _as = this.GetComponent<AudioSource>();

        gameObject.name = fairyData._name;
        this.HP = fairyData.HP;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = fairyData.sprite;
        this.gameObject.GetComponent<Animator>().runtimeAnimatorController = fairyData.animator;

        shotType = fairyData.shotType;
        shotCount = fairyData.shotCount;

        movingStates = 'r';
    }

    void FixedUpdate()
    {
        if (movingStates == 'r')
            transform.position += Vector3.right * moveSpeed;
        else if(movingStates == 'l')
            transform.position += Vector3.left * moveSpeed;

        if (_dpInstance) _dpInstance.transform.position = transform.position;
    }

    public void reduceHP()
    {
        HP--;
        this.gameObject.GetComponent<SpriteRenderer>().DOColor(Color.blue, 0.125f).OnComplete(() => {
            this.gameObject.GetComponent<SpriteRenderer>().DOColor(Color.white, 0.125f);
        });
        if (HP == 0)
        {
            FairyDeath();
        }   
    }

    public void FairyDeath()
    {
        _as.PlayOneShot(fairyData.deathSound);

        _dpInstance = Instantiate(fairyData.deathEffect, transform.position, Quaternion.identity);
        Destroy(_dpInstance, _dpInstance.GetComponent<ParticleSystem>().main.duration);

        Instantiate(fairyData.itemDrops, transform.position, Quaternion.identity);
        
        this.gameObject.GetComponent<SpriteRenderer>().DOColor(Color.clear, 0.25f).OnComplete(() => {
            _gm.fairies.Remove(this.GetComponent<Fairy>());
            _gm.addScore(100);
            Destroy(gameObject);
        });
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FairyBorder"))
        {
            _gm.moveDown();
            StartCoroutine(temporaryInactive(other.GetComponent<BoxCollider2D>()));
        }
        if (other.CompareTag("KillBorder"))
        {
            _gm.bumpUp();
        }
    }

    IEnumerator temporaryInactive(BoxCollider2D border)
    {
        border.enabled = false;
        yield return new WaitForSeconds(1f);
        border.enabled = true;
        GameManager.isChangingDirection = false;
    }

    public void Attack()
    {
        //fairy merah dan fairy biru
        if (shotType == 0)
        {
            shotCount = Random.Range(5, 16);
            _as.PlayOneShot(fairyData.bulletShootSound);
            for (int i = 0; i < shotCount; i++)
            {
                GameObject bullets = _poolManager.InstantiateNewBullet();
                bullets.GetComponent<SpriteRenderer>().sprite = fairyData.bullet;
                bullets.transform.position = transform.position;
                bullets.SetActive(false);
                bullets.GetComponent<Bullet>().bulletSpeed = Random.Range(0.005f, 0.01f);
                bullets.GetComponent<Bullet>().homing = (Random.Range(0, 100) % 2 == 1);
                bullets.GetComponent<Bullet>().direction = (i - shotCount / 2) * (float)(180 / shotCount);
                bullets.SetActive(true);
            }
        }
        //fairy putih
        else if (shotType == 1)
        {
            shotCount = Random.Range(5, 16);
            _as.PlayOneShot(fairyData.bulletShootSound);
            float bulletSpeed = Random.Range(0.005f, 0.01f);
            float offset = (360 / shotCount) / 2;
            for (int i = 0; i < shotCount; i++)
            {
                GameObject bullets = _poolManager.InstantiateNewBullet();
                bullets.GetComponent<SpriteRenderer>().sprite = fairyData.bullet;
                bullets.transform.position = transform.position;
                bullets.SetActive(false);
                bullets.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
                bullets.GetComponent<Bullet>().homing = true;
                bullets.GetComponent<Bullet>().direction = (i * (float)(360 / shotCount)) + offset;
                bullets.SetActive(true);
            }
        }

        //fairy kuning
        else if (shotType == 2)
        {
            shotCount = Random.Range(3, 10);
            float bulletSpeed = Random.Range(0.005f, 0.01f);
            StartCoroutine(yellowFairyShots(shotCount, bulletSpeed, Random.Range(3,9)));
        }
    }

    IEnumerator yellowFairyShots(int shotCount, float bulletSpeed, int count)
    {
        float waitTime = Random.Range(0.125f, 0.5f);
        for(int rep = 0; rep < count; rep++)
        {
            _as.PlayOneShot(fairyData.bulletShootSound);
            for (int i = 0; i < shotCount; i++)
            {
                GameObject bullets = _poolManager.InstantiateNewBullet();
                bullets.GetComponent<SpriteRenderer>().sprite = fairyData.bullet;
                bullets.transform.position = transform.position;
                bullets.SetActive(false);
                bullets.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
                bullets.GetComponent<Bullet>().homing = false;
                bullets.GetComponent<Bullet>().direction = ((i - shotCount / 2) * (float)(180 / shotCount));
                bullets.SetActive(true);
            }
            yield return new WaitForSeconds(waitTime);
        }
    }
}
