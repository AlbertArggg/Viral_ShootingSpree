using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwables : MonoBehaviour
{
    public GameObject explosionFX;
    public float blastRadius = 30f;
    public float explosionForce = 700f;
    public float explosiveDam = 200;
    public GameObject explosionSDFX;

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(explosionSDFX, transform.position, transform.rotation);
        Instantiate(explosionFX, transform.position, transform.rotation);
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            Health dm = nearbyObject.GetComponent<Health>();

            if (dm != null && rb != null)
            {
                float dist = Vector3.Distance(rb.position, transform.position);
                float subH = explosiveDam / dist;
                dm.subHealth(subH);
                rb.AddExplosionForce(explosionForce, transform.position, blastRadius);
            }

            else if (dm != null)
            {
                float dist = Vector3.Distance(rb.position, transform.position);
                float subH = explosiveDam / dist;
                dm.subHealth(subH);
            }

            else if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, blastRadius);
            }
            else
            {
                Debug.Log("nada");
            }
        }
        Destroy(gameObject);
    }
}

