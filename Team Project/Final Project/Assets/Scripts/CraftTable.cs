using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftTable : MonoBehaviour
{
    GameObject player;
    JelloInventory inventory;
    public GameObject ultraJelloPrefab;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        inventory = player.GetComponentInParent<JelloInventory>();
    }

    public bool CheckJelloCount()
    {
        return (inventory.numBlueJello == inventory.numBlueJelloRequired &&
            inventory.numGreenJello == inventory.numGreenJelloRequired &&
            inventory.numYellowJello == inventory.numYellowJelloRequired &&
            inventory.numPurpleJello == inventory.numPurpleJelloRequired);
    }

    public void CraftJello()
    {
        if (CheckJelloCount())
        {
            //spawn boss jello
            //Debug.Log("Jello count good");
            //gameObject.SetActive(false);
            inventory.numBlueJello = -1;
            inventory.AddJello("Blue");
            inventory.numGreenJello = -1;
            inventory.AddJello("Green");
            inventory.numYellowJello = -1;
            inventory.AddJello("Yellow");
            inventory.numPurpleJello = -1;
            inventory.AddJello("Purple");


            Instantiate(ultraJelloPrefab, this.transform);


        } else
        {
            Debug.Log("Not enough jello");
        }
    }
}
