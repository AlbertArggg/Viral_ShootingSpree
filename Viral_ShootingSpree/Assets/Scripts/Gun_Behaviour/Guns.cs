using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guns : MonoBehaviour
{
    public GameObject[] guns;
    public int activeGunInArray;
    public bool switching = false;

    private void Start()
    {
        for (int i = 0; i < guns.Length; i++)
        {
            guns[i].SetActive(true);
        }
        StartCoroutine(deactivateGuns(0));
    }


    IEnumerator deactivateGuns(int active)
    {
        activeGunInArray = active;

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < guns.Length; i++)
        {
            guns[i].SetActive(true);
        }

        for (int i = 0; i < guns.Length; i++)
        {
            if (i != active)
            {
                guns[i].SetActive(false);
            }
            else 
            {
                guns[i].SetActive(true);
                if (i == 0 || i == 2)
                {
                    guns[i].GetComponent<Automatic>().setActive();
                }
                if (i == 1)
                {
                    guns[i].GetComponent<MiniGun>().setActive();
                }
                if (i == 3)
                {
                    guns[i].GetComponent<FlameThrower>().setActive();
                }
            }
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            switching = true;
            StartCoroutine(deactivateGuns(0));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            switching = true;
            StartCoroutine(deactivateGuns(1));
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            switching = true;
            StartCoroutine(deactivateGuns(2));
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            switching = true;
            StartCoroutine(deactivateGuns(3));
        }
    }

}
