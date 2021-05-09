using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] [Range(100, 200)] float PlayerMaxHealth = 100;

    public bool isDead = false;
    public GameObject livingCanvas;
    public Slider healthBar;
    private float playerHealth;
    public GameObject player;
    public GameObject deathScene;
    public GameObject cam;
    public GameObject Dam;
    public GameObject guns;
    public GameObject deathMenu;
    public Animator DamAnim;
    public Animator anim;
    public GameObject musicPlayer;
    public AudioSource sdfxPlayer;
    public AudioClip Death;
    public AudioClip damClip;

    private void Start()
    {
        playerHealth = PlayerMaxHealth;
    }

    public void Update()
    {
        healthBar.value = playerHealth;

        // remove before publishing
        if (Input.GetKeyDown(KeyCode.U))
        {
            subHealth(10);
            Debug.Log(playerHealth);
            Debug.Log(healthBar);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            addHealth(10);
            Debug.Log(playerHealth);
            Debug.Log(healthBar);
        }
        // remove before publishing
    }

    public void addHealth(float _healthAmount)
    {
        playerHealth += _healthAmount;
        if (playerHealth >= PlayerMaxHealth)
        {
            playerHealth = PlayerMaxHealth;
        }
    }

    public void subHealth(float _healthAmount)
    {
        if(isDead == false)
        {
            playerHealth -= _healthAmount;
            Dam.SetActive(true);
            DamAnim.SetTrigger("Dam");
            StartCoroutine(disableDam());
            if (playerHealth <= 0)
            {
                isDead = true;
                guns.SetActive(false);
                livingCanvas.SetActive(false);
                anim.SetTrigger("Dead");
                player.GetComponent<PlayerMovement>().Die();
                cam.GetComponent<Rotation_FPS>().Die();
                StartCoroutine(Die());
            }
            else
            {
                sdfxPlayer.PlayOneShot(damClip);
            }
        }

    }

    IEnumerator disableDam()
    {
        yield return new WaitForSeconds(0.6f);
        Dam.SetActive(false);
    } 

    IEnumerator Die()
    {
        sdfxPlayer.PlayOneShot(Death);
        musicPlayer.SetActive(false);
        yield return new WaitForSeconds(5); 
        deathScene.SetActive(true);
        deathMenu.GetComponent<GameOver>().calculations();
    }

}
