using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public LayerMask deadlyLayers; // Assign layers in the Inspector
    public System.Action onDeath; // Dynamic behavior when dying

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

    public void Die()
    {
        Debug.Log("Enemy collided with a deadly object!");

        // Invoke the custom onDeath action if assigned
        onDeath?.Invoke();

        // Default behavior: Destroy the GameObject
        Destroy(gameObject);
    }
}