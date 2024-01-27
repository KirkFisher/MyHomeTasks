using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject inventoryObj;
    public InventoryObject inventory;
    public bool isOpen;
    public void OnTriggerEnter2D(Collider2D other)
    {
        var item = other.GetComponent<GroundItem>();
        if (item)
        {

            Item _item = new Item(item.item);
            inventory.AddItem(_item, 1);
            Destroy(other.gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isOpen = !isOpen;
            if (isOpen)
            {
                inventoryObj.SetActive(true);
            }
            else
            {
                inventoryObj.SetActive(false);
            }
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Items = new InventorySlot[10];
    }
}
