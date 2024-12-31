using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sher : MonoBehaviour
{
    // Start is called before the first frame update
    public LayerMask deadlyLayers;
    public Transform spawnpointTransform;
    public float respawnDelay = 3f; // Time in seconds before respawn

    private bool isDead = false;

    public ParticleSystem deathParticles;
    private Renderer[] renderers; // To hide the character's visuals
    private Collider2D playerCollider; // Disable collision during death
    private Rigidbody2D playerRigidbody;
    private PlayerMovement movementScript; // Reference to movement script (if any)
    private ParticleSystem deathEffect; // Reference to the Particle System
     void Start()
    {
        // Cache components for quick access
        renderers = GetComponentsInChildren<Renderer>(); // Get all child renderers
        playerCollider = GetComponent<Collider2D>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        movementScript = GetComponent<PlayerMovement>(); // Replace with your movement script
        deathEffect = GetComponentInChildren<ParticleSystem>(); // Find Particle System in children
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object is in the specified layers
        if (IsInLayerMask(collision.gameObject, deadlyLayers))
        {
            Die();
        }
    }
    bool IsInLayerMask(GameObject obj, LayerMask mask)
    {
        return (mask.value & (1 << obj.layer)) > 0;

    }

     void Die()
    {
        isDead = true;
        deathParticles.Play();
        
        Debug.Log("Player Died!");
        // Disable player controls here if needed
        StartCoroutine(Respawn());
    }

        IEnumerator Respawn()
    {
        // Make the player disappear
        SetCharacterVisibility(false);

        // Disable movement and collision
        if (movementScript != null) movementScript.enabled = false;
        if (playerCollider != null) playerCollider.enabled = false;
         if (playerRigidbody != null)
        {
            playerRigidbody.simulated = false; // Turn off Rigidbody2D physics simulation
        }
        // Wait for respawn delay
        yield return new WaitForSeconds(respawnDelay);

        // Move the player to the spawn point
        transform.position = spawnpointTransform.position;
        deathEffect.Stop();

        // Make the player visible again
        SetCharacterVisibility(true);

        // Re-enable movement and collision
        if (movementScript != null) movementScript.enabled = true;
        if (playerCollider != null) playerCollider.enabled = true;
        if (playerRigidbody != null)
        {
            playerRigidbody.simulated = true; // Re-enable Rigidbody2D physics simulation
        }

        Debug.Log("Player Respawned!");
    }

    void SetCharacterVisibility(bool isVisible)
    {
        foreach (Renderer renderer in renderers)
        {
             if (deathEffect != null && renderer.gameObject == deathEffect.gameObject)
            {
                continue;
            }
            renderer.enabled = isVisible;
        }
    }
}
