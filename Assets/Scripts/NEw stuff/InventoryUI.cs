using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public Transform EquipmentSlotParent;
    public GameObject inventoryUI;
    private InventorySlot[] slots;
    private EquipmentSlot[] equipmentSlots;
    private Inventory inventory;
    private EquipmentManager equipmentManager;

    // Start is called before the first frame update
    void Start()
    {
        //inventory = Inventory.instance;
        equipmentManager = GetComponentInParent<EquipmentManager>();
        inventory = GetComponentInParent<Inventory>();
        inventory.onItemChangedCallback += UpdateUI;
        equipmentManager.onEquipmentChangedUI += UpdateEquipmentUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        equipmentSlots = EquipmentSlotParent.GetComponentsInChildren<EquipmentSlot>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateUI()
    {
        Debug.Log("updating ui");

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    //Displays the Equipt Item on the UI
    void UpdateEquipmentUI(int slotIndex)
    {
        Debug.Log("updating equipment ui");

        if (slotIndex < inventory.items.Count)
        {
            equipmentSlots[slotIndex].DisplayItem(inventory.items[slotIndex]);
        }
    }
}
