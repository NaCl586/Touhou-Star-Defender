using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentManager : MonoBehaviour
{
    private GameManager _gm;
    private AudioSource _as;
    public AudioClip itemPickup;
    public AudioClip powerupReady;

    public AudioClip extend;
    public AudioClip bonus;
    public AudioClip doubleShot;
    public AudioClip timeSlow;

    void Start()
    {
        _gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _as = this.GetComponent<AudioSource>();
    }

    public void PickupPowerup(int type)
    {
        _as.PlayOneShot(itemPickup);
        _gm.addScore(50);
        if (GameManager.powerupAmount == 4) return;
        if(GameManager.powerupType == type)
        {
            GameManager.powerupAmount++;
            if (GameManager.powerupAmount == 4)
            {
                setPowerup(type);
            }
        }
        else
        {
            GameManager.powerupType = type;
            GameManager.powerupAmount = 1;
        }
        _gm.setPowerupIcon();
    }

    public void PickupItem(int type)
    {
        //extend
        if (type == -1)
        {
            _as.PlayOneShot(extend);
            _gm.setLives(_gm.getLives() + 1);
            _gm.addScore(50);
        }
        //bonus pts
        else if (type == -2)
        {
            _as.PlayOneShot(bonus);
            _gm.addScore(1000);
        }
        //double shot
        else if (type == -3)
        {
            _as.PlayOneShot(doubleShot);
            GameManager.doubleShotisActive = true;
            GameManager.doubleShotStartTime = Time.time;
            _gm.doubleShotSlider.gameObject.SetActive(true);
            _gm.addScore(50);
        }
        //time slow
        else if (type == -4)
        {
            _as.PlayOneShot(timeSlow);
            GameManager.timeSlowisActive = true;
            GameManager.timeSlowStartTime = Time.time;
            _gm.timeSlowSlider.gameObject.SetActive(true);
            _gm.addScore(50);
        }
        //shield
        else if (type == -5)
        {
            transform.parent.GetComponent<Character>().activateShield();
            _gm.addScore(50);
        }
        setPowerup(type);
    }


    public void setPowerup(int type)
    {
        _as.PlayOneShot(powerupReady);
        if(type >= 0) Character.activePowerup = type;

        //powerup
        if (type == 0) _gm.setPowerupText("Right Swing");
        else if (type == 1) _gm.setPowerupText("Left Swing");
        else if (type == 2) _gm.setPowerupText("Front Hit");
        else if (type == 3) _gm.setPowerupText("V-Strike");
        else if (type == 4) _gm.setPowerupText("Circle Attack");
        else if (type == 5) _gm.setPowerupText("Homing Amulet");

        //item
        else if (type == -1) _gm.setPowerupText("Extend");
        else if (type == -2) _gm.setPowerupText("+1000 Points");
        else if (type == -3) _gm.setPowerupText("Faster Shot");
        else if (type == -4) _gm.setPowerupText("Time Slow");
        else if (type == -5) _gm.setPowerupText("Shield");
    }
}
