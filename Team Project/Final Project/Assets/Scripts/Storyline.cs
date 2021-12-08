using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storyline : MonoBehaviour
{
    private int scene = 0;
    public int timer;

    public GameObject[] scenes;
    public GameObject darkCloud;
    public GameObject lightCloud;


    public void Start()
    {
        darkCloud.SetActive(false);
        lightCloud.SetActive(false);
        scenes[0].SetActive(true);
        for (int i = 1; i < scenes.Length; i++)
        {
            scenes[i].SetActive(false);
        }
    }

    public void OnContinue()
    {
        Debug.Log("in continue");
        if (scene < scenes.Length - 1)
        {
            AdvanceScene();
        } else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        }
    }

    private void AdvanceScene()
    {
        scenes[scene].SetActive(false);
        if (scene == 0)
        {
            lightCloud.SetActive(true);
            scene++;
            Invoke("ActivateScene", 1);
        } else if (scene == scenes.Length - 2)
        {
            lightCloud.SetActive(false);
            darkCloud.SetActive(true);
            scene++;
            AudioManager.instance.PlaySound("Walk");
            Invoke("ActivateScene", 4);
        } else
        {
            scene++;
            Invoke("ActivateScene", 1);
        }
    }

    private void ActivateScene()
    {
        scenes[scene].SetActive(true);
    }
}
