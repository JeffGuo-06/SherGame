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

    
    
    public float xSpeed = 5f;
    public float xVelocity = 0;
    public float yVelocity = 0;
    public float terminalVelocity = -20f;

//JUMPING
     public LayerMask groundLayer; // Assign this in the Inspector to the "Ground" layer
    public Transform groundCheckPoint; // Create an empty GameObject for the check point
    public float groundCheckRadius = 0.2f; // Radius of the ground check circle
    private bool isGrounded;
    public float jumpForce = 15f; // Adjust for jump strength\
    public float jumpVelocity = 10f;
    public int maxJumps;
    public int jumpsLeft;
    private float lastJumpTime;

    public GameObject particlePrefab; // Assign the particle prefab in the Inspector
    public Transform particlePoint;     // Assign a spawn point (optional)

//
    

    void Update()
    {
        float xDirection = 0;

        if (Input.GetKey(leftKey))
        {
            xDirection = -1;
            xVelocity= xDirection *xSpeed;
        }
        else if (Input.GetKey(rightKey))
        {
            xDirection = 1;
            xVelocity= xDirection *xSpeed;
        }
        else{
            xVelocity= xDirection *xSpeed;
        }
        
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
        float timeSinceLastJump = Time.time - lastJumpTime;
        if(isGrounded && timeSinceLastJump >= 0.3){
            
            jumpsLeft=maxJumps;
        }

        
        if(!isGrounded && Input.GetKeyDown(downKey)){
            rb.AddForce(Vector2.down * 100, ForceMode2D.Force);
        }
        
        //JUMP
        if (Input.GetKeyDown(upKey) && jumpsLeft>=1)
        {
            
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            //rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
            jumpsLeft-=1;
            lastJumpTime = Time.time;
            SpawnParticles();
        }
    }

    public void FixedUpdate(){
        if (rb.velocity.y < terminalVelocity)
        {
            rb.velocity = new Vector2(rb.velocity.x, terminalVelocity);
        }
         rb.velocity =  rb.velocity = new Vector2(xVelocity, rb.velocity.y);
    }
    void OnDrawGizmosSelected()
    {
        // Draw the ground check radius in the Scene view for visualization
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
    }

    public void SpawnParticles()
    {
        // Instantiate the particle prefab at the spawn point or the current position
        GameObject particles = Instantiate(particlePrefab, particlePoint ? particlePoint.position : transform.position, Quaternion.identity);

        // Ensure the Particle System starts playing
        ParticleSystem ps = particles.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            ps.Play();
        }
        else{
            Debug.Log ("NO PARTICLES");
        }

        // Destroy the particle GameObject after it finishes playing
        if (ps != null)
        {
            Destroy(particles, ps.main.duration + ps.main.startLifetime.constantMax);
        }
        else
        {
            Destroy(particles, 2f); // Default destroy time if no ParticleSystem found
        }
    }
}

