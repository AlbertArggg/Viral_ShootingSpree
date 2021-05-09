using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 10;
    void Update()
    {
        transform.Rotate(0.0f, 0.01f * rotateSpeed, 0.0f, Space.Self);
    }
}
