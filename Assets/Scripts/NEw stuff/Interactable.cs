using UnityEngine;
using Photon.Pun;
public class Interactable : MonoBehaviourPun
{
    public float radius = 3f;

    bool hasInteracted = false;
    Transform player;

    public virtual void Interact()
    {
        Debug.Log("Interacting with" + transform.name);
    }
    public virtual void Interact(Inventory inventory)
    {
        Debug.Log("Interacting with" + transform.name);
    }
    private void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }
    private void Update()
    {
        //if (!hasInteracted)
        //{
        //    float distance = Vector2.Distance(player.position, transform.position);
        //    if (distance <= radius)
        //    {
        //        Interact();
        //        hasInteracted = true;
        //    }
        //}
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (photonView != null && collision.CompareTag("Player"))
        {
            Interact(collision.GetComponent<Inventory>());
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);

    }
}
