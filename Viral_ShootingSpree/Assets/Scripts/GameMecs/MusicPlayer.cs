using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource musicPlayer;
    public float Vol;


    private void Awake()
    {
        musicPlayer = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Vol = FindObjectOfType<Settings>().GetComponent<Settings>().setVol();
        musicPlayer.volume = Vol;
    }
}
