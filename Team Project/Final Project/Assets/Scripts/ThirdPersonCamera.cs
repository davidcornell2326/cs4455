using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {

    public float maxZDistance = 3;
    public float minZDistance = 1;
    public float bufferDistance = .1f;
    public float smoothing = .5f;

    public Transform rayStart;
    public Transform rayEnd;
    public LayerMask blockingLayer;
    public Transform camTransform;

    void Update() {
        RaycastHit hit;
        Vector3 pos = new Vector3(camTransform.localPosition.x, camTransform.localPosition.y, -maxZDistance);

        if (Physics.Raycast(transform.position, (rayEnd.position - rayStart.position), out hit, maxZDistance, blockingLayer)) {
            if (hit.distance >= minZDistance)
                pos.z = bufferDistance - hit.distance;
        }

        camTransform.localPosition = pos;
    }
}
