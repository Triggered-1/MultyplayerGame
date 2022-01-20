using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    private EquipmentManager equipmentManager;
    [SerializeField] private Slider hpSlider;
    // Start is called before the first frame update
    public override void Start()
    {
        //EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
        equipmentManager = GetComponent<EquipmentManager>();
        equipmentManager.onEquipmentChanged += OnEquipmentChanged;
    }

    private void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            Armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
        }
        if (oldItem != null)
        {
            Armor.RemoveModifier(oldItem.armorModifier);
            damage.RemoveModifier(oldItem.damageModifier);
        }
    }
}
