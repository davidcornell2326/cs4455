using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float jumpHeight = 300f;
    public float sprintMultiplier = 5f;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI sprintText;
    public GameObject winTextObject;

    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private int count;
    private bool sprinting = false;

    // Start is called before the first frame update
    void Start() {
        winTextObject.SetActive(false);
        rb = this.GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
    }

    void OnMove(InputValue movementValue) {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnJump() {
        if (rb.velocity.y <= 1) {
            rb.AddForce(new Vector3(0, jumpHeight, 0));
        }
    }

    void OnSprint(InputValue movementValue) {
        sprinting = !sprinting;
        sprintText.text = "Sprint Mode: " + (sprinting? "ON" : "OFF");
    }

    void SetCountText() {
        countText.text = "Count: " + count.ToString();
        if (GameObject.FindGameObjectsWithTag("Pickup").Length == 0) {
            winTextObject.SetActive(true);
        }
    }

    void FixedUpdate() {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        if (sprinting) {
            movement *= sprintMultiplier;
        }
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Pickup")) {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }
}
