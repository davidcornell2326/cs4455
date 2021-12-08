using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTorches : MonoBehaviour {

    public GameObject blueFlame;
    public GameObject greenFlame;
    public GameObject purpleFlame;
    public GameObject yellowFlame;

    public void ignite(JelloColor color) {
        AudioManager.instance.PlaySound("Big Torch Lighting");
        switch(color) {
            case JelloColor.BLUE:
                blueFlame.SetActive(true);
                break;
            case JelloColor.GREEN:
                greenFlame.SetActive(true);
                break;
            case JelloColor.PURPLE:
                purpleFlame.SetActive(true);
                break;
            case JelloColor.YELLOW:
                yellowFlame.SetActive(true);
                break;
            default:
                Debug.Log("Could not ignite flame: Invalid color.");
                break;
        }
    }
}