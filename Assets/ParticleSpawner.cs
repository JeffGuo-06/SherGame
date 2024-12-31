using System.Collections;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    public GameObject defaultParticlePrefab; // Default particle system prefab

    /// <summary>
    /// Spawns and plays a particle system prefab at the specified location.
    /// </summary>
    /// <param name="position">The world position to spawn the particle system.</param>
    /// <param name="particlePrefab">The particle system prefab to use (optional).</param>
    public void SpawnAndPlay(Vector3 position, GameObject particlePrefab = null)
    {
        if (particlePrefab == null)
        {
            particlePrefab = defaultParticlePrefab;
        }

        if (particlePrefab == null)
        {
            Debug.LogError("No particle prefab provided or assigned!");
            return;
        }

        // Instantiate the particle system at the given position
        GameObject particleObject = Instantiate(particlePrefab, position, Quaternion.identity);

        // Get the ParticleSystem component
        ParticleSystem particleSystem = particleObject.GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            particleSystem.Play();

            // Start a coroutine to destroy the particle system after it finishes
            StartCoroutine(DestroyAfterPlay(particleSystem));
        }
        else
        {
            Debug.LogWarning("Prefab does not contain a ParticleSystem component!");
            Destroy(particleObject, 2f); // Fallback destroy timer
        }
    }

    private IEnumerator DestroyAfterPlay(ParticleSystem particleSystem)
    {
        // Wait for the particle system to finish playing
        yield return new WaitForSeconds(particleSystem.main.duration + particleSystem.main.startLifetime.constantMax);

        // Destroy the GameObject
        Destroy(particleSystem.gameObject);
    }
}
