using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class MainMenuManager : MonoBehaviour
{
    public static bool fromMissionComplete;

    public AudioSource musicAS;

    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject creditsMenu;
    public GameObject missionSelectMenu;

    public Slider musicSlider;
    public Slider soundSlider;

    public Button[] missionList;

    public void Awake()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 0.25f);
        soundSlider.value = PlayerPrefs.GetFloat("soundVolume", 0.25f);

        foreach (GameObject f in GameObject.FindGameObjectsWithTag("Fairy"))
        {
            f.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("soundVolume", 0.25f);
        }

        for (int i = 2; i <= 4; i++)
        {
            if((PlayerPrefs.GetInt(("Mission" + (i - 1) + "Complete"), 0) == 1))
            {
                missionList[i - 1].interactable = true;
                missionList[i - 1].gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.black;
            }
            else
            {
                missionList[i - 1].interactable = false;
                missionList[i - 1].gameObject.transform.GetChild(0).GetComponent<Text>().color = new Color(0.6f, 0.7f, 0.6f, 1f);
            }
        }

        if (fromMissionComplete)
        {
            fromMissionComplete = false;
            MissionSelect();
        }
    }

    public void setVolume(string type)
    {
        if(type == "Music")
        {
            PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
            musicAS.volume = musicSlider.value;
        }
        if (type == "Sound")
        {
            PlayerPrefs.SetFloat("soundVolume", soundSlider.value);
            foreach (GameObject f in GameObject.FindGameObjectsWithTag("Fairy"))
            {
                f.GetComponent<AudioSource>().volume = soundSlider.value;
            }
        }
    }

    public void MainMenu()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        missionSelectMenu.SetActive(false);
    }

    public void OptionsMenu()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
        creditsMenu.SetActive(false);
        missionSelectMenu.SetActive(false);
    }

    public void MissionSelect()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        missionSelectMenu.SetActive(true);
    }

    public void CreditsMenu()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(true);
        missionSelectMenu.SetActive(false);
    }

    public void LoadMission(int mission)
    {
        musicAS.Stop();
        GameManager._lives = 5;
        GameManager._currentScore = 0;
        if (mission == 1) SceneManager.LoadScene(1);
        else if (mission == 2) SceneManager.LoadScene(13);
        else if (mission == 3) SceneManager.LoadScene(25);
        else if (mission == 4) SceneManager.LoadScene(37);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}