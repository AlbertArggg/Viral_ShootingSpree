using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGun : MonoBehaviour
{
    public GameObject GameGen;
    private const float HeatT = 0.1f;
    private const float CoolT = 0.6f;
    public bool alive = true;
    public bool reloading = false;
    public bool alreadyplaying = false;
    bool Shooting = false;

    public GameObject camHolder;
    public Camera camMain;
    public Animator anim;
    public Material barellMat;
    public bool active = true;

    public GameObject slashUI;
    public GameObject ammoTotalText;
    private Text UItotalAmmo;
    public GameObject ammoText;
    private Text UIAmmo;

    public ParticleSystem MuzzleFlash;
    public GameObject Impact;
    public GameObject barrel;
    public AudioSource SDFX;
    public GameObject SDFX2;
    public AudioClip gunshot;
    public AudioClip Spinning;
    public AudioClip SpinningExit;

    public float impactForce = 30f;
    public int totalAmmo = 1000;
    public float damage = 10f;
    public float range = 100f;

    private Color currColor = Color.black;

    private void Awake()
    {
        SDFX2.SetActive(false);
        GameGen = GameObject.FindGameObjectWithTag("GH");
        UItotalAmmo = ammoTotalText.GetComponent<Text>();
        UIAmmo = ammoText.GetComponent<Text>();
        GameGen.GetComponent<GameGenHandler>().RAMNGN(true, 1000);
    }
    void Update()
    {
        totalAmmo = GameGen.GetComponent<GameGenHandler>().SetMINIGUNAmmo();
        AmmoUI();
        if (active != false)
        {
            ShootMiniGun();
            checkSwitch();
        }
    }

    public void setActive()
    {
        active = true;
    }

    public void checkSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4))
        {
            anim.SetTrigger("forceExit");
            active = false;
        }
    }

    public void AmmoUI()
    {
        ammoText.SetActive(true);
        ammoTotalText.SetActive(true);
        slashUI.SetActive(false);
        UItotalAmmo.text = totalAmmo + "";
        UIAmmo.text = "";

        if (totalAmmo <= 100)
        {
            UItotalAmmo.GetComponent<Text>().color = Color.red;
        }

        else
        {
            UItotalAmmo.GetComponent<Text>().color = Color.white;
        }
    }

    private void ShootMiniGun()
    {
        if (alive == true && reloading == false && totalAmmo > 0 && Input.GetButton("Fire1") && Shooting == false)
        {
            Shooting = true;
            StartCoroutine(WarmingUp());
        }

        else if (alive == true && reloading == false && totalAmmo > 0 && Input.GetButton("Fire1"))
        {
            Shooting = true;
            barellHeat(true);
        }
        else
        {
            Shooting = false;
            anim.SetBool("warmingUp", false);
            anim.SetBool("Shooting", false);
            barellHeat(false);
            SDFX2.SetActive(false);
        }
    }

    private void barellHeat(bool increase)
    {
        if (increase == true)
        {
            StartCoroutine(WaitForWarming());
        }
        else
        {
            currColor = Color.Lerp(currColor, Color.black, CoolT * Time.deltaTime);
        }
        barellMat.SetColor("_EmissionColor", currColor);
    }

    IEnumerator WaitForWarming()
    {
        yield return new WaitForSeconds(2);
        currColor = Color.Lerp(currColor, Color.red, HeatT * Time.deltaTime);
        barellMat.SetColor("_EmissionColor", currColor);
    }
    IEnumerator WarmingUp()
    {
        SDFX.PlayOneShot(Spinning, 2f);
        anim.SetBool("warmingUp", true);
        yield return new WaitForSeconds(1);
        anim.SetBool("Shooting", true);
    }

    public void MiniGunActive()
    {
        handleAudio(true);
        MuzzleFlash.Play();
        this.gameObject.GetComponent<WeaponRecoil>().RecoilHandling();
        camHolder.GetComponent<CamRecoil>().HandleCamRecoil();
        totalAmmo--;
        GameGen.GetComponent<GameGenHandler>().addFired();
        GameGen.GetComponent<GameGenHandler>().RAMNGN(false, 1);
        if (totalAmmo <= 0) { totalAmmo = 0; }
        RaycastHit hitInfo;


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

    private void handleAudio(bool _shooting)
    {
        if (Shooting == true)
        {
            SDFX2.SetActive(true);
        }
        else
        {
            SDFX2.SetActive(false);
            if (alreadyplaying == false)
            {
                SDFX.PlayOneShot(SpinningExit);
                alreadyplaying = true;
                StartCoroutine(wait());
            }
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(2);
        alreadyplaying = false;
    }
    public void AddAmmo()
    {
        totalAmmo += 1000;
    }
}
