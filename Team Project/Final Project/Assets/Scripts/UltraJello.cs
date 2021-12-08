using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltraJello : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision) {
        print("collision: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player")) {
            try {
                GameObject parent = this.transform.parent.gameObject;
                print(parent);
                parent.GetComponentInParent<UltraJelloController>().DoEndSequence();
            } catch {
                Debug.Log("Failed to collect ultra jello");
            } finally {
                ;
            }
        }
    }
}
