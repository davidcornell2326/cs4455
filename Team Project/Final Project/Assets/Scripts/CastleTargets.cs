using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleTargets : MonoBehaviour {

    public static CastleTargets instance;
    public int numTargetsRemaining;
    private int totalTargets;

    void Awake() {
        //singleton!
        if (instance == null)
            instance = this;
    }

    void Start() {
        CastleTarget[] targetsRemaining = FindObjectsOfType<CastleTarget>();
        foreach (CastleTarget target in targetsRemaining) {
            if (target.enabled)
                numTargetsRemaining++;
        }
        totalTargets = numTargetsRemaining;

        //print("Number of castle targets required to shoot is: " + numTargetsRemaining);
    }

    public void ReduceTargetCount() {
        numTargetsRemaining--;
        UIManager.instance.UpdateTargetCount((totalTargets - numTargetsRemaining), totalTargets);
        print(numTargetsRemaining);

        if (numTargetsRemaining <= 0) {
            var fences = FindObjectsOfType<CastleFences>();
            foreach (var fence in fences) {
                fence.DropFence();
            }
        }
    }
}
