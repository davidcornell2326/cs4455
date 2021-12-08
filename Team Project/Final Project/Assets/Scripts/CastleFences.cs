using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CastleFences : MonoBehaviour {
    private Animator anim;

    void Start() {
        anim = GetComponentInParent<Animator>();
    }

    public void DropFence() {
        print("Fences have been dropped");
        anim.SetTrigger("drop");
        AudioManager.instance.PlaySound("Fences Drop");
    }
}
