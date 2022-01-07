using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    Item item;
    public Image icon;
    public Button removeButton;
    private Inventory inventory;
    private EquipmentManager equipmentManager;
    private void Start()
    {
        inventory = GetComponentInParent<Inventory>();
        equipmentManager = GetComponentInParent<EquipmentManager>();
    }
    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.Icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;

    }

    public void OnRemoveButton()
    {
        //Inventory.instance.Remove(item);
        inventory.Remove(item);
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use(inventory,equipmentManager);
        }
    }
}
