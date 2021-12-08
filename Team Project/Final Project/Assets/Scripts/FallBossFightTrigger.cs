using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallBossFightTrigger : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision) {
        if (GameObject.Find("Arena Platform").GetComponentInParent<Animator>().enabled) {       // just to make sure player can't activate it just by clipping
            SceneManager.LoadScene("Boss Fight");
        }
    }
}
