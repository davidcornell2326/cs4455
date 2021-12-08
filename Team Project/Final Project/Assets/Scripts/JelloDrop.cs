using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JelloColor
{
    GREEN = 0,
    BLUE = 1,
    PURPLE = 2,
    YELLOW = 3
}

public class JelloDrop : MonoBehaviour {

    public float maxHorizontalForce = 200f;
    public float maxVerticalForce = 200f;
    private Rigidbody rb;

    void Start() {
        rb = GetComponentInParent<Rigidbody>();
        rb.AddForce(new Vector3(
            Random.Range(-maxHorizontalForce, maxHorizontalForce),
            Random.Range(0f, maxVerticalForce),
            Random.Range(-maxHorizontalForce, maxHorizontalForce)
            ));
    }

}
