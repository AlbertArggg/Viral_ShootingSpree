using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public bool inGame = false;

    public int difficulty = 0;
    public float Vol = 1;
    public float Sensitivity = 1000;
    public float AimSensitivity = 250;

    public void Awake()
    {
        GameObject[] settings = GameObject.FindGameObjectsWithTag("settings");
        if (settings.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void inGameBool(bool _bl)
    {
        inGame = _bl;
    }

    public void updateSettings(int _dif, float _vol, float _sens, float _aimSens)
    {
        difficulty = _dif;
        Vol = _vol;
        Sensitivity = _sens;
        AimSensitivity = _aimSens;
    }

    public int setDif()
    {
        return difficulty;
    }

    public float setVol()
    {
        return Vol;
    }

    public float setSens()
    {
        return Sensitivity;
    }

    public float setAimSens()
    {
        return AimSensitivity;
    }
}
