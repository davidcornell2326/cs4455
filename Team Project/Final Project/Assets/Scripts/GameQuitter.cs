using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameQuitter : MonoBehaviour {

    void OnQuit(InputValue quitValue) {
        float quit = quitValue.Get<float>();
        //if (quit > 0) {
        //    QuitGame();
        //}
    }

    public static void QuitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

}
