using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
    public Transform EquipmentSlotParent;
    EquipmentSlot[] equipmentSlots;
    public EquipmentManager equipmentManager;
    Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        //inventory = Inventory.instance;
        //equipmentManager = GetComponentInChildren<EquipmentManager>();
        inventory = GetComponentInChildren<Inventory>();
        equipmentSlots = EquipmentSlotParent.GetComponentsInChildren<EquipmentSlot>();
        equipmentManager.onEquipmentChangedUI += UpdateEquipmentUI;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateEquipmentUI(int slotIndex)
    {
        Debug.Log("updating equipment ui");

        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (i < equipmentManager.currentEquipment.Length)
            {
                equipmentSlots[i].DisplayItem(inventory.items[i]);
            }
            else
            {
                //slots[i].ClearSlot();
            }
        }

    }
}
