using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeanutCollisions : TagCollisionHandler
{
    public ParticleSpawner particleSpawner;
    public GameObject deathParticles;
    protected override void InitializeTagActions()
    {
        tagActions.Add("Jeff", (obj) =>
        {
            Debug.Log ("Destroyed");
            particleSpawner.SpawnAndPlay(transform.position, deathParticles);
            Destroy(gameObject);
        });
        tagActions.Add("PowerUp", (obj) => Debug.Log($"Player picked up PowerUp: {obj.name}"));
    }
}
