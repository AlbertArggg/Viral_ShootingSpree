using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float timeOfFirstButton = 0;
    public bool jetpacking = false;
    public GameObject jetpackAudio;
    public Slider jetPackSlider;
    public float JetFuel = 100;
    public float JetSpeed = 5f;
    public CharacterController controller;
    public bool alive = true;
    public bool isGrounded = true;
    public Transform groundCollider;
    public float groundDistance = 0.4f;
    public float RotateSpeed = 3f;
    public LayerMask groundMask;

    [SerializeField] [Range(1f, 20f)] float speed = 10f;
    [SerializeField] [Range(1.2f, 2f) ] float runMultplyr = 1.6f;
    [SerializeField] [Range(0.5f, 5f)] float jumpHeight = 3f;

    public Vector3 velocity;

    [SerializeField] float gravity = -20f;

    public void Die()
    {
        alive = false;
    }

    private void Awake()
    {
        jetpackAudio.SetActive(false);
    }
    void Update()
    {
        jetPackSlider.value = JetFuel;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (Input.GetButtonDown("Jump") && isGrounded == true && alive == true)
        {
            playerJump();
            timeOfFirstButton = Time.time;
        }

        if (Input.GetKey(KeyCode.E))
        {
            StartCoroutine(jetPack(true));
            jetpacking = true;
        }

        else
        {
            StartCoroutine(jetPack(false));
            jetpacking = false;
        }

        playerMoveRun(move);
        applyGravity();
    }

    IEnumerator jetPack(bool _jettin)
    {
        if (_jettin == true)
        {
            while (Input.GetKey(KeyCode.E) && JetFuel >0)
            {
                if (JetFuel > 0)
                {
                    jetpackAudio.SetActive(true);
                    Vector3 currentVector = Vector3.up;
                    controller.Move((currentVector * JetSpeed * ((1 * JetFuel) / 50) * Time.deltaTime));
                    JetFuel -= 0.1f;
                    yield return new WaitForSeconds(0.2f);
                }
            }
        }

        else
        {
            if (JetFuel < 100)
            {
                JetFuel+= 0.03f;
            }
            jetpackAudio.SetActive(false);
        }

        yield return new WaitForSeconds(0.5f);
    }
    private void playerJump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    private void playerMoveRun(Vector3 move)
    {
        if (alive == true)
        {
            if (Input.GetKey(KeyCode.LeftShift) && jetpacking == false)
            {
                controller.Move(move * speed * Time.deltaTime * runMultplyr);
            }
            else if (jetpacking && JetFuel > 0)
            {
                controller.Move(move * speed * Time.deltaTime * runMultplyr * (JetFuel / 20));
            }
            else
            {
                controller.Move(move * speed * Time.deltaTime);
            }
        }
    }
    private void applyGravity()
    {
        isGrounded = Physics.CheckSphere(groundCollider.position, groundDistance, groundMask);
        if (isGrounded == true && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        else 
        {
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }
}
