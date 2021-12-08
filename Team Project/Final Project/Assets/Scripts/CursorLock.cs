using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLock : MonoBehaviour {
    public static bool isLocked = false;

    private void Start() {
        SetCursorLock(true);
    }

    public static void SetCursorLock(bool l) {
        isLocked = l;
        if (isLocked) {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        } else {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

}