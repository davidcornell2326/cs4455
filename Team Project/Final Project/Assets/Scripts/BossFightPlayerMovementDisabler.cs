using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightPlayerMovementDisabler : MonoBehaviour
{

    public float disableMovementSeconds = 3f;
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        player.canMove = false;
        StartCoroutine(EnableMovementAfterSeconds(disableMovementSeconds));
    }

    private IEnumerator EnableMovementAfterSeconds(float seconds) {
        yield return new WaitForSeconds(seconds);
        player.canMove = true;
    }

    private void Update() {
        print(player.canMove);
    }
}
