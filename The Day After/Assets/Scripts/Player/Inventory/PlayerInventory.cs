using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory
{
    public GameObject[] Inventory { get; private set; }

    public PlayerInventory(PlayerData p_data)
    {
        //Instantiate new Inventory on creation of class instance
        Inventory = new GameObject[p_data.InventorySize];
    }

    public bool AddItemToInv(GameObject itemToAdd)
    {
        for (int i = 0; i < Inventory.Length; i++)
        {
            if (Inventory[i] == null) {
                //Empty spot found, add item and break
                Inventory[i] = itemToAdd;
                //Send Message to invetory display to add item
                GameObject.FindGameObjectWithTag("Inventory Panel").SendMessage("AddToDisplay", itemToAdd);
                return true;
            }
        }

        //No empty spot was found, inventory is full
        return false;
    }

    public bool FindItemInInv(GameObject itemToFind)
    {
        for (int i = 0; i < Inventory.Length; i++)
        {
            Debug.Log("Are we looking for the item?");
            if (Inventory[i].name == itemToFind.name)
            {
                Debug.Log("do we find the item?");
                return true;
            }
        }

        //Item was not found
        return false;
    }
}
