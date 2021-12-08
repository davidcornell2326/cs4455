using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoStation : MonoBehaviour
{
    Animator anim;
    public bool available = true;
    public float cooldown = 15;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void UseStation()
    {
        print("using ammo station");
        if (available)
        {
            print("refilling player ammo");
            AudioManager.instance.PlaySound("Ammo Refill");
            GameObject player = GameObject.Find("Player");
            player.GetComponent<PlayerController>().RefillAmmo();
            player.GetComponent<Health>().RefillHealth();
            available = false;

            AudioManager.instance.PlaySound("Refill Ammo");
            anim.SetTrigger("activate");

            Invoke("ResetStation", cooldown);
        }
    }

    void ResetStation()
    {
        available = true;
        anim.SetTrigger("reset");
    }
}
