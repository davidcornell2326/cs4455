using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeParent : MonoBehaviour
{

    private Slime child;

    private void Start() {
        child = GetComponentInChildren<Slime>();
    }

    public void Land() {
        child.Land();
    }

    public void Jump() {
        child.PlayJumpSound();
    }

}
