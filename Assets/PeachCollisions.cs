using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeachCollisions : TagCollisionHandler
{
    public ParticleSpawner particleSpawner;
    public GameObject deathParticles;
    protected override void InitializeTagActions()
    {
        tagActions.Add("Jeff", (obj) => {});
        tagActions.Add("Sherlyn", (obj) => {
            Debug.Log ("Destroyed");
            particleSpawner.SpawnAndPlay(transform.position, deathParticles);
            Destroy(gameObject);
        });
    }
}
