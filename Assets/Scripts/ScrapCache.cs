using UnityEngine;

public class ScrapCache : MonoBehaviour, IInteractable
{
    [SerializeField] private Stage1State stage1State;
    [SerializeField] private int scrapAmount = 1;

    private bool _collected;

    [Header("Visual Feedback")]
    [SerializeField] private MeshRenderer[] renderersToDisable;
    [SerializeField] private Collider[] collidersToDisable;

    private void Reset()
    {
        // Good defaults when you add the component in the editor.
        if (stage1State == null)
        {
            stage1State = FindFirstObjectByType<Stage1State>();
        }

        if (renderersToDisable == null || renderersToDisable.Length == 0)
        {
            renderersToDisable = GetComponentsInChildren<MeshRenderer>(includeInactive: true);
        }

        if (collidersToDisable == null || collidersToDisable.Length == 0)
        {
            collidersToDisable = GetComponentsInChildren<Collider>(includeInactive: true);
        }
    }

    public void Interact()
    {
        if (_collected)
            return;

        if (stage1State == null)
        {
            Debug.LogWarning("ScrapCache: Stage1State reference missing.");
            return;
        }

        _collected = true;
        stage1State.CollectScrapCache(scrapAmount);
        DisableSelf();
        Debug.Log("Scrap cache collected!");
    }

    private void DisableSelf()
    {
        for (int i = 0; i < renderersToDisable.Length; i++)
        {
            if (renderersToDisable[i] != null) renderersToDisable[i].enabled = false;
        }

        for (int i = 0; i < collidersToDisable.Length; i++)
        {
            if (collidersToDisable[i] != null) collidersToDisable[i].enabled = false;
        }
    }
}

