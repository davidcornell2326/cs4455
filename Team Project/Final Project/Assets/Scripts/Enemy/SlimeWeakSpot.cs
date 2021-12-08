using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeWeakSpot : MonoBehaviour
{

    public Material notInvincibleM1;
    public Material notInvincibleM2;
    public Material invincibleM1;
    public Material invincibleM2;
    public float duration = 1f;
    public bool invincible = false;
    
    
    private Renderer rend;
    private Health health;

    void Start() {
        rend = GetComponentInParent<Renderer>();
        health = GetComponentInParent<Health>();

        // At start, use the first material
        rend.material = notInvincibleM1;
    }

    void Update() {

        if (invincible) {
            health.enabled = false;
        } else {
            health.enabled = true;
        }

        // ping-pong between the materials over the duration
        float lerp = Mathf.PingPong(Time.time, duration) / duration;
        if (!invincible) {
            rend.material.Lerp(notInvincibleM1, notInvincibleM2, lerp);
        } else {
            rend.material.Lerp(invincibleM1, invincibleM2, lerp);
        }
        
    }

    public void Die() {
        Slime slime = GetComponentInParent<Slime>();
        if (slime != null) {
            slime.WeakSpotDestroyed();
        }
        Destroy(this.gameObject);
    }

}
