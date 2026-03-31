using UnityEngine;

public class Beacon : MonoBehaviour, IInteractable
{
    [Header("Beacon Settings")]
    [SerializeField] private MeshRenderer beaconRenderer;
    [SerializeField] private Color inactiveColor = Color.red;
    [SerializeField] private Color activeColor = Color.green;
    [SerializeField] private Stage1State stage1State;

    private bool _isActive;

    private void Reset()
    {
        // Try to auto-assign renderer when added in editor
        if (beaconRenderer == null)
        {
            beaconRenderer = GetComponentInChildren<MeshRenderer>();
        }
    }

    private void Start()
    {
        UpdateVisual();
    }

    public void Interact()
    {
        if (_isActive)
            return;

        _isActive = true;
        UpdateVisual();
        Debug.Log("Beacon activated!");
        if (stage1State != null)
        {
            stage1State.ActivateBeacon();
        }
    }

    private void UpdateVisual()
    {
        if (beaconRenderer == null) return;

        var mat = beaconRenderer.material;
        mat.color = _isActive ? activeColor : inactiveColor;
    }
}

