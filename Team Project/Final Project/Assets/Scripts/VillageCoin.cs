using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageCoin : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject coinTrigger;


    public void dropEnemies() {
        AudioManager.instance.PlaySound("Village Coin");
        for (int i = 0; i < enemies.Length; i++) {
            enemies[i].SetActive(true);
        }
        coinTrigger.SetActive(false);
    }

}
