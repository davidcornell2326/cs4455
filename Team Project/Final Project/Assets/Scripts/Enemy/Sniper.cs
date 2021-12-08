using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Sniper : MonoBehaviour {

    public ParticleSystem deathParticles;
    public Transform groundCheckTransform;
    public float groundRayCastDistance = 0.02f;
    //public float minSecondsBetweenJumps = 0.5f;
    //public float maxSecondsBetweenJumps = 3f;
    //public float jumpVerticalForce = 300f;
    //public float jumpHorizontalMaxForce = 300f;
    public float turnSpeed = 0.01f;
    //public float maxMoveSpeed = 3f;
    //public float moveAcceleration = 100f;
    public float maxAngleDifferenceToShoot = 0.2f;
    public float noticeDistance = 100f;
    //public float stopChasingDistance = 20f;
    //public float retreatDistance = 10f;
    public float shootCooldownSeconds = 1f;
    public Transform projectleSpawn;
    public GameObject projectile;
    public float projectileSpeed = 15f;
    public float projectileLifespan = 3f;
    public GameObject drop;
    public int numDrops = 5;

    private Rigidbody rb;
    private GameObject player;
    //private bool isGrounded = false;
    private bool canShoot = true;

    void Start() {
        rb = GetComponentInParent<Rigidbody>();
        player = FindObjectOfType<PlayerController>().gameObject;
        //StartCoroutine(Jump());
    }

    void FixedUpdate() {

        //isGrounded = Physics.Raycast(groundCheckTransform.position, Vector3.down, groundRayCastDistance, LayerMask.GetMask("Environment"));
        //print(isGrounded);

        float distance = (player.transform.position - this.transform.position).magnitude;
        //print(distance);

        if (distance > noticeDistance) {        // don't do anything if player is too far away
            return;
        }

        Vector3 targetDirection = (player.transform.position - this.transform.position).normalized;
        Vector3 newDirection = Vector3.RotateTowards(this.transform.forward, targetDirection, turnSpeed, 0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
        if (Mathf.Abs((targetDirection - newDirection).magnitude) < maxAngleDifferenceToShoot) {
            if (canShoot) {
                canShoot = false;
                Shoot();
                StartCoroutine(ShootCooldown());
            }
        }

        //float distance = (player.transform.position - this.transform.position).magnitude;
        //if (rb.velocity.magnitude < maxMoveSpeed) {
        //    if (distance > stopChasingDistance) {   // move forward
        //        rb.AddForce(this.transform.forward * moveAcceleration);
        //        rb.velocity = rb.velocity.normalized * Mathf.Clamp(rb.velocity.magnitude, -maxMoveSpeed, maxMoveSpeed);
        //    } else if (distance < retreatDistance) {    // move back
        //        rb.AddForce(-this.transform.forward * moveAcceleration);
        //        rb.velocity = rb.velocity.normalized * Mathf.Clamp(rb.velocity.magnitude, -maxMoveSpeed, maxMoveSpeed);
        //    }
            
        //}

    }

    private IEnumerator ShootCooldown() {
        yield return new WaitForSeconds(shootCooldownSeconds);
        canShoot = true;
    }

    //private IEnumerator Jump() {
    //    while (true) {
    //        yield return new WaitForSeconds(Random.Range(minSecondsBetweenJumps, maxSecondsBetweenJumps));
    //        //print("Attempting Jump");
    //        if (isGrounded) {
    //            //print("Jumping");
    //            this.transform.LookAt(FindObjectOfType<PlayerController>().gameObject.transform);
    //            Shoot();
    //            rb.AddForce(GetJumpForce());
    //        }
    //    }
    //}

    //private Vector3 GetJumpForce() {
    //    Vector3 force = new Vector3();

    //    // chase player here
    //    if (!player) return Vector3.zero;
    //    Vector3 chaseDirection = player.transform.position - this.transform.position;
    //    chaseDirection.Normalize();




    //    //float forceX = Random.Range(-jumpHorizontalMaxForce, jumpHorizontalMaxForce);
    //    //float forceY = jumpVerticalForce;
    //    //float forceZ = Random.Range(-jumpHorizontalMaxForce, jumpHorizontalMaxForce);
    //    float forceX = jumpHorizontalMaxForce;
    //    float forceY = jumpVerticalForce;
    //    float forceZ = jumpHorizontalMaxForce;

    //    force.x = forceX * chaseDirection.x;
    //    force.y = forceY;
    //    force.z = forceZ * chaseDirection.z;

    //    return force;
    //}

    public void Die() {
        AudioManager.instance.PlaySound("Big Slime Death");
        for (int i = 0; i < numDrops; i++) {
            Instantiate(drop, this.transform.position, Quaternion.identity);
        }

        ParticleSystem particles = Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(particles, 2);  //destroys these particles after 2 seconds

        Destroy(gameObject);
    }

    private void Shoot() {
        AudioManager.instance.PlaySound("Shoot");
        GameObject p = Instantiate(projectile, projectleSpawn.position, transform.rotation);
        Destroy(p, projectileLifespan);  //destroy the projectile after 2 seconds if none are found

        p.GetComponentInParent<Rigidbody>().velocity = transform.forward * projectileSpeed;
    }

}
