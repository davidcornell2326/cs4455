using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggerContactDamager : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private string optionalAudioName;

    private void OnTriggerEnter(Collider c)
    {
        Health hp = c.gameObject.GetComponentInParent<Health>();
        if (hp)
        {
            if (optionalAudioName != "")
                AudioManager.instance.PlaySound(optionalAudioName);

            hp.TakeDamage(damage);
            //print("Contact damage taken by player from " + gameObject.name + ", remaining health is: " + hp.currentHealth);
        }
    }
}
