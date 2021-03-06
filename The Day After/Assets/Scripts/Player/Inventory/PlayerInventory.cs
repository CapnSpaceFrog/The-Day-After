using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory
{
    public GameObject[] Inventory;

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
        if (itemToFind == null)
        {
            return true;
        }
        for (int i = 0; i < Inventory.Length; i++)
        {
            if (Inventory[i] == null)
            {
                continue;
            } else if (Inventory[i].name == itemToFind.name)
            {
                GameObject.FindGameObjectWithTag("Inventory Panel").SendMessage("RemoveFromDisplay", itemToFind);
                return true;
            }
        }
        //Item was not found
        return false;
    }
}
