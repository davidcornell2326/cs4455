using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingBean : MonoBehaviour
{

    public float minWaitTime = 1f;
    public float maxWaitTime = 4f;
    public float maxHorizontalJumpVel = 3f;
    public float minVerticalJumpVel = 1f;
    public float maxVerticalJumpVel = 7f;

    private int groundContactCount = 0;
    private Rigidbody rb;

    private void Start() {
        rb = this.GetComponent<Rigidbody>();
        StartCoroutine(Jump());
    }


    private IEnumerator Jump() {
        while(true) {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            if (groundContactCount > 0) {
                float xVel = Random.Range(-maxHorizontalJumpVel, maxHorizontalJumpVel);
                float yVel = Random.Range(minVerticalJumpVel, maxVerticalJumpVel);
                float zVel = Random.Range(-maxHorizontalJumpVel, maxHorizontalJumpVel);
                rb.velocity = new Vector3(xVel, yVel, zVel);
            }

        }
        
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.transform.gameObject.CompareTag("ground")) {
            groundContactCount++;
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.transform.gameObject.CompareTag("ground")) {
            groundContactCount--;
        }
    }
}
