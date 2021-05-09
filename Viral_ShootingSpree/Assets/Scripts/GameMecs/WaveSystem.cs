using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSystem : MonoBehaviour
{
    public int difficulty;
    public int waveNum = 1;
    public int enemiesPerWaveBase = 10;
    public int enemiesPerWave;
    public int spawnAmount;

    public GameObject[] spawnPoints;
    public GameObject[] enemies;
    public GameObject boss;
    public Animator anim;
    public GameObject waveNumText;
    public Text UIWave;

    public int enemiesSpawned = 0;
    public int enemiesKilled = 0;
    public bool bossIsIn = false;

    private void Start()
    {
        difficulty = FindObjectOfType<Settings>().GetComponent<Settings>().setDif();
        difficulty++;
        enemiesPerWave = enemiesPerWaveBase * difficulty;
        UIWave = waveNumText.GetComponent<Text>();
        StartCoroutine(SpawnEnemies());
        anim.SetBool("Default", true);
        anim.SetTrigger("Animate");
        anim.SetBool("Default", true);
    }

    public void Update()
    {
        UIWave.text = waveNum + "";
        if (enemiesKilled >= spawnAmount || Input.GetKeyDown(KeyCode.P))
        {
            enemiesKilled = 0;
            enemiesSpawned = 0;
            bossIsIn = false;
            StartCoroutine(waitForNextWave());
        }
    }

    public void KillEnemies()
    {
        enemiesKilled++;
    }
    IEnumerator SpawnEnemies()
    {
        spawnAmount = waveNum * enemiesPerWave;
        if (enemiesSpawned < spawnAmount)
        {
            if (waveNum % 5 == 0 && bossIsIn == false)
            {
                Instantiate(boss, spawnPoints[0].transform.position, Quaternion.identity);
                bossIsIn = true;
            }
            enemiesSpawned++;
            Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPoints[Random.Range(0, 8)].transform.position, Quaternion.identity);
            yield return new WaitForSeconds(2f);
            StartCoroutine(SpawnEnemies());
        }
        else
        {
            StartCoroutine(CountDown(waveNum));
        }
    }

    IEnumerator CountDown(int _waveNum)
    {
        yield return new WaitForSeconds(90);
        if (waveNum == _waveNum)
        {
            enemiesKilled = 0;
            enemiesSpawned = 0;
            bossIsIn = false;
            StartCoroutine(waitForNextWave());
        }
    }

    IEnumerator waitForNextWave()
    {
        waveNum++;
        anim.SetBool("Default", false);
        anim.SetTrigger("Animate");
        anim.SetBool("Default", true);
        yield return new WaitForSeconds(5f);
        StartCoroutine(SpawnEnemies());
    }
}
