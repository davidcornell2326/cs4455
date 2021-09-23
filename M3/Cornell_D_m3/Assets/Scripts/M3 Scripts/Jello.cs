using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jello : MonoBehaviour
{

    private void Start() {
        this.GetComponent<Animator>().enabled = false;
    }

    private void OnTriggerEnter(Collider c) {
        if (c.gameObject.GetComponent<BallCollector>() != null) {
            Animator anim = this.GetComponent<Animator>();
            if (anim != null) {
                anim.enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider c) {
        if (c.gameObject.GetComponent<BallCollector>() != null) {
            Animator anim = this.GetComponent<Animator>();
            if (anim != null) {
                anim.enabled = false;
            }
        }
    }
}
