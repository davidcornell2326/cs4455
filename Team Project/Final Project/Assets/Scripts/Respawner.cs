using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

class GameState {
    public int blueJello = 0;
    public int greenJello = 0;
    public int purpleJello = 0;
    public int yellowJello = 0;

    public Vector3 spawnPosition = Vector3.zero;

    public GameState(int blue, int green, int purple, int yellow, Vector3 spawn) {
        blueJello = blue;
        greenJello = green;
        purpleJello = purple;
        yellowJello = yellow;
        spawnPosition = spawn;
    }
}

public class Respawner : MonoBehaviour {
    public static Respawner instance;
    public bool reloadSceneOnRespawn = false;

    private GameState lastSavedState;

    void Awake() {
        //singleton!
        if (instance == null)
            instance = this;

        
    }

    private void Start() {
        SetSpawn(0, 0, 0, 0, Vector3.zero + new Vector3(103.5f, 4.6f, 289f)); // hardcode beginning point
    }

    public void SetSpawn(int blue, int green, int purple, int yellow, Vector3 spawn) {
        print("setting players spawn to: " + blue + ", " + green + ", " + purple + ", " + yellow + ", " + spawn);
        lastSavedState = new GameState(blue, green, purple, yellow, spawn);
    }

    public void Respawn(GameObject player) {
        Time.timeScale = 1;
        if (reloadSceneOnRespawn) {
            print("Reloading scene");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        } else {
            //print("respawning player");
            //now we respawn the player at the closest location
            player.GetComponentInParent<Health>().RefillHealth();
        }

        //eventually set up the players inventory
        //JelloInventory inv = player.GetComponentInParent<JelloInventory>();

        print("Respawning the player at the last saved spawn position: " + lastSavedState.spawnPosition);
        player.transform.position = lastSavedState.spawnPosition;
    }
}
