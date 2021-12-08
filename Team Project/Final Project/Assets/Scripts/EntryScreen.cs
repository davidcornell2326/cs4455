using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryScreen : MonoBehaviour
{
    public void LoadStartScene() {
        //print("LOADING");
        SceneManager.LoadScene("Boss Fight");
    }
}
