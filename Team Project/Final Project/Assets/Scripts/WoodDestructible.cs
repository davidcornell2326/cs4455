using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class WoodDestructible : MonoBehaviour {
    public float deathRadius = 5.0f;
    public float deathExplosionForce = 10.0f;
    public int damage = 10;
    public GameObject explosionParticles;
    public LayerMask layersToExplode;
    public float magnitudeRequiredForSound = .25f;

    public void Explode() {
        GetComponentInParent<Health>().RefillHealth();
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, deathRadius, layersToExplode);
        foreach (Collider hit in colliders) {
            if (hit.transform.root == transform) {
                continue;
            }
            print("exploding: " + hit.gameObject.name);

            Rigidbody rb = hit.GetComponentInParent<Rigidbody>();
            Health hp = hit.GetComponentInParent<Health>();

            if (hp != null)
                hp.TakeDamage(damage);

            if (rb != null) {
                print("adding explosion force");
                rb.AddForce(deathExplosionForce * (hit.transform.position - explosionPos).normalized, ForceMode.Impulse);
            }
        }

        if (explosionParticles) {
            var part = Instantiate(explosionParticles, transform.position, Quaternion.identity);
            Destroy(part, 2);
        }

        print("Barrel destryoed");
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.impulse.magnitude >= magnitudeRequiredForSound) {
            AudioManager.instance.PlaySoundAtLocation("Wood Bump", transform.position);
        }
    }
}
