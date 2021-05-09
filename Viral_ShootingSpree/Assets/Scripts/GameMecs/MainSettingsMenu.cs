using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSettingsMenu : MonoBehaviour
{
    public GameObject settings;
    public GameObject Main;

    public float BaseMus = 1;
    public float BaseSens = 1000;
    public float BaseAim = 250;

    public float musicVol = 1;
    public float Sensitivity = 1000;
    public float AimSensitivity = 250;

    public Slider musicSlider;
    public Slider Sens;
    public Slider aimSens;

    public Text musText;
    public Text SensText;
    public Text AimSensText;
    public Text difficultyText;

    public int difficulty = 0;

    public void Awake()
    {
        getData();
    }

    public void getData()
    {
        musicVol = FindObjectOfType<Settings>().GetComponent<Settings>().setVol();
        Sensitivity = FindObjectOfType<Settings>().GetComponent<Settings>().setSens();
        AimSensitivity = FindObjectOfType<Settings>().GetComponent<Settings>().setAimSens();
        difficulty = FindObjectOfType<Settings>().GetComponent<Settings>().setDif();
        updateDif();
    }

    public void MainMenu()
    {
        Main.SetActive(true);
        settings.SetActive(false);
    }

    public void increaseDifficulty()
    {
        difficulty++;
        if (difficulty == 3)
        {
            difficulty = 0;
        }
        updateDif();
    }

    public void decreaseDifficulty()
    {
        difficulty--;
        if (difficulty == -1)
        {
            difficulty = 2;
        }
        updateDif();
    }

    private void updateDif()
    {
        if (difficulty == 0)
        {
            difficultyText.text = "easy";
        }
        else if (difficulty == 1)
        {
            difficultyText.text = "medium";
        }
        else 
        {
            difficultyText.text = "hard";
        }
    }

    public void resetSettings()
    {
        musicVol = BaseMus;
        Sensitivity = BaseSens;
        AimSensitivity = BaseAim;
    }

    public void setVolume(float volume)
    {
        musicVol = volume;
    }

    public void setSensitivity(float sensitivity)
    {
        Sensitivity = sensitivity;
    }

    public void setAimSensitivity(float aimSensitivity)
    {
        AimSensitivity = aimSensitivity;
    }

    public void Update()
    {
        musicSlider.value = musicVol;
        Sens.value = Sensitivity;
        aimSens.value = AimSensitivity;
        musText.text = (musicVol * 100).ToString("F0");
        SensText.text = Sensitivity.ToString("F0");
        AimSensText.text = AimSensitivity.ToString("F0");
        FindObjectOfType<Settings>().GetComponent<Settings>().updateSettings(difficulty, musicVol, Sensitivity, AimSensitivity);
    }
}
