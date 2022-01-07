using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeStatText : MonoBehaviour
{
    private EquipmentManager equipmentManager;
    private PlayerStats playerStats;
    public TMP_Text maxHealthTXT;
    public TMP_Text damageTXT;
    public TMP_Text armorTXT;
    public TMP_Text critChanceTXT;
    public TMP_Text critMultTXT;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GetComponentInParent<PlayerStats>();
        equipmentManager = GetComponentInParent<EquipmentManager>();
        equipmentManager.onEquipmentChangedUI += SetStatText;
        SetStatText(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetStatText(int slotIndex)
    {
        maxHealthTXT.SetText($"Max Health: {playerStats.MaxHealth.GetValue()}");
        damageTXT.SetText($"Damage: {playerStats.damage.GetValue()}");
        armorTXT.SetText($"Armor: {playerStats.Armor.GetValue()}");
        critChanceTXT.SetText($"Crit Chance: {playerStats.CritChance.GetValue()}");
        critMultTXT.SetText($"Crit Mult: {playerStats.CritDamageMultiplier.GetValue()}");
    }
}
