using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peanut : MonoBehaviour
{
    // Start is called before the fi rst frame update
    public bool deadly = true;
    public GameObject sher;
    public GameObject projectilePrefab;


     public Transform shootPoint; // Assign a Transform where the projectile will spawn
    public float projectileSpeed = 100f; // Speed of the projectile
    public float shotDirection = 1f;
    public float shotOffset = 0.1f;
    public float shotCD = 1f;
    private float lastShotTime;
    private Transform sherTransform;

    void Start()
    {
        sherTransform = sher.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(sherTransform.position.x > transform.position.x){
            shotDirection = 1;
        }
        else{
            shotDirection = -1;
        }

        if(Time.time - lastShotTime > shotCD){
            Shoot();
        }

         if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();

        }
    }

    void Shoot()
    {
        // Instantiate the projectile at the shoot point
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position + new Vector3(shotOffset * shotDirection , 0 , 0), shootPoint.rotation);

        // Apply velocity to the projectile
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = shootPoint.right * shotDirection * projectileSpeed; // Use 'right' for 2D
        }
        else
        {
            Debug.LogWarning("Projectile does not have a Rigidbody2D component!");
        }
        lastShotTime = Time.time;
    }
}
