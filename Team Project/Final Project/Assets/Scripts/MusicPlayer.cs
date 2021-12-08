using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoopSong());
    }

    private IEnumerator LoopSong() {
        AudioSource[] sources = GetComponents<AudioSource>();
        AudioSource songStart;
        AudioSource songLoop;
        if (SceneManager.GetActiveScene().name.Equals("Boss Fight")) {
            if (sources[0].clip.name.Equals("Haunting Monster Sound")) {
                songStart = sources[0];
                songLoop = sources[1];
            } else {
                songStart = sources[1];
                songLoop = sources[0];
            }
        } else {
            if (sources[0].clip.name.Equals("Song-Start")) {
                songStart = sources[0];
                songLoop = sources[1];
            } else {
                songStart = sources[1];
                songLoop = sources[0];
            }
        }
        songStart.Play();
        yield return new WaitForSeconds(songStart.clip.length);
        songLoop.Play();    // the AudioSource is already set to loop
    }

}
