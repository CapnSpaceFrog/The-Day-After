using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryDisplay : MonoBehaviour
{
    //Reminder to adjust the array amount to player data inventory var
    private GameObject[] inventorySlots;

    public void Start()
    {
        Vector2 anchorPosition = new Vector2(0, 0);
        inventorySlots = new GameObject[4];

        for (int i = 0; i < 4; i++)
        {
            inventorySlots[i] = CreateInventoryDisplaySlots(anchorPosition);
            anchorPosition += new Vector2(0, 25);
        }
    }
    private GameObject CreateInventoryDisplaySlots(Vector2 anchorPosition)
    {
        GameObject inventorySlot = new GameObject("Inventory Slot", typeof(Image));

        inventorySlot.transform.parent = transform;
        inventorySlot.transform.localPosition = Vector3.zero;

        inventorySlot.GetComponent<RectTransform>().anchoredPosition = anchorPosition;
        inventorySlot.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);

        //Temporary Sprite Display
        Image inventorySprite;
        inventorySprite = inventorySlot.GetComponent<Image>();
        inventorySprite.sprite = Resources.Load("Sprites/Test Sprites/Square");

        return inventorySlot;
    }
}
