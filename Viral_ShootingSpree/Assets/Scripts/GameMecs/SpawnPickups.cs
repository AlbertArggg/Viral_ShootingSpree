using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickups : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject[] Pickups;

    private void Start()
    {
        StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(30);
        Instantiate(Pickups[Random.Range(0, Pickups.Length)], spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position, Quaternion.identity);
        StartCoroutine(Spawn());
    }
}
