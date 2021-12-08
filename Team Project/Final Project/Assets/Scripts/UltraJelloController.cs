using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class UltraJelloController : MonoBehaviour
{

    public void DoEndSequence() {
        Animator anim = GetComponentInParent<Animator>();
        anim.SetTrigger("rise");
    }

    public void StartCameraFade() {
        if (!SceneManager.GetActiveScene().name.Equals("Boss Fight")) {     // on all except Boss Fight, do fake ending
            GameObject.Find("Arena Platform").GetComponentInParent<Animator>().enabled = true;
        } else {
            Volume postProcessing = FindObjectOfType<Volume>();
            postProcessing.enabled = true;
            Light light = transform.GetChild(1).gameObject.GetComponentInParent<Light>();
            StartCoroutine(BrightenLight(light));
        }
    }

    private IEnumerator BrightenLight(Light light) {
        for (int i = 0; i < 200; i++) {
            light.intensity = Mathf.Pow(1.1f, i);
            yield return new WaitForSeconds(0.001f);
        }
        print("Loading Win Screen");
        CursorLock.SetCursorLock(false);
        SceneManager.LoadScene("Win Screen");
    }

    public void SetTriggerEnabled(int i) {
        print("Enabled: " + (i == 1));
        foreach (SphereCollider coll in GetComponentsInChildren<SphereCollider>()) {
            coll.enabled = i == 1;
        }
    }

}
