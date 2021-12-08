using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperProjectile : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision) {
        StartCoroutine(DestroyProjectile());
    }

    private IEnumerator DestroyProjectile() {
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
    }
}
