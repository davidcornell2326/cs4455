using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioEventManager : MonoBehaviour
{
    public EventSound3D eventSound3DPrefab;

    //public AudioClip pickupAudio;

    private UnityAction<GameObject> pickupEventListener;

    void Awake() {
        //pickupEventListener = new UnityAction<GameObject>(pickupEventHandler);
    }

    void OnEnable() {
        EventManager.StartListening<PickupEvent, GameObject>(pickupEventListener);
    }

    void OnDisable() {
        EventManager.StopListening<PickupEvent, GameObject>(pickupEventListener);
    }

    //void pickupEventHandler(GameObject obj)
    //{
    //    if (eventSound3DPrefab)
    //    {
    //        EventSound3D snd = Instantiate(eventSound3DPrefab, obj.transform);
    //        snd.audioSrc.clip = pickupAudio;
    //        snd.audioSrc.minDistance = 5f;
    //        snd.audioSrc.maxDistance = 100f;
    //        snd.audioSrc.Play();
    //    }
    //}

}
