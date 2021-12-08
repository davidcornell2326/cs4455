using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class StupidJello : MonoBehaviour {

    public ParticleSystem deathParticles;
    public Transform groundCheckTransform;
    public float groundRayCastDistance = 0.02f;
    public float minSecondsBetweenJumps = 1f;
    public float maxSecondsBetweenJumps = 5f;
    public float jumpVerticalForce = 200f;
    public float jumpHorizontalMaxForce = 50f;
    public GameObject drop;
    public int numDrops = 5;

    private Rigidbody rb;
    private Animator anim;
    public bool isGrounded = false;
    public int environmentContacts = 0;

    void Start() {
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

    }

    private IEnumerator Jump() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(minSecondsBetweenJumps, maxSecondsBetweenJumps));
            if (isGrounded) {
                anim.SetTrigger("jumping");     // let animation trigger jump
                //rb.AddForce(GetJumpForce());
            }
        }
    }

    public void AnimationTriggeredJump() {
        rb.AddForce(GetJumpForce());
    }

    private Vector3 GetJumpForce() {
        Vector3 force = new Vector3();
        force.x = Random.Range(-jumpHorizontalMaxForce, jumpHorizontalMaxForce);
        force.y = jumpVerticalForce;
        force.z = Random.Range(-jumpHorizontalMaxForce, jumpHorizontalMaxForce);
        return force;
    }

    public void PlayFootstep() {
        AudioManager.instance.PlaySound("Small Jello Footstep");
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

}
