using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JelloDropTrigger : MonoBehaviour {

    public string color;

    private void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.CompareTag("Player")) {
            try {
                JelloInventory jc = collision.gameObject.GetComponentInParent<JelloInventory>();

                if (jc) {
                    jc.AddJello(color);
                    AudioManager.instance.PlaySound("Jello Pickup");
                    //Debug.Log("Successfully added jello to inventory");
                }
            } catch {
                Debug.Log("Failed to add jello to inventory");
            } finally {
                ;
            }
            //EventManager.TriggerEvent<PickupEvent, GameObject>(this.transform.parent.gameObject);
            Destroy(this.transform.parent.gameObject, 0.0f);
        }
    }
}
