using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Health))]
public class Shooter : MonoBehaviour {

    public ParticleSystem deathParticles;
    public Transform groundCheckTransform;
    public float groundRayCastDistance = 0.02f;
    //public float minSecondsBetweenJumps = 0.5f;
    //public float maxSecondsBetweenJumps = 3f;
    //public float jumpVerticalForce = 300f;
    //public float jumpHorizontalMaxForce = 300f;
    public float turnSpeed = 0.01f;
    //public float maxMoveSpeed = 3f;
    public float retreatForce = 300f;
    public float maxAngleDifferenceToShoot = 1f;
    public float noticeDistance = 40f;
    public float stopChasingDistance = 20f;
    public float retreatDistance = 10f;
    public float shootCooldownSeconds = 1f;
    public float gravityMultiplier = 30f;
    public float idleAnimSpeed = 0.4f;
    public Transform projectleSpawn;
    public GameObject projectile;
    public float projectileSpeed = 15f;
    public float projectileLifespan = 3f;
    public GameObject drop;
    public int numDrops = 5;

    private Rigidbody rb;
    private GameObject player;
    private NavMeshAgent navMeshAgent;
    private Animator anim;
    //private bool isGrounded = false;
    private bool canShoot = true;
    private bool isGrounded = false;
    private int terrainContacts = 0;
    private bool isAnimFinished = true;

    void Start() {
        rb = GetComponentInParent<Rigidbody>();
        player = FindObjectOfType<PlayerController>().gameObject;
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        anim = GetComponentInParent<Animator>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.enabled = false;
        //StartCoroutine(Jump());
    }

    void FixedUpdate() {
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;

        //isGrounded = Physics.Raycast(groundCheckTransform.position, -groundCheckTransform.transform.up, groundRayCastDistance, LayerMask.GetMask("Environment"));
        isGrounded = terrainContacts % 2 == 1;
        //print(isGrounded);
        if (!isGrounded) {
            rb.AddForce(Physics.gravity * gravityMultiplier, ForceMode.Acceleration);       // gravity doesn't work well with navmeshagent, so make it fall
        }

        float distance = (player.transform.position - this.transform.position).magnitude;
        //print(distance);
        
        if (distance > noticeDistance) {        // don't do anything if player is too far away
            navMeshAgent.enabled = false;
            anim.speed = 0;
            return;
        } else {
            navMeshAgent.enabled = true;
            anim.speed = 1;
        }

        // Look towards player at all times
        Vector3 targetDirection = (player.transform.position - this.transform.position).normalized;
        Vector3 newDirection = Vector3.RotateTowards(this.transform.forward, targetDirection, turnSpeed, 0f);
        transform.rotation = Quaternion.LookRotation(newDirection);

        // Always try to shoot at player
        if (Mathf.Abs((targetDirection - newDirection).magnitude) < maxAngleDifferenceToShoot) {

            bool hit = Physics.Raycast(projectleSpawn.transform.position, targetDirection, distance, ~LayerMask.GetMask(new string[] { "Player", "Enemy Projectile", "Player Projectile"}));
            if (canShoot && !hit) {
                canShoot = false;
                Shoot();
                StartCoroutine(ShootCooldown());
            }
        }

        float dist = Vector3.Distance(this.transform.position, player.transform.position);
        RaycastHit hit2;
        Physics.Raycast(this.transform.position, player.transform.position, out hit2, dist);
        bool canSeePlayer = !(hit2.Equals(null));
        //print(canSeePlayer);
        if (distance > stopChasingDistance || !canSeePlayer) {   // move toward player
            navMeshAgent.enabled = true;
            rb.velocity = Vector3.zero;
            if (navMeshAgent.isOnNavMesh) {
                navMeshAgent.SetDestination(player.transform.position);
            }
            //print(navMeshAgent.velocity.magnitude / navMeshAgent.speed);
            anim.speed = navMeshAgent.velocity.magnitude / navMeshAgent.speed;
            //print("chasing?");



        } else if (distance < retreatDistance) {    // move back

           


            navMeshAgent.enabled = false;
            rb.velocity = Vector3.zero;
            Vector3 retreatDirection = new Vector3(this.transform.position.x - player.transform.position.x, 0, this.transform.position.z - player.transform.position.z).normalized;
            rb.AddForce(retreatDirection * retreatForce);
            anim.speed = 0.8f;




        } else {        // just fall or stand still
            navMeshAgent.enabled = false;
            rb.velocity = Vector3.zero;
            if (isAnimFinished) {
                anim.speed = 0f;
            }
        }


    }

    private IEnumerator ShootCooldown() {
        yield return new WaitForSeconds(shootCooldownSeconds);
        canShoot = true;
    }

    private void OnCollisionEnter(Collision collision) {
        if (LayerMask.LayerToName(collision.gameObject.layer).Equals("Environment")) {
            terrainContacts++;
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (LayerMask.LayerToName(collision.gameObject.layer).Equals("Environment")) {
            terrainContacts--;
        }
    }

    public void SetAnimFinished(int value) {
        isAnimFinished = value == 1;
    }

    public void Die() {
        AudioManager.instance.PlaySound("Big Slime Death");
        for (int i = 0; i < numDrops; i++) {
            Instantiate(drop, this.transform.position, Quaternion.identity);
        }

        ParticleSystem particles = Instantiate(deathParticles, this.transform.position, Quaternion.identity);
        Destroy(particles, 2);  //destroys these particles after 2 seconds

        Destroy(gameObject);
    }

    public void PlayFootstep() {;
        AudioManager.instance.PlaySound("Small Jello Footstep");
    }

    private void Shoot() {
        AudioManager.instance.PlaySound("Shoot");
        GameObject p = Instantiate(projectile, projectleSpawn.position, transform.rotation);
        Destroy(p, projectileLifespan);  //destroy the projectile after seconds if none are found

        p.GetComponentInParent<Rigidbody>().velocity = transform.forward * projectileSpeed;
    }

}
