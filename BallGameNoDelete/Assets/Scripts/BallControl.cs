using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BallControl : MonoBehaviour
{
    public float speed = 5;
    private Rigidbody rigidbody;
    private float distToGround;
    private bool isGrounded;
    private float jumpTimer;
    public float jumpDuration;
    public float jumpForce;
    bool jumpHeld;
    private Vector3 spawnLoc;
    float slideAcceleration;
    public CinemachineVirtualCamera vCam;
    public float defaultFoV;
    float deathTimer;

    private void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
        spawnLoc = gameObject.transform.position;
    }

    private void Update()
    {
        // movement
        if (Input.GetAxis("Horizontal") > 0)
        {
            rigidbody.AddForce(Vector3.right * speed * Time.deltaTime);
        }

        else if (Input.GetAxis("Horizontal") < 0)
        {
            rigidbody.AddForce(Vector3.left * speed * Time.deltaTime);
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            rigidbody.AddForce(Vector3.forward * speed * Time.deltaTime);
        }

        else if (Input.GetAxis("Vertical") < 0)
        {
            rigidbody.AddForce(Vector3.back * speed * Time.deltaTime);
        }

        // check if grounded
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.4f);

        // jumping
        if (isGrounded == true) { jumpTimer = jumpDuration; }

        // remove jump potential after jump button is lifted
        if (Input.GetButtonUp("Jump")) { jumpTimer = 0f; }

        if (Input.GetButton("Jump")) // jump
        {
            if (jumpTimer > 0f)
            {
                rigidbody.AddForce(Vector3.up * jumpForce * Time.deltaTime);
                //rigidbody.velocity = new Vector3(0f, jumpForce, 0f);
                jumpTimer -= Time.deltaTime;
            }
        }

        // increase fall speed
        if (rigidbody.velocity.y < -0.1 && isGrounded == false)
        {
            rigidbody.velocity += Vector3.up * Physics2D.gravity.y * (jumpForce / 1500f) * Time.deltaTime;
            deathTimer--;
            print(deathTimer);
        }
        else
            deathTimer = 350f;

        if (deathTimer < 0f)
            transform.position = spawnLoc;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "DeathZone")
        {
            transform.position = spawnLoc;
        }
        else if (collision.gameObject.tag == "Checkpoint")
        {
            spawnLoc = collision.gameObject.transform.position;
            collision.gameObject.GetComponent<Rotation>().rotY = .3f;
        }
        else if (collision.gameObject.tag == "Slide")
        {
            slideAcceleration = 0f;
            vCam.m_Lens.FieldOfView = defaultFoV;
        }
    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Slide")
        {
            if (slideAcceleration < 20f)
            {
                vCam.m_Lens.FieldOfView += .05f;
                slideAcceleration += .01f;
            }
            rigidbody.AddForce(Vector3.down * speed * 10f * slideAcceleration * Time.deltaTime);
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Slide")
        {
            slideAcceleration = 0f;
            vCam.m_Lens.FieldOfView = defaultFoV;
        }
    }
}