//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation_FPS : MonoBehaviour
{
    public Transform playerBody;
    float xRotation = 0f;
    public bool alive = true;
    [SerializeField] [Range(0,90)] float maxRotation = 50;
    [SerializeField] [Range(0f, 1000f)] float mouseSensitivity = 500f;
    [SerializeField] [Range(0f, 1000f)] float AimMouseSensitivity = 250f;

    public void Die()
    {
        alive = false;
    }

    private void Awake()
    {
        mouseSensitivity = FindObjectOfType<Settings>().GetComponent<Settings>().setSens();
        AimMouseSensitivity = FindObjectOfType<Settings>().GetComponent<Settings>().setAimSens();
    }

    void Update()
    {
        if (alive == true && Input.GetButton("Fire2"))
        {
            float mouseX = Input.GetAxis("Mouse X") * AimMouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * AimMouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -maxRotation, maxRotation);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
        else if (alive == true)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -maxRotation, maxRotation);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
