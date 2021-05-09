using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Automatic : MonoBehaviour
{
    public bool active = false;
    public bool m4 = false;
    public bool plr = false;
    public bool alive = true;
    public bool reloading = false;
    public bool AimAbility = false;
    public bool canAim = false;
    public bool dualWeild = false;

    public Animator anim;
    public AudioSource SDFX;
    public AudioClip gunshot;
    public AudioClip reload;
    public GameObject camHolder;
    public Camera camMain;
    public ParticleSystem MuzzleFlash;
    public GameObject Impact;
    public GameObject ammoText;
    public GameObject ammoTotalText;
    public GameObject cursor;
    public GameObject slash;
    public GameObject GameGen;
    private Text UIAmmo;
    private Text UItotalAmmo;


    public int MagCap = 40;
    public int totalAmmo = 300;
    public int totalAmmoCap = 300;
    public int reloadTime = 3;
    public float damage = 34f;
    public float range = 100f;
    public float fireRate = 0.1f;
    public float impactForce = 30f;

    private int mag;
    private float FR = 0.3f;

    public void setActive()
    {
        active = true;
    }

    private void Awake()
    {
        GameGen = GameObject.FindGameObjectWithTag("GH");
        UIAmmo = ammoText.GetComponent<Text>();
        UItotalAmmo = ammoTotalText.GetComponent<Text>();
        mag = MagCap;
        totalAmmo = totalAmmoCap;

        if (m4) { GameGen.GetComponent<GameGenHandler>().RAM4(true, 500); }
        else if (plr) { GameGen.GetComponent<GameGenHandler>().RAPLR(true, 500); }
    }
    void Update()
    {
        ammoText.SetActive(true);
        ammoTotalText.SetActive(true);
        slash.SetActive(true);

        if (m4) { totalAmmo = GameGen.GetComponent<GameGenHandler>().SetM4Ammo(); }
        else if (plr) { totalAmmo = GameGen.GetComponent<GameGenHandler>().SetPLRAmmo(); }

        if (active)
        {
            HandleAim();
            AmmoUI();
            ShootingCheck();
            ReloadCheck();
            checkSwitch();
        }
        
    }

    private void checkSwitch()
    {
        if (m4)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4))
            {
                anim.SetTrigger("forceExit");
                reloading = false;
                active = false;
            }
        }
        if (plr)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha4))
            {
                anim.SetTrigger("forceExit");
                reloading = false;
                active = false;
            }
        }
    }

    private void HandleAim()
    {
        if (AimAbility == true)
        {
            if (Input.GetButton("Fire2") && canAim == true)
            {
                if (dualWeild == false)
                {
                    cursor.SetActive(false);
                }
                anim.SetBool("Aiming", true);
            }
            else
            {
                cursor.SetActive(true);
                anim.SetBool("Aiming", false);
            }
        }
    }

    private void AmmoUI()
    {
        UIAmmo.text = mag + "";
        UItotalAmmo.text = totalAmmo + "";

        if (mag <= MagCap / 5)
        {
            UIAmmo.GetComponent<Text>().color = Color.red;
        }

        else
        {
            UIAmmo.GetComponent<Text>().color = Color.white;
        }

        if (totalAmmo <= MagCap)
        {
            UItotalAmmo.GetComponent<Text>().color = Color.red;
        }

        else
        {
            UItotalAmmo.GetComponent<Text>().color = Color.white;
        }
    }
    public void ShootingCheck()
    {
        FR -= Time.deltaTime;
        Shoot();
    }
    private void Shoot()
    {
        if (alive == true && reloading == false && mag > 0 && Input.GetButton("Fire1") && FR <= 0)
        {
            FR = fireRate;
            this.gameObject.GetComponent<WeaponRecoil>().RecoilHandling();
            camHolder.GetComponent<CamRecoil>().HandleCamRecoil();
            if (dualWeild == true) { mag--; }
            mag--;
            GameGen.GetComponent<GameGenHandler>().addFired();
            RaycastHit hitInfo;
            MuzzleFlash.Play();
            SDFX.PlayOneShot(gunshot);

            if (Physics.Raycast(camMain.transform.position, camMain.transform.forward, out hitInfo, range))
            {
                Debug.Log(hitInfo.transform.name);
                Enemy_Target target = hitInfo.transform.GetComponent<Enemy_Target>();

                if (target != null)
                {
                    target.TakeDamage(damage);
                    GameGen.GetComponent<GameGenHandler>().addhits();
                    GameObject impactEffect = Instantiate(Impact, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                    Destroy(impactEffect, 1f);
                }

                if (hitInfo.rigidbody != null)
                {
                    hitInfo.rigidbody.AddForce(-hitInfo.normal * impactForce);
                }

            }
        }
    }
    private void ReloadCheck()
    {
        if (Input.GetKeyDown(KeyCode.R) || mag <= 0)
        {
            if (alive == false)
            {
                return;
            }

            else if (reloading == false && totalAmmo <= 0)
            {
                // something
            }
            else if (reloading == false && mag != MagCap)
            {
                canAim = false;
                anim.SetBool("Aiming", false);
                reloading = true;
                StartCoroutine(Reload());
                reloadBool(true);
            }
        }
    }
    IEnumerator Reload()
    {
        SDFX.PlayOneShot(reload);
        TriggerReload();
        yield return new WaitForSeconds(reloadTime);

        if (totalAmmo >= MagCap)
        {
            int remainder = MagCap - mag;
            totalAmmo -= remainder;
            if (m4)
            {
                GameGen.GetComponent<GameGenHandler>().RAM4(false, totalAmmo);
            }
            else if (plr)
            {
                GameGen.GetComponent<GameGenHandler>().RAPLR(false, totalAmmo);
            }
            mag = MagCap;
        }
        else
        {
            mag = totalAmmo;
            totalAmmo = 0;
            if (m4)
            {
                GameGen.GetComponent<GameGenHandler>().RAM4(false, 0);
            }
            else if (plr)
            {
                GameGen.GetComponent<GameGenHandler>().RAPLR(false, 0);
            }
        }

        reloading = false;
        canAim = true;
        reloadBool(false);
    }

    public void reloadBool(bool _reload)
    {
        reloading = _reload;
        anim.SetBool("IsReloading", _reload);
    }

    public void TriggerReload()
    {
        anim.SetTrigger("Reload");
        Debug.Log("triggered Reload");
    }
    public void Die()
    {
        alive = false;
    }

    public void AddAmmo()
    {
        totalAmmo += totalAmmoCap;
    }
}
