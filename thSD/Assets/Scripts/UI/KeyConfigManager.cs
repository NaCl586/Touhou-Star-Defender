using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyConfigManager : MonoBehaviour
{
    [Header("References")]
    public Button[] buttons;
    public Text moveLeftText;
    public Text moveRightText;
    public Text shootText;
    public Text focusText;
    public Text powerupText;

    //playerprefs strings
    private string moveLeft;
    private string moveRight;
    private string shoot;
    private string focus;
    private string powerup;

    //keybind indexes
    private int[] values;

    //remapping
    string tempKey;
    string keyName;
    Text tempText;
    bool bindingProcess;
    bool prompt;

    private void Awake()
    {
        values = (int[])System.Enum.GetValues(typeof(KeyCode));
    }

    void Start()
    {
        moveLeft = PlayerPrefs.GetString("moveLeft", "Left");
        moveRight = PlayerPrefs.GetString("moveRight", "Right");
        shoot = PlayerPrefs.GetString("shoot", "Z");
        focus = PlayerPrefs.GetString("focus", "LShift");
        powerup = PlayerPrefs.GetString("powerup", "X");

        setTexts();
    }

    public void remap(Text text)
    {
        setTexts();
        tempKey = text.name;
        tempText = text;
        prompt = false;
        bindingProcess = true;
        enableButtons(false);
    }

    public void enableButtons(bool enable)
    {
        foreach (Button button in buttons)
        {
            button.interactable = enable;
        }
    }

    void Update()
    {
        if (bindingProcess)
        {
            if (!prompt)
                tempText.text = "<color=yellow>Mapping</color>";
            else
                tempText.text = "<color=red>Already used</color>";

            bool flag = false;
            for (int i = 0; i < values.Length; i++)
            {
                if (Input.GetKey((KeyCode)values[i]))
                {
                    keyName = Utils.keycodeToChar((KeyCode)values[i]);
                    flag = true;
                    break;
                }
            }

            if (flag)
            {
                //escape cancel binding process
                if (keyName == "Escape")
                {
                    setTexts();
                    bindingProcess = false;
                    StartCoroutine(waitUntilEnable());
                }
                //cek jika ada kesamaan dengan yang sudah ada
                else if ((keyName == moveLeft && tempKey != "MoveLeft_Text") ||
                       (keyName == moveRight && tempKey != "MoveRight_Text") ||
                       (keyName == focus && tempKey != "Focus_Text") ||
                       (keyName == powerup && tempKey != "Powerup_Text") ||
                       (keyName == shoot && tempKey != "Shoot_Text"))
                {
                    prompt = true;
                }
                else
                {
                    if (tempKey == "Focus_Text")
                    {
                        focus = keyName;
                        PlayerPrefs.SetString("focus", keyName);
                    }
                    else if (tempKey == "Powerup_Text")
                    {
                        powerup = keyName;
                        PlayerPrefs.SetString("powerup", keyName);
                    }
                    else if (tempKey == "MoveLeft_Text")
                    {
                        moveLeft = keyName;
                        PlayerPrefs.SetString("moveLeft", keyName);
                    }
                    else if (tempKey == "MoveRight_Text")
                    {
                        moveRight = keyName;
                        PlayerPrefs.SetString("moveRight", keyName);
                    }
                    else if (tempKey == "Shoot_Text")
                    {
                        shoot = keyName;
                        PlayerPrefs.SetString("shoot", keyName);
                    }
                    tempText.text = keyName;
                    bindingProcess = false;
                    StartCoroutine(waitUntilEnable());
                }
            }
        }
    }

    IEnumerator waitUntilEnable()
    {
        yield return new WaitForSeconds(0.125f);
        enableButtons(true);
    }

    void setTexts()
    {
        moveLeftText.text = moveLeft;
        moveRightText.text = moveRight;
        shootText.text = shoot;
        focusText.text = focus;
        powerupText.text = powerup;
    }

    public void restoreDefaults()
    {
        PlayerPrefs.SetString("moveLeft", "Left");
        PlayerPrefs.SetString("moveRight", "Right");
        PlayerPrefs.SetString("shoot", "Z");
        PlayerPrefs.SetString("focus", "LShift");
        PlayerPrefs.SetString("powerup", "X");

        moveLeft = "Left";
        moveRight = "Right";
        shoot = "Z";
        focus = "LShift";
        powerup = "X";

        setTexts();
    }
}
