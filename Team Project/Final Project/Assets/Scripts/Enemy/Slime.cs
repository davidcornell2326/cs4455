using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour {

    public ParticleSystem deathParticles;
    public Transform[] spawns;
    public GameObject[] enemySpawnTypes;
    public float avgNumSpawns = 3f;
    public float groundRayCastDistance = 0.02f;
    public float minSecondsBetweenJumps = 6f;
    public float maxSecondsBetweenJumps = 15f;
    public float jumpVerticalForce = 1000f;
    public GameObject drop;
    public int numDrops = 5;
    public float invincibilityDuration = 10f;
    public int maxNumEnemies = 8;

    private Animator anim;
    private PlayerController player;
    private bool isInMiddle = false;
    private string[] jumpFromSideTriggers = { "JumpAcross", "JumpStraight" };
    private string[] jumpFromMiddleTriggers = { "JumpBack", "JumpLeft", "JumpRight" };
    private int numWeakSpotsRemaining = 3;
    private SlimeWeakSpot[] weakSpots;


    void Start() {
        anim = GetComponentInParent<Animator>();
        player = FindObjectOfType<PlayerController>();
        weakSpots = GetComponentsInChildren<SlimeWeakSpot>();
        StartCoroutine(Jump());
    }

    private IEnumerator Jump() {

        while (true) {
            yield return new WaitForSeconds(Random.Range(minSecondsBetweenJumps, maxSecondsBetweenJumps));

            if (isInMiddle) {
                anim.SetTrigger(jumpFromMiddleTriggers[Random.Range(0, jumpFromMiddleTriggers.Length)]);
                isInMiddle = false;
            } else {
                string choice = jumpFromSideTriggers[Random.Range(0, jumpFromSideTriggers.Length)];
                anim.SetTrigger(choice);
                if (choice.Equals("JumpStraight")) {
                    isInMiddle = true;
                }
            }
        }
    }

    public void Land() {
        PlayLandSound();
        float chance = avgNumSpawns / spawns.Length;
        if (numWeakSpotsRemaining == 2) {       // increase spawns as it gets closer to death
            chance *= 1.25f;
        } else if (numWeakSpotsRemaining == 1) {
            chance *= 1.5f;
        }
        ContactDamager[] enemies = FindObjectsOfType<ContactDamager>();
        int numEnemies = enemies.Length;
        foreach (Transform t in spawns) {
            if (Random.Range(0f, 1f) < chance && numEnemies < maxNumEnemies + 1) {
                SpawnEnemy(t);
            }
        }
    }

    private void SpawnEnemy(Transform t) {
        if (Vector3.Distance(t.position, player.transform.position) < 6) {      // don't spawn if too close to player
            return;
        }
        int choice = Random.Range(0, enemySpawnTypes.Length);
        Instantiate(enemySpawnTypes[choice], t.position, t.rotation);
    }

    public void WeakSpotDestroyed() {
        numWeakSpotsRemaining--;
        if (numWeakSpotsRemaining <= 0) {
            Die();
        } else {
            AudioManager.instance.PlaySound("Boss Scream");
            StartCoroutine(Invincibility());
        }
    }

    private IEnumerator Invincibility() {
        
        foreach (SlimeWeakSpot weakSpot in weakSpots) {
            if (weakSpot) {
                weakSpot.invincible = true;
            }
        }
        yield return new WaitForSeconds(invincibilityDuration);
        foreach (SlimeWeakSpot weakSpot in weakSpots) {
            if (weakSpot) {
                weakSpot.invincible = false;
            }
        }
    }

    public void PlayJumpSound() {
        AudioManager.instance.PlaySound("Boss Jump");
    }

    public void PlayLandSound() {
        AudioManager.instance.PlaySound("Boss Land");
    }

    public void Die() {
        AudioManager.instance.PlaySound("Boss Death");

        for (int i = 0; i < numDrops; i++) {
            //Instantiate(drop, this.transform.position, Quaternion.identity);
            Instantiate(drop, new Vector3(0, 2.5f, 0), Quaternion.identity);   // specific to the boss
        }

        ContactDamager[] enemies = FindObjectsOfType<ContactDamager>();
        foreach (ContactDamager enemy in enemies) {
            Destroy(enemy.gameObject);
        }

        GameObject.Find("Bridges").GetComponentInParent<Animator>().SetTrigger("RaiseBridges");
        AudioManager.instance.PlaySound("Bridge Rising");

        ParticleSystem particles = Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(particles, 10);  //destroys these particles after 2 seconds

        Destroy(gameObject);
    }

}
