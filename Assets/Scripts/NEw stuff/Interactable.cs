using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;

    bool hasInteracted = false;
    Transform player;

    public virtual void Interact()
    {
        Debug.Log("Interacting with" + transform.name);
    }
    private void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }
    private void Update()
    {
        if (!hasInteracted)
        {
            float distance = Vector2.Distance(player.position, transform.position);
            if (distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);

    }
}
