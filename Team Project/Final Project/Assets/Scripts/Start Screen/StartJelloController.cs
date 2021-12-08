using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartJelloController : MonoBehaviour
{

    Animator anim;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        anim.SetBool("hasCollided", true);
    }

    public void PlayLandingSound() {
        AudioManager.instance.PlaySound("Medium Jello Footstep");
    }

    public void PlaySquishSound() {
        AudioManager.instance.PlaySound("Small Jello Footstep");
    }

}
