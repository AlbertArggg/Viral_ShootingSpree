using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            FindObjectOfType<Health>().addHealth(50);
            Destroy(gameObject);
        }
    }
}
