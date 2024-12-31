using System.Collections;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    public PlayerConfig config; // Reference to the shared config
    public Transform spawnpointTransform; // Unique to each player
    public ParticleSystem deathParticles; // Unique to each player

    public bool isDead { get; private set; } = false;

    private Renderer[] renderers; // To hide the character's visuals
    private Collider2D playerCollider; // Disable collision during death
    private Rigidbody2D playerRigidbody;

    private void Start()
    {
        // Cache components for quick access
        renderers = GetComponentsInChildren<Renderer>();
        playerCollider = GetComponent<Collider2D>();
        playerRigidbody = GetComponent<Rigidbody2D>();

        if (config == null)
        {
            Debug.LogError($"PlayerConfig is not assigned on {gameObject.name}!");
        }
    }

    public void Die()
    {
        if (isDead) return; // Prevent multiple death triggers
        isDead = true;

        if (deathParticles != null)
        {
            deathParticles.Play();
        }
        else{
            Debug.Log("NO PARTICLES");
        }

        Debug.Log($"{gameObject.name} Died!");
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        // Make the player disappear
        SetCharacterVisibility(false);

        // Disable movement and collision
        // Disable movement and collision
        if (playerCollider != null)
        {
            playerCollider.enabled = false;
        }

        if (playerRigidbody != null)
        {
            playerRigidbody.simulated = false;
        }

        // Wait for respawn delay from config
        yield return new WaitForSeconds(config.respawnDelay);

        // Move the player to the spawn point
        if (spawnpointTransform != null)
        {
            transform.position = spawnpointTransform.position;
        }


        // Make the player visible again
        SetCharacterVisibility(true);

        // Re-enable movement and collision
        if (playerCollider != null)
        {
            playerCollider.enabled = true;
        }

        if (playerRigidbody != null)
        {
            playerRigidbody.simulated = true;
        }
        isDead = false;
       
    }

    private void SetCharacterVisibility(bool isVisible)
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = isVisible;
        }
    }
}
