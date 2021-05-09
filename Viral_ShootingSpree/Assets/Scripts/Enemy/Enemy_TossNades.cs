using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_TossNades : MonoBehaviour
{
    public Rigidbody grenade;
    public Transform grenadeInstantiatePoint;
    public float nadesTimer1 = 30;
    public float nadesTimer2 = 60;
    public float TossPower = 1000;


    private void Start()
    {
        StartCoroutine(Tossem());
    }

    IEnumerator Tossem()
    {
        yield return new WaitForSeconds(Random.Range(nadesTimer1,nadesTimer2));
        Vector3 direction = gameObject.transform.forward;
        Rigidbody clone;
        clone = Instantiate(grenade, grenadeInstantiatePoint.position, grenadeInstantiatePoint.rotation);
        clone.AddForce(direction * TossPower);
        StartCoroutine(Tossem());
    }
}
