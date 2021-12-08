using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]

public class SpawnSetter : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponentInChildren<PlayerController>() || other.gameObject.GetComponentInParent<PlayerController>()) {
            PlayerController player = FindObjectOfType<PlayerController>();
            JelloInventory inv = player.GetComponentInParent<JelloInventory>();

            Respawner.instance.SetSpawn(inv.numBlueJello, inv.numGreenJello, inv.numPurpleJello, inv.numYellowJello, transform.position);
            //print("SpawnSetter: " + this.gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.GetComponentInChildren<PlayerController>() || other.gameObject.GetComponentInParent<PlayerController>()) {
            PlayerController player = FindObjectOfType<PlayerController>();
            JelloInventory inv = player.GetComponentInParent<JelloInventory>();

            Respawner.instance.SetSpawn(inv.numBlueJello, inv.numGreenJello, inv.numPurpleJello, inv.numYellowJello, transform.position);
            //print("SpawnSetter: " + this.gameObject.name);
        }
    }
}
