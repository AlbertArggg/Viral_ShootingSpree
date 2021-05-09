using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlameThrower : MonoBehaviour
{
    public GameObject GameGen;
    public bool IsShooting = false;
    public bool active = true;
    public bool alive = true;
    public int Ammo = 500;
    public GameObject SDFX;
    public GameObject Fire;
    public GameObject ammoText;
    public GameObject camMain;
    public GameObject slash;
    public GameObject ammoText1;
    public Text UIAmmo;
    public float damage = 20f;
    public float range = 30f;

    private void Awake()
    {
        GameGen = GameObject.FindGameObjectWithTag("GH");
        UIAmmo = ammoText.GetComponent<Text>();
        GameGen.GetComponent<GameGenHandler>().RAFT(true, 1000);
        StartCoroutine(Shooting());
    }

    public void setActive()
    {
        active = true;
        StartCoroutine(Shooting());
    }

    void Update()
    {
        Ammo = GameGen.GetComponent<GameGenHandler>().SetFLAMEAmmo();
        slash.SetActive(false);
        ammoText1.SetActive(false);
        if (active)
        {
            ShootCheck();
            checkSwitch();
        }
        UI();
    }

    public void checkSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            active = false;
        }
    }
    private void UI()
    {
        if (Ammo <= 0) { Ammo = 0; } 
        UIAmmo.text = Ammo + "";
    }

    private void ShootCheck()
    {
        if (Input.GetButton("Fire1") && alive == true && Ammo >= 0)
        {
            IsShooting = true;
            SDFX.SetActive(true);
            Fire.SetActive(true);
        }

        else
        {
            StartCoroutine(waitTime());
        }
    }

    IEnumerator Shooting()
    {
        if (IsShooting == true)
        {
            Ammo--;
            GameGen.GetComponent<GameGenHandler>().RAFT(false, 1);
            RaycastHit hitInfo;
            if (Physics.Raycast(camMain.transform.position, camMain.transform.forward, out hitInfo, range))
            {
                Debug.Log(hitInfo.transform.name);
                Enemy_Target target = hitInfo.transform.GetComponent<Enemy_Target>();

                if (target != null)
                {
                    target.TakeDamage(damage);
                }
            }
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Shooting());
    }

    IEnumerator waitTime()
    {
        yield return new WaitForSeconds(0.6f);
        SDFX.SetActive(false);
        IsShooting = false;
        Fire.SetActive(false);
    }
}
