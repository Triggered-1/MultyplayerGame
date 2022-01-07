using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{

    public EquipmentSlotEnum equipmentSlot;

    public float armorModifier;
    public float damageModifier;

    public override void Use(Inventory inventory,EquipmentManager equipmentManager)
    {
        base.Use();

        //EquipmentManager.instance.Equip(this);
        equipmentManager.Equip(this);
        RemoveFromInventory(inventory);
    }
}

public enum EquipmentSlotEnum
{
    Weapon, Offhand, Accessory1, Accessory2, Accessory3, Accessory4
}