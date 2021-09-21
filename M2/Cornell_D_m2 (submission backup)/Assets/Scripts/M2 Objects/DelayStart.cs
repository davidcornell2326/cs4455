using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DelayStart : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(ResetTimeScale());
    }

    private IEnumerator ResetTimeScale() {
        yield return new WaitForSecondsRealtime(3);
        SceneManager.LoadScene(1);      // load demo scene
    }

}
