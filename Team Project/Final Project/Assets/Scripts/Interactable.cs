using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public ParticleSystem interactionParticles;
    public UnityEvent onInteract;


    public void Interact() {
        if (interactionParticles) {
            // Special check to prevent ammo station particles going every time:
            AmmoStation ammoStation = this.gameObject.GetComponentInParent<AmmoStation>();
            if (ammoStation != null) {
                if (!ammoStation.available) {
                    return;
                }
            }


            Instantiate(interactionParticles, transform.position, transform.rotation);
        }
            

        onInteract.Invoke();
    }
}
