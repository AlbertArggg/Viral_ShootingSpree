using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    public GameObject Main;
    public GameObject settings;

    public AudioSource audioSource;
    public AudioClip soundEffectPlay;
    public AudioClip soundEffectQuit;
    public GameObject Title;
    public GameObject subTitle;
    public GameObject PlayButtonText;
    public GameObject quitButtonText;
    private Text UITitle;
    private Text UIsubTitle;
    private Text UIPlay;
    private Text UIQuit;
    public Animator anim;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
        
        UITitle = Title.GetComponent<Text>();
        UIsubTitle = subTitle.GetComponent<Text>();
        UIPlay = PlayButtonText.GetComponent<Text>();
        UIQuit = quitButtonText.GetComponent<Text>();
    }

    public void SettingsMenu()
    {
        settings.SetActive(true);
        Main.SetActive(false);
    }

    public void PlayGame()
    {
        audioSource.Stop();
        anim.SetTrigger("Play");
        UIPlay.GetComponent<Text>().color = Color.red;
        audioSource.PlayOneShot(soundEffectPlay);
        StartCoroutine(WaitThenPlay(true, 8));
    }

    public void QuitGame()
    {
        audioSource.Stop();
        anim.SetTrigger("Play");
        UIQuit.GetComponent<Text>().color = Color.red;
        audioSource.PlayOneShot(soundEffectQuit);
        StartCoroutine(WaitThenPlay(false, 3));

        Debug.Log("Quit button works");
    }

    IEnumerator WaitThenPlay(bool _playingGame, int wait)
    {
        yield return new WaitForSeconds(wait);

        if (_playingGame == true)
        {
            Application.LoadLevel(1);
        }
        else
        {
            Application.Quit();
        }            
    }
}
