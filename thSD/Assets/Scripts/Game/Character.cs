using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Character : MonoBehaviour
{
    public SpriteRenderer hitbox_front;
    public SpriteRenderer hitbox_back;
    public SpriteRenderer shield;
    private SpriteRenderer _character;

    public float rotationHitbox = 30f;
    public float moveSpeed = 0.05f;

    public Animator animator;

    public AudioClip shootSound;
    public AudioClip deathSound;
    public AudioClip powerupSound;

    public AudioClip shieldActive;
    public AudioClip shieldBreak;

    private AudioSource _audioSource;
    private float _lastShootTime;
    private ShotPool _poolManager;
    private GameManager _gm;

    private bool isDead = false;
    public bool isInvincible = false;

    public static int activePowerup = -1;
    public List<GameObject> powerupList = new List<GameObject>();

    private Vector3 _initPos;

    public static bool isShielded = false;

    void Start()
    {
        _initPos = transform.position;
        _gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _character = this.GetComponent<SpriteRenderer>();
        _audioSource = this.GetComponent<AudioSource>();
        _poolManager = GameObject.FindGameObjectWithTag("ShotPool").GetComponent<ShotPool>();
        Respawn();

        shield.color = isShielded ? Color.white : Color.clear;
    }

    private void Update()
    {
        hitbox_front.gameObject.transform.Rotate(new Vector3(0, 0, Time.deltaTime * rotationHitbox));
        hitbox_back.gameObject.transform.Rotate(new Vector3(0, 0, -1 * Time.deltaTime * rotationHitbox));
        shield.gameObject.transform.Rotate(new Vector3(0, 0, Time.deltaTime * rotationHitbox));
    }

    public void activateShield()
    {
        isShielded = true;
        shield.DOColor(Color.white, 0.25f);
        _audioSource.PlayOneShot(shieldActive);
    }

    public void deactivateShield()
    {
        isShielded = false;
        shield.DOColor(Color.clear, 0.25f);
        _audioSource.PlayOneShot(shieldBreak);

        isInvincible = true;
        StartCoroutine(invincibleFade());
        Invoke("removeInvincibility", 3f);
    }

    public void Respawn()
    {
        transform.position = _initPos;
        isDead = false;
        hitbox_front.gameObject.SetActive(true);
        hitbox_back.gameObject.SetActive(true);
        isInvincible = true;
        StartCoroutine(invincibleFade());
        Invoke("removeInvincibility", 3f);
    }

    public void removeInvincibility()
    {
        isInvincible = false;
    }

    public void Death()
    {
        _audioSource.PlayOneShot(deathSound);
        _character.DOColor(Color.clear, 0.05f);
        hitbox_front.gameObject.SetActive(false);
        hitbox_back.gameObject.SetActive(false);
        GameManager.powerupAmount = 0;
        _gm.setPowerupIcon();
        if (!isDead)
        {
            isDead = true;
            _gm.setLives(_gm.getLives() - 1);
            Invoke("Respawn", 3f);
        }
    }

    IEnumerator invincibleFade()
    {
        while (isInvincible)
        {
            _character.DOColor(new Color(1, 1, 1, 0.125f), 0.125f);
            yield return new WaitForSeconds(0.125f);
            _character.DOColor(new Color(1, 1, 1, 0.5f), 0.125f);
            yield return new WaitForSeconds(0.125f);
        }
        _character.color = Color.white;
    }

    void FixedUpdate()
    {
        if (isDead) return;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            hitbox_front.DOColor(Color.white, 0.25f);
            hitbox_back.DOColor(new Color(1,1,1,0.375f), 0.25f);
            if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x >= -4)
            {
                transform.position += (Vector3.left * moveSpeed);
                animator.SetBool("isMoving", true);
                _character.flipX = false;
            }
            else if (Input.GetKey(KeyCode.RightArrow) && transform.position.x <= 4)
            {
                transform.position += (Vector3.right * moveSpeed);
                animator.SetBool("isMoving", true);
                _character.flipX = true;
            }
            else animator.SetBool("isMoving", false);
        }
        else
        {
            hitbox_front.DOColor(Color.clear, 0.25f);
            hitbox_back.DOColor(Color.clear, 0.25f);
            if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x >= -4)
            {
                transform.position += (Vector3.left * moveSpeed * 2);
                animator.SetBool("isMoving", true);
                _character.flipX = false;
            }
            else if (Input.GetKey(KeyCode.RightArrow) && transform.position.x <= 4)
            {
                transform.position += (Vector3.right * moveSpeed * 2);
                animator.SetBool("isMoving", true);
                _character.flipX = true;
            }
            else animator.SetBool("isMoving", false);
        }

        //shoot
        if (Input.GetKey(KeyCode.Z) && (Time.time - _lastShootTime > GameManager.shootFrequency))
        {
            _lastShootTime = Time.time;
            _poolManager.InstantiateNewBullet();
            _audioSource.PlayOneShot(shootSound);
        }

        //powerup
        if (Input.GetKey(KeyCode.X) && activePowerup != -1)
        {
            Instantiate(powerupList[activePowerup], transform.position, Quaternion.identity);
            activePowerup = -1;
            _audioSource.PlayOneShot(powerupSound);
            GameManager.powerupAmount = 0;
            _gm.setPowerupIcon();
        }
    }
}
