using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject platformPathStart;
    public GameObject platformPathEnd;
    public float speed;
    private Transform startTransform;
    private Transform endTransform;
    private GameObject platform;

    // Start is called before the first frame update
    void Start()
    {
        platform = gameObject.transform.GetChild(0).gameObject;

        startTransform = platformPathStart.transform;
        endTransform = platformPathEnd.transform;

        platform.transform.position = startTransform.position;
        platform.transform.rotation = startTransform.rotation;

        StartCoroutine(MovePlatform(platform, endTransform, speed));
    }

    // Update is called once per frame
    void Update()
    {
        if (speed == 0f || startTransform.position == endTransform.position) {
            return;
        }
        if (platform.transform.position == endTransform.position) {
            StartCoroutine(MovePlatform(platform, startTransform, speed));
        }
        if (platform.transform.position == startTransform.position) {
            StartCoroutine(MovePlatform(platform, endTransform, speed));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.SetParent(platform.transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.parent = null;
        }
    }

    private IEnumerator MovePlatform(GameObject obj, Transform target, float speed)
    {
        Vector3 startPosition = obj.transform.position;
        Quaternion startRotation = obj.transform.rotation;

        Vector3 targetPosition = target.position;
        Quaternion targetRotation = target.rotation;
        float time = 0f;

        while (obj.transform.position != targetPosition)
        {
            obj.transform.position = Vector3.Lerp(startPosition, targetPosition, (time / Vector3.Distance(startPosition, targetPosition)) * speed);
            float angle = Quaternion.Angle(startRotation, targetRotation) * Mathf.Deg2Rad;
            obj.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, (time / (Quaternion.Angle(startRotation, targetRotation) * Mathf.Deg2Rad) / Vector3.Distance(startPosition, targetPosition)) * speed );

            time += Time.deltaTime;
            yield return null;
        }
    }
}
