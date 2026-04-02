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
    [Tooltip("How much vertical distance above/below the player is still interactable.")]
    [SerializeField] private float verticalInteractAllowance = 1.5f;
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
        // Find nearby interactables in a box so jumping doesn't break interaction.
        // We measure "range" in XZ, but allow extra Y tolerance.
        Vector3 halfExtents = new Vector3(interactRange, verticalInteractAllowance, interactRange);
        Collider[] hits = Physics.OverlapBox(transform.position, halfExtents, Quaternion.identity, interactLayerMask);
        if (hits.Length == 0)
        {
            Debug.Log("No interactable in range.");
            return;
        }

        Collider closest = null;
        float closestSqrDistance = float.MaxValue;

        for (int i = 0; i < hits.Length; i++)
        {
            Vector3 diff = hits[i].transform.position - transform.position;
            diff.y = 0f; // choose nearest by horizontal distance only
            float sqrDistance = diff.sqrMagnitude;
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

