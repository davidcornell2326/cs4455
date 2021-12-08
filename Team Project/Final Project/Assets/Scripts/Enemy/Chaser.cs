using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Chaser : MonoBehaviour {

    public ParticleSystem deathParticles;
    public Transform groundCheckTransform;
    public float groundRayCastDistance = 0.02f;
    public float minSecondsBetweenJumps = 0.5f;
    public float maxSecondsBetweenJumps = 3f;
    public float jumpVerticalForce = 300f;
    public float jumpHorizontalMaxForce = 300f;
    public float noticeDistance = 40f;
    public GameObject drop;
    public int numDrops = 5;


    private GameObject player;
    private Rigidbody rb;
    private Animator anim;
    public bool isGrounded = false;
    private bool isAnimFinished = true;
    private int environmentContacts = 0;

    void Start() {
        player = FindObjectOfType<PlayerController>().gameObject;
        rb = GetComponentInParent<Rigidbody>();
        anim = GetComponentInParent<Animator>();
        //StartCoroutine(Jump());     // moved to OnEnable to accommodate pausing
    }

    private void OnEnable() {
        environmentContacts = 0;
        StartCoroutine(Jump());
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Environment")) {
            environmentContacts++;
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Environment")) {
            environmentContacts--;
        }
    }

    void FixedUpdate() {

        bool oldIsGrounded = isGrounded;
        //isGrounded = Physics.Raycast(groundCheckTransform.position, Vector3.down, groundRayCastDistance, LayerMask.GetMask("Environment"));
        isGrounded = environmentContacts % 2 == 1;
        if (!oldIsGrounded && isGrounded) {
            anim.SetTrigger("landing");
        }

        float distance = (player.transform.position - this.transform.position).magnitude;
        //print(distance);

        if (distance > noticeDistance) {        // don't do anything if player is too far away
            if (isAnimFinished) {
                anim.speed = 0;
            }
            return;
        } else {
            anim.speed = 1;
        }

    }

    private IEnumerator Jump() {
        while(true) {
            yield return new WaitForSeconds(Random.Range(minSecondsBetweenJumps, maxSecondsBetweenJumps));
            if (isGrounded) {
                anim.SetTrigger("jumping");     // let animation trigger jump
            }
        }
    }

    public void AnimationTriggeredJump() {
        rb.AddForce(GetJumpForce());
    }

    private Vector3 GetJumpForce() {
        Vector3 force = new Vector3();

        // chase player here
        if (!player) return Vector3.zero;
        Vector3 chaseDirection = player.transform.position - this.transform.position;
        chaseDirection.Normalize();




        //float forceX = Random.Range(-jumpHorizontalMaxForce, jumpHorizontalMaxForce);
        //float forceY = jumpVerticalForce;
        //float forceZ = Random.Range(-jumpHorizontalMaxForce, jumpHorizontalMaxForce);
        float forceX = jumpHorizontalMaxForce;
        float forceY = jumpVerticalForce;
        float forceZ = jumpHorizontalMaxForce;

        force.x = forceX * chaseDirection.x;
        force.y = forceY;
        force.z = forceZ * chaseDirection.z;

        return force;
    }

    public void SetAnimFinished(int value) {
        isAnimFinished = value == 1;
    }

    public void PlayFootstep() {
        AudioManager.instance.PlaySound("Medium Jello Footstep");
    }

    public void BeginDeath() {
        GetComponentInParent<Health>().RefillHealth();
        anim.SetTrigger("startDeath");
    }

    public void Die() {
        AudioManager.instance.PlaySound("Big Slime Death");
        for (int i = 0; i < numDrops; i++) {
            Instantiate(drop, this.transform.position, Quaternion.identity);
        }

        ParticleSystem particles = Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(particles, 2);  //destroys these particles after 2 seconds
        
        Destroy(gameObject);
    }

}
