using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public Transform groundCheckOrigin;
    public bool isGrounded;
    public LayerMask groundLayer;

    [SerializeField] private float groundContacts = 0;

    [SerializeField]
    private float groundRayCastDistance = .015f;
    [SerializeField]
    private float extraRayRadius = .15f;

    // Update is called once per frame
    void Update()
    {
        isGrounded = 
            Physics.Raycast(groundCheckOrigin.position, Vector3.down, groundRayCastDistance, groundLayer)   ||
            Physics.Raycast(groundCheckOrigin.position + new Vector3(extraRayRadius, 0, 0), Vector3.down, groundRayCastDistance, groundLayer)   ||
            Physics.Raycast(groundCheckOrigin.position + new Vector3(-extraRayRadius, 0, 0), Vector3.down, groundRayCastDistance, groundLayer)   ||
            Physics.Raycast(groundCheckOrigin.position + new Vector3(0, 0, extraRayRadius), Vector3.down, groundRayCastDistance, groundLayer)   ||
            Physics.Raycast(groundCheckOrigin.position + new Vector3(0, 0, -extraRayRadius), Vector3.down, groundRayCastDistance, groundLayer) || 
            groundContacts > 0;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.layer == groundLayer) {
            groundContacts++;
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.layer == groundLayer) {

            groundContacts--;
        }
    }
}
