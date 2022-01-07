using UnityEngine;

[CreateAssetMenu(fileName = "New Item",menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string Name = "New Item";
    public Sprite Icon = null;
    public bool isDefaultItem = false;

    public virtual void Use()
    {
        //Use the Item
        //Something might happpen

        Debug.Log("Using " + name);
    }
    public virtual void Use(Inventory inventory)
    {
        //Use the Item
        //Something might happpen

        Debug.Log("Using " + name);
    }public virtual void Use(Inventory inventory,EquipmentManager equipmentManager)
    {
        //Use the Item
        //Something might happpen

        Debug.Log("Using " + name);
    }

    public void RemoveFromInventory(Inventory inventory)
    {
        //Inventory.instance.Remove(this);
        inventory.Remove(this);
    }

}
