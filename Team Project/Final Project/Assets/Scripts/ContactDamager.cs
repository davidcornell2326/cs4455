using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamager : MonoBehaviour {
    [SerializeField]
    private int damage = 1;


    private void OnCollisionEnter(Collision collision) {
        Health hp = collision.gameObject.GetComponentInParent<Health>();
        if (hp && hp.GetComponentInParent<PlayerController>()) {
            hp.TakeDamage(damage);
            ContactDamageEffects(hp);
        }
    }

    public void ContactDamageEffects(Health hp) {
        // This is where any effects, e.g. sound effects for contact damage, can go
        print("Contact damage taken by player from " + this.gameObject.name + ", remaining health is: " + hp.currentHealth);
    }
}
