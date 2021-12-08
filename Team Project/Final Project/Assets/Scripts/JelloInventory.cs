using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JelloInventory : MonoBehaviour
{
    public int numBlueJello = 0;
    public int numBlueJelloRequired = 5;

    public int numGreenJello = 0;
    public int numGreenJelloRequired = 5;

    public int numPurpleJello = 0;
    public int numPurpleJelloRequired = 5;

    public int numYellowJello = 0;
    public int numYellowJelloRequired = 5;

    private LevelTorches levelTorches;

    private void Start() {
        UIManager.instance.SetInventorySlider(numBlueJello, numBlueJelloRequired, JelloColor.BLUE);
        UIManager.instance.SetInventorySlider(numGreenJello, numGreenJelloRequired, JelloColor.GREEN);
        UIManager.instance.SetInventorySlider(numPurpleJello, numPurpleJelloRequired, JelloColor.PURPLE);
        UIManager.instance.SetInventorySlider(numYellowJello, numYellowJelloRequired, JelloColor.YELLOW);
        levelTorches = FindObjectOfType<LevelTorches>();
    }

    public void AddJello(string color) {
        //print("Obtained a " + color + " jello");
        numBlueJello = Mathf.Min(numBlueJello, numBlueJelloRequired);
        numGreenJello = Mathf.Min(numGreenJello, numGreenJelloRequired);
        numPurpleJello = Mathf.Min(numPurpleJello, numPurpleJelloRequired);
        numYellowJello = Mathf.Min(numYellowJello, numYellowJelloRequired);

        switch (color) {
            case "Blue":
                if (numBlueJello < numBlueJelloRequired) {
                    numBlueJello++;
                    UIManager.instance.SetInventorySlider(numBlueJello, numBlueJelloRequired, JelloColor.BLUE);

                    if (numBlueJello == numBlueJelloRequired) {
                        levelTorches.ignite(JelloColor.BLUE);
                    }
                }
                break;
            case "Green":
                if (numGreenJello < numGreenJelloRequired) {
                    numGreenJello++;
                    UIManager.instance.SetInventorySlider(numGreenJello, numGreenJelloRequired, JelloColor.GREEN);

                    if (numGreenJello == numGreenJelloRequired) {
                        levelTorches.ignite(JelloColor.GREEN);
                    }
                }
                break;
            case "Purple":
                if (numPurpleJello < numPurpleJelloRequired) {
                    numPurpleJello++;
                    UIManager.instance.SetInventorySlider(numPurpleJello, numPurpleJelloRequired, JelloColor.PURPLE);

                    if (numPurpleJello == numPurpleJelloRequired) {
                        levelTorches.ignite(JelloColor.PURPLE);
                    }
                }
                break;
            case "Yellow":
                if (numYellowJello < numYellowJelloRequired) {
                    numYellowJello++;
                    UIManager.instance.SetInventorySlider(numYellowJello, numYellowJelloRequired, JelloColor.YELLOW);

                    if (numYellowJello == numYellowJelloRequired) {
                        levelTorches.ignite(JelloColor.YELLOW);
                    }
                }
                break;
            case "Ultra":
                print("TRIGGER ENDING SEQUENCE");
                break;
            default:
                break;
        }
    }
}