using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentSlot : MonoBehaviour
{
    Item item;
    public Image icon;
    public Button removeButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void DisplayItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.Icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }
}
