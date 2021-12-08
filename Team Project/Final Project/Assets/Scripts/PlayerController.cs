using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {
    [Header("Movement Constraints")]
    [SerializeField] private float horizontalSpeed = 10f;
    [SerializeField] private float jumpForce = 700f;
    [SerializeField] private float rotationXSpeed = 1f;
    public float rotationYSpeed = 1f;
    public float lowerRotLimit = -30;
    public float upperRotLimit = 30;
    public float rootMovementSpeed;
    [SerializeField]
    [Tooltip("this value will add a force to the player downward. A value of 1 will be the same as the gravity on the Rigidbody, and a value of 2 will be twice as much. Do not have this and gravity on the Rigidbody checked.")]
    private float gravityOverrideMultiplier = 3;

    [Header("Particles")]
    public ParticleSystem jumpParticles;
    public ParticleSystem onGroundParticles;
    public ParticleSystem shotParticles;

    [Header("Weapon")]
    [SerializeField] private Transform projectleSpawn;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float projectileSpeed = 1f;
    [SerializeField] private float shootCooldown = 1f;
    [SerializeField] private Animator weaponAnim;

    [Header("Misc")]
    public float interactionRadius = 2;
    [SerializeField] private Transform groundCheckTransform;


    private Rigidbody rb;
    private Transform cameraPositionHolder;
    private Animator anim;
    private Health healthComponent;
    private GroundChecker groundChecker;

    private float movementX = 0;
    private float movementY = 0;
    [SerializeField] private Vector2 filteredMovement;
    [SerializeField] private float filterStrength = 5f;
    public bool canMove = true;
    public bool isGrounded = false;

    private float lastJumpTime = 0;
    [Tooltip("The time in seconds that it takes for the player to be able to jump again")]
    [SerializeField] private float jumpCooldown = .1f;
    private float lastShootTime = 0;
    public int ammo = 0;
    public int maxAmmo = 100;


    // Start is called before the first frame update
    void Start() {
        rb = GetComponentInParent<Rigidbody>();
        anim = gameObject.GetComponentInParent<Animator>();
        healthComponent = GetComponentInParent<Health>();
        cameraPositionHolder = GameObject.Find("Camera Position Holder").GetComponentInParent<Transform>();
        groundChecker = GetComponentInChildren<GroundChecker>();

        ammo = maxAmmo;
    }

    // Update is called once per frame
    void Update() {
        isGrounded = groundChecker.isGrounded;

        //pause and play the onGroundParticles when in the air
        if (isGrounded && onGroundParticles.isPaused)
            onGroundParticles.Play();
        else if (!isGrounded && onGroundParticles.isPlaying)
            onGroundParticles.Pause();

        filteredMovement.x = (float) Math.Round(Mathf.Clamp(Mathf.Lerp(filteredMovement.x, movementX, Time.deltaTime * filterStrength), -1, 1), 2);
        filteredMovement.y = (float) Math.Round(Mathf.Clamp(Mathf.Lerp(filteredMovement.y, movementY, Time.deltaTime * filterStrength), -1, 1), 2);

        //movement.y = rb.velocity.y;
        //rb.velocity = movement;

        //set the animator values
        if (anim && canMove) {
            anim.SetFloat("velX", filteredMovement.x);
            anim.SetFloat("velY", filteredMovement.y);
            anim.SetBool("isGrounded", isGrounded);
        }

        if (isGrounded && (Mathf.Abs(movementX) > 0 || Mathf.Abs(movementY) > 0))
            AudioManager.instance.PlaySound("Walk");
        else
            AudioManager.instance.StopSound("Walk");


    }

    private void FixedUpdate() {
        isGrounded = groundChecker.isGrounded;

        //add "gravity"
        GetComponentInParent<Rigidbody>().AddForce(Physics.gravity * gravityOverrideMultiplier, ForceMode.Acceleration);
    }

    void OnMove(InputValue movementValue) {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnJump() {
        if (isGrounded) { //we can jump because we are on the ground!
            if (lastJumpTime + jumpCooldown < Time.time) { //we can jump because our cooldown is over
                lastJumpTime = Time.time;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

                if (jumpParticles)
                    Instantiate(jumpParticles, groundCheckTransform.position, transform.rotation);

                if (anim)
                    anim.SetTrigger("jump");

                AudioManager.instance.PlaySoundAtLocation("Player Jump", transform.position);
                AudioManager.instance.StopSound("Walk");
            }
        }
    }

    void OnPause(InputValue value) {
        UIManager.instance.TogglePause();
    }

    void OnInteract() {
        //print("INTERACTING at: " + transform.position);
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactionRadius);
        foreach (Collider c in colliders) {
            //test if the collider gameObject contains the Interactable script
            if (c.GetComponentInParent<Interactable>()) {
                c.GetComponentInParent<Interactable>().Interact();
                return;
            }
        }
    }

    void OnLook(InputValue lookValue) {
        Vector2 lookVector = lookValue.Get<Vector2>();

        float currXRotation = cameraPositionHolder.eulerAngles.x;
        float heightRotValue = 0;
        if ((currXRotation >= lowerRotLimit && currXRotation <= 360)
            || (currXRotation >= -1 && currXRotation <= upperRotLimit)) {
            //can rotate anywhere
            heightRotValue = -lookVector.y * rotationYSpeed;
        } else if (lookVector.y <= 0 && currXRotation >= (lowerRotLimit - 10)) {
            //can rotate up
            heightRotValue = -lookVector.y * rotationYSpeed;
        } else if (lookVector.y >= 0 && currXRotation <= (upperRotLimit + 10)) {
            // can rotate down
            heightRotValue = -lookVector.y * rotationYSpeed;
        }

        transform.Rotate(0, lookVector.x * rotationXSpeed, 0);
        cameraPositionHolder.Rotate(heightRotValue, 0, 0);
    }

    void OnAnimatorMove() {
        //Vector3 newRootPosition;


        //if (isGrounded) {
        //    //use root motion as is if on the ground		
        //    //Vector3.LerpUnclamped(transform.position, anim.rootPosition, rootMovementSpeed)
        //    newRootPosition = anim.rootPosition;
        //    rb.MovePosition(newRootPosition);
        //} else {
        anim.rootPosition = transform.position;

        Vector3 xDir = new Vector3(transform.forward.z, 0, -transform.forward.x).normalized;
        Vector3 yDir = transform.forward.normalized;
        Vector3 movement = ((xDir * filteredMovement.x) + (yDir * filteredMovement.y)).normalized *
            Mathf.Max(
                Mathf.Abs(filteredMovement.x),
                Mathf.Abs(filteredMovement.y)
            );
        movement *= horizontalSpeed;

        movement.y = rb.velocity.y;
        if (canMove) {
            rb.velocity = movement;
        }
        //}
    }

    void OnFire() {
        if (ammo <= 0) {
            Debug.LogWarning("Out of ammo!");
            return;
        }
        if (Time.time > lastShootTime + shootCooldown) {
            RemoveAmmo();

            GameObject p = Instantiate(projectile, projectleSpawn.position, transform.rotation);
            Destroy(p, 2);  //destroy the projectile after 2 seconds if none are found

            p.GetComponentInParent<Rigidbody>().velocity = (projectleSpawn.forward * projectileSpeed);
            lastShootTime = Time.time;

            if (weaponAnim)
                weaponAnim.SetTrigger("shoot");

            AudioManager.instance.PlaySound("Shoot");

            if (shotParticles)
                Instantiate(shotParticles, projectleSpawn.position, projectleSpawn.rotation);
        }
    }

    public void Die() {
        print("Player has died");
        Time.timeScale = 0;
        //GameQuitter.QuitGame();
        UIManager.instance.ShowDeathScreen();
    }

    private void RemoveAmmo() {
        ammo--;
        UIManager.instance.SetAmmo(((float) ammo) / maxAmmo);
    }

    public void RefillAmmo() {
        ammo = maxAmmo;
        UIManager.instance.SetAmmo(((float) ammo) / maxAmmo);
    }


}
