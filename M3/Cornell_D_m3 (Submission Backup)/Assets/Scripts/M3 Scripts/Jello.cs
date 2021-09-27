using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jello : MonoBehaviour
{

    private bool isAnimComplete = true;     // true when animation is on first frame

    private void Start() {
        this.GetComponent<Animator>().enabled = false;
    }

    private void OnTriggerEnter(Collider c) {
        print("Trigger enter");
        if (c.gameObject.GetComponent<BallCollector>() != null) {
            Animator anim = this.GetComponent<Animator>();
            if (anim != null) {
                anim.enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider c) {
        print("Trigger exit");
        if (c.gameObject.GetComponent<BallCollector>() != null) {
            Animator anim = this.GetComponent<Animator>();
            if (anim != null) {
                StartCoroutine(StopAnimation(anim));
            }
        }
    }

    private IEnumerator StopAnimation(Animator anim) {
        while (!isAnimComplete) {
            yield return null;
        }
        anim.enabled = false;
    }

    //private bool IsAnimComplete(Animator anim) {
    //    float time = anim.GetCurrentAnimatorStateInfo(0).normalizedTime % 1;    // ranges from 0 to 1
    //    print(time);
    //    return (time == 0);
    //}

    public void SetAnimComplete(int complete) {
        isAnimComplete = complete == 1;
    }

}
