using UnityEngine;

public class ExtractionPad : MonoBehaviour, IInteractable
{
    [SerializeField] private Stage1State stage1State;
    [SerializeField] private MeshRenderer padRenderer;
    [SerializeField] private Color lockedColor = new Color(0.8f, 0.2f, 0.2f);
    [SerializeField] private Color readyColor = new Color(0.2f, 0.4f, 1f);
    [SerializeField] private Color completedColor = new Color(0.2f, 1f, 0.3f);

    private void Reset()
    {
        if (padRenderer == null)
        {
            padRenderer = GetComponentInChildren<MeshRenderer>();
        }
    }

    private void Start()
    {
        UpdateVisual();
    }

    private void Update()
    {
        // Keeps color in sync if beacon is activated after scene start.
        UpdateVisual();
    }

    public void Interact()
    {
        if (stage1State == null)
        {
            Debug.LogWarning("ExtractionPad: Stage1State reference is missing.");
            return;
        }

        if (!stage1State.BeaconActivated)
        {
            Debug.Log("Extraction locked: Activate the beacon first.");
            return;
        }

        stage1State.CompleteStage();
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if (padRenderer == null || stage1State == null)
            return;

        Color color = lockedColor;
        if (stage1State.StageCompleted)
        {
            color = completedColor;
        }
        else if (stage1State.BeaconActivated)
        {
            color = readyColor;
        }

        padRenderer.material.color = color;
    }
}

