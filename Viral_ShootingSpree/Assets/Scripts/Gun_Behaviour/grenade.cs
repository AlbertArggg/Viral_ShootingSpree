using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenade : MonoBehaviour
{
    public float delay = 3f;
    public GameObject explosionFX;
    public float blastRadius = 30f;
    public float explosionForce = 700f;
    public float explosiveDam = 200;

    public AudioSource grenadeAS;
    public AudioClip grenadeHit;
    public GameObject explosionSDFX;
    public AudioClip explosion;

    private void Start()
    {
        StartCoroutine(BlowUp());
    }

    private void OnCollisionEnter(Collision collision)
    {
        grenadeAS.PlayOneShot(grenadeHit,0.2f);
    }
    IEnumerator BlowUp()
    {
        yield return new WaitForSeconds(delay);
        Instantiate(explosionSDFX,transform.position, transform.rotation);
        Instantiate(explosionFX, transform.position, transform.rotation);
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            Health dm = nearbyObject.GetComponent<Health>();
            Enemy_Target et = nearbyObject.GetComponent<Enemy_Target>();

            if (dm != null && rb != null)
            {
                float dist = Vector3.Distance(rb.position, transform.position);
                float subH = explosiveDam / dist;
                dm.subHealth(subH);
                rb.AddExplosionForce(explosionForce, transform.position, blastRadius);
            }

            else if (et != null && rb != null)
            {
                float dist = Vector3.Distance(rb.position, transform.position);
                float subH = explosiveDam / dist;
                et.TakeDamage(subH);
                rb.AddExplosionForce(explosionForce, transform.position, blastRadius);
            }

            else if (et != null)
            {
                float dist = Vector3.Distance(rb.position, transform.position);
                float subH = explosiveDam / dist;
                et.TakeDamage(subH);
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
