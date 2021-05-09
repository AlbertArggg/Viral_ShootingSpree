using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TossGrenade : MonoBehaviour
{
    bool alive = true;
    public Camera           camMain;
    public AudioSource SDFX;
    public GameObject grenadesText;
    public Rigidbody grenade;
    private Text UIGrenades;
    public Transform grenadeInstantiatePoint;
    public AudioClip grenadeToss;
    public int currGrenades;
    public int nadeCooldown = 2;
    bool throwable = true;

    [SerializeField] [Range(1, 5)] int maxGrenades = 3;
    [SerializeField] [Range(30f, 200f)] float TossPower = 100f;

    private void Awake()
    {
        UIGrenades = grenadesText.GetComponent<Text>();
        currGrenades = maxGrenades;
    }
    void Update()
    {
        UiHandler();
        GrenadeCheck();
    }

    private void UiHandler()
    {
        UIGrenades.text = currGrenades + "";

        if (currGrenades <= 0)
        {
            UIGrenades.GetComponent<Text>().color = Color.red;
        }

        else
        {
            UIGrenades.GetComponent<Text>().color = Color.white;
        }
    }

    private void GrenadeCheck()
    {
        if (alive == true && throwable == true && currGrenades > 0 && Input.GetKeyDown(KeyCode.F))
        {
            throwable = false;
            currGrenades--;
            Vector3 direction = camMain.transform.forward;
            Rigidbody clone;
            clone = Instantiate(grenade, grenadeInstantiatePoint.position, grenadeInstantiatePoint.rotation);
            clone.AddForce(direction * TossPower);
            SDFX.PlayOneShot(grenadeToss, 0.2f);
            StartCoroutine(grenadeWaitTime());
        }
    }

    IEnumerator grenadeWaitTime()
    {
        yield return new WaitForSeconds(nadeCooldown);
        throwable = true;
    }
    public void Die()
    {
        alive = false;
    }

    public void AddAmmo()
    {
        currGrenades += 3;
    }
}
