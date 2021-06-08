using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryDisplay : MonoBehaviour
{
    private Player player;

    //Reminder to adjust the array amount to player data inventory var
    [SerializeField]
    private Image[] inventorySlots;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Start()
    {
        InstantiateDisplaySlots();
    }

    private void AddToDisplay(GameObject itemToUpdate)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (player.InvManager.Inventory[i].name == itemToUpdate.name)
            {
                inventorySlots[i].sprite = itemToUpdate.GetComponent<SpriteRenderer>().sprite;
            }
        }
    }

    private void InstantiateDisplaySlots()
    {
        Vector2 anchorPosition = new Vector2(0, 172);
        inventorySlots = new Image[player.P_Data.InventorySize];

        for (int i = 0; i < player.P_Data.InventorySize; i++)
        {
            inventorySlots[i] = CreateInventoryDisplaySlot(anchorPosition);
            anchorPosition += new Vector2(0, -115);
        }
    }

    private Image CreateInventoryDisplaySlot(Vector2 anchorPosition)
    {
        GameObject inventorySlot = new GameObject("Inventory Slot", typeof(Image));

        inventorySlot.transform.parent = transform;
        inventorySlot.transform.localPosition = Vector3.zero;

        inventorySlot.GetComponent<RectTransform>().anchoredPosition = anchorPosition;

        //Temporary Sprite Display
        Image slotSprite;
        slotSprite = inventorySlot.GetComponent<Image>();
        slotSprite.sprite = Resources.Load<Sprite>("Sprites/OutlineEmpty");

        return slotSprite;
    }
}
