using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;

public class Inventory : MonoBehaviourPun
{
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    private PhotonView view;

    public List<Item> items = new List<Item>();
    public int Space = 20;
    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

    public bool Add(Item item)
    {
        if (!item.isDefaultItem)
        {
            if (items.Count >= Space)
            {
                Debug.Log("Not enough room");
                return false;
            }
            items.Add(item);
            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
        }

        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}
