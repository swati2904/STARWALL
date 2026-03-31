using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable
{
    void Interact();
}

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private float interactRange = 2f;
    [SerializeField] private LayerMask interactLayerMask;

    // Called by Input System "Interact" action
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        TryInteract();
    }

    private void TryInteract()
    {
        // Find nearby interactables in a small radius for beginner-friendly interaction.
        Collider[] hits = Physics.OverlapSphere(transform.position, interactRange, interactLayerMask);
        if (hits.Length == 0)
        {
            Debug.Log("No interactable in range.");
            return;
        }

        Collider closest = null;
        float closestSqrDistance = float.MaxValue;

        for (int i = 0; i < hits.Length; i++)
        {
            float sqrDistance = (hits[i].transform.position - transform.position).sqrMagnitude;
            if (sqrDistance < closestSqrDistance)
            {
                closestSqrDistance = sqrDistance;
                closest = hits[i];
            }
        }

        var interactable = closest != null ? closest.GetComponent<IInteractable>() : null;
        if (interactable != null)
        {
            interactable.Interact();
        }
    }
}

