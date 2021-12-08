using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour {
    [SerializeField] public int maxHealth = 100;
    [SerializeField] public int currentHealth = 1;
    public int healthReward = 1;
    public UnityEvent deathEvent;
    public UnityEvent hurtEvent;
    [SerializeField] private ParticleSystem onHitParticles;
    [SerializeField] private ParticleSystem onDeathParticles;
    [SerializeField] private string onHitSoundName;
    [SerializeField] private string onDeathSoundName;

    private PlayerController player;
    private Health playerHealth;
    private bool invincible = false;


    void Start() {
        currentHealth = maxHealth;
        player = FindObjectOfType<PlayerController>();
        playerHealth = player.GetComponentInParent<Health>();
        UpdatePlayerHealth();
    }

    public void TakeDamage(int damage) {
        //print("Taking " + damage + " damage (old health: " + currentHealth + ")");
        if (invincible) {
            return;
        }

        currentHealth -= damage;
        if (onHitParticles)
            Instantiate(onHitParticles, transform.position, transform.rotation);

        if (currentHealth <= 0)
            Die();
        else {
            if (onHitSoundName != null && onHitSoundName != "")
                AudioManager.instance.PlaySound(onHitSoundName);
            
            if (this.Equals(playerHealth)) {
                this.invincible = true;
                StartCoroutine(Invincibility());
            }
            
            hurtEvent.Invoke();
        }
        UpdatePlayerHealth();
    }

    public void AddHealth(int h) {
        currentHealth += h;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        UpdatePlayerHealth();
    }

    public void RefillHealth() {
        currentHealth = maxHealth;
        UpdatePlayerHealth();
    }

    private void UpdatePlayerHealth() {
        UIManager.instance.SetHP();
    }

    private IEnumerator Invincibility() {
        yield return new WaitForSeconds(2f);
        this.invincible = false;
    }

    public void Die() {
        UpdatePlayerHealth();
        if (playerHealth.currentHealth > 0) {       // if player isn't the one dying
            playerHealth.AddHealth(healthReward);   // reward player with health
        }

        if (onDeathParticles)
            Instantiate(onDeathParticles, transform.position, transform.rotation);
        if (onDeathSoundName != null && onDeathSoundName != "")
            AudioManager.instance.PlaySoundAtLocation(onDeathSoundName, transform.position);

        //this will allow us to call any events in the death sequence from the unity editor
        deathEvent.Invoke();
    }
}
