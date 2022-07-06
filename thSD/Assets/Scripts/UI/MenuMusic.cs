using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    void Awake()
    {
        this.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("musicVolume", 0.25f);
    }
}
