using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentManager : MonoBehaviour
{
    private GameManager _gm;
    private AudioSource _as;
    public AudioClip itemPickup;
    public AudioClip powerupReady;

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
    
    public void setPowerup(int type)
    {
        _as.PlayOneShot(powerupReady);
        Character.activePowerup = type;

        if (type == 0) _gm.setPowerupText("Right Swing");
        else if (type == 1) _gm.setPowerupText("Left Swing");
        else if (type == 2) _gm.setPowerupText("Front Hit");
        else if (type == 3) _gm.setPowerupText("V-Strike");
        else if (type == 4) _gm.setPowerupText("Circle Attack");
        else if (type == 5) _gm.setPowerupText("Homing Amulet");
    }
}
