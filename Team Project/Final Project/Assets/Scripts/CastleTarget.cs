using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleTarget : MonoBehaviour {

    public Material m1;
    public Material m2;
    public float duration = 1f;

    private Renderer rend;

    void Start() {
        rend = GetComponentInParent<Renderer>();

        // At start, use the first material
        rend.material = m1;
    }

    void Update() {

        // ping-pong between the materials over the duration
        float lerp = Mathf.PingPong(Time.time, duration) / duration;
        rend.material.Lerp(m1, m2, lerp);

    }

    public void onDeath() {
        //print("DYING TARGET");
        if (CastleTargets.instance)
            CastleTargets.instance.ReduceTargetCount();
        AudioManager.instance.PlaySound("Target Break");
        Destroy(gameObject);
    }
}