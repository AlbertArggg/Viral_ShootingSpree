using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public bool isARAmmo;
    public bool isMiniGunAmmo;
    public bool isFlameAmmo;
    public bool isGrenadeAmmo;

    public GameObject GameGen;
    public GameObject Player;
    public GameObject[] spawns;

    private void Awake()
    {
        GameGen = GameObject.FindGameObjectWithTag("GH");
        Player = GameObject.FindGameObjectWithTag("player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            if (isARAmmo)
            {
                GameGen.GetComponent<GameGenHandler>().RAM4(true,500);
                GameGen.GetComponent<GameGenHandler>().RAPLR(true,500);
            }

            else if (isMiniGunAmmo)
            {
                GameGen.GetComponent<GameGenHandler>().RAMNGN(true,500);
            }

            else if (isFlameAmmo)
            {
                GameGen.GetComponent<GameGenHandler>().RAFT(true,500);
            }
            else if (isGrenadeAmmo)
            {
                Player.GetComponent<TossGrenade>().AddAmmo();
            }

            Destroy(gameObject);
        }
    }
}
