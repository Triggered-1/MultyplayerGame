using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public Equipment[] currentEquipment;
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;
    public delegate void OnEquipmentChangedUI(int slotIndex);
    public OnEquipmentChangedUI onEquipmentChangedUI;

    Inventory inventory;

    private void Start()
    {
        inventory = GetComponent<Inventory>();

        int numbSlots = System.Enum.GetNames(typeof(EquipmentSlotEnum)).Length;
        currentEquipment = new Equipment[numbSlots];
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipmentSlot;

        Equipment oldItem = null;

        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }
        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }
        if (onEquipmentChangedUI != null)
        {
            onEquipmentChangedUI.Invoke(slotIndex);
        }
        currentEquipment[slotIndex] = newItem;
    }

    public void Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }
        }

    }

    public void DropEquipment()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Instantiate(currentEquipment[i].PickUpGameObject, this.transform.position, Quaternion.identity);
        }
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }
}
