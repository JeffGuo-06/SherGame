using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public KeyCode upKey = KeyCode.W;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode downKey = KeyCode.S;
    public KeyCode rightKey = KeyCode.D;

    public PlayerConfig config; // Reference to the shared config

    private float xVelocity = 0;
    private bool isGrounded;
    private int jumpsLeft;
    private float lastJumpTime;

    public LayerMask groundLayer; 
    public Transform groundCheckPoint; 
    public float groundCheckRadius = 0.2f;

    public GameObject particlePrefab; 
    public Transform particlePoint;

    void Start()
    {
        jumpsLeft = config.maxJumps;
    }

    void Update()
    {
        float xDirection = 0;

        if (Input.GetKey(leftKey))
        {
            xDirection = -1;
        }
        else if (Input.GetKey(rightKey))
        {
            xDirection = 1;
        }

        xVelocity = xDirection * config.xSpeed;

        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
        if (isGrounded && Time.time - lastJumpTime >= 0.3f)
        {
            jumpsLeft = config.maxJumps;
        }

        if (Input.GetKeyDown(downKey) && !isGrounded)
        {
            rb.AddForce(Vector2.down * 100, ForceMode2D.Force);
        }

        if (Input.GetKeyDown(upKey) && jumpsLeft > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, config.jumpVelocity);
            jumpsLeft--;
            lastJumpTime = Time.time;
            SpawnParticles();
        }
    }

    void FixedUpdate()
    {
        if (rb.velocity.y < config.terminalVelocity)
        {
            rb.velocity = new Vector2(rb.velocity.x, config.terminalVelocity);
        }
        rb.velocity = new Vector2(xVelocity, rb.velocity.y);
    }

    public void SpawnParticles()
    {
        GameObject particles = Instantiate(particlePrefab, particlePoint ? particlePoint.position : transform.position, Quaternion.identity);
        ParticleSystem ps = particles.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            ps.Play();
            Destroy(particles, ps.main.duration + ps.main.startLifetime.constantMax);
        }
        else
        {
            Destroy(particles, 2f);
        }
    }
}
