using Photon.Pun;
using UnityEngine;

public class ItemPickUp : Interactable
{
    public Item item;

    public override void Interact(Inventory inventory)
    {
        base.Interact(inventory);

        PickUp(inventory);
    }

    void PickUp(Inventory inventory)
    {
        Debug.Log("Picking Up " + item.name);
        bool wasPickedUp = inventory.Add(item);
        if (wasPickedUp)
        {
            photonView.RPC("DestroyPickUpRPC", RpcTarget.MasterClient);
        }
    }

    [PunRPC]
    private void DestroyPickUpRPC()
    {
        PhotonNetwork.Destroy(gameObject);
    }
}
