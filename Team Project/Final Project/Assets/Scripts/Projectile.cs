using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Projectile : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private ParticleSystem onDeathParticles;


    private void OnCollisionEnter(Collision collision)
    {
        var hp = collision.gameObject.GetComponentInParent<Health>();
        if (hp && hp.enabled)
        {
            hp.TakeDamage(damage);
            OnDeath();
            Destroy(gameObject);    //destroy this projectile after 
        }
    }

    public void OnDeath()
    {
        ParticleSystem particles = Instantiate(onDeathParticles, transform.position, Quaternion.identity);
        //Destroy(particles, 5)
    }
}
