using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jellyfish : MonoBehaviour
{
    public ParticleSystem deathParticles;
    public float shootDistance = 20f;
    public float noticeDistance = 30f;
    public float minShootCooldownSeconds = 1.5f;
    public float maxShootCooldownSeconds = 3f;
    public GameObject projectile;
    public Transform projectleSpawn;
    public float projectileSpeed = 15f;
    public float projectileLifespan = 3f;
    public int numProjectiles = 16;
    public GameObject drop;
    public int numDrops = 5;

    private GameObject player;
    private Animator anim;
    private bool canShoot = false;

    void Start() {
        player = FindObjectOfType<PlayerController>().gameObject;
        anim = GetComponentInParent<Animator>();
        //StartCoroutine(ShootCooldown());     // moved to OnEnable to accommodate pausing
    }

    private void OnEnable() {
        StartCoroutine(ShootCooldown());
    }

    void FixedUpdate() {

        float distance = (player.transform.position - this.transform.position).magnitude;
        //print(distance);

        if (distance > noticeDistance) {        // don't do anything if player is too far away
            anim.speed = 0;
            return;
        } else {
            anim.speed = 1;
        }

        if (canShoot & Mathf.Abs((player.transform.position - this.transform.position).magnitude) < shootDistance) {
            canShoot = false;
            Shoot();
            StartCoroutine(ShootCooldown());
        }
    }

    private IEnumerator ShootCooldown() {
        yield return new WaitForSeconds(Random.Range(minShootCooldownSeconds, maxShootCooldownSeconds));
        canShoot = true;
    }

    public void Die() {
        AudioManager.instance.PlaySound("Small Slime Death");
        for (int i = 0; i < numDrops; i++) {
            Instantiate(drop, this.transform.position, Quaternion.identity);
        }

        ParticleSystem particles = Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(particles, 2);  //destroys these particles after 2 seconds

        Destroy(gameObject);
    }

    private void Shoot() {
        AudioManager.instance.PlaySound("Shoot");
        float theta = 360f / numProjectiles;
        Quaternion spawnRotation = this.transform.rotation;
        for (int i = 0; i < numProjectiles; i++) {
            GameObject p = Instantiate(projectile, projectleSpawn.position, spawnRotation);
            p.GetComponentInParent<Rigidbody>().velocity = p.transform.forward * projectileSpeed;
            Destroy(p, projectileLifespan);  //destroy the projectile after seconds if none are found
            spawnRotation *= Quaternion.Euler(0, theta, 0);
        }
    }
}
