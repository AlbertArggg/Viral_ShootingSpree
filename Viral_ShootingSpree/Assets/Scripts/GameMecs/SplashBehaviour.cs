using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashBehaviour : MonoBehaviour
{
    public GameObject[] bars;
    public AudioSource sdfx;
    public AudioClip heartBeat;
    int i = 0;
    private void Start()
    {
        StartCoroutine(Loading());
    }

    IEnumerator Loading()
    {
        yield return new WaitForSeconds(1.5f);
        bars[i].SetActive(true);
        i++;
        sdfx.PlayOneShot(heartBeat);

        if (i == 10)
        {
            Application.LoadLevel(2);
        }
        StartCoroutine(Loading());
    }
}
